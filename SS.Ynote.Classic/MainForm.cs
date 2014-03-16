

namespace SS.Ynote.Classic
{
       #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;
    using FastColoredTextBoxNS;
    using SS.Ynote.Classic.Features.Packages;
    using SS.Ynote.Classic.Features.Project;
    using SS.Ynote.Classic.Features.RunScript;
    using SS.Ynote.Classic.UI;
    using WeifenLuo.WinFormsUI.Docking;
    using Timer = System.Windows.Forms.Timer;

    #endregion

    public partial class MainForm : Form, IYnote
    {
        #region Private Fields

        /// <summary>
        ///     _mru list
        /// </summary>
        private readonly Queue<string> _mru;

        /// <summary>
        ///     _num - document number
        /// </summary>
        private int _num;

        #endregion

        #region Properties

        /// <summary>
        ///     Active Editor
        /// </summary>
        private Editor ActiveEditor
        {
            get { return dock.ActiveDocument as Editor; }
        }

        /// <summary>
        ///     Panel
        /// </summary>
        public DockPanel Panel
        {
            get { return dock; }
        }

        #endregion

        #region Constructor

        /// <summary>
        ///     Default Constructor
        /// </summary>
        /// <param name="filename"></param>
        public MainForm(string filename)
        {
#if DEBUG
            var spstart = new Stopwatch();
            spstart.Start();
#endif
            InitializeComponent();
            Icon = Properties.Resources.ynote_favicon;
            SettingsBase.LoadSettings();
            InitSettings();
            InitTimer();
            dock.Theme = new VS2012LightTheme();
            _mru = new Queue<string>();
            LoadPlugins();
#if DEBUG
            spstart.Stop();
            Debug.WriteLine(spstart.ElapsedMilliseconds + " Milliseconds to load!");
#endif
            if (filename == null)
                CreateNewDoc();
            else
                OpenFile(filename);
        }

        #endregion

        #region Methods

        #region File/IO

        /// <summary>
        ///     Open File
        /// </summary>
        /// <param name="name"></param>
        public void OpenFile(string name)
        {
#if DEBUG
            var stop = new Stopwatch();
            stop.Start();
#endif
            OpenDefault(name);
            SaveRecentFile(name, recentfilesmenu);
#if DEBUG
            Debug.WriteLine("Total Time Taken to Open File : " + stop.ElapsedMilliseconds + "Milliseconds.");
#endif
        }

        /// <summary>
        ///     SaveEditor without encoding
        /// </summary>
        /// <param name="edit"></param>
        public void SaveEditor(Editor edit)
        {
            SaveEditor(edit, Encoding.Default.BodyName);
        }

        public void CreateNewDoc()
        {
#if DEBUG
            var sp = new Stopwatch();
            sp.Start();
#endif
            _num++;
            var edit = new Editor {Text = "untitled" + _num};
            edit.Show(dock, DockState.Document);
#if DEBUG
            sp.Stop();
            Debug.WriteLine("Time Taken to Create New Document : " + sp.ElapsedMilliseconds + "Milliseconds");
#endif
        }

        /// <summary>
        ///     Default Method for Opening a File
        /// </summary>
        /// <param name="name"></param>
        /// <param name="encname"></param>
        /// <param name="isreadonly"></param>
        private void OpenDefault(string name, string encname, bool isreadonly)
        {
            if (!File.Exists(name))
            {
                var result =
                    MessageBox.Show(
                        string.Format("The File {0} does not exist.\r\nWould you like to create it ?", name),
                        "Ynote Classic", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    File.WriteAllText(name, "");
                    OpenDefault(name, encname, isreadonly);
                }
                return;
            }
            if (dock.Documents.Cast<Editor>().Any(editor => editor.Name == name)) return;
            var edit = new Editor();
            edit.tb.Text = File.ReadAllText(name, Encoding.GetEncoding(encname));
            edit.Text = Path.GetFileName(name);
            edit.tb.ReadOnly = isreadonly;
            edit.Name = name;
            edit.tb.IsChanged = false;
            edit.tb.ClearUndo();
            edit.ChangeLang(FileExtensions.GetLanguage(FileExtensions.BuildDictionary(), Path.GetExtension(name)));
            edit.Show(dock, DockState.Document);
        }

        private void OpenDefault(string name, bool isreadonly)
        {
            OpenDefault(name, Encoding.Default.BodyName, isreadonly);
        }

        private void OpenDefault(string name)
        {
            OpenDefault(name, false);
        }

        /// <summary>
        ///     Save a typeof(Editor), with encoding.getEncoding( "name")
        /// </summary>
        /// <param name="edit"></param>
        /// <param name="name"></param>
        private void SaveEditor(Editor edit, string name)
        {
            if (edit.Name == "Editor")
            {
                using (var s = new SaveFileDialog())
                {
                    s.Filter = "All Files(*.*)|*.*";
                    s.ShowDialog();
                    if (s.FileName == "") return;
                    edit.tb.SaveToFile(s.FileName, Encoding.GetEncoding(name));
                    edit.Text = Path.GetFileName(s.FileName);
                    edit.Name = s.FileName;
                }
            }
            else
            {
                edit.tb.SaveToFile(edit.Name, Encoding.GetEncoding(name));
                edit.Text = Path.GetFileName(edit.Name);
                edit.Name = edit.Name;
            }
        }

