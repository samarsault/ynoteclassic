using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using SS.Ynote.Classic.Features.Extensibility;
using SS.Ynote.Classic.Features.Packages;
using SS.Ynote.Classic.Features.Project;
using SS.Ynote.Classic.Features.RunScript;
using SS.Ynote.Classic.Features.Search;
using SS.Ynote.Classic.Features.Syntax;
using SS.Ynote.Classic.UI;
using WeifenLuo.WinFormsUI.Docking;
using Timer = System.Windows.Forms.Timer;

namespace SS.Ynote.Classic
{
    public partial class MainForm : Form, IYnote
    {
        #region Private Fields

        private IncrementalSearcher _incrementalSearcher;

        /// <summary>
        ///     _mru list
        /// </summary>
        private Queue<string> _mru;

        #endregion Private Fields

        #region Properties

        /// <summary>
        ///     Active Editor
        /// </summary>
        private Editor ActiveEditor
        {
            get { return Panel.ActiveDocument as Editor; }
        }

        /// <summary>
        ///     Panel
        /// </summary>
        public DockPanel Panel { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        ///     Default Constructor
        /// </summary>
        /// <param name="filename"></param>
        public MainForm(string fn)
        {
            InitializeComponent();
            InitSettings();
            LoadPlugins();
            if (string.IsNullOrEmpty(fn))
                CreateNewDoc();
            else
                OpenFile(fn);
            InitTimer();
        }

        #endregion Constructor

        #region Methods

        #region FILE/IO

        /// <summary>
        ///     Create New Document
        /// </summary>
        public void CreateNewDoc()
        {
            var edit = new Editor {Text = "untitled"};
            edit.Show(Panel);
        }

        /// <summary>
        ///     Opens a File
        /// </summary>
        /// <param name="name"></param>
        public void OpenFile(string name)
        {
            while (true)
            {
                if (!File.Exists(name))
                {
                    var result =
                        MessageBox.Show(
                            string.Format("The File {0} does not exist.\r\nWould you like to create it ?", name),
                            "Ynote Classic", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result != DialogResult.Yes) return;
                    File.WriteAllText(name, null);
                    continue;
                }
                if (Panel.Documents.Cast<DockContent>().Any(doc => doc is Editor && doc.Name == name))
                    return;
                var edit = new Editor {Text = Path.GetFileName(name), Name = name};
                if (FileExtensions.FileExtensionsDictionary == null)
                    FileExtensions.BuildDictionary();
                var lang = FileExtensions.GetLanguage(FileExtensions.FileExtensionsDictionary, Path.GetExtension(name));
                if (lang.IsBase)
                {
                    edit.Highlighter.HighlightSyntax(lang.SyntaxBase, new TextChangedEventArgs(edit.Tb.Range));
                    edit.Syntax = lang.SyntaxBase;
                }
                else
                {
                    edit.Highlighter.HighlightSyntax(lang.Language, new TextChangedEventArgs(edit.Tb.Range));
                    edit.Tb.Language = lang.Language;
                }
                edit.Show(Panel);
                var info = new FileInfo(name);
                var encoding = EncodingDetector.DetectTextFileEncoding(name) ?? Encoding.Default;
                if (info.Length > 9242800) // if greather than approx 10mb
                    edit.Tb.OpenBindingFile(name, encoding);
                else
                    edit.Tb.OpenFile(name, encoding);
                edit.Tb.ReadOnly = info.IsReadOnly;
                break;
            }
        }

        /// <summary>
        ///     SaveEditor without encoding
        /// </summary>
        /// <param name="edit"></param>
        public void SaveEditor(Editor edit)
        {
            SaveEditor(edit, Encoding.Default);
        }

        private void OpenFileAsync(string name)
        {
            BeginInvoke((MethodInvoker) (() => OpenFile(name)));
        }

        private void OpenFile(string name, Encoding encoding)
        {
            if (!File.Exists(name))
            {
                var result =
                    MessageBox.Show(
                        string.Format("The File {0} does not exist.\r\nWould you like to create it ?", name),
                        "Ynote Classic", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes) return;
                File.WriteAllText(name, null);
                OpenFile(name, encoding);
            }
            else
            {
                if (Panel.Documents.Cast<DockContent>().Any(doc => doc is Editor && doc.Name == name))
                    return;
                var edit = new Editor {Text = Path.GetFileName(name), Name = name};
                if (FileExtensions.FileExtensionsDictionary == null)
                    FileExtensions.BuildDictionary();
                var lang = FileExtensions.GetLanguage(FileExtensions.FileExtensionsDictionary,
                    Path.GetExtension(name));
                if (lang.IsBase)
                {
                    edit.Highlighter.HighlightSyntax(lang.SyntaxBase, new TextChangedEventArgs(edit.Tb.Range));
                    edit.Syntax = lang.SyntaxBase;
                }
                else
                {
                    edit.Highlighter.HighlightSyntax(lang.Language, new TextChangedEventArgs(edit.Tb.Range));
                    edit.Tb.Language = lang.Language;
                }
                edit.Show(Panel);
                var info = new FileInfo(name);
                if (info.Length > 9242800) // if greather than approx 10mb
                    edit.Tb.OpenBindingFile(name, encoding);
                else
                    edit.Tb.OpenFile(name, encoding);
                edit.Tb.ReadOnly = info.IsReadOnly;
            }
        }

        /// <summary>
        ///     Save a typeof(Editor), with encoding.getEncoding( "name")
        /// </summary>
        /// <param name="edit"></param>
        /// <param name="name"></param>
        private static void SaveEditor(Editor edit, Encoding encoding)
        {
            try
            {
                if (!edit.IsSaved)
                {
                    using (var s = new SaveFileDialog())
                    {
                        s.Filter = "All Files(*.*)|*.*";
                        s.ShowDialog();
                        if (s.FileName == "") return;
                        edit.Tb.SaveToFile(s.FileName, encoding);
                        edit.Text = Path.GetFileName(s.FileName);
                        edit.Name = s.FileName;
                    }
                }
                else
                {
                    edit.Tb.SaveToFile(edit.Name, encoding);
                    edit.Text = Path.GetFileName(edit.Name);
                    edit.Name = edit.Name;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving File !!" + ex.Message, null, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        #region Recent File Handlers

        /// <summary>
        ///     Saves Recent File to List
        /// </summary>
        /// <param name="path"></param>
        /// <param name="recent"></param>
        private void SaveRecentFiles()
        {
            if (_mru == null)
                LoadRecentList();
            // LoadRecentList(); //load list from file
            while (_mru.Count > SettingsBase.RecentFileNumber) //keep list number not exceeded given value
                _mru.Dequeue();
            //writing menu list to file
            using (var stringToWrite = new StreamWriter(SettingsBase.SettingsDir + "Recent.info"))
            {
                //create file called "Recent.txt" located on app folder
                foreach (var item in _mru)
                    stringToWrite.WriteLine(item); //write list to stream
                stringToWrite.Flush(); //write stream to file
                stringToWrite.Close(); //close the stream and reclaim memory
            }
        }

        /// <summary>
        /// Loads the List of Recent files from list
        /// </summary>
        void LoadRecentList()
        {
            string rfPath = SettingsBase.SettingsDir + "Recent.info";
            if (!Directory.Exists(SettingsBase.SettingsDir))
                Directory.CreateDirectory(SettingsBase.SettingsDir);
            if(!File.Exists(rfPath))
                File.WriteAllText(rfPath, string.Empty);
            _mru = new Queue<string>();
            using (var listToRead = new StreamReader(rfPath))
            {
                //read file stream
                string line;
                while ((line = listToRead.ReadLine()) != null) //read each line until end of file
                    _mru.Enqueue(line); //insert to list
                listToRead.Close(); //close the stream
            }
            foreach (var item in _mru)
                recentfilesmenu.MenuItems.Add(item, 
                    (sender, args) => OpenFile(((MenuItem) (sender)).Text));
        }

        /// <summary>
        ///     Adds a recentfile to menu
        /// </summary>
        /// <param name="name"></param>
        private void AddRecentFile(string name)
        {
            if (_mru == null)
                LoadRecentList();
            if (!_mru.Contains(name))
            {
                var fileRecent = new MenuItem(name); //create new menu for each item in list
                _mru.Enqueue(name);
                fileRecent.Click += (sender, args) => OpenFile(((MenuItem) (sender)).Text);
                recentfilesmenu.MenuItems.Add(fileRecent); //add the menu to "recent" menu
            }
        }

        #endregion Recent File Handlers

        #region Menu Builders

        /// <summary>
        ///     Builds the Language Menu -View->SyntaxH Highlighter->{Language(enumeration)}
        /// </summary>
        private void BuildLangMenu()
        {
            var menus = Enum.GetValues(typeof (Language)).Cast<Language>();

            foreach (var m in menus.Select(lang => new MenuItem(lang.ToString())))
            {
                m.Click += LangMenuItemClicked;
                milanguage.MenuItems.Add(m);
            }
            foreach (var item in SyntaxHighlighter.LoadedSyntaxes)
                milanguage.MenuItems.Add(new MenuItem(Path.GetFileNameWithoutExtension(item.SysPath),
                    LangMenuItemClicked) {Tag = item});
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
            if (ActiveEditor == null) return;
            if (item.Tag == null)
            {
                var lang = item.Text.ToEnum<Language>();
                ActiveEditor.Highlighter.HighlightSyntax(lang, new TextChangedEventArgs(ActiveEditor.Tb.Range));
                ActiveEditor.Tb.Language = lang;
                ActiveEditor.Syntax = null;
            }
            else
            {
                var syntax = item.Tag as SyntaxBase;
                ActiveEditor.Highlighter.HighlightSyntax(syntax, new TextChangedEventArgs(ActiveEditor.Tb.Range));
                ActiveEditor.Syntax = syntax;
            }
            langmenu.Text = item.Text;
        }

        /// <summary>
        ///     Trims Punctuation from start and end of string.
        /// </summary>
        private static string TrimPunctuation(string value)
        {
            // Count start punctuation.
            var removeFromStart = 0;
            foreach (var t in value)
            {
                if (char.IsPunctuation(t))
                    removeFromStart++;
                else
                    break;
            }

            // Count end punctuation.
            var removeFromEnd = 0;
            for (var i = value.Length - 1; i >= 0; i--)
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
            string ending = null;
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
        private static MenuItem[] BuildEncodingList(EventHandler handler)
        {
            return
                Encoding.GetEncodings()
                    .Select(info => new MenuItem(info.DisplayName, handler) {Tag = info.GetEncoding()})
                    .ToArray();
        }

        #endregion Menu Builders

        #region MISC

        private static string ConvertToText(string rtf)
        {
            using (var rtb = new RichTextBox())
            {
                rtb.Rtf = rtf;
                return rtb.Text;
            }
        }

        /// <summary>
        ///     Initialize Settings
        /// </summary>
        private void InitSettings()
        {
            SettingsBase.LoadSettings();
            if (!SettingsBase.ShowMenuBar) Menu = null;
            Panel.DocumentStyle = SettingsBase.DocumentStyle;
            Panel.DocumentTabStripLocation = SettingsBase.TabLocation;
            mihiddenchars.Checked = SettingsBase.HiddenChars;
            status.Visible = statusbarmenuitem.Checked = SettingsBase.ShowStatusBar;
        }

        /// <summary>
        ///     Initialize Information Timer
        /// </summary>
        private void InitTimer()
        {
            using (var infotimer = new Timer {Interval = 200})
            {
                infotimer.Tick += (sender, args) => UpdateDocumentInfo();
                infotimer.Start();
            }
        }

        /// <summary>
        ///     Updates the Document Info
        /// </summary>
        private void UpdateDocumentInfo()
        {
            if (!(Panel.ActiveDocument is Editor) || ActiveEditor == null) return;
            var nCol = ActiveEditor.Tb.Selection.Start.iChar + 1;
            var line = ActiveEditor.Tb.Selection.Start.iLine + 1;
            infolabel.Text = string.Format(" Line : {0} Col : {1} Size : {2} bytes Selected : {3}", line, nCol,
                ActiveEditor.Tb.Text.Length, ActiveEditor.Tb.SelectedText.Length);
        }

        /// <summary>
        ///     Sorts Array By Length
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private static IEnumerable<string> SortByLength(IEnumerable<string> e)
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
        private static bool CheckForUpdates()
        {
            const string url = "https://raw.githubusercontent.com/samarjeet27/ynoteclassic/master/updates.xml";
            return RemoteFileExists(url);
        }

        private static bool RemoteFileExists(string url)
        {
            var bResult = false;
            using (var client = new WebClient())
            {
                try
                {
                    client.UseDefaultCredentials = true;
                    var stream = client.OpenRead(url);
                    if (stream != null)
                        bResult = true;
                }
                catch
                {
                    bResult = false;
                }
            }
            return bResult;
        }

        #endregion MISC

        #region Plugins

        /// <summary>
        ///     Load Plugins
        /// </summary>
        private void LoadPlugins()
        {
            if (!Directory.Exists(SettingsBase.SettingsDir + @"\Plugins"))
                Directory.CreateDirectory(SettingsBase.SettingsDir);
            using (var dircatalog = new DirectoryCatalog(SettingsBase.SettingsDir + @"\Plugins"))
            {
                using (var container = new CompositionContainer(dircatalog))
                {
                    var plugins = container.GetExportedValues<IYnotePlugin>();
                    foreach (var plugin in plugins)
                        plugin.Main(this);
                }
            }
        }

        #endregion

        #endregion

        #region Overrides

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            SaveRecentFiles();
            base.OnClosing(e);
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
                dialog.Filter = "All Files (*.*)|*.*";
                var res = dialog.ShowDialog() == DialogResult.OK;
                if (!res) return;
                OpenFileAsync(dialog.FileName);
                AddRecentFile(dialog.FileName);
            }
        }

        private void UndoMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.Undo();
        }

        private void RedoMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.Redo();
        }

        private void CutMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.Cut();
        }

