#define DEVBUILD

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using SS.Ynote.Classic.Core;
using SS.Ynote.Classic.Core.Extensibility;
using SS.Ynote.Classic.Core.Project;
using SS.Ynote.Classic.Core.Search;
using SS.Ynote.Classic.Core.Settings;
using SS.Ynote.Classic.Core.Syntax;
using SS.Ynote.Classic.Extensibility;
using SS.Ynote.Classic.Extensibility.Packages;
using SS.Ynote.Classic.UI;
using WeifenLuo.WinFormsUI.Docking;
using AutocompleteItem = AutocompleteMenuNS.AutocompleteItem;
using Timer = System.Windows.Forms.Timer;

namespace SS.Ynote.Classic
{
    public partial class MainForm : Form, IYnote
    {
        #region Private Fields

        /// <summary>
        ///     Incremental Searcher
        /// </summary>
        private IncrementalSearcher _incrementalSearcher;

        /// <summary>
        ///     Recent Files List
        /// </summary>
        private Queue<string> _mru;

        /// <summary>
        ///     Recent Project list
        /// </summary>
        private IList<string> _projs;

        /// <summary>
        ///     The index of the Shell
        /// </summary>
        private int shell_num;

        #endregion Private Fields

        #region Properties

        /// <summary>
        ///     Active Editor
        /// </summary>
        private Editor ActiveEditor
        {
            get { return dock.ActiveDocument as Editor; }
        }

