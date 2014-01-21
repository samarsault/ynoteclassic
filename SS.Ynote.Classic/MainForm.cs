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
using FastColoredTextBoxNS;
using SS.Ynote.Classic.Plugins;
using SS.Ynote.Classic.Project;
using SS.Ynote.Classic.UI;
using WeifenLuo.WinFormsUI.Docking;
using Timer = System.Windows.Forms.Timer;

namespace SS.Ynote.Classic
{
    public partial class MainForm : Form, IYnote
    {
        private readonly Queue<string> Mru;
        private int _num;

        public MainForm(string filename)
        {
#if DEBUG
            var spstart = new Stopwatch();
            spstart.Start();
#endif
            InitializeComponent();
            SettingsBase.LoadSettings();
            InitSettings();
            InitTimer();
            dock.Theme = new VS2012LightTheme();
            Mru = new Queue<string>();
#if DEBUG
            spstart.Stop();
            MessageBox.Show(spstart.ElapsedMilliseconds + " Milliseconds to load!");
#endif
            LoadPlugins();
            if (filename == string.Empty) return;
            OpenFile(filename);
        }

        private Editor ActiveEditor
        {
            get { return dock.ActiveContent as Editor; }
        }

        public void OpenFile(string name)
        {
            if (FileTypePlugins.Count() != 0)
                foreach (var filetype in FileTypePlugins)
                {
                    if (filetype.Extensions.Contains(Path.GetExtension(name)))
                        filetype.Open(name, dock);
                    else
                        OpenDefault(name);
                }
            else
                OpenDefault(name);
            SaveRecentFile(name, recentfilesmenu);
        }

        public void SaveEditor(Editor edit)
        {
            if (edit.Name == "Editor")
            {
                using (var s = new SaveFileDialog())
                {
                    s.Filter = "All Files(*.*)|*.*";
                    s.ShowDialog();
                    if (s.FileName == "") return;
                    edit.tb.SaveToFile(s.FileName, Encoding.Default);
                    edit.Text = Path.GetFileName(s.FileName);
                    edit.Name = s.FileName;
                }
            }
            else
            {
                edit.tb.SaveToFile(edit.Name, Encoding.Default);
                edit.Name = edit.Name;
            }
        }

        public DockPanel Panel
        {
            get { return dock; }
        }

        private void InitSettings()
        {
            if (!SettingsBase.ShowMenuBar) Menu = null;
            dock.DocumentStyle = SettingsBase.DocumentStyle;
            dock.DocumentTabStripLocation = SettingsBase.TabLocation;
            status.Visible = SettingsBase.ShowStatusBar;
        }

        private void InitTimer()
        {
            var infotimer = new Timer {Interval = 40};
            infotimer.Tick += infotimer_Tick;
            infotimer.Start();
        }

        private void SaveRecentFile(string path, MenuItem recent)
        {
            recent.MenuItems.Clear(); //clear all recent list from menu
            LoadRecentList(); //load list from file
            if (!(Mru.Contains(path))) //prevent duplication on recent list
                Mru.Enqueue(path); //insert given path into list
            while (Mru.Count > 10) //keep list number not exceeded given value
            {
                Mru.Dequeue();
            }
            foreach (string item in Mru)
            {
                var fileRecent = new MenuItem(item); //create new menu for each item in list
                fileRecent.Click += item_Click;
                recent.MenuItems.Add(fileRecent); //add the menu to "recent" menu
            }
            //writing menu list to file
            var stringToWrite = new StreamWriter(Application.StartupPath + "\\User\\Recent.info");
            //create file called "Recent.txt" located on app folder
            foreach (string item in Mru)
                stringToWrite.WriteLine(item); //write list to stream
            stringToWrite.Flush(); //write stream to file
            stringToWrite.Close(); //close the stream and reclaim memory
        }

        /// <summary>
        ///     load recent file list from file
        /// </summary>
        private void LoadRecentList()
        {
            Mru.Clear();
            try
            {
                var listToRead = new StreamReader(Application.StartupPath + "\\User\\Recent.info"); //read file stream
                string line;
                while ((line = listToRead.ReadLine()) != null) //read each line until end of file
                    Mru.Enqueue(line); //insert to list
                listToRead.Close(); //close the stream
            }
            catch
            {
                //throw;
            }
        }

