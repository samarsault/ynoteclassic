using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using AutocompleteItem = AutocompleteMenuNS.AutocompleteItem;
using SnippetAutocompleteItem = AutocompleteMenuNS.SnippetAutocompleteItem;

namespace SS.Ynote.Classic.UI
{
    public partial class ConsoleUI : Form
    {
        private readonly IYnote _ynote;

        public ConsoleUI(IYnote host)
        {
            InitializeComponent();
            completemenu.AllowsTabKey = true;
            ActiveEditor = host.Panel.ActiveDocument as Editor;
            BuildAutoComplete();
            textBox1.Focus();
            _ynote = host;
            LostFocus += (o, a) => Close();
        }

        private Editor ActiveEditor { get; set; }

        private void BuildAutoComplete()
        {
            var items = new List<AutocompleteItem>();
            foreach (var lang in Enum.GetValues(typeof (Language)))
                items.Add(new AutocompleteItem("SetSyntax:" + lang));
            var ynotesnippet = new YnoteSnippet();
            string str = string.Empty;
            ynotesnippet.GenerateSnippetFileDictionary().TryGetValue(ActiveEditor.tb.Language, out str);
            ynotesnippet.ReadSnippet(str);
            foreach(var item in ynotesnippet.Items)
                    items.Add(new AutocompleteItem("Snippet:"+item.Text){Tag = item});
            items.AddRange(ynotesnippet.Items.Select(snippet => new AutocompleteItem("Snippet:" + snippet.Text) {Tag = snippet.Gen()}));
            items.Add(new AutocompleteItem("File:New"));
            items.Add(new AutocompleteItem("File:Open"));
            items.Add(new AutocompleteItem("File:Save"));
            items.Add(new AutocompleteItem("File:Print"));
            items.Add(new AutocompleteItem("File:Properties"));
            items.Add(new AutocompleteItem("File:Close"));
            items.Add(new AutocompleteItem("File:CloseAll"));
            items.Add(new AutocompleteItem("Bookmarks:Manager"));
            items.Add(new AutocompleteItem("Bookmarks:Toggle"));
            items.Add(new AutocompleteItem("Bookmarks:Remove"));
            items.Add(new AutocompleteItem("Bookmarks:ClearAll"));
            items.Add(new AutocompleteItem("CodeFolding:FoldAll"));
            items.Add(new AutocompleteItem("CodeFolding:UnFoldAll"));
            items.Add(new AutocompleteItem("CodeFolding:FoldSelection"));
            items.Add(new AutocompleteItem("Indent:Increase"));
            items.Add(new AutocompleteItem("Indent:Decrease"));
            items.Add(new AutocompleteItem("Indent:Do"));
            items.Add(new AutocompleteItem("Line:MoveLineDown"));
            items.Add(new AutocompleteItem("Line:MoveLineUp"));
            items.Add(new AutocompleteItem("Line:Duplicate"));
            items.Add(new AutocompleteItem("Line:Join"));
            items.Add(new AutocompleteItem("Line:Sort"));
            items.Add(new AutocompleteItem("Export:Html"));
            items.Add(new AutocompleteItem("Export:Rtf"));
            items.Add(new AutocompleteItem("Comment:Toggle"));
            items.Add(new AutocompleteItem("Comment:Remove"));
            items.Add(new AutocompleteItem("Macros:Record"));
            items.Add(new AutocompleteItem("Macros:StopRecord"));
            items.Add(new AutocompleteItem("Macros:Run"));
            items.Add(new AutocompleteItem("Selection:Readonly"));
            items.Add(new AutocompleteItem("Selection:Writeable"));
            items.Add(new AutocompleteItem("Macros:Clear"));
            items.Add(new AutocompleteItem("View:ZoomIn"));
            items.Add(new AutocompleteItem("View:ZoomOut"));
            items.Add(new SnippetAutocompleteItem("ProcStart:^"));
            items.Add(new AutocompleteItem("Console:Close"));
            completemenu.SetAutocompleteMenu(textBox1, completemenu);
            completemenu.SetAutocompleteItems(items);
        }