        /// <summary>
        ///     dock
        /// </summary>
        public DockPanel Panel { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        ///     Default Constructor
        /// </summary>
        /// <param name="file"></param>
        public MainForm(string file)
        {
#if DEBUG
            var sp = new Stopwatch();
            sp.Start();
#endif
            Globals.Settings = GlobalSettings.Load(GlobalSettings.SettingsDir + "User.ynotesettings");
            InitializeComponent();
            InitSettings();
            Panel = dock;
            LoadPlugins();
            if (Globals.Settings.ShowStatusBar)
                InitTimer();
            Globals.Ynote = this;
            LoadLayout(file);
#if DEBUG
            sp.Stop();
            Debug.WriteLine(string.Format("Form Construction Time : {0} ms", sp.ElapsedMilliseconds));
    #if DEVBUILD
            GlobalSettings.BuildNumber = File.ReadAllLines(Application.StartupPath + "\\Build.number")[0].ToInt();
            GlobalSettings.BuildNumber++;
    #endif
#endif
        }

        #endregion Constructor

        #region Methods

        #region FILE/IO

        /// <summary>
        ///     Create New Document
        /// </summary>
        public void CreateNewDoc()
        {
#if DEBUG
            var watch = new Stopwatch();
            watch.Start();
#endif
            var edit = new Editor();
            edit.Text = "untitled";
            edit.Show(Panel);

#if DEBUG
            watch.Stop();
            Debug.WriteLine("CreateNewDoc()  - "+ watch.ElapsedMilliseconds + " ms");
#endif
        }

        /// <summary>
        ///     Opens a File
        /// </summary>
        /// <param name="file"></param>
        public void OpenFile(string file)
        {
            var edit = OpenEditor(file);
            edit.Show(dock, DockState.Document);
        }

        /// <summary>
        ///     SaveEditor without encoding
        /// </summary>
        /// <param name="edit"></param>
        public void SaveEditor(Editor edit)
        {
            SaveEditor(edit, Encoding.GetEncoding(Globals.Settings.DefaultEncoding));
        }

        static Editor OpenEditor(string file)
        {
            if (!File.Exists(file))
            {
                DialogResult result = MessageBox.Show("Cannot find File " + file + "\nWould you like to create it ?", "Ynote Classic",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    File.Create(file).Dispose();
                    OpenEditor(file);
                }
            }
            var edit = new Editor();
            edit.Name = file;
            if (FileTypes.FileTypesDictionary == null)
                FileTypes.BuildDictionary();
            var lang = FileTypes.GetLanguage(FileTypes.FileTypesDictionary, Path.GetExtension(file));
            edit.Text = Path.GetFileName(file);
            edit.Tb.Language = lang;
            edit.HighlightSyntax(lang);
            Encoding encoding = EncodingDetector.DetectTextFileEncoding(file) ??
                                Encoding.GetEncoding(Globals.Settings.DefaultEncoding);
            var info = new FileInfo(file);
            if (info.Length > 5242800) // if greather than approx 5mb
                edit.Tb.OpenBindingFile(file, encoding);
            else
                edit.Tb.OpenFile(file, encoding);
            edit.Tb.ReadOnly = info.IsReadOnly;
            return edit;
        }

        private void OpenFileAsync(string name)
        {
            BeginInvoke((MethodInvoker) (() => OpenFile(name)));
        }

        private static string BuildDialogFilter(string lang, FileDialog dlg)
        {
            var builder = new StringBuilder();
            builder.Append("All Files (*.*)|*.*|Text Files (*.txt)|*.txt");
            if (FileTypes.FileTypesDictionary == null)
                FileTypes.BuildDictionary();
            for (int i = 0; i < FileTypes.FileTypesDictionary.Count(); i++)
            {
                var keyval = "*" + FileTypes.FileTypesDictionary.Keys.ElementAt(i).ElementAt(0);
                builder.AppendFormat("|{0} Files ({1})|{1}", FileTypes.FileTypesDictionary.Values.ElementAt(i), keyval);
                if (lang == FileTypes.FileTypesDictionary.Values.ElementAt(i))
                    dlg.FilterIndex = i + 3;
            }
            // foreach (var extension in FileTypes.FileTypesDictionary)
            // {
            //     string keyval = null;
            //     keyval += "*" + extension.Key.ElementAt(0);
            //     builder.AppendFormat("|{0} Files ({1})|{1}", extension.Value, keyval);
            // }
            return builder.ToString();
        }

        /// <summary>
        ///     Save a typeof(Editor), with encoding.getEncoding( "name")
        /// </summary>
        /// <param name="edit"></param>
        /// <param name="encoding"></param>
        private void SaveEditor(Editor edit, Encoding encoding)
        {
            try
            {
                string fileName;
                if (!edit.IsSaved)
                {
                    using (var s = new SaveFileDialog())
                    {
                        s.Title = "Save " + edit.Text;
                        s.Filter = BuildDialogFilter(edit.Tb.Language, s);
                        if (s.ShowDialog() != DialogResult.OK) return;
                        fileName = s.FileName;
                    }
                }
                else
                {
                    fileName = edit.Name;
                }
                if (Globals.Settings.UseTabs)
                {
                    string tabSpaces = new string(' ', edit.Tb.TabLength);
                    var tx = edit.Tb.Text;
                    string text = Regex.Replace(tx, tabSpaces, "\t");
                    File.WriteAllText(fileName, text);
                }
                else
                {
                    edit.Tb.SaveToFile(fileName, encoding);
                }
                edit.Text = Path.GetFileName(fileName);
                edit.Name = fileName;
                Trace("Saved File to " + fileName, 100000);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving File !!\n" + ex.Message, null, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        ///     Reverts the File to the Original State
        /// </summary>
        private void RevertFile()
        {
            if (ActiveEditor == null || !ActiveEditor.IsSaved) return;
            ActiveEditor.Tb.OpenFile(ActiveEditor.Name);
            ActiveEditor.Text = Path.GetFileName(ActiveEditor.Name);
        }

        #endregion FILE/IO

        #region Recent Handlers

        /// <summary>
        ///     Saves Recent File to List
        /// </summary>
        private void SaveRecentFiles()
        {
            if (_mru == null)
                LoadRecentList();
            // LoadRecentList(); //load list from file
            while (_mru.Count > Convert.ToInt32(Globals.Settings.RecentFileNumber))
                //keep list number not exceeded given value
                _mru.Dequeue();
            //writing menu list to file
            using (var stringToWrite = new StreamWriter(GlobalSettings.SettingsDir + "Recent.info"))
            {
                //create file called "Recent.txt" located on app folder
                foreach (var item in _mru)
                    stringToWrite.WriteLine(item); //write list to stream
                stringToWrite.Flush(); //write stream to file
                stringToWrite.Close(); //close the stream and reclaim memory
            }
        }

        /// <summary>
        ///     Loads the List of Recent files from list
        /// </summary>
        private void LoadRecentList()
        {
            var rfPath = GlobalSettings.SettingsDir + "Recent.info";
            if (!Directory.Exists(GlobalSettings.SettingsDir))
                Directory.CreateDirectory(GlobalSettings.SettingsDir);
            if (!File.Exists(rfPath))
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

        private void LoadRecentProjects()
        {
            _projs = new List<string>();
            string file = GlobalSettings.SettingsDir + "Projects.ynote";
            if (!File.Exists(file))
                return;
            foreach (var line in File.ReadAllLines(file))
                _projs.Add(line);
        }

        private void SaveRecentProjects()
        {
            string file = GlobalSettings.SettingsDir + "Projects.ynote";
            File.WriteAllLines(file, _projs.ToArray());
        }

        #endregion

        #region Menu Builders

        /// <summary>
        ///     Builds the Language Menu -View->Syntax Highlighter->{Language(enumeration)}
        /// </summary>
        private void BuildLangMenu()
        {
            foreach (var m in SyntaxHighlighter.Scopes.Select(lang => new MenuItem(lang)))
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
            if (ActiveEditor == null) return;
            var lang = item.Text;
            ActiveEditor.HighlightSyntax(lang);
            ActiveEditor.Tb.Language = lang;
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
                return string.Empty;
            }
            // Substring.
            return value.Substring(removeFromStart,
                value.Length - removeFromEnd - removeFromStart);
        }

        #endregion

        #region MISC

        public void Trace(string message, int timeout)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                mistats.Text = message;
                status.Invalidate();
                // UpdateDocumentInfo();
                Thread.Sleep(timeout);
            });
        }

        private static string ConvertToText(string rtf)
        {
            using (var rtb = new RichTextBox())
            {
                rtb.Rtf = rtf;
                return rtb.Text;
            }
        }

        /// <summary>
        ///     Initialize Globals.Globals.Settings
        /// </summary>
        private void InitSettings()
        {
            if (!Globals.Settings.ShowMenuBar)
                ToggleMenu(false);
            dock.DocumentStyle = Globals.Settings.DocumentStyle;
            dock.DocumentTabStripLocation = Globals.Settings.TabLocation;
            mihiddenchars.Checked = Globals.Settings.HiddenChars;
            status.Visible = statusbarmenuitem.Checked = Globals.Settings.ShowStatusBar;
            toolBar.Visible = mitoolbar.Checked = Globals.Settings.ShowToolBar;
        }