        /// <summary>
        ///     Saves and checks if document has been saved
        /// </summary>
        /// <param name="edit"></param>
        /// <returns></returns>
        private bool DoSave(Editor edit)
        {
            if (edit.Name == "Editor")
            {
                using (var s = new SaveFileDialog())
                {
                    s.Filter = "All Files(*.*)|*.*";
                    s.ShowDialog();
                    if (s.FileName == "") return false;
                    edit.tb.SaveToFile(s.FileName, Encoding.Default);
                    edit.Text = Path.GetFileName(s.FileName);
                    edit.Name = s.FileName;
                }
            }
            else
            {
                edit.tb.SaveToFile(edit.Name, Encoding.Default);
                edit.Text = Path.GetFileName(edit.Name);
                edit.Name = edit.Name;
            }
            return true;
        }

        #endregion

        #region Recent File Handlers

        /// <summary>
        ///     Saves Recent File to List
        /// </summary>
        /// <param name="path"></param>
        /// <param name="recent"></param>
        private void SaveRecentFile(string path, MenuItem recent)
        {
            recent.MenuItems.Clear(); //clear all recent list from menu
            LoadRecentList(); //load list from file
            if (!(_mru.Contains(path))) //prevent duplication on recent list
                _mru.Enqueue(path); //insert given path into list
            while (_mru.Count > 10) //keep list number not exceeded given value
            {
                _mru.Dequeue();
            }
            foreach (string item in _mru)
            {
                var fileRecent = new MenuItem(item); //create new menu for each item in list
                fileRecent.Click += (sender, args) => OpenFile(((MenuItem) (sender)).Text);
                ;
                recent.MenuItems.Add(fileRecent); //add the menu to "recent" menu
            }
            //writing menu list to file
            var stringToWrite = new StreamWriter(SettingsBase.SettingsDir + "Recent.info");
            //create file called "Recent.txt" located on app folder
            foreach (string item in _mru)
                stringToWrite.WriteLine(item); //write list to stream
            stringToWrite.Flush(); //write stream to file
            stringToWrite.Close(); //close the stream and reclaim memory
        }

        /// <summary>
        ///     Loads the List of Recent files from list
        /// </summary>
        private void LoadRecentList()
        {
            _mru.Clear();
            try
            {
                var listToRead = new StreamReader(SettingsBase.SettingsDir + "Recent.info"); //read file stream
                string line;
                while ((line = listToRead.ReadLine()) != null) //read each line until end of file
                    _mru.Enqueue(line); //insert to list
                listToRead.Close(); //close the stream
            }
            catch (Exception)
            {
                //throw;
            }
        }

        /// <summary>
        ///     Adds a recentfile to menu
        /// </summary>
        /// <param name="name"></param>
        private void AddRecentFile(string name)
        {
            var fileRecent = new MenuItem(name); //create new menu for each item in list
            fileRecent.Click += (sender, args) => OpenFile(((MenuItem) (sender)).Text);
            recentfilesmenu.MenuItems.Add(fileRecent); //add the menu to "recent" menu
        }

        #endregion

        #region Language Menu/Encoding Menu Builder

        /// <summary>
        ///     Builds the Language Menu -View->SyntaxH Highlighter->{Language(enumeration)}
        /// </summary>
        private void BuildLangMenu()
        {
            var menus = Enum.GetValues(typeof (Language)).Cast<Language>();
            foreach (MenuItem m in menus.Select(lang => new MenuItem {Text = lang.ToString()}))
            {
                m.Click += LangMenuItemClicked;
                milanguage.MenuItems.Add(m);
            }
            milanguage.GetMenuByName("Text").Checked = true;
        }

        /// <summary>
        ///     Occurs when BuildLangMenu() is called
        ///     and adds the click handler of every menu item so generated to LangMenuItemClicked delegate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LangMenuItemClicked(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            if (item == null) return;
            foreach (MenuItem t in item.Parent.MenuItems)
                t.Checked = false;
            item.Checked = true;
            if (ActiveEditor != null)
            {
                ActiveEditor.ChangeLang(item.Text.ToEnum<Language>());
                langmenu.Text = item.Text;
            }
        }

        /// <summary>
        ///     Trims Punctuation from start and end of string.
        /// </summary>
        private static string TrimPunctuation(string value)
        {
            // Count start punctuation.
            int removeFromStart = 0;
            foreach (char t in value)
            {
                if (char.IsPunctuation(t))
                {
                    removeFromStart++;
                }
                else
                {
                    break;
                }
            }

            // Count end punctuation.
            int removeFromEnd = 0;
            for (int i = value.Length - 1; i >= 0; i--)
            {
                if (char.IsPunctuation(value[i]))
                {
                    removeFromEnd++;
                }
                else
                {
                    break;
                }
            }
            // No characters were punctuation.
            if (removeFromStart == 0 &&
                removeFromEnd == 0)
            {
                return value;
            }
            // All characters were punctuation.
            if (removeFromStart == value.Length &&
                removeFromEnd == value.Length)
            {
                return "";
            }
            // Substring.
            return value.Substring(removeFromStart,
                value.Length - removeFromEnd - removeFromStart);
        }

        /// <summary>
        ///     Gets the Line Ending of Text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string GetTextLineEnding(string text)
        {
            string ending = string.Empty;
            if (text.Contains("\r"))
                ending = "\r";
            if (text.Contains("\n"))
                ending = "\n";
            if (text.Contains("\r\n"))
                ending = "\r\n";
            return ending;
        }

        /// <summary>
        ///     Builds the Supported Encodings Lists
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        private MenuItem[] BuildEncodingList(EventHandler handler)
        {
            return
                Encoding.GetEncodings()
                    .Select(info => new MenuItem(info.DisplayName, handler) {Tag = info.Name})
                    .ToArray();
        }

        #endregion

        #region MISC

        /// <summary>
        ///     Initialize Settings
        /// </summary>
        private void InitSettings()
        {
            if (!SettingsBase.ShowMenuBar) Menu = null;
            dock.DocumentStyle = SettingsBase.DocumentStyle;
            dock.DocumentTabStripLocation = SettingsBase.TabLocation;
            status.Visible = SettingsBase.ShowStatusBar;
        }

