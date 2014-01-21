using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.UI
{
    public partial class Editor : DockContent
    {
        /// <summary>
        ///     Private _hyperlink
        /// </summary>
        private readonly Style _hyperlink;

        /// <summary>
        ///     private var _invisiblecharstyle
        /// </summary>
        private readonly Style _invisibleCharsStyle = new InvisibleCharsRenderer(Pens.Gray);

        /// <summary>
        ///     Syntax Highligher
        /// </summary>
        public ISyntaxHighlighter Highlighter;

        /// <summary>
        ///     Default Constructor
        /// </summary>
        public Editor()
        {
            InitializeComponent();
            InitEvents();
            Highlighter = new SyntaxHighlighter();
            YnoteThemeReader.ApplyTheme(SettingsBase.ThemeFile, Highlighter, codebox);
            InitSettings();
            _hyperlink = new TextStyle(Brushes.Blue, null, FontStyle.Underline);
        }

        private void BuildAutoComplete()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        ///     Get the TB
        /// </summary>
        public FastColoredTextBox tb
        {
            get { return codebox; }
        }

        private void InitSettings()
        {
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
            documentMap1.Enabled = SettingsBase.ShowDocumentMap;
            documentMap1.Visible = SettingsBase.ShowDocumentMap;
            if (!SettingsBase.ShowDocumentMap) return;
            documentMap1.Target = codebox;
            documentMap1.BackColor = codebox.BackColor;
            documentMap1.ForeColor = codebox.SelectionColor;
        }
        /// <summary>
        ///     Changes Language
        /// </summary>
        /// <param name="lang"></param>
        public void ChangeLang(Language lang)
        {
            Highlighter.HighlightSyntax(lang, codebox.Range);
            codebox.Language = lang;
        }

        /// <summary>
        ///     Initialize Events
        /// </summary>
        private void InitEvents()
        {
            codebox.TextChangedDelayed += codebox_TextChangedDelayed;
        }

        /// <summary>
        ///     Do MISC Formatting
        /// </summary>
        /// <param name="r"></param>
        private void DoFormatting(Range r)
        {
            r.ClearStyle(_hyperlink);
            if (SettingsBase.HiddenChars)
            {
                r.ClearStyle(_invisibleCharsStyle);
                r.SetStyle(_invisibleCharsStyle, @".$|.\r\n|\s");
            }
            r.SetStyle(_hyperlink,
                @"((http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)");
        }

        private void codebox_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            Highlighter.HighlightSyntax(codebox.Language, e.ChangedRange);
            DoFormatting(e.ChangedRange);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!codebox.IsChanged) return;
            var result = MessageBox.Show(string.Format("Save Changes To [{0}] ?", Text), "Save",
                MessageBoxButtons.YesNoCancel);
            switch (result)
            {
                case DialogResult.Yes:
                    SaveFile();
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
                case DialogResult.No:
                    base.OnClosing(e);
                    break;
            }
        }

        private void SaveFile()
        {
            if (Name == "Editor")
            {
                using (var dlg = new SaveFileDialog())
                {
                    dlg.Filter = "All Files (*.*)|*.*";
                    DialogResult result = dlg.ShowDialog();
                    if (result == DialogResult.OK)
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
            Process.Start(Name);
        }

        private void menuItem11_Click(object sender, EventArgs e)
        {
            DockPanel dockPanel = DockPanel;
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                Form activeMdi = ActiveMdiChild;
                foreach (Form form in MdiChildren.Where(form => form != activeMdi))
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
            foreach (Editor doc in DockPanel.Documents)
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