        private void ToggleMenu(bool visible)
        {
            foreach (MenuItem menu in Menu.MenuItems)
                menu.Visible = visible;
        }

        /// <summary>
        ///     Initializes Custom Controls
        /// </summary>
        private void InitTimer()
        {
            var infotimer = new Timer();
            infotimer.Interval = 500;
            infotimer.Tick += (sender, args) => UpdateDocumentInfo();
            infotimer.Start();
        }

        /// <summary>
        ///     Updates the Document Info
        /// </summary>
        private void UpdateDocumentInfo()
        {
            BeginInvoke((MethodInvoker) (() =>
            {
                if (!(dock.ActiveDocument is Editor) || ActiveEditor == null) return;
                if (ActiveEditor.Tb.Selection.IsEmpty)
                {
                    int nCol = ActiveEditor.Tb.Selection.Start.iChar + 1;
                    int line = ActiveEditor.Tb.Selection.Start.iLine + 1;
                    mistats.Text = string.Format("Line {0}, Column {1}", line, nCol);
                }
                else
                {
                    mistats.Text = string.Format("{0} Characters Selected", ActiveEditor.Tb.SelectedText.Length);
                }
            }));
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

        #endregion MISC

        #region Plugins

        /// <summary>
        ///     Load Plugins
        /// </summary>
        private void LoadPlugins()
        {
            if (!Directory.Exists(GlobalSettings.SettingsDir + @"\Plugins"))
                Directory.CreateDirectory(GlobalSettings.SettingsDir + @"Plugins");
            using (var dircatalog = new DirectoryCatalog(GlobalSettings.SettingsDir + @"\Plugins"))
            {
                using (var container = new CompositionContainer(dircatalog))
                {
                    var plugins = container.GetExportedValues<IYnotePlugin>();
                    foreach (IYnotePlugin plugin in plugins)
                        plugin.Main(this);
                }
            }
        }

        #endregion Plugins

        #region Layouts

        private IDockContent GetContentFromPersistString(string persistString)
        {
            string[] parsedStrings = persistString.Split(new[] { ',' });

            if (persistString == typeof (Shell).ToString())
                return new Shell("cmd.exe", null);
            if (parsedStrings[0] == typeof (Editor).ToString())
            {
                if (parsedStrings[1] == "Editor")
                    return null;
                else
                {
                    Editor edit = OpenEditor(parsedStrings[1]);
                    return edit;
                }
            }
            if (parsedStrings[0] == typeof (ProjectPanel).ToString())
            {
                    var projp = new ProjectPanel();
                    if(parsedStrings[1] != "ProjectPanel")
                        projp.OpenProject(YnoteProject.Load(parsedStrings[1]));
                    return projp;
            }
            return null;
        }

        private void LoadLayout(string file)
        {
            if (!Globals.Settings.LoadLayout)
            {
                if (string.IsNullOrEmpty(file))
                    CreateNewDoc();
                else
                    OpenFile(file);
            }
            else
            {
                dock.SuspendLayout(true);
                string filename = Application.StartupPath + "\\Dock.xml";
                if (File.Exists(filename))
                    dock.LoadFromXml(filename, GetContentFromPersistString);
                else
                    CreateNewDoc();
                dock.ResumeLayout(true, true);
            }
        }
        #endregion

        #endregion

        #region Overrides

        protected override void OnClosing(CancelEventArgs e)
        {
            SaveRecentFiles(); 
            GlobalSettings.Save(Globals.Settings, GlobalSettings.SettingsDir + "User.ynotesettings");
            if(Globals.Settings.LoadLayout)
                dock.SaveAsXml("Dock.xml");
            if (_projs != null)
                SaveRecentProjects();
            if(Globals.ActiveProject != null && Globals.ActiveProject.IsSaved)
                dock.SaveAsXml(Globals.ActiveProject.LayoutFile);
#if DEVBUILD
            File.WriteAllText(Application.StartupPath + "\\Build.number", GlobalSettings.BuildNumber.ToString());
#endif
            base.OnClosing(e);
        }

        protected override void OnResize(EventArgs e)
        {
            if (Globals.Settings.MinimizeToTray)
            {
                var nicon = new NotifyIcon();
                nicon.Icon = Icon;
                nicon.DoubleClick += delegate
                {
                    Show();
                    WindowState = FormWindowState.Normal;
                };
                nicon.BalloonTipIcon = ToolTipIcon.Info;
                nicon.BalloonTipTitle = "Ynote Classic";
                nicon.BalloonTipText = "Ynote Classic has minimized to the System Tray";
                if (FormWindowState.Minimized == WindowState)
                {
                    nicon.Visible = true;
                    nicon.ShowBalloonTip(500);
                    Hide();
                }
                else if (FormWindowState.Normal == WindowState)
                {
                    nicon.Visible = false;
                }
            }
            base.OnResize(e);
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
                dialog.Multiselect = true;
                var res = dialog.ShowDialog() == DialogResult.OK;
                if (!res) return;
                foreach (var file in dialog.FileNames)
                {
                    OpenFileAsync(file);
                    AddRecentFile(file);
                }
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
            Shell shell;
            shell_num++;
            if (dock.ActiveDocument is Editor)
                shell = ActiveEditor.Name == "Editor"
                    ? new Shell("cmd.exe", null)
                    : new Shell("cmd.exe", "/k cd " + Path.GetDirectoryName(ActiveEditor.Name));
            else
                shell = new Shell("cmd.exe", null);

            shell.Text = "Shell" + shell_num;
            shell.Show(dock, DockState.DockBottom);
        }

        private void mifind_Click(object sender, EventArgs e)
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
            var combined = date.Add(time).ToString("DD/MM/YYYY");
            if (ActiveEditor != null) ActiveEditor.Tb.InsertText(combined);
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

        private void mifindinfiles_Click(object sender, EventArgs e)
        {
            using (var findinfiles = new FindInFiles())
            {
                findinfiles.StartPosition = FormStartPosition.CenterParent;
                findinfiles.ShowDialog(this);
            }
        }

        private void mifindinproject_Click(object sender, EventArgs e)
        {
            using (var findinfiles = new FindInFiles())
            {
                findinfiles.Directory = Globals.ActiveProject.Path;
                findinfiles.StartPosition = FormStartPosition.CenterParent;
                findinfiles.ShowDialog(this);
            }
        }

        private void mifindchar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press Alt+F+{char} you want to find", "Ynote Classic", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
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
                edit.Show(dock, DockState.Document);
            }
        }