        /// <summary>
        ///     Initialize Information Timer
        /// </summary>
        private void InitTimer()
        {
            var infotimer = new Timer {Interval = 40};
            infotimer.Tick += infotimer_Tick;
            infotimer.Start();
        }

        /// <summary>
        ///     Sorts Array By Length
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private IEnumerable<string> SortByLength(IEnumerable<string> e)
        {
            // Use LINQ to sort the array received and return a copy.
            var sorted = from s in e
                orderby s.Length ascending
                select s;
            return sorted;
        }

        /// <summary>
        ///     Simple Update Method
        ///     Designed to be as simple as it could be
        /// </summary>
        /// <returns></returns>
        private bool CheckForUpdates()
        {
            using (var reader = XmlReader.Create("http://dat.sscorps.tk/ynoteupdate.xml"))
            {
                if (reader.IsStartElement())
                    if (reader.Name == "UpdateAvailable")
                        return true;
                return false;
            }
        }

        #endregion

        #region Plugins

        [ImportMany("IYnotePlugin")]
        public static IEnumerable<IYnotePlugin> Plugins { get; private set; }

        /// <summary>
        ///     Load Plugins
        /// </summary>
        private void LoadPlugins()
        {
            var dircatalog = new DirectoryCatalog(Application.StartupPath + @"\Plugins\");
            var container = new CompositionContainer(dircatalog);
            Plugins = container.GetExportedValues<IYnotePlugin>();
            LoadYnotePlugins(this, pluginsmenuitem);
        }

        /// <summary>
        ///     Loads IYnotePlugin Instances
        /// </summary>
        private static void LoadYnotePlugins(IYnote ynote, MenuItem addto)
        {
            foreach (var plugin in Plugins)
            {
                plugin.Initialize(ynote);
                addto.MenuItems.Add(plugin.MainMenuItem);
                addto.Visible = true;
            }
        }

        #endregion

        #endregion

        #region Overrides

        /// <summary>
        ///     Override on Load Method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            LoadRecentList();
            foreach (string r in _mru)
                AddRecentFile(r);
            base.OnLoad(e);
        }

