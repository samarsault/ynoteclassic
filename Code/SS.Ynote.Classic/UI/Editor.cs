using FastColoredTextBoxNS;
using SS.Ynote.Classic.Core;
using SS.Ynote.Classic.Core.Extensibility;
using SS.Ynote.Classic.Core.Settings;
using SS.Ynote.Classic.Core.Snippets;
using SS.Ynote.Classic.Core.Syntax;
using SS.Ynote.Classic.Extensibility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.UI
{
    public partial class Editor : DockContent
    {
        #region Fields

        /// <summary>
        ///     Syntax Highligher
        /// </summary>
        private readonly SyntaxHighlighter Highlighter;

        private Dictionary<Keys, string> Shortcuts;

        /// <summary>
        ///     Auto Complete Menu
        /// </summary>
        private AutocompleteMenu _autocomplete;

        /// <summary>
        ///     Invisible Char Style
        /// </summary>
        private Style _invisibleCharsStyle;

        /// <summary>
        ///     Document Map
        /// </summary>
        private DocumentMap map;

        #endregion Fields

        #region Constructor

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
            if (Globals.Snippets == null)
            {
                snipthread.SetApartmentState(ApartmentState.STA);
                snipthread.Start();
                snipthread.Join();
            }
            ThreadPool.QueueUserWorkItem(delegate { Shortcuts = GetDictionary(); });
            if (SyntaxHighlighter.LoadedSyntaxes.Any()) return;
            Highlighter.LoadAllSyntaxes();
        }

        #endregion Constructor

        #region Properties

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

        #endregion Properties

        #region Methods

        /// <summary>
        ///     Highlights Syntax
        /// </summary>
        /// <param name="lang"></param>
        public void HighlightSyntax(string lang)
        {
            Highlighter.HighlightSyntax(lang, new TextChangedEventArgs(codebox.Range));
        }

        /// <summary>
        ///     Loads Snippets
        /// </summary>
        private void LoadSnippets()
        {
            Globals.Snippets = new List<YnoteSnippet>();
            string[] snippets = Directory.GetFiles(GlobalSettings.SettingsDir, "*.ynotesnippet",
                SearchOption.AllDirectories);
            Thread.Sleep(5);
            foreach (string snippet in snippets)
            {
                YnoteSnippet snip = YnoteSnippet.Read(snippet);
                Globals.Snippets.Add(snip);
            }
        }

        public void ForceAutoComplete()
        {
            if (_autocomplete != null)
                _autocomplete.Show(true);
        }

        public void RePaintTheme()
        {
            codebox.ClearStylesBuffer();
            if (Globals.Settings.ThemeFile != null)
                YnoteThemeReader.ApplyTheme(Globals.Settings.ThemeFile, Highlighter, codebox);
            Highlighter.HighlightSyntax(codebox.Language, new TextChangedEventArgs(codebox.Range));
            if (ShowDocumentMap)
                ThemifyDocumentMap();
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
            codebox.HotkeysMapping =
                HotkeysMapping.Parse(File.ReadAllText(GlobalSettings.SettingsDir + "Editor.ynotekeys"));
            if (Globals.Settings.ImeMode)
                codebox.ImeMode = ImeMode.On;
            if (Globals.Settings.ShowChangedLine)
                codebox.ChangedLineColor = ControlPaint.LightLight(codebox.CurrentLineColor);
            if (Globals.Settings.ShowDocumentMap)
            {
                CreateDocumentMap();
            }
            if (Globals.Settings.WrapWidth > 0)
            {
                codebox.WordWrapMode = WordWrapMode.WordWrapPreferredWidth;
                codebox.PreferredLineWidth = Globals.Settings.WrapWidth;
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
            ThemifyDocumentMap();
            map.Location = new Point(144, 0);
            map.ScrollbarVisible = false;
            map.Size = new Size(140, 262);
            map.TabIndex = 2;
            map.Visible = true;
            map.Target = codebox;
            Controls.Add(map);
        }

        private void ThemifyDocumentMap()
        {
            map.BackColor = codebox.BackColor;
            map.ForeColor = codebox.SelectionColor;
        }

        /// <summary>
        ///     Initialize Events
        /// </summary>
        private void InitEvents()
        {
            codebox.TextChangedDelayed += codebox_TextChangedDelayed;
            codebox.DragDrop += codebox_DragDrop;
            codebox.TraceMessage += (sender, args) => Globals.Ynote.Trace(sender as string, 100000);
            codebox.DragEnter += (sender, e) => e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
            codebox.KeyDown += codebox_KeyDown;
            if (Globals.Settings.HighlightSameWords)
                codebox.SelectionChangedDelayed += codebox_SelectionChangedDelayed;
        }

        /// <summary>
        ///     Builds Context Menu
        /// </summary>
        private void BuildContextMenu()
        {
            string file = GlobalSettings.SettingsDir + "ContextMenu.ysr";
            contextmenu.MenuItems.AddRange(YnoteScript.Get<MenuItem[]>(codebox, file, "*.BuildContextMenu"));
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

        /// <summary>
        ///     Initializes AutoComplteMenu
        /// </summary>
        private void CreateAutoCompleteMenu()
        {
            _autocomplete = new AutocompleteMenu(codebox);
            _autocomplete.AppearInterval = 50;
            _autocomplete.AllowTabKey = true;
        }

        private void DistractionFreeDimensions()
        {
            codebox.Anchor = AnchorStyles.None;
            // Debug.WriteLine(string.Format("Name : {4} \nHeight : {0} * {1}\n, Width : {2} * {3}\n", Height, codebox.Height, Width, codebox.Width, Text));
            codebox.Height = Height;
            codebox.Width = Width / 2;
            codebox.Left = (ClientSize.Width - codebox.Width) / 2;
            codebox.Dock = DockStyle.None;
            BackColor = codebox.BackColor;
        }

        public void ToggleDistrationFreeMode()
        {
            DistractionFree = !DistractionFree;
            if (DistractionFree)
                DistractionFreeDimensions();
            else
                codebox.Dock = DockStyle.Fill;
        }

        /// <summary>
        ///     Insert a Snippet
        /// </summary>
        /// <param name="snippet"></param>
        public void InsertSnippet(YnoteSnippet snippet)
        {
#if DEBUG
            var watch = new Stopwatch();
            watch.Start();
#endif
            var selection = codebox.Selection.Clone();
            var content = snippet.GetSubstitutedContent(this);
            codebox.InsertText(content);
            var nselection = codebox.Selection.Clone();
            for (int i = selection.Start.iLine; i <= nselection.Start.iLine; i++)
            {
                codebox.Selection.Start = new Place(0, i);
                codebox.DoAutoIndent(i);
            }
            codebox.Selection = nselection;
            if (snippet.Content.Contains('^'))
                PositionCaretTo('^');
#if DEBUG
            watch.Stop();
            Debug.WriteLine(watch.ElapsedMilliseconds + " ms InsertSnippet()");
#endif
        }

        /// <summary>
        ///     Position Charet to c
        /// </summary>
        /// <param name="c"></param>
        private void PositionCaretTo(char c)
        {
            while (codebox.Selection.CharBeforeStart != c)
                if (!codebox.Selection.GoLeftThroughFolded())
                    break;
            codebox.Selection.GoLeft(true);
            codebox.ClearSelected();
        }

        #endregion Methods

        #region Events

        private void codebox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                var fragment = Tb.Selection.GetFragment(@"\w");
                foreach (var snippet in Globals.Snippets)
                {
                    if (snippet.Scope.Contains(codebox.Language) && snippet.Tab == fragment.Text)
                    {
                        e.Handled = true;
                        codebox.BeginUpdate();
                        codebox.Selection.BeginUpdate();
                        codebox.Selection = fragment;
                        codebox.ClearSelected();
                        InsertSnippet(snippet);
                        codebox.Selection.EndUpdate();
                        codebox.EndUpdate();
                    }
                }
            }
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                if (_autocomplete == null)
                    CreateAutoCompleteMenu();
                var word = codebox.Selection.GetFragment(@"\w");
                if (!string.IsNullOrEmpty(word.Text))
                    _autocomplete.Items.AddItem(new AutocompleteItem(word.Text));
            }
            if (Shortcuts != null)
                ProcessShortcuts(e);
        }

        private void ProcessShortcuts(KeyEventArgs e)
        {
            foreach (var item in Shortcuts)
            {
                if (item.Key == e.KeyData)
                {
                    e.Handled = true;
                    Commander.RunCommand(Globals.Ynote, item.Value);
                }
            }
        }

        private static Dictionary<Keys, string> GetDictionary()
        {
            string file = GlobalSettings.SettingsDir + "User.ynotekeys";
            if (File.Exists(file))
            {
                Dictionary<Keys, string> dictionary = new Dictionary<Keys, string>();
                var converter = new KeysConverter();
                string[] lines = File.ReadAllLines(file);
                foreach (string line in lines)
                {
                    string[] items = line.Split('=');
                    var keys = (Keys)converter.ConvertFromString(items[0]);
                    dictionary.Add(keys, items[1]);
                }
                return dictionary;
            }
            return null;
        }

        public void RebuildAutocompleteMenu()
        {
            if (_autocomplete == null)
                CreateAutoCompleteMenu();
            ThreadPool.QueueUserWorkItem(delegate
            {
                if (string.IsNullOrEmpty(codebox.Text))
                    return;
                foreach (var matches in codebox.GetRanges(@"\w+"))
                    _autocomplete.Items.AddItem(new AutocompleteItem(matches.Text));
            });
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

        private void codebox_DragDrop(object sender, DragEventArgs e)
        {
            var fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (var file in fileList)
                BeginInvoke((MethodInvoker)(() => Globals.Ynote.OpenFile(file)));
        }

        private void codebox_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            HighlightSyntax(codebox.Language);
            HighlightInvisbleCharacters(e.ChangedRange);
            if (codebox.IsChanged && !Text.Contains("*"))
                Text += "*";
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
                for (int i = 0; i < dockPanel.DocumentsToArray().Length; i++)
                {
                    var document = dockPanel.DocumentsToArray()[i];
                    if (!document.DockHandler.IsActivated)
                        document.DockHandler.Close();
                }
            }
        }

        private void menuItem12_Click(object sender, EventArgs e)
        {
            foreach (Editor doc in DockPanel.Documents.ToArray())
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

        #endregion Events

        #region Overrides

        protected override void OnShown(EventArgs e)
        {
            if (Globals.DistractionFree)
                ToggleDistrationFreeMode();
            base.OnShown(e);
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
                        Globals.Ynote.SaveEditor(this);
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
            return GetType() + "," + Name;
        }

        #endregion Overrides
    }
}