using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using SS.Ynote.Classic.Core.Extensibility;
using SS.Ynote.Classic.Core.Settings;
using SS.Ynote.Classic.Core.Snippets;
using SS.Ynote.Classic.Core.Syntax;
using SS.Ynote.Classic.Core.Syntax.Framework;
using SS.Ynote.Classic.Extensibility;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.UI
{
    public partial class Editor : DockContent
    {
        /// <summary>
        ///     Syntax Highligher
        /// </summary>
        public readonly SyntaxHighlighter Highlighter;

        /// <summary>
        ///     Ynote Snippets
        /// </summary>
        private IList<YnoteSnippet> Snippets;

        /// <summary>
        ///     Syntax
        /// </summary>
        public SyntaxBase Syntax;

        /// <summary>
        ///     Invisible Char Style
        /// </summary>
        private Style _invisibleCharsStyle;

        internal AutocompleteMenu autocomplete;
        private DocumentMap map;

        /// <summary>
        ///     Default Constructor
        /// </summary>
        public Editor()
        {
            InitializeComponent();
            InitEvents();
            codebox.Dock = DockStyle.Fill;
            Highlighter = new SyntaxHighlighter();
            InitSettings();
            var snipthread = new Thread(LoadSnippets);
            snipthread.SetApartmentState(ApartmentState.STA);
            snipthread.Start();
            snipthread.Join();
            if (SyntaxHighlighter.LoadedSyntaxes.Any()) return;
            Highlighter.LoadAllSyntaxes();
        }

        /// <summary>
        ///     Whether to Show Document Map
        /// </summary>
        public bool ShowDocumentMap
        {
            get
            {
                if (map == null)
                    return false;
                if (map != null || map.Visible)
                    return true;
                return false;
            }
            set
            {
                if (value)
                    if (map == null) CreateDocumentMap();
                map.Visible = value;
                Globals.Settings.ShowDocumentMap = value;
            }
        }

        /// <summary>
        ///     Whether the Current Document is in distractionfree mode or not
        /// </summary>
        public bool DistractionFree { get; set; }

        /// <summary>
        ///     Get the TB
        /// </summary>
        public FastColoredTextBox Tb
        {
            get { return codebox; }
        }

        /// <summary>
        ///     Whether the Document is saved
        /// </summary>
        public bool IsSaved
        {
            get { return Name != "Editor"; }
        }

        /// <summary>
        ///     Highlights Syntax
        /// </summary>
        /// <param name="syntax"></param>
        public void HighlightSyntax(SyntaxDesc syntax)
        {
            var args = new TextChangedEventArgs(codebox.Range);
            if (syntax.SyntaxBase == null)
            {
                Highlighter.HighlightSyntax(syntax.Language, args);
                codebox.Language = syntax.Language;
            }
            else
            {
                Highlighter.HighlightSyntax(syntax.SyntaxBase, args);
                Syntax = syntax.SyntaxBase;
            }
        }

        private void LoadSnippets()
        {
#if DEBUG
            var watch = new Stopwatch();
            watch.Start();
#endif
            Snippets = new List<YnoteSnippet>();
            string[] snippets = Directory.GetFiles(GlobalSettings.SettingsDir, "*.ynotesnippet",
                SearchOption.AllDirectories);
            Thread.Sleep(5);
            foreach (string snippet in snippets)
            {
                YnoteSnippet snip = YnoteSnippet.Read(snippet);
                Snippets.Add(snip);
            }
#if DEBUG
            watch.Stop();
            Debug.WriteLine(string.Format("Loaded {0} Snippets in {1} ms", Snippets.Count, watch.ElapsedMilliseconds));
#endif
        }

        private void InitSettings()
        {
            YnoteThemeReader.ApplyTheme(Globals.Settings.ThemeFile, Highlighter, codebox);
            codebox.AllowDrop = true;
            codebox.ShowScrollBars = Globals.Settings.ScrollBars;
            codebox.AutoCompleteBrackets = Globals.Settings.AutoCompleteBrackets;
            codebox.TabLength = Globals.Settings.TabSize;
            codebox.Font = new Font(Globals.Settings.FontFamily, Globals.Settings.FontSize);
            codebox.ShowFoldingLines = Globals.Settings.ShowFoldingLines;
            codebox.ShowLineNumbers = Globals.Settings.ShowLineNumbers;
            codebox.HighlightFoldingIndicator = Globals.Settings.HighlightFolding;
            codebox.FindEndOfFoldingBlockStrategy = Globals.Settings.FoldingStrategy;
            codebox.BracketsHighlightStrategy = Globals.Settings.BracketsStrategy;
            codebox.CaretVisible = Globals.Settings.ShowCaret;
            codebox.ShowFoldingLines = Globals.Settings.ShowFoldingLines;
            codebox.LineInterval = Globals.Settings.LineInterval;
            codebox.LeftPadding = Globals.Settings.PaddingWidth;
            codebox.VirtualSpace = Globals.Settings.EnableVirtualSpace;
            codebox.WideCaret = Globals.Settings.BlockCaret;
            codebox.WordWrap = Globals.Settings.WordWrap;
            codebox.Zoom = Globals.Settings.Zoom;
            codebox.HotkeysMapping = HotkeysMapping.Parse(File.ReadAllText(GlobalSettings.SettingsDir + "User.ynotekeys"));
            if (Globals.Settings.ImeMode)
                codebox.ImeMode = ImeMode.On;
            if (Globals.Settings.ShowChangedLine)
                codebox.ChangedLineColor = ControlPaint.LightLight(codebox.CurrentLineColor);
            if (Globals.Settings.ShowDocumentMap)
            {
                CreateDocumentMap();
            }
            if (!Globals.Settings.ShowRuler) return;
            var ruler = new Ruler
            {
                Dock = DockStyle.Top,
                Location = new Point(0, 0),
                Size = new Size(284, 24),
                TabIndex = 1,
                Target = codebox
            };
            Controls.Add(ruler);
        }

        private void CreateDocumentMap()
        {
            map = new DocumentMap();
            map.Dock = DockStyle.Right;
            map.BackColor = codebox.BackColor;
            map.ForeColor = codebox.SelectionColor;
            map.Location = new Point(144, 0);
            map.ScrollbarVisible = false;
            map.Size = new Size(140, 262);
            map.TabIndex = 2;
            map.Visible = true;
            map.Target = codebox;
            Controls.Add(map);
        }

        /// <summary>
        ///     Initialize Events
        /// </summary>
        private void InitEvents()
        {
            codebox.TextChangedDelayed += codebox_TextChangedDelayed;
            codebox.DragDrop += codebox_DragDrop;
            codebox.DragEnter +=
                (sender, e) =>
                    e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
            codebox.KeyDown += codebox_KeyDown;
            if (Globals.Settings.HighlightSameWords)
                codebox.SelectionChangedDelayed += codebox_SelectionChangedDelayed;
        }

        private void codebox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                var fragment = Tb.Selection.GetFragment(@"\w");
                foreach (var snippet in Snippets)
                {
                    if (snippet.Scope == codebox.Language.ToString())
                    {
                        if (snippet.Tab == fragment.Text)
                        {
                            codebox.BeginUpdate();
                            codebox.Selection.BeginUpdate();
                            e.Handled = true;
                            codebox.Selection = fragment;
                            codebox.ClearSelected();
                            InsertSnippet(snippet);
                            codebox.Selection.EndUpdate();
                            codebox.EndUpdate();
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Space)
            {
                if (autocomplete == null)
                    CreateAutoCompleteMenu();
                var word = codebox.Selection.GetFragment(@"\w");
                autocomplete.Items.AddItem(new AutocompleteItem(word.Text));
            }
        }

        public void ToggleDistrationFreeMode()
        {
            DistractionFree = !DistractionFree;
            if (DistractionFree)
            {
                // etner distraction free
                codebox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                codebox.Height = Height;
                codebox.Width = Width/2;
                codebox.Dock = DockStyle.None;
                BackColor = codebox.BackColor;
            }
            else
                codebox.Dock = DockStyle.Fill;
        }


        public void InsertSnippet(YnoteSnippet snippet)
        {
            var selection = codebox.Selection.Clone();
            snippet.SubstituteContent(this);
            codebox.InsertText(snippet.Content);
            var nselection = codebox.Selection.Clone();
            for (int i = selection.Start.iLine; i <= nselection.Start.iLine; i++)
            {
                codebox.Selection.Start = new Place(0, i);
                codebox.DoAutoIndent(i);
            }
            codebox.GoEnd();
            PositionCaretTo('^');
        }

        private void PositionCaretTo(char c)
        {
            while (codebox.Selection.CharBeforeStart != c)
                if (!codebox.Selection.GoLeftThroughFolded())
                    break;
            codebox.Selection.GoLeft(true);
            codebox.ClearSelected();
        }
        private void codebox_SelectionChangedDelayed(object sender, EventArgs e)
        {
            codebox.VisibleRange.ClearStyle(codebox.SameWordsStyle);
            if (!codebox.Selection.IsEmpty)
                return; //user selected diapason

            //get fragment around caret
            var fragment = codebox.Selection.GetFragment(@"\w");
            string text = fragment.Text;
            if (text.Length == 0)
                return;
            //highlight same words
            var ranges = codebox.VisibleRange.GetRanges("\\b" + text + "\\b").ToArray();
            if (ranges.Length > 1)
                foreach (var r in ranges)
                    r.SetStyle(codebox.SameWordsStyle);
        }

        private void CreateAutoCompleteMenu()
        {
            autocomplete = new AutocompleteMenu(codebox);
            autocomplete.AppearInterval = 50;
            autocomplete.AllowTabKey = true;
        }

        private void codebox_DragDrop(object sender, DragEventArgs e)
        {
            var fileList = (string[]) e.Data.GetData(DataFormats.FileDrop, false);
            foreach (var file in fileList)
                BeginInvoke((MethodInvoker) (() => OpenFile(file)));
        }

        private void OpenFile(string file)
        {
            IYnote ynote = Application.OpenForms[0] as IYnote;
            if (ynote == null)
                ynote = Application.OpenForms[1] as IYnote;
            ynote.OpenFile(file);
        }

        /// <summary>
        ///     Highlights Invisble Characters
        /// </summary>
        /// <param name="r"></param>
        private void HighlightInvisbleCharacters(Range r)
        {
            if (!Globals.Settings.HiddenChars) return;
            if (_invisibleCharsStyle == null)
                _invisibleCharsStyle = new InvisibleCharsRenderer(Pens.Gray);
            r.ClearStyle(_invisibleCharsStyle);
            r.SetStyle(_invisibleCharsStyle, @".$|.\r\n|\s");
        }

        private void codebox_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            if (Syntax == null)
                Highlighter.HighlightSyntax(codebox.Language, e);
            else
                Highlighter.HighlightSyntax(Syntax, e);
            HighlightInvisbleCharacters(e.ChangedRange);
            if (codebox.IsChanged && !Text.Contains("*"))
                Text += "*";
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (codebox.IsChanged)
            {
                var result = MessageBox.Show(string.Format("Save Changes To [{0}] ?", Text), "Save",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (result)
                {
                    case DialogResult.Yes:
                        SaveFile();
                        break;

                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;

                    case DialogResult.No:
                        break;
                }
            }
            codebox.CloseBindingFile();
            base.OnClosing(e);
        }

        protected override string GetPersistString()
        {
            return GetType() + "," + Name + "," + Text;
        }

        private void SaveFile()
        {
            if (!IsSaved)
            {
                using (var dlg = new SaveFileDialog())
                {
                    dlg.Filter = "All Files (*.*)|*.*";
                    var result = dlg.ShowDialog() == DialogResult.OK;
                    if (result)
                        codebox.SaveToFile(dlg.FileName, Encoding.Default);
                }
            }
            else
            {
                codebox.SaveToFile(Name, Encoding.Default);
            }
        }

        private void menuItem13_Click(object sender, EventArgs e)
        {
            if (!IsSaved) return;
            Process.Start(Path.GetDirectoryName(Name));
        }

        private void menuItem11_Click(object sender, EventArgs e)
        {
            var dockPanel = DockPanel;
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                var activeMdi = ActiveMdiChild;
                foreach (var form in MdiChildren.Where(form => form != activeMdi))
                {
                    form.Close();
                }
            }
            else
            {
                for (var i = 0; i < dockPanel.DocumentsToArray().Length; i++)
                {
                    var document = dockPanel.DocumentsToArray()[i];
                    if (!document.DockHandler.IsActivated)
                        document.DockHandler.Close();
                }
            }
        }

        private void menuItem12_Click(object sender, EventArgs e)
        {
            foreach (Editor doc in DockPanel.Documents.ToList())
                doc.Close();
        }

        private void menuItem9_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void contextmenu_Popup(object sender, EventArgs e)
        {
            if (contextmenu.MenuItems.Count != 0) return;
            BuildContextMenu();
        }

        private void BuildContextMenu()
        {
            string file = GlobalSettings.SettingsDir + "ContextMenu.ysr";
            contextmenu.MenuItems.AddRange(YnoteScript.Get<MenuItem[]>(codebox, file, "*.BuildContextMenu"));
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Text);
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            if (IsSaved)
                Clipboard.SetText(Name);
            else
                MessageBox.Show("File Not Saved ! ", "Ynote Classic", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}