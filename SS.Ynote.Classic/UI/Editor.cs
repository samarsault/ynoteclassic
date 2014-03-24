#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using SS.Ynote.Classic.Features.Snippets;
using WeifenLuo.WinFormsUI.Docking;

#endregion

namespace SS.Ynote.Classic.UI
{
    public partial class Editor : DockContent
    {
        /// <summary>
        ///     Syntax Highligher
        /// </summary>
        private readonly ISyntaxHighlighter Highlighter;

        /// <summary>
        ///     private var _invisiblecharstyle
        /// </summary>
        private readonly Style _invisibleCharsStyle;

        /// <summary>
        ///     Default Constructor
        /// </summary>
        public Editor()
        {
            InitializeComponent();
            InitEvents();
            Highlighter = new SyntaxHighlighter();
            _invisibleCharsStyle = new InvisibleCharsRenderer(Pens.Gray);
            YnoteThemeReader.ApplyTheme(SettingsBase.ThemeFile, Highlighter, codebox);

            InitSettings();
        }

        public AutocompleteMenu AutoCompleteMenu { get; private set; }

        /// <summary>
        ///     Get the TB
        /// </summary>
        public FastColoredTextBox tb
        {
            get { return codebox; }
        }

        public bool IsSaved
        {
            get { return Name != "Editor"; }
        }

        private void InitSettings()
        {
            //TODO:Settings
            codebox.AllowDrop = true;
            codebox.TabLength = SettingsBase.TabSize;
            codebox.Font = new Font(SettingsBase.FontFamily, SettingsBase.FontSize);
            codebox.ShowFoldingLines = SettingsBase.ShowFoldingLines;
            codebox.ShowLineNumbers = SettingsBase.ShowLineNumbers;
            codebox.HighlightFoldingIndicator = SettingsBase.HighlightFolding;
            codebox.FindEndOfFoldingBlockStrategy = SettingsBase.FoldingStrategy;
            codebox.BracketsHighlightStrategy = SettingsBase.BracketsStrategy;
            codebox.CaretVisible = SettingsBase.ShowCaret;
            codebox.ShowFoldingLines = SettingsBase.ShowFoldingLines;
            codebox.WordWrapMode = SettingsBase.WordWrapMode;
            codebox.LineInterval = SettingsBase.LineInterval;
            codebox.LeftPadding = SettingsBase.PaddingWidth;
            codebox.Zoom = SettingsBase.Zoom;
            codebox.HotkeysMapping = HotkeysMapping.Parse(File.ReadAllText(SettingsBase.SettingsDir + "User.ynotekeys"));
            if (SettingsBase.ShowDocumentMap)
            {
                var map = new DocumentMap
                {
                    Dock = DockStyle.Right,
                    BackColor = codebox.BackColor,
                    ForeColor = codebox.SelectionColor,
                    Location = new Point(144, 0),
                    Name = "dM1",
                    ScrollbarVisible = false,
                    Size = new Size(140, 262),
                    TabIndex = 2,
                    Visible = true,
                    Target = codebox
                };
                Controls.Add(map);
            }
            if (!SettingsBase.ShowRuler) return;
            var ruler = new Ruler
            {
                Dock = DockStyle.Top,
                Location = new Point(0, 0),
                Name = "ruler",
                Size = new Size(284, 24),
                TabIndex = 1,
                Target = codebox
            };
            Controls.Add(ruler);
        }

        /// <summary>
        ///     Changes Language
        /// </summary>
        /// <param name="lang"></param>
        public void ChangeLang(Language lang)
        {
            Highlighter.HighlightSyntax(lang, new TextChangedEventArgs(codebox.Range));
            codebox.Language = lang;
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
            codebox.LanguageChanged += (sender, args) => BuildAutoCompleteMenu();
            codebox.AutoIndentNeeded += codebox_AutoIndentNeeded;
        }