        /// <summary>
        ///     OnClosed Method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            SettingsBase.SaveConfiguration();
            base.OnClosed(e);
        }

        #endregion

        #region Events

        private void NewMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewDoc();
        }

        private void OpenMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.ShowReadOnly = true;
                dialog.Filter = "All Files (*.*)|*.*";
                dialog.ShowDialog();
                if (dialog.FileName != "")
                {
                    if (dialog.ReadOnlyChecked)
                        OpenDefault(dialog.FileName, true);
                    else
                        OpenFile(dialog.FileName);
                }
            }
        }

        private void UndoMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.Undo();
        }

        private void RedoMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.Redo();
        }

        private void CutMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.Cut();
        }

        private void CopyMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.Copy();
        }

        private void PasteMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.Paste();
        }

        private void CommandPrompt_Click(object sender, EventArgs e)
        {
            var cmd = new Cmd("cmd.exe", null);
            cmd.Show(dock, DockState.DockBottom);
        }


        private void findmenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.ShowFindDialog();
        }

        private void replacemenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.ShowReplaceDialog();
        }

        private void increaseindent_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.IncreaseIndent();
        }

        private void decreaseindent_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.DecreaseIndent();
        }

        private void doindent_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.DoAutoIndent();
        }

        private void commentline_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.CommentSelected();
        }

        private void uncommentline_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.CommentSelected();
        }

        private void gotofirstlinemenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.GoHome();
        }

        private void gotoendmenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.GoEnd();
        }

        private void navforwardmenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.NavigateForward();
        }

        private void navbackwardmenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.NavigateBackward();
        }

        private void selectallmenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.SelectAll();
        }

        private void clearallmenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.Clear();
        }

        private void foldallmenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.CollapseAllFoldingBlocks();
        }

        private void unfoldmenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.ExpandAllFoldingBlocks();
        }

        private void foldselected_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
                ActiveEditor.tb.CollapseBlock(ActiveEditor.tb.Selection.Start.iLine, ActiveEditor.tb.Selection.End.iLine);
        }

        private void unfoldselected_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
                ActiveEditor.tb.CollapseBlock(ActiveEditor.tb.Selection.Start.iLine, ActiveEditor.tb.Selection.End.iLine);
        }

        private void datetime_Click(object sender, EventArgs e)
        {
            var date = DateTime.Now;
            var time = new TimeSpan(36, 0, 0, 0);
            var combined = date.Add(time);
            if (ActiveEditor != null) ActiveEditor.tb.InsertText(combined.ToString("DD/MM/YYYY"));
        }

        private void fileastext_Click(object sender, EventArgs e)
        {
            using (var openfile = new OpenFileDialog())
            {
                openfile.Filter = "All Files (*.*)|*.*|Text Documents(*.txt)|*.txt";
                openfile.ShowDialog();
                if (openfile.FileName != "" && ActiveEditor != null)
                    ActiveEditor.tb.InsertText(File.ReadAllText(openfile.FileName));
            }
        }

        private void filenamemenuitem_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.InsertText(ActiveEditor.Text);
        }

        private void fullfilenamemenuitem_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.InsertText(ActiveEditor.Name);
        }

        private void findinfilesmenu_Click(object sender, EventArgs e)
        {
            var findinfiles = new FindInFiles(this);
            findinfiles.Show(dock, DockState.DockBottom);
        }

        private void replacemode_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.IsReplaceMode = !ActiveEditor.tb.IsReplaceMode;
        }

        private void movelineup_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.MoveSelectedLinesUp();
        }

        private void movelinedown_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.MoveSelectedLinesDown();
        }

        private void duplicatelinemenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.ProcessKey(Keys.Control | Keys.D);
        }

        private void removeemptylines_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
            {
                var iLines = ActiveEditor.tb.FindLines(@"^\s*$", RegexOptions.None);
                ActiveEditor.tb.RemoveLines(iLines);
            }
        }

        private void caseuppermenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
            {
                var upper = ActiveEditor.tb.SelectedText.ToUpper();
                ActiveEditor.tb.SelectedText = upper;
            }
        }

        private void caselowermenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor == null) return;
            var lower = ActiveEditor.tb.SelectedText.ToLower();
            ActiveEditor.tb.SelectedText = lower;
        }

        private void casetitlemenu_Click(object sender, EventArgs e)
        {
            var cultureinfo = Thread.CurrentThread.CurrentCulture;
            var info = cultureinfo.TextInfo;
            if (ActiveEditor != null) ActiveEditor.tb.SelectedText = info.ToTitleCase(ActiveEditor.tb.SelectedText);
        }

        private void swapcase_Click(object sender, EventArgs e)
        {
            if (ActiveEditor == null) return;
            string input = ActiveEditor.tb.SelectedText;
            var reversedCase = new string(
                input.Select(c => char.IsLetter(c)
                    ? (char.IsUpper(c)
                        ? char.ToLower(c)
                        : char.ToUpper(c))
                    : c).ToArray());
            ActiveEditor.tb.SelectedText = reversedCase;
        }

        private void commentmenu_Popup(object sender, EventArgs e)
        {
            try
            {
                if (ActiveEditor != null && ActiveEditor.tb.Text.Contains(ActiveEditor.tb.CommentPrefix))
                {
                    commentline.Enabled = false;
                    uncommentline.Enabled = true;
                }
                else
                {
                    commentline.Enabled = true;
                    uncommentline.Enabled = false;
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void Addbookmarkmenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.Bookmarks.Add(ActiveEditor.tb.Selection.Start.iLine);
        }

        private void removebookmarkmenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.Bookmarks.Remove(ActiveEditor.tb.Selection.Start.iLine);
        }

        private void navigatethroughbookmarks_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press Ctrl + Shift + N To Navigate through bookmarks");
        }

        private void menuItem28_Click(object sender, EventArgs e)
        {
            using (var rtfs = new SaveFileDialog())
            {
                rtfs.Filter = "Rich Text Documents (*.rtf)|*.rtf";
                rtfs.ShowDialog();
                if (rtfs.FileName != "")
                    if (ActiveEditor != null) File.WriteAllText(rtfs.FileName, ActiveEditor.tb.Rtf);
            }
        }

        private void htmlexport_Click(object sender, EventArgs e)
        {
            using (var htmls = new SaveFileDialog())
            {
                htmls.FileName = "HTML Web Page (*.htm), (*.html)|*.htm|Shtml Page (*.shtml)|*.shtml";
                htmls.ShowDialog();
                if (htmls.FileName != "")
                    if (ActiveEditor != null) File.WriteAllText(htmls.FileName, ActiveEditor.tb.Html);
            }
        }

        private void pngexport_Click(object sender, EventArgs e)
        {
            if (ActiveEditor == null) return;
            var bmp = new Bitmap(ActiveEditor.tb.Width, ActiveEditor.tb.Height);
            ActiveEditor.tb.DrawToBitmap(bmp, new Rectangle(0, 0, ActiveEditor.tb.Width, ActiveEditor.tb.Height));
            using (var pngs = new SaveFileDialog())
            {
                pngs.Filter = "Portable Network Graphics (*.png)|*.png|JPEG (*.jpg)|*.jpg";
                pngs.ShowDialog();
                if (!string.IsNullOrEmpty(pngs.FileName))
                    bmp.Save(pngs.FileName);
            }
        }

        private void menuItem33_Click(object sender, EventArgs e)
        {
            using (var fb = new FolderBrowserDialog())
            {
                fb.ShowDialog();
                if (fb.SelectedPath == null) return;
                foreach (string file in Directory.GetFiles(fb.SelectedPath))
                    OpenFile(file);
            }
        }

        private void fromrtf_Click(object sender, EventArgs e)
        {
            var o = new OpenFileDialog {Filter = "RTF Files (*.rtf)|*.rtf"};
            o.ShowDialog();
            if (o.FileName == "") return;
            var edit = new Editor();
            edit.tb.Text = ConvertToText(File.ReadAllText(o.FileName));
            edit.Name = o.FileName;
            edit.Text = Path.GetFileName(o.FileName);
            edit.Show(dock, DockState.Document);
        }

        private static string ConvertToText(string rtf)
        {
            using (var rtb = new RichTextBox())
            {
                rtb.Rtf = rtf;
                return rtb.Text;
            }
        }

        private void menuItem15_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null && ActiveEditor.Name != "Editor")
                NativeMethods.ShowFileProperties(ActiveEditor.Name);
            else
                MessageBox.Show("File Not Saved!", "Ynote Classic");
        }

        private void menuItem17_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null && ActiveEditor.Name != "Editor")
            {
                var result =
                    MessageBox.Show(string.Format("Are you sure you want to delete {0} ?", ActiveEditor.Text),
                        "Delete File", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    File.Delete(ActiveEditor.Name);
                    ActiveEditor.Close();
                }
            }
            else
            {
                MessageBox.Show("File Is Not Saved!");
            }
        }

        private void menuItem16_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null && ActiveEditor.Name != "Editor")
            {
                string dir = Path.GetDirectoryName(ActiveEditor.Name);
                if (dir != null) Process.Start(dir);
            }
            else
            {
                MessageBox.Show("File Not Saved!");
            }
        }

        private void ExitMenu_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuItem49_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.Zoom += 10;
        }

        private void menuItem50_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.Zoom -= 10;
        }

        private void menuItem51_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.Zoom = 100;
        }

        private void menuItem57_Click(object sender, EventArgs e)
        {
            var splitedit = new Editor {Name = ActiveEditor.Name, Text = "[Split] " + ActiveEditor.Text};
            splitedit.tb.SourceTextBox = ActiveEditor.tb;
            splitedit.tb.ReadOnly = true;
            splitedit.Show(ActiveEditor.Pane, DockAlignment.Bottom, 0.5);
        }

        private void menuItem54_Click(object sender, EventArgs e)
        {
            if (menuItem54.Checked)
            {
                Opacity = 1.0;
                menuItem54.Checked = false;
            }
            else
            {
                Opacity = 0.7;
                menuItem54.Checked = true;
            }
        }

        private void menuItem55_Click(object sender, EventArgs e)
        {
            if (menuItem55.Checked)
            {
                menuItem55.Checked = false;
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
            }
            else
            {
                menuItem55.Checked = true;
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
        }

        private void wordwrapmenu_Click(object sender, EventArgs e)
        {
            if (wordwrapmenu.Checked)
            {
                wordwrapmenu.Checked = false;
                ActiveEditor.tb.WordWrap = false;
            }
            else
            {
                wordwrapmenu.Checked = true;
                ActiveEditor.tb.WordWrap = true;
            }
        }

        private void aboutmenu_Click(object sender, EventArgs e)
        {
            var ab = new About();
            ab.ShowDialog();
        }

        private void menuItem61_Click(object sender, EventArgs e)
        {
            Process.Start("http://facebook.com/sscorpscom");
        }

        private void menuItem11_Click(object sender, EventArgs e)
        {
            Process.Start("http://ynoteclassic.codeplex.com/wiki");
        }

        private void infotimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (dock.ActiveContent == null) return;
                int nCol = ActiveEditor.tb.Selection.Start.iChar + 1;
                int Line = ActiveEditor.tb.Selection.Start.iLine + 1;
                infolabel.Text = string.Format(" Line : {0} Col : {1} Sel : {2} Size : {3}", Line, nCol,
                    ActiveEditor.tb.Text.Length, ActiveEditor.tb.SelectedText.Length);
            }
            catch (Exception)
            {
                // CarryOn();
            }
        }

        private void zoom_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int i = e.ClickedItem.Text.ToInt();
            if (ActiveEditor == null) return;
            ActiveEditor.tb.Zoom = i;
            SettingsBase.Zoom = i;
        }

        private void CompareMenu_Click(object sender, EventArgs e)
        {
            var diff = new Diff();
            diff.Show(dock, DockState.Document);
        }

        private void pluginmanagermenu_Click(object sender, EventArgs e)
        {
            var manager = new PluginManager {StartPosition = FormStartPosition.CenterParent};
            manager.ShowDialog(this);
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            SaveEditor(ActiveEditor);
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            using (var sf = new SaveFileDialog())
            {
                sf.Filter = "All Files (*.*)|*.*";
                sf.ShowDialog();
                if (sf.FileName == "") return;
                if (ActiveEditor != null)
                {
                    ActiveEditor.tb.SaveToFile(sf.FileName, Encoding.Default);
                    ActiveEditor.Text = Path.GetFileName(sf.FileName);
                    ActiveEditor.Name = sf.FileName;
                }
            }
        }

        private void menuItem21_Click(object sender, EventArgs e)
        {
            foreach (Editor doc in dock.Documents)
                SaveEditor(doc);
        }

        private void OptionsMenu_Click(object sender, EventArgs e)
        {
            var optionsdialog = new Options();
            optionsdialog.Show();
        }

        private void menuItem35_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.MacrosManager.IsRecording = !ActiveEditor.tb.MacrosManager.IsRecording;
        }

        private void menuItem41_Click(object sender, EventArgs e)
        {
            if (ActiveEditor.tb.MacrosManager.Macros != null)
                ActiveEditor.tb.MacrosManager.ExecuteMacros();
        }

        private void menuItem43_Click(object sender, EventArgs e)
        {
            using (var sf = new SaveFileDialog())
            {
                sf.Filter = "Ynote Macros(*.ymc)|*.ymc";
                sf.InitialDirectory = SettingsBase.SettingsDir + @"Macros\";
                sf.ShowDialog();
                if (sf.FileName == "") return;
                if (!ActiveEditor.tb.MacrosManager.MacroIsEmpty)
                    File.WriteAllText(sf.FileName, ActiveEditor.tb.MacrosManager.Macros);
                else
                    MessageBox.Show("Macro Is Empty!");
            }
        }

        private void menuItem63_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.MacrosManager.ClearMacros();
        }

        private void menuItem30_Click(object sender, EventArgs e)
        {
            try
            {
                if (ActiveEditor != null && ActiveEditor.Name != "Editor")
                {
                    SaveEditor(ActiveEditor);
                    Process.Start(ActiveEditor.Name);
                }
                else
                    MessageBox.Show("File Not Saved!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        private void statusbarmenuitem_Click(object sender, EventArgs e)
        {
            status.Visible = !statusbarmenuitem.Checked;
            statusbarmenuitem.Checked = !statusbarmenuitem.Checked;
            SettingsBase.ShowStatusBar = statusbarmenuitem.Checked;
            SettingsBase.SaveConfiguration();
        }

        private void incrementalsearchmenu_Click(object sender, EventArgs e)
        {
            if (incrementalSearcher1.Visible) incrementalSearcher1.Exit();
            else
            {
                if (ActiveEditor != null)
                    incrementalSearcher1.Tb = ActiveEditor.tb;
                incrementalSearcher1.Visible = true;
                incrementalSearcher1.FocusTextBox();
            }
        }

        private void menuItem13_Click(object sender, EventArgs e)
        {
            if (ActiveEditor == null || ActiveEditor.Name == "Editor") return;
            ActiveEditor.tb.Text = File.ReadAllText(ActiveEditor.Name);
        }

        private void menuItem18_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.Print(new PrintDialogSettings {ShowPrintDialog = true});
        }

        private void reopenclosedtab_Click(object sender, EventArgs e)
        {
            var recentlyclosed = _mru.Last();
            if (recentlyclosed != null) OpenFile(recentlyclosed);
        }

        private void menuItem27_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.ShowGoToDialog();
        }

        private void menuitem_Click(object sender, EventArgs e)
        {
            var m = sender as MenuItem;
            foreach (MenuItem item in colorschememenu.MenuItems)
                item.Checked = false;
            if (m != null)
            {
                m.Checked = true;
                SettingsBase.ThemeFile = m.Name;
            }
        }

        private void colorschememenu_Select(object sender, EventArgs e)
        {
            if (colorschememenu.MenuItems.Count != 0) return;
            foreach (string file in Directory.GetFiles(Application.StartupPath + "\\Themes"))
            {
                var menuitem = new MenuItem {Text = Path.GetFileNameWithoutExtension(file), Name = file};
                menuitem.Click += menuitem_Click;
                colorschememenu.MenuItems.Add(menuitem);
            }
        }

        private void menuItem29_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.ProcessKey(Keys.Control | Keys.J);
        }

        private void milanguage_Select(object sender, EventArgs e)
        {
            if (milanguage != null && milanguage.MenuItems.Count == 0)
            {
                BuildLangMenu();
                foreach (MenuItem menu in milanguage.MenuItems)
                    menu.Checked = false;
                if (ActiveEditor != null) milanguage.GetMenuByName(ActiveEditor.tb.Language.ToString()).Checked = true;
            }
        }

        private void menuItem65_Click(object sender, EventArgs e)
        {
            var fctb = ActiveEditor.tb;
            var lines = fctb.SelectedText.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            Array.Sort(lines);
            var formedtext = lines.Aggregate<string, string>(null, (current, str) => current + (str + "\r\n"));
            fctb.SelectedText = formedtext;
        }

        private void menuItem69_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.SortLines();
        }

        private void gotobookmark_Click(object sender, EventArgs e)
        {
            var bookmarksmanager = new BookmarksInfos(ActiveEditor.tb) {StartPosition = FormStartPosition.CenterScreen};
            bookmarksmanager.Show();
        }

        private void menuItem71_Click(object sender, EventArgs e)
        {
            Process.Start("charmap.exe");
        }

        private void splitlinemenu_Click(object sender, EventArgs e)
        {
            var splitline = new UtilDialog(ActiveEditor.tb, InsertType.Splitter);
            splitline.Show();
        }

        private void emptycolumns_Click(object sender, EventArgs e)
        {
            var emptycols = new UtilDialog(ActiveEditor.tb, InsertType.Column);
            emptycols.Show();
        }

        private void emptylines_Click(object sender, EventArgs e)
        {
            var utilDialog = new UtilDialog(ActiveEditor.tb, InsertType.Line);
            utilDialog.Show();
        }

        private void menuItem42_Click(object sender, EventArgs e)
        {
            var macrodlg = new UtilDialog(ActiveEditor.tb, InsertType.Macro);
            macrodlg.Show();
        }

        private void menuItem72_Click(object sender, EventArgs e)
        {
            var console = new ConsoleUI(this) {StartPosition = FormStartPosition.CenterParent, LangMenu = langmenu};
            console.ShowDialog(this);
        }

        private void menuItem75_Click(object sender, EventArgs e)
        {
            if (ActiveEditor.tb.SelectedText != "")
            {
                string trimmed = "";
                string[] lines = ActiveEditor.tb.SelectedText.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                trimmed = lines.Length != 1
                    ? lines.Aggregate(trimmed, (current, line) => current + (line.TrimEnd() + Environment.NewLine))
                    : ActiveEditor.tb.SelectedText.TrimEnd();
                ActiveEditor.tb.SelectedText = trimmed;
            }
            else
            {
                MessageBox.Show("Noting Selected to Perform Function", "Ynote Classic");
            }
        }

        private void menuItem76_Click(object sender, EventArgs e)
        {
            if (ActiveEditor.tb.SelectedText != "")
            {
                string trimmed = "";
                string[] lines = ActiveEditor.tb.SelectedText.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                trimmed = lines.Length != 1
                    ? lines.Aggregate(trimmed, (current, line) => current + (line.TrimStart() + Environment.NewLine))
                    : ActiveEditor.tb.SelectedText.TrimStart();
                ActiveEditor.tb.SelectedText = trimmed;
            }
            else
            {
                MessageBox.Show("Noting Selected to Perform Function", "Ynote Classic");
            }
        }

        private void menuItem79_Click(object sender, EventArgs e)
        {
            if (ActiveEditor.tb.SelectedText != "")
            {
                string trimmed = "";
                string[] lines = ActiveEditor.tb.SelectedText.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                trimmed = lines.Length != 1
                    ? lines.Aggregate(trimmed, (current, line) => current + (line.Trim() + Environment.NewLine))
                    : ActiveEditor.tb.SelectedText.Trim();
                ActiveEditor.tb.SelectedText = trimmed;
            }
            else
            {
                MessageBox.Show("Noting Selected to Perform Function", "Ynote Classic");
            }
        }

        private void menuItem78_Click(object sender, EventArgs e)
        {
            if (ActiveEditor.tb.SelectedText != "")
                ActiveEditor.tb.SelectedText = ActiveEditor.tb.SelectedText.Replace("\r\n", " ");
            else
                MessageBox.Show("Nothing Selected to Perfrom Function", "Ynote Classic");
        }

        private void menuItem80_Click(object sender, EventArgs e)
        {
            if (ActiveEditor.tb.SelectedText != "")
                ActiveEditor.tb.SelectedText = ActiveEditor.tb.SelectedText.Replace(" ", "\r\n");
            else
                MessageBox.Show("Nothing Selected to Perfrom Function", "Ynote Classic");
        }

        private void menuItem77_Click(object sender, EventArgs e)
        {
            if (ActiveEditor.tb.SelectedText != "")
                ActiveEditor.tb.SelectedText = ActiveEditor.tb.SelectedText.Replace("\r\n", string.Empty);
            else
                MessageBox.Show("Nothing Selected to Perfrom Function", "Ynote Classic");
        }

        private void menuItem84_Click(object sender, EventArgs e)
        {
            var fileswitcher = new SwitchFile(this) {StartPosition = FormStartPosition.CenterParent};
            fileswitcher.ShowDialog(this);
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            if (ActiveEditor.Name != "Editor")
            {
                string extension = Path.GetExtension(ActiveEditor.Name);
                string filename = Path.GetTempFileName() + extension;
                File.WriteAllText(filename, ActiveEditor.tb.Text);
                var diff = new Diff(ActiveEditor.Name, filename);
                diff.DoCompare();
                diff.Show(dock, DockState.Document);
            }
            else
            {
                MessageBox.Show("File Is Not Saved!", "Ynote Classic");
            }
        }

        private void menuItem85_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press F3 to Find Next");
        }

        private void menuItem87_Click(object sender, EventArgs e)
        {
            if (ActiveEditor.tb.SelectedText != "")
            {
                string trimmed = "";
                string[] lines = ActiveEditor.tb.SelectedText.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                trimmed = lines.Length != 1
                    ? lines.Aggregate(trimmed,
                        (current, line) => current + (TrimPunctuation(line) + Environment.NewLine))
                    : TrimPunctuation(ActiveEditor.tb.SelectedText);
                ActiveEditor.tb.SelectedText = trimmed;
            }
            else
            {
                MessageBox.Show("Noting Selected to Perform Function", "Ynote Classic");
            }
        }


        private void menuItem109_Click(object sender, EventArgs e)
        {
            SettingsBase.HiddenChars = menuItem109.Checked; // If hiddenchars is checked
        }

        private void menuItem91_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.Text = ActiveEditor.tb.Text.Replace(GetTextLineEnding(ActiveEditor.tb.Text), "\r\n");
        }

        private void menuItem92_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.Text = ActiveEditor.tb.Text.Replace(GetTextLineEnding(ActiveEditor.tb.Text), "\r");
        }

        private void menuItem94_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.Text = ActiveEditor.tb.Text.Replace(GetTextLineEnding(ActiveEditor.tb.Text), "\n");
        }

        private void removelinemenu_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.ClearCurrentLine();
        }

        private void miopenencoding_Select(object sender, EventArgs e)
        {
            if (miopenencoding.MenuItems.Count == 0)
                miopenencoding.MenuItems.AddRange(BuildEncodingList(openencoding_click));
        }

        private void openencoding_click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog
            {
                Filter = "All Files(*.*)|*.*",
                ShowReadOnly = true
            })
            {
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK) return;
                var item = sender as MenuItem;
                if (item != null) OpenDefault(dialog.FileName, item.Tag.ToString(), dialog.ReadOnlyChecked);
            }
        }

        private void saveencoding_click(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null) SaveEditor(ActiveEditor, menuItem.Tag.ToString());
        }

        private void misaveencoding_Select(object sender, EventArgs e)
        {
            if (misaveencoding.MenuItems.Count == 0)
                misaveencoding.MenuItems.AddRange(BuildEncodingList(saveencoding_click));
        }

        private void menuItem95_Click(object sender, EventArgs e)
        {
            var manager = new ProjectPanel(this);
            manager.Show(dock, DockState.DockLeft);
        }

        private void menuItem38_Select(object sender, EventArgs e)
        {
            mimacros.MenuItems.Clear();
            foreach (string file in Directory.GetFiles(SettingsBase.SettingsDir + @"Macros\", "*.ymc"))
                mimacros.MenuItems.Add(new MenuItem(Path.GetFileNameWithoutExtension(file), macroitem_click)
                {
                    Name = file
                });
        }

        private void macroitem_click(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            if (item != null) ActiveEditor.tb.MacrosManager.ExecuteMacros(item.Name);
            /*ActiveEditor.tb.MacrosManager.Macros = File.ReadAllText(item.Name));
            ActiveEditor.tb.MacrosManager.ExecuteMacros();*/
        }

        private void menuItem88_Click(object sender, EventArgs e)
        {
            var length = ActiveEditor.tb.TabLength;
            string formed = string.Empty;
            for (int i = 0; i < length; i++)
            {
                formed += " ";
            }
            ActiveEditor.tb.SelectedText = ActiveEditor.tb.SelectedText.Replace(formed, "\t");
        }

        private void menuItem98_Click(object sender, EventArgs e)
        {
            var length = ActiveEditor.tb.TabLength;
            string formed = string.Empty;
            for (int i = 0; i < length; i++)
            {
                formed += " ";
            }
            ActiveEditor.tb.SelectedText = ActiveEditor.tb.SelectedText.Replace("\t", formed);
        }

        private void miscripts_Select(object sender, EventArgs e)
        {
            if (miscripts.MenuItems.Count != 0) return;
            foreach (var file in Directory.GetFiles(SettingsBase.SettingsDir + @"Scripts\"))
                miscripts.MenuItems.Add(new MenuItem(Path.GetFileNameWithoutExtension(file), scriptitem_clicked)
                {
                    Name = file
                });
        }

        private void scriptitem_clicked(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            if (item != null) YnoteScript.RunScript(this, item.Name);
        }

        private void menuItem100_Click(object sender, EventArgs e)
        {
            var fctb = ActiveEditor.tb;
            var form = new HotkeysEditorForm(fctb.HotkeysMapping);
            if (form.ShowDialog() == DialogResult.OK)
            {
                fctb.HotkeysMapping = form.GetHotkeys();
                File.WriteAllText(SettingsBase.SettingsDir + "User.ynotekeys", form.GetHotkeys().ToString());
            }
        }

        private void langmenu_Click(object sender, EventArgs e)
        {
            if (langmenu.HasDropDownItems)
            {
                var item =
                    langmenu.DropDownItems.Cast<ToolStripMenuItem>()
                        .FirstOrDefault(c => ActiveEditor != null && c.Text == ActiveEditor.tb.Language.ToString());
                if (item != null) item.Checked = true;
            }

            //    langmenu.GetMenuByName("Text").Checked = true;
        }

        private void langmenu_MouseEnter(object sender, EventArgs e)
        {
            var menus = Enum.GetValues(typeof (Language)).Cast<Language>();
            foreach (var m in menus.Select(lang => new ToolStripMenuItem {Text = lang.ToString()}))
            {
                m.Click += langmenuitem_click;
                langmenu.DropDownItems.Add(m);
            }
        }

        private void langmenuitem_click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            foreach (ToolStripMenuItem ditem in langmenu.DropDownItems)
                ditem.Checked = false;
            item.Checked = true;
            langmenu.Text = item.Text;
            if (ActiveEditor != null) ActiveEditor.ChangeLang(item.Text.ToEnum<Language>());
        }

        private void menuItem32_Click(object sender, EventArgs e)
        {
            if (ActiveEditor == null) return;
            if (DoSave(ActiveEditor))
            {
                var run = new RunDialog(ActiveEditor.Name, dock);
                run.Show();
            }
        }

        private void menuItem45_Click(object sender, EventArgs e)
        {
            var editor = new RunScriptEditor {StartPosition = FormStartPosition.CenterParent};
            editor.ShowDialog(this);
        }

        private void dock_ActiveDocumentChanged(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
            {
                langmenu.Text = ActiveEditor.tb.Language.ToString();
                if (incrementalSearcher1.Visible) incrementalSearcher1.Tb = ActiveEditor.tb;
            }
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.GoLeftBracket('(', ')');
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.GoRightBracket('(', ')');
        }

        private void menuItem26_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.GoLeftBracket('{', '}');
        }

        private void menuItem34_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.GoRightBracket('{', '}');
        }

        private void menuItem82_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.GoLeftBracket('[', ']');
        }

        private void menuItem101_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.GoRightBracket('[', ']');
        }

        private void menuItem96_Click(object sender, EventArgs e)
        {
            if (ActiveEditor == null) return;
            var lines = ActiveEditor.tb.SelectedText.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            var results = SortByLength(lines);
            ActiveEditor.tb.SelectedText = results.Aggregate<string, string>(null,
                (current, result) => current + (result + "\r\n"));
        }

        private void menuItem108_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
                ActiveEditor.tb.Selection.SetStyle(new TextStyle(null, new SolidBrush(Color.FromArgb(180, Color.Red)),
                    FontStyle.Regular));
        }


        private void menuItem111_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
                ActiveEditor.tb.Selection.SetStyle(new TextStyle(null, new SolidBrush(Color.FromArgb(180, Color.Blue)),
                    FontStyle.Regular));
        }

        private void menuItem112_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
                ActiveEditor.tb.Selection.SetStyle(new TextStyle(null,
                    new SolidBrush(Color.FromArgb(180, Color.Gainsboro)), FontStyle.Regular));
        }

        private void menuItem113_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
                ActiveEditor.tb.Selection.SetStyle(new TextStyle(null, new SolidBrush(Color.FromArgb(180, Color.Green)),
                    FontStyle.Regular));
        }

        private void menuItem114_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
                ActiveEditor.tb.Selection.SetStyle(new TextStyle(null, new SolidBrush(Color.FromArgb(180, Color.Yellow)),
                    FontStyle.Regular));
        }

        private void menuItem107_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.tb.Selection.ClearStyle(StyleIndex.All);
        }

        private void menuItem106_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null && ActiveEditor.AutoCompleteMenu != null)
                ActiveEditor.AutoCompleteMenu.Show(true);
        }

        private void menuItem119_Click(object sender, EventArgs e)
        {
            if (ActiveEditor == null) return;
            var selected = ActiveEditor.tb.SelectedText;
            byte[] bytes = Encoding.Default.GetBytes(selected);
            ActiveEditor.tb.SelectedText = BitConverter.ToString(bytes).Replace("-", "");
        }

        private void menuItem120_Click(object sender, EventArgs e)
        {
            byte[] asciiBytes = Encoding.ASCII.GetBytes(ActiveEditor.tb.SelectedText);
            var builder = new StringBuilder();
            foreach (byte b in asciiBytes)
                builder.Append(b + " ");
            ActiveEditor.tb.SelectedText = builder.ToString();
        }

        private void menuItem121_Click(object sender, EventArgs e)
        {
            if (CheckForUpdates())
            {
                var result = MessageBox.Show("Updates are available? Would you like to download it ?", "Updater",
                    MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    Process.Start("http://ynoteclassic.codeplex.com");
            }
            else
            {
                MessageBox.Show("No updates are available till date\r\nPlease check later.");
            }
        }

        #endregion
    }
}