        private void CopyMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.Copy();
        }

        private void PasteMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.Paste();
        }

        private void CommandPrompt_Click(object sender, EventArgs e)
        {
            var cmd = new Cmd("cmd.exe", null);
            cmd.Show(Panel, DockState.DockBottom);
        }

        private void findmenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.ShowFindDialog();
        }

        private void replacemenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.ShowReplaceDialog();
        }

        private void increaseindent_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.IncreaseIndent();
        }

        private void decreaseindent_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.DecreaseIndent();
        }

        private void doindent_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.DoAutoIndent();
        }

        private void commentline_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
            {
                ActiveEditor.Tb.CommentSelected();
            }
        }

        private void gotofirstlinemenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.GoHome();
        }

        private void gotoendmenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.GoEnd();
        }

        private void navforwardmenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.NavigateForward();
        }

        private void navbackwardmenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.NavigateBackward();
        }

        private void selectallmenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.SelectAll();
        }

        private void foldallmenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.CollapseAllFoldingBlocks();
        }

        private void unfoldmenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.ExpandAllFoldingBlocks();
        }

        private void foldselected_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
                ActiveEditor.Tb.CollapseBlock(ActiveEditor.Tb.Selection.Start.iLine, ActiveEditor.Tb.Selection.End.iLine);
        }

        private void unfoldselected_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
                ActiveEditor.Tb.CollapseBlock(ActiveEditor.Tb.Selection.Start.iLine, ActiveEditor.Tb.Selection.End.iLine);
        }

        private void datetime_Click(object sender, EventArgs e)
        {
            var date = DateTime.Now;
            var time = new TimeSpan(36, 0, 0, 0);
            var combined = date.Add(time);
            if (ActiveEditor != null) ActiveEditor.Tb.InsertText(combined.ToString("DD/MM/YYYY"));
        }

        private void fileastext_Click(object sender, EventArgs e)
        {
            using (var openfile = new OpenFileDialog())
            {
                openfile.Filter = "All Files (*.*)|*.*|Text Documents(*.txt)|*.txt";
                openfile.ShowDialog();
                if (openfile.FileName != "" && ActiveEditor != null)
                    ActiveEditor.Tb.InsertText(File.ReadAllText(openfile.FileName));
            }
        }

        private void filenamemenuitem_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.InsertText(ActiveEditor.Text);
        }

        private void fullfilenamemenuitem_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.InsertText(ActiveEditor.Name);
        }

        private void findinfilesmenu_Click(object sender, EventArgs e)
        {
            var findinfiles = new FindInFiles(this);
            findinfiles.Show(Panel, DockState.DockBottom);
        }

        private void replacemode_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.IsReplaceMode = !ActiveEditor.Tb.IsReplaceMode;
        }

        private void movelineup_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.MoveSelectedLinesUp();
        }

        private void movelinedown_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.MoveSelectedLinesDown();
        }

        private void duplicatelinemenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.DuplicateLine(ActiveEditor.Tb.Selection.Start.iLine);
        }

        private void removeemptylines_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
            {
                var iLines = ActiveEditor.Tb.FindLines(@"^\s*$", RegexOptions.None);
                ActiveEditor.Tb.RemoveLines(iLines);
            }
        }

        private void caseuppermenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
            {
                var upper = ActiveEditor.Tb.SelectedText.ToUpper();
                ActiveEditor.Tb.SelectedText = upper;
            }
        }

        private void caselowermenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor == null) return;
            var lower = ActiveEditor.Tb.SelectedText.ToLower();
            ActiveEditor.Tb.SelectedText = lower;
        }

        private void casetitlemenu_Click(object sender, EventArgs e)
        {
            var cultureinfo = Thread.CurrentThread.CurrentCulture;
            var info = cultureinfo.TextInfo;
            if (ActiveEditor != null) ActiveEditor.Tb.SelectedText = info.ToTitleCase(ActiveEditor.Tb.SelectedText);
        }

        private void swapcase_Click(object sender, EventArgs e)
        {
            if (ActiveEditor == null) return;
            var input = ActiveEditor.Tb.SelectedText;
            var reversedCase = new string(
                input.Select(c => char.IsLetter(c)
                    ? (char.IsUpper(c)
                        ? char.ToLower(c)
                        : char.ToUpper(c))
                    : c).ToArray());
            ActiveEditor.Tb.SelectedText = reversedCase;
        }

        private void commentmenu_Popup(object sender, EventArgs e)
        {
            if (ActiveEditor != null && ActiveEditor.Tb.Text.Contains(ActiveEditor.Tb.CommentPrefix))
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

        private void Addbookmarkmenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.Bookmarks.Add(ActiveEditor.Tb.Selection.Start.iLine);
        }

        private void removebookmarkmenu_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.Bookmarks.Remove(ActiveEditor.Tb.Selection.Start.iLine);
        }

        private void navigatethroughbookmarks_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press Ctrl + Shift + N To Navigate through bookmarks");
        }

        private void rtfExport_Click(object sender, EventArgs e)
        {
            using (var rtfs = new SaveFileDialog())
            {
                rtfs.Filter = "Rich Text Documents (*.rtf)|*.rtf";
                rtfs.ShowDialog();
                if (rtfs.FileName != "")
                    if (ActiveEditor != null) File.WriteAllText(rtfs.FileName, ActiveEditor.Tb.Rtf);
            }
        }

        private void htmlexport_Click(object sender, EventArgs e)
        {
            using (var htmls = new SaveFileDialog())
            {
                htmls.FileName = "HTML Web Page (*.htm), (*.html)|*.htm|Shtml Page (*.shtml)|*.shtml";
                var result = htmls.ShowDialog() == DialogResult.OK;
                if (result)
                    if (ActiveEditor != null) File.WriteAllText(htmls.FileName, ActiveEditor.Tb.Html);
            }
        }

        private void pngexport_Click(object sender, EventArgs e)
        {
            if (ActiveEditor == null) return;
            var bmp = new Bitmap(ActiveEditor.Tb.Width, ActiveEditor.Tb.Height);
            ActiveEditor.Tb.DrawToBitmap(bmp, new Rectangle(0, 0, ActiveEditor.Tb.Width, ActiveEditor.Tb.Height));
            using (var pngs = new SaveFileDialog())
            {
                pngs.Filter = "Portable Network Graphics (*.png)|*.png|JPEG (*.jpg)|*.jpg";
                pngs.ShowDialog();
                if (!string.IsNullOrEmpty(pngs.FileName))
                    bmp.Save(pngs.FileName);
            }
        }

        private void mifromdir_Click(object sender, EventArgs e)
        {
            using (var fb = new FolderBrowserDialog())
            {
                fb.ShowDialog();
                if (fb.SelectedPath == null) return;
                foreach (var file in Directory.GetFiles(fb.SelectedPath))
                    OpenFile(file);
            }
        }

        private void fromrtf_Click(object sender, EventArgs e)
        {
            using (var o = new OpenFileDialog {Filter = "RTF Files (*.rtf)|*.rtf"})
            {
                o.ShowDialog();
                if (o.FileName == "") return;
                var edit = new Editor();
                edit.Tb.Text = ConvertToText(File.ReadAllText(o.FileName));
                edit.Name = o.FileName;
                edit.Text = Path.GetFileName(o.FileName);
                edit.Show(Panel, DockState.Document);
            }
        }

        private void miproperties_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null && ActiveEditor.IsSaved)
                NativeMethods.ShowFileProperties(ActiveEditor.Name);
            else
                MessageBox.Show("File Not Saved!", "Ynote Classic");
        }

        private void midelete_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null && ActiveEditor.IsSaved)
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

        private void miopencontaining_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null && ActiveEditor.IsSaved)
            {
                var dir = Path.GetDirectoryName(ActiveEditor.Name);
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

        private void mizoomin_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.Zoom += 10;
        }

        private void mizoomout_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.Zoom -= 10;
        }

        private void mirestoredefault_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.Zoom = 100;
        }

        private void menuItem57_Click(object sender, EventArgs e)
        {
            var splitedit = new Editor {Name = ActiveEditor.Name, Text = "[Split] " + ActiveEditor.Text};
            splitedit.Tb.SourceTextBox = ActiveEditor.Tb;
            splitedit.Tb.ReadOnly = true;
            splitedit.Show(ActiveEditor.Pane, DockAlignment.Bottom, 0.5);
        }

        private void mitransparent_Click(object sender, EventArgs e)
        {
            mitransparent.Checked = !mitransparent.Checked;
            Opacity = mitransparent.Checked ? 0.7 : 1;
        }

        private void mifullscreen_Click(object sender, EventArgs e)
        {
            if (mifullscreen.Checked)
            {
                mifullscreen.Checked = false;
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
            }
            else
            {
                mifullscreen.Checked = true;
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
        }

        private void wordwrapmenu_Click(object sender, EventArgs e)
        {
            if (wordwrapmenu.Checked)
            {
                wordwrapmenu.Checked = false;
                ActiveEditor.Tb.WordWrap = false;
            }
            else
            {
                wordwrapmenu.Checked = true;
                ActiveEditor.Tb.WordWrap = true;
            }
        }

        private void aboutmenu_Click(object sender, EventArgs e)
        {
            using (var ab = new About())
            {
                ab.ShowDialog();
            }
        }

        private void mifb_Click(object sender, EventArgs e)
        {
            Process.Start("http://facebook.com/sscorpscom");
        }

        private void miwikimenu_Click(object sender, EventArgs e)
        {
            Process.Start("http://ynoteclassic.codeplex.com/documentation");
        }

        private void zoom_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var i = e.ClickedItem.Text.ToInt();
            if (ActiveEditor == null) return;
            ActiveEditor.Tb.Zoom = i;
            SettingsBase.Zoom = i;
        }

        private void CompareMenu_Click(object sender, EventArgs e)
        {
            var diff = new Diff();
            diff.Show(Panel, DockState.Document);
        }

        private void pluginmanagermenu_Click(object sender, EventArgs e)
        {
            using (var manager = new PackageManager {StartPosition = FormStartPosition.CenterParent})
            {
                manager.ShowDialog(this);
            }
        }

        private void savemenu_Click(object sender, EventArgs e)
        {
            BeginInvoke((MethodInvoker)(() => SaveEditor(ActiveEditor)));
        }

        private void misaveas_Click(object sender, EventArgs e)
        {
            using (var sf = new SaveFileDialog())
            {
                sf.Filter = "All Files (*.*)|*.*";
                sf.ShowDialog();
                if (sf.FileName == "") return;
                if (ActiveEditor != null)
                {
                    ActiveEditor.Tb.SaveToFile(sf.FileName, Encoding.Default);
                    ActiveEditor.Text = Path.GetFileName(sf.FileName);
                    ActiveEditor.Name = sf.FileName;
                }
            }
        }

        private void misaveall_Click(object sender, EventArgs e)
        {
            //TODO:Check If Working
            foreach (Editor doc in Panel.Documents)
                BeginInvoke((MethodInvoker)(() => SaveEditor(doc)));
        }

        private void OptionsMenu_Click(object sender, EventArgs e)
        {
            var optionsdialog = new Options();
            optionsdialog.Show();
        }

        private void mimacrorecord_Click(object sender, EventArgs e)
        {
            ActiveEditor.Tb.MacrosManager.IsRecording = !ActiveEditor.Tb.MacrosManager.IsRecording;
        }

        private void miExecmacro_Click(object sender, EventArgs e)
        {
            if (ActiveEditor.Tb.MacrosManager.Macros != null)
                ActiveEditor.Tb.MacrosManager.ExecuteMacros();
        }

        private void misavemacro_Click(object sender, EventArgs e)
        {
            using (var sf = new SaveFileDialog())
            {
                sf.Filter = "Ynote Macros(*.ymc)|*.ymc";
                sf.InitialDirectory = SettingsBase.SettingsDir + @"Macros\";
                sf.ShowDialog();
                if (sf.FileName == "") return;
                if (!ActiveEditor.Tb.MacrosManager.MacroIsEmpty)
                    File.WriteAllText(sf.FileName, ActiveEditor.Tb.MacrosManager.Macros);
                else
                    MessageBox.Show("Macro Is Empty!");
            }
        }

        private void miclearmacro_Click(object sender, EventArgs e)
        {
            ActiveEditor.Tb.MacrosManager.ClearMacros();
        }

        private void menuItem30_Click(object sender, EventArgs e)
        {
            try
            {
                if (ActiveEditor != null && ActiveEditor.IsSaved)
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
            if (ActiveEditor == null) return;
            if (_incrementalSearcher == null)
            {
                _incrementalSearcher = new IncrementalSearcher {Dock = DockStyle.Bottom};
                Controls.Add(_incrementalSearcher);
                _incrementalSearcher.Tb = ActiveEditor.Tb;
                _incrementalSearcher.tbFind.Text = ActiveEditor.Tb.SelectedText;
                _incrementalSearcher.FocusTextBox();
            }
            else
            {
                if (_incrementalSearcher != null
                    && _incrementalSearcher.Visible) _incrementalSearcher.Exit();
                else
                {
                    _incrementalSearcher.Tb = ActiveEditor.Tb;
                    _incrementalSearcher.tbFind.Text = ActiveEditor.Tb.SelectedText;
                    _incrementalSearcher.Visible = true;
                    _incrementalSearcher.FocusTextBox();
                }
            }
        }

        private void mirevert_Click(object sender, EventArgs e)
        {
            if (ActiveEditor == null || !ActiveEditor.IsSaved) return;
            ActiveEditor.Tb.OpenFile(ActiveEditor.Name);
            ActiveEditor.Text = Path.GetFileName(ActiveEditor.Name);
        }

        private void miprint_Click(object sender, EventArgs e)
        {
            ActiveEditor.Tb.Print(new PrintDialogSettings {ShowPrintDialog = true});
        }

        private void reopenclosedtab_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mru == null)
                    LoadRecentList();
                var recentlyclosed = _mru.Last();
                if (recentlyclosed != null) OpenFile(recentlyclosed);
            }
            catch
            {
                recentfilesmenu.PerformSelect();
            }
        }

        private void migotoline_Click(object sender, EventArgs e)
        {
            ActiveEditor.Tb.ShowGoToDialog();
        }

        private void colorschemeitem_Click(object sender, EventArgs e)
        {
            var m = sender as MenuItem;
            foreach (MenuItem item in colorschememenu.MenuItems)
                item.Checked = false;
            if (m == null) return;
            m.Checked = true;
            SettingsBase.ThemeFile = m.Name;
            SettingsBase.SaveConfiguration();
        }

        private void colorschememenu_Select(object sender, EventArgs e)
        {
            if (colorschememenu.MenuItems.Count != 0) return;
            foreach (
                var menuitem in
                    Directory.GetFiles(SettingsBase.SettingsDir + "\\Themes")
                        .Select(file => new MenuItem {Text = Path.GetFileNameWithoutExtension(file), Name = file}))
            {
                menuitem.Click += colorschemeitem_Click;
                colorschememenu.MenuItems.Add(menuitem);
            }
        }

        private void menuItem29_Click(object sender, EventArgs e)
        {
            if (ActiveEditor == null) return;
            ActiveEditor.Tb.SelectedText = string.Join(" ", ActiveEditor.Tb.SelectedText.Split(new[]
            {
                Environment.NewLine
            }, StringSplitOptions.None));
        }

        private void milanguage_Select(object sender, EventArgs e)
        {
            if (milanguage == null || milanguage.MenuItems.Count != 0) return;
            // Build the Language Menu
            BuildLangMenu();
            //
            foreach (MenuItem menu in milanguage.MenuItems)
                menu.Checked = false;
            if (ActiveEditor == null) return;
            if (ActiveEditor.Syntax == null)
                milanguage.GetMenuByName(ActiveEditor.Tb.Language.ToString()).Checked = true;
            else
                milanguage.GetMenuByName(Path.GetFileNameWithoutExtension(ActiveEditor.Syntax.SysPath)).Checked
                    = true;
        }

        private void menuItem65_Click(object sender, EventArgs e)
        {
            var fctb = ActiveEditor.Tb;
            var lines = fctb.SelectedText.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            Array.Sort(lines);
            var formedtext = lines.Aggregate<string, string>(null, (current, str) => current + (str + "\r\n"));
            fctb.SelectedText = formedtext;
        }

        private void menuItem69_Click(object sender, EventArgs e)
        {
            ActiveEditor.Tb.SortLines();
        }

        private void gotobookmark_Click(object sender, EventArgs e)
        {
            var bookmarksmanager = new BookmarksInfos(ActiveEditor.Tb) {StartPosition = FormStartPosition.CenterScreen};
            bookmarksmanager.Show();
        }

        private void menuItem71_Click(object sender, EventArgs e)
        {
            Process.Start("charmap.exe");
        }

        private void splitlinemenu_Click(object sender, EventArgs e)
        {
            var splitline = new UtilDialog(ActiveEditor.Tb, InsertType.Splitter);
            splitline.Show();
        }

        private void emptycolumns_Click(object sender, EventArgs e)
        {
            var emptycols = new UtilDialog(ActiveEditor.Tb, InsertType.Column);
            emptycols.ShowDialog(this);
        }

        private void emptylines_Click(object sender, EventArgs e)
        {
            var utilDialog = new UtilDialog(ActiveEditor.Tb, InsertType.Line);
            utilDialog.ShowDialog(this);
        }

        private void mimultimacro_Click(object sender, EventArgs e)
        {
            if (ActiveEditor == null) return;
            var macrodlg = new MacroExecDialog(ActiveEditor.Tb);
            macrodlg.ShowDialog(this);
        }

        private void commanderMenu_Click(object sender, EventArgs e)
        {
            using (var console = new Commander(this))
            {
                console.StartPosition = FormStartPosition.CenterParent;
                console.LangMenu = langmenu;
                console.ShowDialog(this);
            }
        }

        private void menuItem75_Click(object sender, EventArgs e)
        {
            if (ActiveEditor.Tb.SelectedText != "")
            {
                var trimmed = "";
                var lines = ActiveEditor.Tb.SelectedText.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                trimmed = lines.Length != 1
                    ? lines.Aggregate(trimmed, (current, line) => current + (line.TrimEnd() + Environment.NewLine))
                    : ActiveEditor.Tb.SelectedText.TrimEnd();
                ActiveEditor.Tb.SelectedText = trimmed;
            }
            else
            {
                MessageBox.Show("Noting Selected to Perform Function", "Ynote Classic");
            }
        }

        private void menuItem76_Click(object sender, EventArgs e)
        {
            if (ActiveEditor.Tb.SelectedText != "")
            {
                var trimmed = string.Empty;
                var lines = ActiveEditor.Tb.SelectedText.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                trimmed = lines.Length != 1
                    ? lines.Aggregate(trimmed, (current, line) => current + (line.TrimStart() + Environment.NewLine))
                    : ActiveEditor.Tb.SelectedText.TrimStart();
                ActiveEditor.Tb.SelectedText = trimmed;
            }
            else
            {
                MessageBox.Show("Noting Selected to Perform Function", "Ynote Classic");
            }
        }

        private void menuItem79_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ActiveEditor.Tb.SelectedText))
            {
                var trimmed = "";
                var lines = ActiveEditor.Tb.SelectedText.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                trimmed = lines.Length != 1
                    ? lines.Aggregate(trimmed, (current, line) => current + (line.Trim() + Environment.NewLine))
                    : ActiveEditor.Tb.SelectedText.Trim();
                ActiveEditor.Tb.SelectedText = trimmed;
            }
            else
            {
                MessageBox.Show("Noting Selected to Perform Function", "Ynote Classic");
            }
        }

        private void menuItem78_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ActiveEditor.Tb.SelectedText))
                ActiveEditor.Tb.SelectedText = ActiveEditor.Tb.SelectedText.Replace("\r\n", " ");
            else
                MessageBox.Show("Nothing Selected to Perfrom Function", "Ynote Classic");
        }

        private void menuItem80_Click(object sender, EventArgs e)
        {
            if (ActiveEditor.Tb.SelectedText != "")
                ActiveEditor.Tb.SelectedText = ActiveEditor.Tb.SelectedText.Replace(" ", "\r\n");
            else
                MessageBox.Show("Nothing Selected to Perfrom Function", "Ynote Classic");
        }

        private void menuItem77_Click(object sender, EventArgs e)
        {
            if (ActiveEditor.Tb.SelectedText != "")
                ActiveEditor.Tb.SelectedText = ActiveEditor.Tb.SelectedText.Replace("\r\n", string.Empty);
            else
                MessageBox.Show("Nothing Selected to Perfrom Function", "Ynote Classic");
        }

        private void menuItem84_Click(object sender, EventArgs e)
        {
            var fileswitcher = new SwitchFile(this) {StartPosition = FormStartPosition.CenterParent};
            fileswitcher.ShowDialog(this);
        }

        private void mishowunsaved_Click(object sender, EventArgs e)
        {
            if (ActiveEditor.IsSaved)
            {
                var extension = Path.GetExtension(ActiveEditor.Name);
                var filename = Path.GetTempFileName() + extension;
                File.WriteAllText(filename, ActiveEditor.Tb.Text);
                var diff = new Diff(ActiveEditor.Name, filename);
                diff.DoCompare();
                diff.Show(Panel, DockState.Document);
            }
            else
            {
                MessageBox.Show("File Is Not Saved!", "Ynote Classic");
            }
        }

        private void mifindNext_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press F3 to Find Next");
        }

        private void mitrimpunctuation_Click(object sender, EventArgs e)
        {
            if (ActiveEditor.Tb.SelectedText != "")
            {
                var trimmed = "";
                var lines = ActiveEditor.Tb.SelectedText.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                trimmed = lines.Length != 1
                    ? lines.Aggregate(trimmed,
                        (current, line) => current + (TrimPunctuation(line) + Environment.NewLine))
                    : TrimPunctuation(ActiveEditor.Tb.SelectedText);
                ActiveEditor.Tb.SelectedText = trimmed;
            }
            else
            {
                MessageBox.Show("Noting Selected to Perform Function", "Ynote Classic");
            }
        }

        private void mihiddenchars_Click(object sender, EventArgs e)
        {
            mihiddenchars.Checked = !mihiddenchars.Checked;
            SettingsBase.HiddenChars = mihiddenchars.Checked; // If hiddenchars is checked
        }

        private void mitocrlf_Click(object sender, EventArgs e)
        {
            ActiveEditor.Tb.Text = ActiveEditor.Tb.Text.Replace(GetTextLineEnding(ActiveEditor.Tb.Text), "\r\n");
        }

        private void mitocr_Click(object sender, EventArgs e)
        {
            ActiveEditor.Tb.Text = ActiveEditor.Tb.Text.Replace(GetTextLineEnding(ActiveEditor.Tb.Text), "\r");
        }

        private void mitolf_Click(object sender, EventArgs e)
        {
            ActiveEditor.Tb.Text = ActiveEditor.Tb.Text.Replace(GetTextLineEnding(ActiveEditor.Tb.Text), "\n");
        }

        private void removelinemenu_Click(object sender, EventArgs e)
        {
            ActiveEditor.Tb.ClearCurrentLine();
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
            })
            {
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK) return;
                var item = sender as MenuItem;
                if (item != null) OpenFile(dialog.FileName, item.Tag as Encoding);
            }
        }

        private void saveencoding_click(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null) SaveEditor(ActiveEditor, menuItem.Tag as Encoding);
        }

        private void misaveencoding_Select(object sender, EventArgs e)
        {
            if (misaveencoding.MenuItems.Count == 0)
                misaveencoding.MenuItems.AddRange(BuildEncodingList(saveencoding_click));
        }

        private void menuItem95_Click(object sender, EventArgs e)
        {
            var manager = new ProjectPanel(this);
            manager.Show(Panel, DockState.DockLeft);
        }

        private void mimacros_Select(object sender, EventArgs e)
        {
        }

        private void macroitem_click(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            if (item != null) ActiveEditor.Tb.MacrosManager.ExecuteMacros(item.Name);
            /*ActiveEditor.tb.MacrosManager.Macros = File.ReadAllText(item.Name));
            ActiveEditor.tb.MacrosManager.ExecuteMacros();*/
        }

        private void mispacestotab_Click(object sender, EventArgs e)
        {
            var length = ActiveEditor.Tb.TabLength;
            var builder = new StringBuilder();
            for (var i = 0; i < length; i++)
                builder.Append(" ");
            ActiveEditor.Tb.SelectedText = ActiveEditor.Tb.SelectedText.Replace(builder.ToString(), "\t");
        }

        private void mitabtospaces_Click(object sender, EventArgs e)
        {
            var length = ActiveEditor.Tb.TabLength;
            var builder = new StringBuilder();
            for (var i = 0; i < length; i++)
                builder.Append(" ");
            ActiveEditor.Tb.SelectedText = ActiveEditor.Tb.SelectedText.Replace("\t", builder.ToString());
        }

        private void scriptitem_clicked(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            if (item != null) YnoteScript.RunScript(this, item.Name);
        }

        private void menuItem100_Click(object sender, EventArgs e)
        {
            var fctb = ActiveEditor.Tb;
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
                        .FirstOrDefault(c => ActiveEditor != null && c.Text == ActiveEditor.Tb.Language.ToString());
                if (item != null) item.Checked = true;
            }

            //    langmenu.GetMenuByName("Text").Checked = true;
        }

        private void langmenu_MouseEnter(object sender, EventArgs e)
        {
            if (langmenu.DropDownItems.Count != 0) return;
            var menus = Enum.GetValues(typeof (Language)).Cast<Language>();

            foreach (var m in menus.Select(lang => new ToolStripMenuItem(lang.ToString())))
            {
                m.Click += langitem_Click;
                langmenu.DropDownItems.Add(m);
            }
            foreach (var item in SyntaxHighlighter.LoadedSyntaxes)
                langmenu.DropDownItems.Add(new ToolStripMenuItem(Path.GetFileNameWithoutExtension(item.SysPath), null,
                    langitem_Click)
                {
                    Tag = item
                });
        }

        private void langitem_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            if (item.Tag == null)
            {
                var lang = item.Text.ToEnum<Language>();
                ActiveEditor.Tb.Language = lang;
                ActiveEditor.Highlighter.HighlightSyntax(ActiveEditor.Tb.Language,
                    new TextChangedEventArgs(ActiveEditor.Tb.Range));
                ActiveEditor.Syntax = null;
            }
            else
            {
                var synbase = item.Tag as SyntaxBase;
                ActiveEditor.Syntax = synbase;
                ActiveEditor.Highlighter.HighlightSyntax(synbase, new TextChangedEventArgs(ActiveEditor.Tb.Range));
            }
            langmenu.Text = item.Text;
        }

        private void mirun_Click(object sender, EventArgs e)
        {
            if (ActiveEditor == null) return;
            if (ActiveEditor.IsSaved)
            {
                var run = new RunDialog(ActiveEditor.Name, Panel);
                run.Show();
            }
            else
            {
                MessageBox.Show("Please Save the Current Document before proceeding!", "RunScript Executor");
            }
        }

        private void miruneditor_Click(object sender, EventArgs e)
        {
            var editor = new RunScriptEditor {StartPosition = FormStartPosition.CenterParent};
            editor.ShowDialog(this);
        }

        private void dock_ActiveDocumentChanged(object sender, EventArgs e)
        {
            if (ActiveEditor == null) return;
            langmenu.Text = ActiveEditor.Syntax == null
                ? ActiveEditor.Tb.Language.ToString()
                : Path.GetFileNameWithoutExtension(ActiveEditor.Syntax.SysPath);
            if (_incrementalSearcher != null && _incrementalSearcher.Visible) _incrementalSearcher.Tb = ActiveEditor.Tb;
        }

        private void migoleftbracket_Click(object sender, EventArgs e)
        {
            ActiveEditor.Tb.GoLeftBracket('(', ')');
        }

        private void migorightbracket_Click(object sender, EventArgs e)
        {
            ActiveEditor.Tb.GoRightBracket('(', ')');
        }

        private void migoleftbracket2_Click(object sender, EventArgs e)
        {
            ActiveEditor.Tb.GoLeftBracket('{', '}');
        }

        private void migorightbracket2_Click(object sender, EventArgs e)
        {
            ActiveEditor.Tb.GoRightBracket('{', '}');
        }

        private void migoleftbracket3_Click(object sender, EventArgs e)
        {
            ActiveEditor.Tb.GoLeftBracket('[', ']');
        }

        private void migorightbracket3_Click(object sender, EventArgs e)
        {
            ActiveEditor.Tb.GoRightBracket('[', ']');
        }

        private void misortlength_Click(object sender, EventArgs e)
        {
            if (ActiveEditor == null) return;
            var lines = ActiveEditor.Tb.SelectedText.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            var results = SortByLength(lines);
            ActiveEditor.Tb.SelectedText = results.Aggregate<string, string>(null,
                (current, result) => current + (result + "\r\n"));
        }

        private void mimarkRed_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
                ActiveEditor.Tb.Selection.SetStyle(new TextStyle(null, new SolidBrush(Color.FromArgb(180, Color.Red)),
                    FontStyle.Regular));
        }

        private void mimarkblue_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
                ActiveEditor.Tb.Selection.SetStyle(new TextStyle(null, new SolidBrush(Color.FromArgb(180, Color.Blue)),
                    FontStyle.Regular));
        }

        private void mimarkgray_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
                ActiveEditor.Tb.Selection.SetStyle(new TextStyle(null,
                    new SolidBrush(Color.FromArgb(180, Color.Gainsboro)), FontStyle.Regular));
        }

        private void mimarkgreen_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
                ActiveEditor.Tb.Selection.SetStyle(new TextStyle(null, new SolidBrush(Color.FromArgb(180, Color.Green)),
                    FontStyle.Regular));
        }

        private void mimarkyellow_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
                ActiveEditor.Tb.Selection.SetStyle(new TextStyle(null, new SolidBrush(Color.FromArgb(180, Color.Yellow)),
                    FontStyle.Regular));
        }

        private void miclearMarked_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.Selection.ClearStyle(StyleIndex.All);
        }

        private void misnippets_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null && ActiveEditor.AutoCompleteMenu != null)
                ActiveEditor.AutoCompleteMenu.Show(true);
        }

        private void miseltohex_Click(object sender, EventArgs e)
        {
            if (ActiveEditor == null) return;
            var selected = ActiveEditor.Tb.SelectedText;
            var bytes = Encoding.Default.GetBytes(selected);
            ActiveEditor.Tb.SelectedText = BitConverter.ToString(bytes).Replace("-", "");
        }

        private void miseltoASCII_Click(object sender, EventArgs e)
        {
            var asciiBytes = Encoding.ASCII.GetBytes(ActiveEditor.Tb.SelectedText);
            var builder = new StringBuilder();
            foreach (var b in asciiBytes)
                builder.Append(b + " ");
            ActiveEditor.Tb.SelectedText = builder.ToString();
        }

        private void miupdates_Click(object sender, EventArgs e)
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

        private void migoogle_Click(object sender, EventArgs e)
        {
            var console = new Commander(this) {StartPosition = FormStartPosition.CenterParent};
            console.AddText("Google:");
            console.ShowDialog(this);
        }

        private void miwiki_Click(object sender, EventArgs e)
        {
            var console = new Commander(this) {StartPosition = FormStartPosition.CenterParent};
            console.AddText("Wikipedia:");
            console.ShowDialog(this);
        }

        private void micomparedocwith_Click(object sender, EventArgs e)
        {
            if (ActiveEditor == null) return;
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "All Files (*.*)|*.*";
                var ok = ofd.ShowDialog() == DialogResult.OK;
                if (ok)
                {
                    var diff = new Diff(ActiveEditor.Name, ofd.FileName);
                    diff.Show(Panel, DockState.Document);
                }
            }
        }

        private void micopyhtml_Click(object sender, EventArgs e)
        {
            if (ActiveEditor.Tb.Html != null) Clipboard.SetText(ActiveEditor.Tb.Html);
        }

        private void micopyrtf_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) Clipboard.SetText(ActiveEditor.Tb.Rtf);
        }

        private void minewscript_Click(object sender, EventArgs e)
        {
            CreateNewDoc();
            ActiveEditor.Text = "Script";
            ActiveEditor.Tb.Text =
                "using SS.Ynote.Classic;\r\n\r\nstatic void Run(IYnote ynote)\r\n{\r\n// your code\r\n}";
            ActiveEditor.Highlighter.HighlightSyntax(Language.CSharp, new TextChangedEventArgs(ActiveEditor.Tb.Range));
            ActiveEditor.Tb.Language = Language.CSharp;
        }

        private void minewsnippet_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
                OpenFile(string.Format(@"{0}Snippets\{1}.ynotesnippet", SettingsBase.SettingsDir,
                    ActiveEditor.Tb.Language));
        }

        private void midocinfo_Click(object sender, EventArgs e)
        {
            var allwords = Regex.Matches(ActiveEditor.Tb.Text, @"[\S]+");
            var selectionWords = Regex.Matches(ActiveEditor.Tb.SelectedText, @"[\S]+");
            var message =
                string.Format(
                    "Words : {0}\r\nSelected Words : {1}\r\nLines : {2}\r\nColumn : {3}", allwords.Count,
                    selectionWords.Count
                    , ActiveEditor.Tb.LinesCount, ActiveEditor.Tb.Selection.Start.iChar + 1);
            MessageBox.Show(message, "Document Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void miclose_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
                ActiveEditor.Close();
        }

        private void miprojpage_Click(object sender, EventArgs e)
        {
            Process.Start("http://ynoteclassic.codeplex.com");
        }

        private void mibugreport_Click(object sender, EventArgs e)
        {
            Process.Start("http://ynoteclassic.codeplex.com/workitem/list/basic");
        }

        private void miforum_Click(object sender, EventArgs e)
        {
            Process.Start("http://ynoteclassic.codeplex.com/discussions");
        }

        private void filemenu_Select(object sender, EventArgs e)
        {
            if (recentfilesmenu.MenuItems.Count != 0) return;
            if(_mru == null) LoadRecentList();
            foreach (var r in _mru)
                AddRecentFile(r);
        }

        private void macrosmenu_Select(object sender, EventArgs e)
        {
            mimacros.MenuItems.Clear();
            var mnum = 0;
            foreach (
                var item in
                    Directory.GetFiles(SettingsBase.SettingsDir + @"Macros\", "*.ymc")
                        .Select(
                            file => new MenuItem(Path.GetFileNameWithoutExtension(file), macroitem_click) {Name = file})
                )
            {
                if (mnum < 10)
                    item.Shortcut = ("Alt" + mnum).ToEnum<Shortcut>();
                mnum++;
                mimacros.MenuItems.Add(item);
            }
            if (miscripts.MenuItems.Count != 0) return;
            var si = 0;
            foreach (
                var item in
                    Directory.GetFiles(SettingsBase.SettingsDir + @"Scripts\", "*.ys")
                        .Select(
                            file =>
                                new MenuItem(Path.GetFileNameWithoutExtension(file), scriptitem_clicked) {Name = file}))
            {
                if (si < 10)
                    item.Shortcut = ("Ctrl" + si).ToEnum<Shortcut>();
                si++;
                miscripts.MenuItems.Add(item);
            }
        }

        #endregion Events
    }
}