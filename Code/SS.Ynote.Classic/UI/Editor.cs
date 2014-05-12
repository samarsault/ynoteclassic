using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CSScriptLibrary;
using FastColoredTextBoxNS;
using SS.Ynote.Classic.Features.Extensibility;
using SS.Ynote.Classic.Features.Snippets;
using SS.Ynote.Classic.Features.Syntax;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.UI
{
    public partial class Editor : DockContent
    {
        /// <summary>
        ///     Syntax Highligher
        /// </summary>
        public readonly ISyntaxHighlighter Highlighter;

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

        /// <summary>
        ///     Default Constructor
        /// </summary>
        public Editor()
        {
            InitializeComponent();
            InitEvents();
            Highlighter = new SyntaxHighlighter();
            YnoteThemeReader.ApplyTheme(Settings.ThemeFile, Highlighter, codebox);
            InitSettings();
            if (SyntaxHighlighter.LoadedSyntaxes.Any()) return;
            Highlighter.LoadAllSyntaxes();
        }


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

        private void LoadSnippets(Language language)
        {
            if (Snippets == null)
                Snippets = new List<YnoteSnippet>();
            var dir = YnoteSnippet.GetDirectory(language);
            foreach (var snipfile in Directory.GetFiles(dir))
            {
                YnoteSnippet snippet = YnoteSnippet.Read(snipfile);
                Snippets.Add(snippet);
            }
        }

        private void InitSettings()
        {
            codebox.AllowDrop = true;
            codebox.AutoCompleteBrackets = Settings.AutoCompleteBrackets;
            codebox.TabLength = Settings.TabSize;
            codebox.Font = new Font(Settings.FontFamily, Settings.FontSize);
            codebox.ShowFoldingLines = Settings.ShowFoldingLines;
            codebox.ShowLineNumbers = Settings.ShowLineNumbers;
            codebox.HighlightFoldingIndicator = Settings.HighlightFolding;
            codebox.FindEndOfFoldingBlockStrategy = Settings.FoldingStrategy;
            codebox.BracketsHighlightStrategy = Settings.BracketsStrategy;
            codebox.CaretVisible = Settings.ShowCaret;
            codebox.ShowFoldingLines = Settings.ShowFoldingLines;
            codebox.LineInterval = Settings.LineInterval;
            codebox.LeftPadding = Settings.PaddingWidth;
            codebox.VirtualSpace = Settings.EnableVirtualSpace;
            codebox.WideCaret = Settings.BlockCaret;
            codebox.WordWrap = Settings.WordWrap;
            codebox.Zoom = Settings.Zoom;
            codebox.HotkeysMapping = HotkeysMapping.Parse(File.ReadAllText(Settings.SettingsDir + "User.ynotekeys"));
            if (Settings.IMEMode)
                codebox.ImeMode = ImeMode.On;
            if (Settings.ShowDocumentMap)
            {
                var map = new DocumentMap
                {
                    Dock = DockStyle.Right,
                    BackColor = codebox.BackColor,
                    ForeColor = codebox.SelectionColor,
                    Location = new Point(144, 0),
                    ScrollbarVisible = false,
                    Size = new Size(140, 262),
                    TabIndex = 2,
                    Visible = true,
                    Target = codebox
                };
                Controls.Add(map);
            }
            if (!Settings.ShowRuler) return;
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
            codebox.LanguageChanged += (sender, args) => LoadSnippets(codebox.Language);
            // if (Settings.AutoCompleteBrackets)
            //  codebox.AutoIndentNeeded += codebox_AutoIndentNeeded;
            if (Settings.HighlightSameWords)
                codebox.SelectionChangedDelayed += codebox_SelectionChangedDelayed;
        }

        private void codebox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                var fragment = Tb.Selection.GetFragment(@"\w");
                if (Snippets == null)
                    LoadSnippets(codebox.Language);
                foreach (var snippet in Snippets)
                {
                    if (snippet.Tab == fragment.Text)
                    {
                        codebox.BeginUpdate();
                        e.Handled = true;
                        InsertSnippet(snippet, fragment);
                        codebox.EndUpdate();
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

        private void InsertSnippet(YnoteSnippet snippet, Range fragment)
        {
            codebox.Selection = fragment;
            codebox.ClearSelected();
            codebox.InsertText(snippet.Content);
            snippet.SubstituteContent(this);
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
            var edit = new Editor {Name = file, Text = Path.GetFileName(file)};
            edit.Tb.IsChanged = false;
            edit.Tb.ClearUndo();
            //edit.ChangeLang(FileTypes.GetLanguage(FileTypes.BuildDictionary(), Path.GetExtension(file)));
            if (FileTypes.FileTypesDictionary == null)
                FileTypes.BuildDictionary();
            var lang = FileTypes.GetLanguage(FileTypes.FileTypesDictionary, Path.GetExtension(file));
            edit.HighlightSyntax(lang);
            edit.Show(DockPanel, DockState.Document);
            edit.Tb.OpenFile(file);
        }

        /// <summary>
        ///     Do MISC Formatting
        /// </summary>
        /// <param name="r"></param>
        private void DoFormatting(Range r)
        {
            if (!Settings.HiddenChars) return;
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
            DoFormatting(e.ChangedRange);
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
            if (!e.Cancel)
            {
                if (DockPanel.Documents.Count() == 1)
                    Application.Exit();
                DockPanel = null;
            }
            base.OnClosing(e);
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
            var file = Settings.SettingsDir + "ContextMenu.ys";
            var asm = file + ".cache";
            CSScript.GlobalSettings.TargetFramework = "v3.5";
            try
            {
                // var helper =
                //     new AsmHelper(CSScript.LoadMethod(File.ReadAllText(ysfile), GetReferences()));
                // helper.Invoke("*.Run", ynote);
                var assembly = !File.Exists(asm)
                    ? CSScript.LoadMethod(File.ReadAllText(file), asm, false, YnoteScript.GetReferences())
                    : Assembly.LoadFrom(asm);
                using (var execManager = new AsmHelper(assembly))
                {
                    var items = (MenuItem[]) (execManager.Invoke("*.BuildContextMenu", codebox));
                    contextmenu.MenuItems.AddRange(items);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an Error running the script : \r\n" + ex.Message, "YnoteScript Host",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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