        private static SCommand Parse(string command)
        {
            try
            {
                var cmd = new SCommand();
                int l = command.IndexOf(":");
                if (l > 0)
                    cmd.Key = command.Substring(0, l);
                string result = command.Substring(command.LastIndexOf(':') + 1);
                cmd.Value = result;
                return cmd;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Parse Error : " + ex.Message, "Error");
                return null;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            completemenu.Show(textBox1, true);
            base.OnShown(e);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            completemenu.Show(textBox1, true);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                RunCommand(Parse(textBox1.Text));
        }

        private void RunCommand(SCommand c)
        {
            switch (c.Key)
            {
                case "SetSyntax":
                    SetSyntax(c.Value.ToEnum<Language>());
                    break;
                case "File":
                    FileFunc(c.Value);
                    break;
                case "Bookmarks":
                    BookmarkFunc(c.Value);
                    break;
                case "CodeFolding":
                    CodeFoldingFunc(c.Value);
                    break;
                case "Snippet":
                        ActiveEditor.tb.InsertText(c.Value);
                    break;
                case "Indent":
                    IndentFunc(c.Value);
                    break;
                case "Line":
                    LineFunc(c.Value);
                    break;
                case "Export":
                    Export(c.Value);
                    break;
                case "Comment":
                    CommentFunc(c.Value);
                    break;
                case "Macros":
                    MacroFunc(c.Value);
                    break;
                case "View":
                    Viewfunc(c.Value);
                    break;
                case "ProcStart":
                    Process.Start(c.Value);
                    break;
                case "Selection":
                    SelectionFunc(c.Value);
                    break;
                case "Console":
                    Close();
                    break;
            }
            Close();
        }

        private void SelectionFunc(string val)
        {
            if (val == "Readonly")
                ActiveEditor.tb.Selection.ReadOnly = true;
            else if (val == "Writeable")
                ActiveEditor.tb.Selection.ReadOnly = false;
        }

        private void Viewfunc(string str)
        {
            if (str == "ZoomIn")
                ActiveEditor.tb.Zoom += 10;
            else if (str == "ZoomOut")
                ActiveEditor.tb.Zoom -= 10;
        }

        private void MacroFunc(string str)
        {
            if (str == "Record")
                ActiveEditor.tb.MacrosManager.IsRecording = true;
            else if (str == "StopRecord")
                ActiveEditor.tb.MacrosManager.IsRecording = false;
            else if (str == "Run")
                ActiveEditor.tb.MacrosManager.ExecuteMacros();
            else if (str == "Clear")
                ActiveEditor.tb.MacrosManager.ClearMacros();
        }

        private void CommentFunc(string func)
        {
            if (func == "Toggle")
                ActiveEditor.tb.InsertLinePrefix(ActiveEditor.tb.CommentPrefix);
            else if (func == "Remove")
                ActiveEditor.tb.RemoveLinePrefix(ActiveEditor.tb.CommentPrefix);
        }

        private void Export(string func)
        {
            var dialog = new SaveFileDialog();
            if (func == "Rtf")
            {
                dialog.Filter = "Rich Text (*.rtf)|*.rtf";
                dialog.Tag = ActiveEditor.tb.Rtf;
            }
            else if (func == "Html")
            {
                dialog.Filter = "HTML Documents(*.html)|*.rtf";
                dialog.Tag = ActiveEditor.tb.Html;
            }
            dialog.ShowDialog();
            if (!string.IsNullOrEmpty(dialog.FileName))
                File.WriteAllText(dialog.FileName, dialog.Tag.ToString());
        }

        private void LineFunc(string str)
        {
            if (str == "MoveLineDown")
                ActiveEditor.tb.MoveSelectedLinesDown();
            else if (str == "MoveLineUp")
                ActiveEditor.tb.MoveSelectedLinesUp();
            else if (str == "Duplicate")
            {
                FastColoredTextBox fctb = ActiveEditor.tb;
                fctb.Selection.Start = new Place(0, ActiveEditor.tb.Selection.Start.iLine);
                fctb.Selection.Expand();
                object text = fctb.Selection.Text;
                fctb.Selection.Start = new Place(0, ActiveEditor.tb.Selection.Start.iLine);
                fctb.InsertText(text + "\r\n");
            }
            else if (str == "Join")
            {
                string[] lines = ActiveEditor.tb.SelectedText.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                ActiveEditor.tb.SelectedText = string.Join(" ", lines);
            }
            else if (str == "Sort")
            {
                var fctb = ActiveEditor.tb;
                if (fctb.SelectedText == null)
                {
                    MessageBox.Show("No Text/Lines is/are Selected to Reverse");
                    return;
                }
                string[] lines = fctb.SelectedText.Split(new[] {Environment.NewLine},
                    StringSplitOptions.RemoveEmptyEntries);
                Array.Reverse(lines);
                string formedtext = null;
                foreach (string line in lines)
                    formedtext += line + "\r\n";
                fctb.SelectedText = formedtext;
            }
        }

        private void IndentFunc(string str)
        {
            if (str == "Increase")
                ActiveEditor.tb.IncreaseIndent();
            else if (str == "Decrease")
                ActiveEditor.tb.DecreaseIndent();
            else if (str == "Do")
                ActiveEditor.tb.DoAutoIndent();
        }

        private void CodeFoldingFunc(string str)
        {
            if (str == "FoldAll")
                ActiveEditor.tb.CollapseAllFoldingBlocks();
            else if (str == "FoldSelection")
                ActiveEditor.tb.CollapseBlock(ActiveEditor.tb.Selection.Start.iLine, ActiveEditor.tb.Selection.End.iLine);
            else if (str == "UnFoldAll")
                ActiveEditor.tb.ExpandAllFoldingBlocks();
        }

        private void BookmarkFunc(string func)
        {
            if (func == "Toggle")
                ActiveEditor.tb.Bookmarks.Add(ActiveEditor.tb.Selection.Start.iLine);
            else if (func == "Remove")
                ActiveEditor.tb.Bookmarks.Remove(ActiveEditor.tb.Selection.Start.iLine);
            else if (func == "ClearAll")
                ActiveEditor.tb.Bookmarks.Clear();
            else if (func == "Manager")
            {
                var manager = new BookmarksInfos(ActiveEditor.tb);
                manager.StartPosition = FormStartPosition.CenterParent;
                manager.ShowDialog(this);
            }
        }

        private void SetSyntax(Language Lang)
        {
            ActiveEditor.Highlighter.HighlightSyntax(Lang, ActiveEditor.tb.Range);
            ActiveEditor.tb.Language = Lang;
        }

        private void FileFunc(string func)
        {
            if (func == "New")
            {
                _ynote.CreateNewDoc();
            }
            if (func == "Open")
            {
                using (var dialog = new OpenFileDialog())
                {
                    dialog.Filter = "All Files (*.*)|*.*";
                    dialog.ShowDialog();
                    if (dialog.FileName != "")
                        _ynote.OpenFile(dialog.FileName);
                }
            }
            else if (func == "Save")
                _ynote.SaveEditor(ActiveEditor);
            else if (func == "Print")
                ActiveEditor.tb.Print(new PrintDialogSettings {ShowPrintPreviewDialog = true});
            else if (func == "Properties")
            {
                if (ActiveEditor.Name != "Editor")
                    NativeMethods.ShowFileProperties(ActiveEditor.Name);
                else
                    MessageBox.Show("File is Not Saved!");
            }
            else if (func == "Close")
                ActiveEditor.Close();
            else if (func == "CloseAll")
                foreach (Editor edit in _ynote.Panel.Documents)
                    edit.Close();
        }
    }

    public class SCommand
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}