        private void OpenDefault(string name, bool isreadonly)
        {
            var edit = new Editor();
            edit.tb.Text = File.ReadAllText(name);
            edit.Text = Path.GetFileName(name);
            edit.tb.ReadOnly = isreadonly;
            edit.Name = name;
            edit.tb.IsChanged = false;
            edit.tb.ClearUndo();
            edit.Show(dock, DockState.Document);
        }

        private void OpenDefault(string name)
        {
            OpenDefault(name, false);
        }

        private void OpenDefault(string name, string encname, bool isreadonly)
        {
            var edit = new Editor();
            edit.tb.Text = File.ReadAllText(name, Encoding.GetEncoding(encname));
            edit.Text = Path.GetFileName(name);
            edit.tb.ReadOnly = isreadonly;
            edit.Name = name;
            edit.tb.IsChanged = false;
            edit.tb.ClearUndo();
            edit.Show(dock, DockState.Document);
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
            MessageBox.Show(sp.ElapsedMilliseconds + "Milliseconds");
#endif
        }

        private void SaveWithEncoding(Editor edit, string name)
        {
            if (edit.Name == "Editor")
            {
                using (var s = new SaveFileDialog())
                {
                    s.Filter = "All Files(*.*)|*.*";
                    s.ShowDialog();
                    if (s.FileName == "") return;
                    edit.tb.SaveToFile(s.FileName, Encoding.GetEncoding(name));
                    edit.Name = s.FileName;
                }
            }
            else
            {
                edit.tb.SaveToFile(edit.Name, Encoding.GetEncoding(name));
                edit.Name = edit.Name;
            }
        }

        private void SaveAs()
        {
            using (var sf = new SaveFileDialog())
            {
                sf.Filter = "All Files (*.*)|*.*";
                sf.ShowDialog();
                if (sf.FileName == "") return;
                ActiveEditor.tb.SaveToFile(sf.FileName, Encoding.Default);
                ActiveEditor.Text = Path.GetFileName(sf.FileName);
                ActiveEditor.Name = sf.FileName;
            }
        }

        private void BuildLangMenu()
        {
            var menus = Enum.GetValues(typeof (Language)).Cast<Language>();
            foreach (MenuItem m in menus.Select(lang => new MenuItem {Text = lang.ToString()}))
            {
                m.Click += m_Click;
                milanguage.MenuItems.Add(m);
            }
            milanguage.GetMenuByName("Text").Checked = true;
        }

        private void AddRecentFile(string name)
        {
            var fileRecent = new MenuItem(name); //create new menu for each item in list
            fileRecent.Click += item_Click;
            recentfilesmenu.MenuItems.Add(fileRecent); //add the menu to "recent" menu
        }

        private void item_Click(object sender, EventArgs e)
        {
            var menu = sender as MenuItem;
            if (menu != null) OpenFile(menu.Text);
        }

        protected override void OnLoad(EventArgs e)
        {
            LoadRecentList();
            foreach (string r in Mru)
                AddRecentFile(r);
            base.OnLoad(e);
        }
        protected override void OnClosed(EventArgs e)
        {
            SettingsBase.SaveConfiguration();
            base.OnClosed(e);
        }
        private void m_Click(object sender, EventArgs e)
        {
            foreach (MenuItem t in milanguage.MenuItems)
                t.Checked = false;
            var item = sender as MenuItem;
            item.Checked = true;
            ActiveEditor.ChangeLang(item.Text.ToEnum<Language>());
        }

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
            var cmd = new Cmd();
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
            var findinfiles = new FindInFiles();
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
            var iLines = ActiveEditor.tb.FindLines(@"^\s*$", RegexOptions.None);
            ActiveEditor.tb.RemoveLines(iLines);
        }

        private void caseuppermenu_Click(object sender, EventArgs e)
        {
            var upper = ActiveEditor.tb.SelectedText.ToUpper();
            ActiveEditor.tb.SelectedText = upper;
        }