        private void codebox_AutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            // start of a tag
            // the tag start line look as follows:
            // TAGNAME VALUES* (ATTR-NAME=ATTR-VALUE)* {
            // We want to shift the next line when the current line (afte trimming) ends with a { and doesn't start with a comment
            // TODO: a line within a multi line comment will be a false positive
            string trimmedLine = args.LineText.Trim();
            if (!(trimmedLine.StartsWith(codebox.CommentPrefix))
                && trimmedLine.EndsWith("{"))
            {
                // increase indent
                args.ShiftNextLines = args.TabLength;
                return;
            }

            if (!(trimmedLine.StartsWith(codebox.CommentPrefix))
                && trimmedLine.EndsWith("}"))
            {
                // decrease indent
                args.Shift = -args.TabLength;
                args.ShiftNextLines = -args.TabLength;
            }
        }

        private void BuildAutoCompleteMenu()
        {
            if (AutoCompleteMenu == null)
                AutoCompleteMenu = new AutocompleteMenu(codebox)
                {
                    AppearInterval = 50,
                    AllowTabKey = true
                };
            ICollection<AutocompleteItem> items =
                YnoteSnippet.Read(codebox.Language)
                    .Select(snippet => snippet.ToAutoCompleteItem())
                    .Cast<AutocompleteItem>()
                    .ToList();
            //  foreach(var snippet in YnoteSnippet.Read(codebox.Language))
            //      items.Add(snippet.ToAutoCompleteItem());
            AutoCompleteMenu.Items.SetAutocompleteItems(items);
        }

        private void codebox_DragDrop(object sender, DragEventArgs e)
        {
            var fileList = (string[]) e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file in fileList)
                OpenFile(file);
        }

        private void OpenFile(string file)
        {
            var edit = new Editor {Name = file, Text = Path.GetFileName(file)};
            edit.tb.Text = File.ReadAllText(file, Encoding.Default);
            edit.tb.IsChanged = false;
            edit.tb.ClearUndo();
            edit.ChangeLang(FileExtensions.GetLanguage(FileExtensions.BuildDictionary(), Path.GetExtension(file)));
            edit.Show(DockPanel, DockState.Document);
        }

        /// <summary>
        ///     Do MISC Formatting
        /// </summary>
        /// <param name="r"></param>
        private void DoFormatting(Range r)
        {
            if (!SettingsBase.HiddenChars) return;
            r.ClearStyle(_invisibleCharsStyle);
            r.SetStyle(_invisibleCharsStyle, @".$|.\r\n|\s");
        }

        private void codebox_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            Highlighter.HighlightSyntax(codebox.Language, e);
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
                        base.OnClosing(e);
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    case DialogResult.No:
                        base.OnClosing(e);
                        break;
                }
            }
            if (!e.Cancel)
            {
#if DEBUG
                Debug.WriteLine(DockPanel.Documents.Any());
#endif
                if (DockPanel.Documents.Count() == 1)
                    Application.Exit();
                DockPanel = null;
            }
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

        private void menuItem1_Click(object sender, EventArgs e)
        {
            codebox.Cut();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            codebox.Copy();
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            codebox.Paste();
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            codebox.Undo();
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            codebox.Redo();
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            codebox.SelectAll();
        }

        private void menuItem13_Click(object sender, EventArgs e)
        {
            Process.Start(Path.GetDirectoryName(Name));
        }

        private void menuItem11_Click(object sender, EventArgs e)
        {
            var dockPanel = DockPanel;
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                Form activeMdi = ActiveMdiChild;
                foreach (var form in MdiChildren.Where(form => form != activeMdi))
                {
                    form.Close();
                }
            }
            else
            {
                for (int i = 0; i < dockPanel.DocumentsToArray().Length; i++)
                {
                    IDockContent document = dockPanel.DocumentsToArray()[i];
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

        private void menuItem10_Click(object sender, EventArgs e)
        {
            codebox.CollapseBlock(codebox.Selection.Start.iLine, codebox.Selection.End.iLine);
        }

        private void menuItem9_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}