        private void miproperties_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null && ActiveEditor.IsSaved)
                NativeMethods.ShowFileProperties(ActiveEditor.Name);
            else
                MessageBox.Show("File Not Saved!", "Ynote Classic");
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

        private void mitransparent_Click(object sender, EventArgs e)
        {
            mitransparent.Checked = !mitransparent.Checked;
            Opacity = mitransparent.Checked ? 0.7 : 1;
        }

        private void mifullscreen_Click(object sender, EventArgs e)
        {
            mifullscreen.Checked = !mifullscreen.Checked;
            if (!mifullscreen.Checked)
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
            }
            else
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
        }

        private void wordwrapmenu_Click(object sender, EventArgs e)
        {
            wordwrapmenu.Checked = !wordwrapmenu.Checked;
            foreach (var document in dock.Documents)
                if (document is Editor)
                    (document as Editor).Tb.WordWrap = wordwrapmenu.Checked;
            Globals.Settings.WordWrap = wordwrapmenu.Checked;
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
            Globals.Settings.Zoom = i;
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
            BeginInvoke((MethodInvoker) (() => SaveEditor(ActiveEditor)));
        }

        private void misaveas_Click(object sender, EventArgs e)
        {
            if (ActiveEditor == null) return;
            using (var sf = new SaveFileDialog())
            {
                sf.Filter = BuildDialogFilter(ActiveEditor.Tb.Language, sf);
                if (sf.ShowDialog() != DialogResult.OK) return;
                ActiveEditor.Tb.SaveToFile(sf.FileName, Encoding.GetEncoding(Globals.Settings.DefaultEncoding));
                ActiveEditor.Text = Path.GetFileName(sf.FileName);
                ActiveEditor.Name = sf.FileName;
            }
        }

        private void misaveall_Click(object sender, EventArgs e)
        {
            foreach (Editor doc in dock.Documents)
                BeginInvoke((MethodInvoker) (() => SaveEditor(doc)));
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
                sf.Filter = "Ynote Macro File(*.ynotemacro)|*.ynotemacro";
                sf.InitialDirectory = GlobalSettings.SettingsDir + @"User\";
                if (sf.ShowDialog() != DialogResult.OK)
                    return;
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
                    if (Path.GetExtension(ActiveEditor.Name) == ".ys")
                        YnoteScript.RunScript(this, ActiveEditor.Name);
                    else
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

        private void mitoolbar_Click(object sender, EventArgs e)
        {
            mitoolbar.Checked = !mitoolbar.Checked;
            toolBar.Visible = mitoolbar.Checked;
            Globals.Settings.ShowToolBar = mitoolbar.Checked;
        }

        private void statusbarmenuitem_Click(object sender, EventArgs e)
        {
            status.Visible = !statusbarmenuitem.Checked;
            statusbarmenuitem.Checked = !statusbarmenuitem.Checked;
            Globals.Settings.ShowStatusBar = statusbarmenuitem.Checked;
        }

        private void mincrementalsearch_Click(object sender, EventArgs e)
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
            RevertFile();
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
            Globals.Settings.ThemeFile = m.Name;
        }

        private void colorschememenu_Select(object sender, EventArgs e)
        {
            if (colorschememenu.MenuItems.Count != 0) return;
            foreach (
                var menuitem in
                    Directory.GetFiles(GlobalSettings.SettingsDir, "*.ynotetheme", SearchOption.AllDirectories)
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
            milanguage.GetMenuByName(ActiveEditor.Tb.Language.ToString()).Checked = true;
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
            var items = new List<AutocompleteItem>();
            foreach (var bookmark in ActiveEditor.Tb.Bookmarks)
                items.Add(
                    new CommandAutocompleteItem(bookmark.LineIndex + 1 + ":" + ActiveEditor.Tb[bookmark.LineIndex].Text));
            var bookmarkwindow = new CommandWindow(items);
            bookmarkwindow.ProcessCommand += bookmarkwindow_ProcessCommand;
            bookmarkwindow.ShowDialog(this);
        }

        private void bookmarkwindow_ProcessCommand(object sender, CommandWindowEventArgs e)
        {
            var markCommand = YnoteCommand.FromString(e.Text);
            int index = markCommand.Key.ToInt() - 1;
            foreach (var bookmark in ActiveEditor.Tb.Bookmarks)
                if (bookmark.LineIndex == index)
                    bookmark.DoVisible();
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
            using (var console = new Commander())
            {
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
            var items = new List<AutocompleteItem>();
            foreach (var doc in dock.Documents)
                items.Add(new FuzzyAutoCompleteItem((doc as DockContent).Text));
            var fileswitcher = new CommandWindow(items);
            fileswitcher.ProcessCommand += fileswitcher_ProcessCommand;
            fileswitcher.ShowDialog(this);
        }

        private void fileswitcher_ProcessCommand(object sender, CommandWindowEventArgs e)
        {
            foreach (DockContent content in dock.Documents)
            {
                if (content.Text == e.Text)
                {
                    content.Show(dock);
                }
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
            Globals.Settings.HiddenChars = mihiddenchars.Checked; // If hiddenchars is checked
        }

        private void removelinemenu_Click(object sender, EventArgs e)
        {
            ActiveEditor.Tb.ClearCurrentLine();
        }

        private void macroitem_click(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            if (item != null) ActiveEditor.Tb.MacrosManager.ExecuteMacros(item.Name);
            /*ActiveEditor.tb.MacrosManager.Macros = File.ReadAllText(item.Name));
            ActiveEditor.tb.MacrosManager.ExecuteMacros();*/
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
                File.WriteAllText(GlobalSettings.SettingsDir + "User.ynotekeys", form.GetHotkeys().ToString());
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
            if (SyntaxHighlighter.Scopes == null)
                return;
            foreach (var m in SyntaxHighlighter.Scopes.Select(lang => new ToolStripMenuItem(lang.ToString())))
            {
                m.Click += langitem_Click;
                langmenu.DropDownItems.Add(m);
            }
        }

        private void langitem_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            var lang = item.Text;
            ActiveEditor.Tb.Language = lang;
            ActiveEditor.HighlightSyntax(ActiveEditor.Tb.Language);
            langmenu.Text = item.Text;
        }
        private void mirunscripts_Click(object sender, EventArgs e)
        {
            if (ActiveEditor == null) return;
            if (ActiveEditor.IsSaved)
            {
                var run = new RunDialog(ActiveEditor.Name, dock);
                run.Show();
            }
            else
            {
                MessageBox.Show("Please Save the Current Document before proceeding!", "RunScript Executor");
            }
        }
        private void dock_ActiveDocumentChanged(object sender, EventArgs e)
        {
            if (ActiveEditor == null) return;
            langmenu.Text = ActiveEditor.Tb.Language;
            if (_incrementalSearcher != null && _incrementalSearcher.Visible) _incrementalSearcher.Tb = ActiveEditor.Tb;
        }

        private void migoleftbracket_Click(object sender, EventArgs e)
        {
            ActiveEditor.Tb.GoLeftBracket();
        }

        private void migorightbracket_Click(object sender, EventArgs e)
        {
            ActiveEditor.Tb.GoRightBracket();
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
            ActiveEditor.ForceAutoComplete();
        }
        private void miupdates_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + "\\sup.exe",
                    Application.StartupPath + @"\Config\AppFiles.xml http://updateServer.com/UpdateFiles.xml " +
                    Application.ProductVersion);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void migoogle_Click(object sender, EventArgs e)
        {
            var console = new Commander();
            console.AddText("Google:");
            console.ShowDialog(this);
        }

        private void miwiki_Click(object sender, EventArgs e)
        {
            var console = new Commander();
            console.AddText("Wikipedia:");
            console.ShowDialog(this);
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
            "using SS.Ynote.Classic;\r\n\r\nstatic void Main(IYnote ynote)\r\n{\r\n// your code\r\n}";
            ActiveEditor.HighlightSyntax("CSharp");
            ActiveEditor.Tb.Language = "CSharp";
            ActiveEditor.Tb.DoAutoIndent();
        }

        private void minewsnippet_Click(object sender, EventArgs e)
        {
            CreateNewDoc();
            ActiveEditor.Text = "Snippet";
            ActiveEditor.Tb.Text =
            "<?xml version=\"1.0\"?>\n<YnoteSnippet>\n<description></description>\n<content><!-- content of your snippet --></content>\n<tabTrigger><!-- text to trigger the snippet --></tabTrigger>\n<scope><!-- the scope of the snippet --></scope>\n</YnoteSnippet>";
            ActiveEditor.HighlightSyntax("Xml");
            ActiveEditor.Tb.Language = "Xml"; 
            ActiveEditor.Tb.DoAutoIndent();
        }

        private void midocinfo_Click(object sender, EventArgs e)
        {
            if (!(dock.ActiveDocument is Editor) || ActiveEditor == null) return;
            var allwords = Regex.Matches(ActiveEditor.Tb.Text, @"[\S]+");
            var selectionWords = Regex.Matches(ActiveEditor.Tb.SelectedText, @"[\S]+");
            var message = string.Empty;
            var startline = ActiveEditor.Tb.Selection.Start.iLine;
            var endlines = ActiveEditor.Tb.Selection.End.iLine;
            var sellines = (endlines - startline) + 1;
            if (ActiveEditor.IsSaved)
            {
                var enc = EncodingDetector.DetectTextFileEncoding(ActiveEditor.Name) ?? Encoding.Default;
                message =
                    string.Format(
                        "Encoding : {4}\r\nWords : {0}\r\nSelected Words : {1}\r\nLines : {2}\r\nSelected Lines : {5}\r\nColumn : {3}",
                        allwords.Count, selectionWords.Count, ActiveEditor.Tb.LinesCount,
                        ActiveEditor.Tb.Selection.Start.iChar + 1,
                        enc.EncodingName, sellines);
            }
            else
                message =
                    string.Format(
                        "Words : {0}\r\nSelected Words : {1}\r\nLines : {2}\r\nSelected Lines : {4}\r\nColumn : {3}",
                        allwords.Count, selectionWords.Count, ActiveEditor.Tb.LinesCount,
                        ActiveEditor.Tb.Selection.Start.iChar + 1, sellines);
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
            if (_mru == null) LoadRecentList();
            foreach (var r in _mru)
                AddRecentFile(r);
        }

        private void macrosmenu_Select(object sender, EventArgs e)
        {
            mimacros.MenuItems.Clear();
            var mnum = 0;
            foreach (
                var item in
                    Directory.GetFiles(GlobalSettings.SettingsDir, "*.ynotemacro", SearchOption.AllDirectories)
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
                    Directory.GetFiles(GlobalSettings.SettingsDir, "*.ys", SearchOption.AllDirectories)
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

        private void miprotectfile_Click(object sender, EventArgs e)
        {
            if (ActiveEditor == null || !ActiveEditor.IsSaved)
            {
                MessageBox.Show("Error : Document not saved !", null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            using (var dlg = new PasswordDialog())
            {
                var result = dlg.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    var bytes = Encryption.Encrypt(File.ReadAllBytes(ActiveEditor.Name), dlg.Password);
                    if (bytes != null)
                        File.WriteAllBytes(ActiveEditor.Name, bytes);
                }
            }
            RevertFile();
        }

        private void midecryptfile_Click(object sender, EventArgs e)
        {
            if (ActiveEditor == null || !ActiveEditor.IsSaved)
            {
                MessageBox.Show("Error : Document not saved !", null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            using (var dlg = new PasswordDialog())
            {
                var result = dlg.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    var bytes = Encryption.Decrypt(File.ReadAllBytes(ActiveEditor.Name), dlg.Password);
                    if (bytes != null)
                        File.WriteAllBytes(ActiveEditor.Name, bytes);
                }
            }
            RevertFile();
        }

        private void minewsyntax_Click(object sender, EventArgs e)
        {
            CreateNewDoc();
            ActiveEditor.Text = "SyntaxFile";
            ActiveEditor.Tb.Text =
                "<?xml version=\"1.0\"?>\r\n\t<YnoteSyntax>\r\n\t\t<Syntax CommentPrefix=\"\" Extensions=\"\"/>\r\n\t\t<Brackets Left=\"\" Right=\"\"/>\r\n\t\t<Rule Type=\"\" Regex=\"\"/>\r\n\t\t<Folding Start=\"\" End=\"\"/>\r\n\t</YnoteSyntax>";
            ActiveEditor.HighlightSyntax("Xml");
            ActiveEditor.Tb.Language = "Xml";
        }

        private void miinscliphis_Click(object sender, EventArgs e)
        {
            var lst = new List<AutocompleteItem>();
            foreach (Editor doc in dock.Documents.OfType<Editor>())
                foreach (var historyitem in doc.Tb.ClipboardHistory)
                    lst.Add(new AutocompleteItem(historyitem));
            using (var cmw = new CommandWindow(lst))
            {
                cmw.ProcessCommand += (o, args) => ActiveEditor.Tb.InsertText(args.Text);
                cmw.ShowDialog(this);
            }
        }


        private void misplitbelow_Click(object sender, EventArgs e)
        {
            var splitedit = new Editor {Name = ActiveEditor.Name, Text = "[Split] " + ActiveEditor.Text};
            splitedit.Tb.SourceTextBox = ActiveEditor.Tb;
            splitedit.Tb.ReadOnly = true;
            splitedit.Show(ActiveEditor.Pane, DockAlignment.Bottom, 0.5);
        }

        private void misplitbeside_Click(object sender, EventArgs e)
        {
            var splitedit = new Editor {Name = ActiveEditor.Name, Text = "[Split] " + ActiveEditor.Text};
            splitedit.Tb.SourceTextBox = ActiveEditor.Tb;
            splitedit.Tb.ReadOnly = true;
            splitedit.Show(ActiveEditor.Pane, DockAlignment.Right, 0.5);
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            var splitedit = new Editor {Name = ActiveEditor.Name, Text = "[Split] " + ActiveEditor.Text};
            splitedit.Tb.SourceTextBox = ActiveEditor.Tb;
            splitedit.Tb.ReadOnly = true;
            ActiveEditor.Tb.VisibleRangeChangedDelayed +=
                (o, args) =>
                    UpdateScroll(splitedit.Tb, ActiveEditor.Tb.VerticalScroll.Value,
                        ActiveEditor.Tb.Selection.Start.iLine);
            splitedit.Show(ActiveEditor.Pane, DockAlignment.Right, 0.5);
        }

        private void UpdateScroll(FastColoredTextBox tb, int vPos, int curLine)
        {
            //
            if (vPos <= tb.VerticalScroll.Maximum)
            {
                tb.VerticalScroll.Value = vPos;
                tb.UpdateScrollbars();
            }

            if (curLine < tb.LinesCount)
                tb.Selection = new Range(tb, 0, curLine, 0, curLine);
        }

        private void mimap_Click(object sender, EventArgs e)
        {
            mimap.Checked = !mimap.Checked;
            foreach (Editor content in dock.Documents.OfType<Editor>())
                content.ShowDocumentMap = mimap.Checked;
        }

        private void viewmenu_Select(object sender, EventArgs e)
        {
            if (ActiveEditor == null)
                return;
            mimap.Checked = Globals.Settings.ShowDocumentMap;
            midistractionfree.Checked = ActiveEditor.DistractionFree;
            wordwrapmenu.Checked = Globals.Settings.WordWrap;
            mimenu.Checked = Globals.Settings.ShowMenuBar;
            mihiddenchars.Checked = Globals.Settings.HiddenChars;
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            var console = new Commander();
            console.IsSnippetMode = true;
            console.ShowDialog(this);
        }

        private void distractionfree_Click(object sender, EventArgs e)
        {
            if (Panel.ActiveDocument == null || !(Panel.ActiveDocument is Editor))
                return;
            if (ActiveEditor.DistractionFree)
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
            }
            else
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            foreach(DockContent edit in dock.Contents)
                if(edit is Editor)
                    ((Editor)(edit)).ToggleDistrationFreeMode();
            Globals.DistractionFree = ActiveEditor.DistractionFree;
        }

        private void commentmenu_Click(object sender, EventArgs e)
        {
            ActiveEditor.Tb.CommentSelected();
        }

        private void mimenu_Click(object sender, EventArgs e)
        {
            mimenu.Checked = !mimenu.Checked;
            ToggleMenu(mimenu.Checked);
            Globals.Settings.ShowMenuBar = mimenu.Checked;
        }


        private void migotosymbol_Click(object sender, EventArgs e)
        {
            if (ActiveEditor == null)
                return;
            var lst = new List<AutocompleteItem>();
            Regex regex = ActiveEditor.Tb.ClassNameRegex;
            if (regex == null)
                return;
            var matches = regex.Matches(ActiveEditor.Tb.Text);
            foreach (Match match in matches)
            {
                var capture = match.Captures[0];
                lst.Add(new FuzzyAutoCompleteItem(capture.Value) {Tag = capture.Index});
            }
            var cwin = new CommandWindow(lst);
            cwin.ProcessCommand += cwin_ProcessCommand;
            cwin.Tag = lst;
            cwin.ShowDialog(this);
        }

        private void cwin_ProcessCommand(object sender, CommandWindowEventArgs e)
        {
            foreach (var item in (sender as CommandWindow).Tag as IEnumerable<AutocompleteItem>)
                if (item.Text == e.Text)
                {
                    ActiveEditor.Tb.SelectionStart = (int) (item.Tag);
                    ActiveEditor.Tb.DoSelectionVisible();
                }
        }

        private void miclearbookmarks_Click(object sender, EventArgs e)
        {
            var bookmarkedlines = new List<int>();
            foreach (var bookmark in ActiveEditor.Tb.Bookmarks)
                bookmarkedlines.Add(bookmark.LineIndex);
            foreach (var i in bookmarkedlines)
                ActiveEditor.Tb.UnbookmarkLine(i);
        }

        private void mireindentline_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null) ActiveEditor.Tb.DoAutoIndent(ActiveEditor.Tb.Selection.Start.iLine);
        }

        private void miselectedfile_Click(object sender, EventArgs e)
        {
            try
            {
                if (ActiveEditor.IsSaved)
                {
                    var dir = Path.GetDirectoryName(ActiveEditor.Name);
                    string filename = ActiveEditor.Tb.SelectedText;
                    if (Path.IsPathRooted(filename))
                        OpenFile(ActiveEditor.Tb.SelectedText);
                    else
                        foreach (var file in Directory.GetFiles(dir))
                            if (Path.GetFileName(file) == filename || Path.GetFileNameWithoutExtension(file) == filename)
                                OpenFile(file);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void migotofileinproject_Click(object sender, EventArgs e)
        {
            var autocompletelist = new List<AutocompleteItem>();
            var files = Directory.GetFiles(Globals.ActiveProject.Path, "*.*", SearchOption.AllDirectories);
            foreach (var file in files)
                autocompletelist.Add(new FuzzyAutoCompleteItem(Path.GetFileName(file)));
            var commandwindow = new CommandWindow(autocompletelist);
            commandwindow.ProcessCommand += commandwindow_ProcessCommand;
            commandwindow.ShowDialog(this);
        }

        private void commandwindow_ProcessCommand(object sender, CommandWindowEventArgs e)
        {
            var files = Directory.GetFiles(Globals.ActiveProject.Path, "*.*", SearchOption.AllDirectories);
             foreach (var file in files)
             {
                 if (Path.GetFileName(file) == e.Text)
                 {
                     OpenFile(file);
                     return;
                 }
             }
        }


        private void miopenproject_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Ynote Project Files (*.ynoteproject)|*.ynoteproject|All Files (*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    OpenProject(YnoteProject.Load(dlg.FileName));
                    if (!_projs.Contains(dlg.FileName))
                        _projs.Add(dlg.FileName);
                }
            }
        }

        private void OpenProject(YnoteProject project)
        {
            if (Globals.ActiveProject == project || project == null)
                return;
            ProjectPanel panel = null;
            if (File.Exists(project.LayoutFile))
            {
                foreach (DockContent item in dock.Contents.ToArray())
                    item.Close();
                panel = new ProjectPanel();
                //       dock.SuspendLayout(true);
                dock.LoadFromXml(project.LayoutFile, GetContentFromPersistString);
                panel.Show(dock, DockState.DockLeft);
                panel.OpenProject(project);
                //      dock.ResumeLayout(true,true);
            }
            else
            {
                foreach (DockContent content in dock.Contents)
                    if (content is ProjectPanel)
                        panel = content as ProjectPanel;
                if (panel == null)
                    panel = new ProjectPanel();
                panel.Show(dock, DockState.DockLeft);
                panel.OpenProject(project);
            }
            this.Text = project.Name + " - Ynote Classic";
        }

        private void micloseproj_Click(object sender, EventArgs e)
        {
            ProjectPanel proj = null;
            foreach (var window in dock.Contents)
                if (window is ProjectPanel)
                    proj = window as ProjectPanel;
            if (proj != null)
            {
                proj.CloseProject();
                Globals.ActiveProject = null;
                this.Text = "Ynote Classic";
            }
        }

        private void mieditproj_Click(object sender, EventArgs e)
        {
           if (string.IsNullOrEmpty(Globals.ActiveProject.FilePath))
               return;
           OpenFile(Globals.ActiveProject.FilePath);
        }

        private void misaveproj_Click(object sender, EventArgs e)
        {
            var proj = Globals.ActiveProject;
            if (proj == null)
                return;
            if (!proj.IsSaved)
            {
                using (var dlg = new SaveFileDialog())
                {
                    dlg.Filter = "Ynote Project Files (*.ynoteproject)|*.ynoteproject";
                    var result = dlg.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        proj.Name = Path.GetFileNameWithoutExtension(dlg.FileName);
                        proj.Save(dlg.FileName);
                        proj.FilePath = dlg.FileName;
                        foreach(DockContent content in dock.Contents)
                            if(content is ProjectPanel)
                                (content as ProjectPanel).OpenProject(proj);
                        if (!_projs.Contains(proj.FilePath))
                            _projs.Add(proj.FilePath);
                    }
                }
            }
                
        }

        private void miaddtoproj_Click(object sender, EventArgs e)
        {
            using (var browser = new FolderBrowserDialogEx())
            {
                browser.ShowEditBox = true;
                browser.ShowFullPathInEditBox = true;
                if (browser.ShowDialog() == DialogResult.OK)
                {
                    var proj = new YnoteProject();
                    proj.Path = browser.SelectedPath;
                    OpenProject(proj);
                }
            }
        }

        private void miproject_Select(object sender, EventArgs e)
        {
            if (_projs == null)
                LoadRecentProjects();
            if (miopenrecent.MenuItems.Count == 0)
                foreach (var item in _projs)
                    miopenrecent.MenuItems.Add(new MenuItem(Path.GetFileNameWithoutExtension(item), openrecentproj_Click)
                    {
                        Name = item
                    });
        }

        private void openrecentproj_Click(object sender, EventArgs e)
        {
            var menu = sender as MenuItem;
            OpenProject(YnoteProject.Load(menu.Name));
        }


        private void miswitchproj_Click(object sender, EventArgs e)
        {
            if(miopenrecent.MenuItems.Count == 0)
                miproject_Select(null,null);
            var completemenu = new List<AutocompleteItem>();
            foreach (MenuItem item in miopenrecent.MenuItems)
                completemenu.Add(new FuzzyAutoCompleteItem(item.Text));
            var cwindow = new CommandWindow(completemenu);
            cwindow.ProcessCommand += cwindow_ProcessCommand;
            cwindow.ShowDialog(this);
        }

        private void cwindow_ProcessCommand(object sender, CommandWindowEventArgs e)
        {
            foreach (MenuItem item in miopenrecent.MenuItems)
            {
                if (item.Text == e.Text)
                {
                    var proj = YnoteProject.Load(item.Name);
                    OpenProject(proj);
                }
            }
        }

        private void toolBar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "newToolStripButton")
                CreateNewDoc();
            else if (e.ClickedItem.Name == "openToolStripButton")
                OpenMenuItem.PerformClick();
            else if (e.ClickedItem.Name == "saveToolStripButton")
                SaveEditor(ActiveEditor);
            else if (e.ClickedItem.Name == "cutToolStripButton")
                CutMenuItem.PerformClick();
            else if (e.ClickedItem.Name == "copyToolStripButton")
                CopyMenuItem.PerformClick();
            else if (e.ClickedItem.Name == "pasteToolStripButton")
                PasteMenuItem.PerformClick();
            else if (e.ClickedItem.Name == "addbookmark")
                Addbookmarkmenu.PerformClick();
            else if (e.ClickedItem.Name == "removebookmark")
                removebookmarkmenu.PerformClick();
            else if (e.ClickedItem.Name == "helpToolStripButton")
                miwiki.PerformClick();
        }
        private void mirefreshproj_Click(object sender, EventArgs e)
        {
            if (Globals.ActiveProject != null)
                OpenProject(Globals.ActiveProject);
        }
        #endregion
    }
}