        private void caselowermenu_Click(object sender, EventArgs e)
        {
            string lower = ActiveEditor.tb.SelectedText.ToLower();
            ActiveEditor.tb.SelectedText = lower;
        }

        private void casetitlemenu_Click(object sender, EventArgs e)
        {
            var cultureinfo = Thread.CurrentThread.CurrentCulture;
            var info = cultureinfo.TextInfo;
            ActiveEditor.tb.SelectedText = info.ToTitleCase(ActiveEditor.tb.SelectedText);
        }

        private void swapcase_Click(object sender, EventArgs e)
        {
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
                if (ActiveEditor.tb.Text.Contains(ActiveEditor.tb.CommentPrefix))
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
            ActiveEditor.tb.Bookmarks.Add(ActiveEditor.tb.Selection.Start.iLine);
        }

        private void removebookmarkmenu_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.Bookmarks.Remove(ActiveEditor.tb.Selection.Start.iLine);
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
                    File.WriteAllText(rtfs.FileName, ActiveEditor.tb.Rtf);
            }
        }

        private void htmlexport_Click(object sender, EventArgs e)
        {
            using (var htmls = new SaveFileDialog())
            {
                htmls.FileName = "HTML Web Page (*.htm), (*.html)|*.htm|Shtml Page (*.shtml)|*.shtml";
                htmls.ShowDialog();
                if (htmls.FileName != "")
                    File.WriteAllText(htmls.FileName, ActiveEditor.tb.Html);
            }
        }

        private void pngexport_Click(object sender, EventArgs e)
        {
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
            if (ActiveEditor.Name != "Editor")
                NativeMethods.ShowFileProperties(ActiveEditor.Name);
            else
                MessageBox.Show("File Not Saved!", "Ynote Classic");
        }

        private void menuItem17_Click(object sender, EventArgs e)
        {
            if (ActiveEditor.Name != "Editor")
            {
                DialogResult result =
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
            if (ActiveEditor.Name != "Editor")
            {
                string dir = Path.GetDirectoryName(ActiveEditor.Name);
                if (dir != null) Process.Start(dir);
            }
            else
            {
                MessageBox.Show("File Not Saved!");
            }
        }

        private void settextreadonly_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.Selection.ReadOnly = true;
        }

        private void settextwriteable_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.Selection.ReadOnly = false;
        }

        private void menuItem26_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void ExitMenu_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuItem49_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.Zoom += 10;
        }

        private void menuItem50_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.Zoom -= 10;
        }

        private void menuItem51_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.Zoom = 100;
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
                infolabel.Text = " Line : " + Line + "   Col : " + nCol + "   Sel : " +
                                 ActiveEditor.tb.Selection.Text.Length + "   Size : " + ActiveEditor.tb.Text.Length +
                                 " byte(s)";
            }
            catch (Exception)
            {
                // CarryOn();
            }
        }

        private void zoom_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int i = e.ClickedItem.Text.ToInt();
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
            var manager = new PluginManager();
            manager.Show();
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            SaveEditor(ActiveEditor);
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            SaveAs();
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
                sf.InitialDirectory = Application.StartupPath + @"\User\Macros\";
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
                if (ActiveEditor.Name != "Editor")
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

        private void menuItem31_Click(object sender, EventArgs e)
        {
            var run = new RunDialog();
            if (ActiveEditor.Name != "Editor")
                run.File = ActiveEditor.Name;
            else
                run.File = Path.GetTempFileName() + ".run";
            run.Show();
        }

        private void statusbarmenuitem_Click(object sender, EventArgs e)
        {
            status.Visible = !statusbarmenuitem.Checked;
            statusbarmenuitem.Checked = !statusbarmenuitem.Checked;
            SettingsBase.ShowStatusBar = statusbarmenuitem.Checked;
            SettingsBase.SaveConfiguration();
        }

        private void menuItem32_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Ynote Macros (*.ymc)|*.ymc";
                ofd.ShowDialog();
                if (string.IsNullOrEmpty(ofd.FileName)) return;
                File.Copy(ofd.FileName, Application.StartupPath + @"\User\Macros\" + Path.GetFileName(ofd.FileName));
                MessageBox.Show("Macro successfully copied to Macros Directory", "Ynote Classic");
            }
        }

        private void incrementalsearchmenu_Click(object sender, EventArgs e)
        {
            incrementalSearcher1.tb = ActiveEditor.tb;
            incrementalSearcher1.Visible = true;
        }

        private void menuItem13_Click(object sender, EventArgs e)
        {
            if (ActiveEditor.Name == "Editor") return;
            string file = ActiveEditor.Name;
            ActiveEditor.Close();
            OpenFile(file);
        }

        private void menuItem18_Click(object sender, EventArgs e)
        {
            ActiveEditor.tb.Print(new PrintDialogSettings {ShowPrintDialog = true});
        }

        private void reopenclosedtab_Click(object sender, EventArgs e)
        {
            string recentlyclosed = Mru.Last();
            OpenFile(recentlyclosed);
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
            m.Checked = true;
            SettingsBase.ThemeFile = m.Name;
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
                milanguage.GetMenuByName(ActiveEditor.tb.Language.ToString()).Checked = true;
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
            ActiveEditor.tb.ProcessKey(Keys.F9);
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
            var console = new ConsoleUI(this) {StartPosition = FormStartPosition.CenterParent};
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

        private void menuItem82_Click(object sender, EventArgs e)
        {
            File.Delete(Application.StartupPath + "\\User\\Recent.info");
            recentfilesmenu.MenuItems.Clear();
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

        /// <summary>
        ///     TrimPunctuation from start and end of string.
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

        private void menuItem109_Click(object sender, EventArgs e)
        {
            SettingsBase.HiddenChars = true;
        }

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
            ActiveEditor.tb.ProcessKey(Keys.Control | Keys.Delete);
        }

        private MenuItem[] BuildEncodingList(EventHandler handler)
        {
            return
                Encoding.GetEncodings()
                    .Select(info => new MenuItem(info.DisplayName, handler) {Tag = info.Name})
                    .ToArray();
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
                OpenDefault(dialog.FileName, item.Tag.ToString(), dialog.ReadOnlyChecked);
            }
        }

        private void saveencoding_click(object sender, EventArgs e)
        {
            SaveWithEncoding(ActiveEditor, (sender as MenuItem).Tag.ToString());
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


        #region Plugins

        [ImportMany("IYnotePlugin")]
        private IEnumerable<IYnotePlugin> Plugins { get; set; }

        [ImportMany("IFileTypePlugin")]
        private IEnumerable<IFileTypePlugin> FileTypePlugins { get; set; }

        /// <summary>
        ///     Load Plugins
        /// </summary>
        private void LoadPlugins()
        {
            var dircatalog = new DirectoryCatalog(Application.StartupPath + "\\Plugins");
            var container = new CompositionContainer(dircatalog);
            Plugins = container.GetExportedValues<IYnotePlugin>();
            FileTypePlugins = container.GetExportedValues<IFileTypePlugin>();
            LoadYnotePlugins();
        }

        /// <summary>
        ///     Loads IYnotePlugin Instances
        /// </summary>
        private void LoadYnotePlugins()
        {
            foreach (var plugin in Plugins)
            {
                plugin.Ynote = this;
                plugin.Initialize();
                pluginsmenuitem.MenuItems.Add(plugin.MenuItem);
                pluginsmenuitem.Visible = true;
            }
        }

        #endregion

        private void menuItem38_Select(object sender, EventArgs e)
        {
            mimacros.MenuItems.Clear();
            foreach (string file in Directory.GetFiles(Application.StartupPath + @"\User\Macros\", "*.ymc"))
                mimacros.MenuItems.Add(new MenuItem(Path.GetFileNameWithoutExtension(file), macroitem_click) {Name = file});
        }

        private void macroitem_click(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            ActiveEditor.tb.MacrosManager.Macros = File.ReadAllText(item.Name);
            ActiveEditor.tb.MacrosManager.ExecuteMacros();
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

    }
}