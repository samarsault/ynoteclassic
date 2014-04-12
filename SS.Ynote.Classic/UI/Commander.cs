using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using FastColoredTextBoxNS;
using SS.Ynote.Classic.Features.Extensibility;
using SS.Ynote.Classic.Features.RunScript;
using SS.Ynote.Classic.Features.Syntax;
using WeifenLuo.WinFormsUI.Docking;
using AutocompleteItem = AutocompleteMenuNS.AutocompleteItem;

namespace SS.Ynote.Classic.UI
{
    public partial class Commander : Form
    {
        private readonly IYnote _ynote;

        private bool _addedText;

        public Commander(IYnote host)
        {
            InitializeComponent();
            completemenu.AllowsTabKey = true;
            ActiveEditor = host.Panel.ActiveDocument as Editor;
            BuildAutoComplete();
            tbcommand.Focus();
            _ynote = host;
            LostFocus += (o, a) => Close();
        }

        public ToolStripDropDownButton LangMenu { private get; set; }

        /// <summary>
        ///     Get The Active Editor
        /// </summary>
        private Editor ActiveEditor { get; set; }

        public void AddText(string text)
        {
            tbcommand.Text = text;
            tbcommand.Select(tbcommand.Text.Length, 0);
            _addedText = true;
        }

        private void BuildAutoComplete()
        {
            var items = new List<AutocompleteItem>();
            items.AddRange(from object lang in Enum.GetValues(typeof (Language))
                select new AutocompleteItem("SetSyntax:" + lang));
            items.AddRange(from item in SyntaxHighlighter.LoadedSyntaxes
                where item != null
                select new AutocompleteItem("SetSyntaxFile:" + Path.GetFileNameWithoutExtension(item.SysPath)));
            items.Add(new AutocompleteItem("File:New"));
            items.Add(new AutocompleteItem("File:Open"));
            items.Add(new AutocompleteItem("File:Save"));
            items.Add(new AutocompleteItem("File:Print"));
            items.Add(new AutocompleteItem("File:Properties"));
            items.Add(new AutocompleteItem("File:Close"));
            items.Add(new AutocompleteItem("File:CloseAll"));
            items.Add(new AutocompleteItem("Navigate:GoLeftBracket()"));
            items.Add(new AutocompleteItem("Navigate:GoRightBracket()"));
            items.Add(new AutocompleteItem("Navigate:GoLeftBracket[]"));
            items.Add(new AutocompleteItem("Navigate:GoRightBracket[]"));
            items.Add(new AutocompleteItem("Navigate:GoLeftBracket{}"));
            items.Add(new AutocompleteItem("Navigate:GoRightBracket{}"));
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
            items.Add(new AutocompleteItem("Macros:Clear"));
            items.AddRange(
                Directory.GetFiles(SettingsBase.SettingsDir + "Macros", "*.ymc")
                    .Select(macro => new AutocompleteItem("Macro:" + Path.GetFileNameWithoutExtension(macro))));
            items.AddRange(
                Directory.GetFiles(SettingsBase.SettingsDir + "Scripts", "*.ys")
                    .Select(script => new AutocompleteItem("Script:" + Path.GetFileNameWithoutExtension(script))));
            items.AddRange(
                RunConfiguration.GetConfigurations()
                    .Select(config => new AutocompleteItem("Run:" + Path.GetFileNameWithoutExtension(config))));
            items.Add(new AutocompleteItem("Selection:Readonly"));
            items.Add(new AutocompleteItem("Selection:Writeable"));
            items.Add(new AutocompleteItem("View:ZoomIn"));
            items.Add(new AutocompleteItem("View:ZoomOut"));
            items.Add(new AutocompleteItem("ProcStart:^"));
            items.Add(new AutocompleteItem("Google:"));
            items.Add(new AutocompleteItem("Wikipedia:"));
            items.Add(new AutocompleteItem("Console:Close"));
            completemenu.SetAutocompleteMenu(tbcommand, completemenu);
            completemenu.SetAutocompleteItems(items);
        }

        private static SCommand Parse(string command)
        {
            try
            {
                var cmd = new SCommand();
                var l = command.IndexOf(":");
                if (l > 0)
                    cmd.Key = command.Substring(0, l);
                var result = command.Substring(command.LastIndexOf(':') + 1);
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
            if (_addedText) return;
            completemenu.Show(tbcommand, true);
            base.OnShown(e);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            completemenu.Show(tbcommand, true);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RunCommand(Parse(tbcommand.Text));
            }
            if (e.KeyCode == Keys.Escape)
            {
                completemenu.Items = null;
                Close();
            }
        }

        private void RunCommand(SCommand c)
        {
            switch (c.Key)
            {
                case "SetSyntax":
                    ActiveEditor.Highlighter.HighlightSyntax(c.Value.ToEnum<Language>(),
                        new TextChangedEventArgs(ActiveEditor.Tb.Range));
                    ActiveEditor.Tb.Language = c.Value.ToEnum<Language>();
                    ActiveEditor.Syntax = null;
                    if (LangMenu != null) LangMenu.Text = c.Value;
                    break;

                case "SetSyntaxFile":
                    foreach (var syntax in SyntaxHighlighter.LoadedSyntaxes.Where(syntax => syntax.SysPath ==
                                                                                            string.Format(
                                                                                                "{0}\\Syntaxes\\{1}.xml",
                                                                                                SettingsBase.SettingsDir,
                                                                                                c.Value)))
                    {
                        ActiveEditor.Highlighter.HighlightSyntax(syntax, new TextChangedEventArgs(ActiveEditor.Tb.Range));
                        ActiveEditor.Syntax = syntax;
                        if (LangMenu != null) LangMenu.Text = c.Value;
                    }
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

                case "Macro":
                    RunMacro(c.Value);
                    break;

                case "Script":
                    YnoteScript.RunScript(_ynote, string.Format(@"{0}Scripts\{1}.ys", SettingsBase.SettingsDir, c.Value));
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

                case "Google":
                    Process.Start(string.Format("http://www.google.com/search?q={0}", c.Value));
                    break;

                case "Wikipedia":
                    Process.Start(string.Format("http://wikipedia.org/w/index.php?search={0}", c.Value));
                    break;

                case "Selection":
                    SelectionFunc(c.Value);
                    break;

                case "Console":
                    Close();
                    break;

                case "Navigate":
                    NavigateFunc(c.Value);
                    break;

                case "Run":
                    ExecuteRunScript(c.Value);
                    break;
            }
            completemenu.Items = null;
            if (!IsDisposed)
                Close();
        }

        private void NavigateFunc(string val)
        {
            switch (val)
            {
                case "GoLeftBracket()":
                    ActiveEditor.Tb.GoLeftBracket('(', ')');
                    break;

                case "GoRightBracket()":
                    ActiveEditor.Tb.GoRightBracket('(', ')');
                    break;

                case "GoLeftBracket[]":
                    ActiveEditor.Tb.GoLeftBracket('[', ']');
                    break;

                case "GoRightBracket[]":
                    ActiveEditor.Tb.GoRightBracket('[', ']');
                    break;

                case "GoLeftBracket{}":
                    ActiveEditor.Tb.GoLeftBracket('{', '}');
                    break;

                case "GoRightBracket{}":
                    ActiveEditor.Tb.GoRightBracket('{', '}');
                    break;
            }
        }

        private void ExecuteRunScript(string val)
        {
            var item = RunConfiguration.ToRunConfig(SettingsBase.SettingsDir + @"RunScripts\" + val + ".run");
            if (item == null) return;
            if (ActiveEditor != null) item.ProcessConfiguration(ActiveEditor.Name);
            var temp = Path.GetTempFileName() + ".bat";
            File.WriteAllText(temp, item.ToBatch());
            var console = new Cmd("cmd.exe", "/k " + temp);
            console.Show(_ynote.Panel, DockState.DockBottom);
        }

        private void RunMacro(string macro)
        {
            ActiveEditor.Tb.MacrosManager.ExecuteMacros(string.Format(@"{0}Macros\{1}.ymc", SettingsBase.SettingsDir,
                macro));
        }

        private void SelectionFunc(string val)
        {
            switch (val)
            {
                case "Readonly":
                    ActiveEditor.Tb.Selection.ReadOnly = true;
                    break;
                case "Writeable":
                    ActiveEditor.Tb.Selection.ReadOnly = false;
                    break;
            }
        }

        private void Viewfunc(string str)
        {
            if (str == "ZoomIn")
                ActiveEditor.Tb.Zoom += 10;
            else if (str == "ZoomOut")
                ActiveEditor.Tb.Zoom -= 10;
        }

        private void MacroFunc(string str)
        {
            if (str == "Record")
                ActiveEditor.Tb.MacrosManager.IsRecording = true;
            else if (str == "StopRecord")
                ActiveEditor.Tb.MacrosManager.IsRecording = false;
            else if (str == "Run")
                ActiveEditor.Tb.MacrosManager.ExecuteMacros();
            else if (str == "Clear")
                ActiveEditor.Tb.MacrosManager.ClearMacros();
        }

        private void CommentFunc(string func)
        {
            if (func == "Toggle")
                ActiveEditor.Tb.InsertLinePrefix(ActiveEditor.Tb.CommentPrefix);
            else if (func == "Remove")
                ActiveEditor.Tb.RemoveLinePrefix(ActiveEditor.Tb.CommentPrefix);
        }

        private void Export(string func)
        {
            using (var dialog = new SaveFileDialog())
            {
                switch (func)
                {
                    case "Rtf":
                        dialog.Filter = "Rich Text (*.rtf)|*.rtf";
                        dialog.Tag = ActiveEditor.Tb.Rtf;
                        break;

                    case "Html":
                        dialog.Filter = "HTML Documents(*.html)|*.rtf";
                        dialog.Tag = ActiveEditor.Tb.Html;
                        break;
                }
                dialog.ShowDialog();
                if (!string.IsNullOrEmpty(dialog.FileName))
                    File.WriteAllText(dialog.FileName, dialog.Tag.ToString());
            }
        }

        private void LineFunc(string str)
        {
            switch (str)
            {
                case "MoveLineDown":
                    ActiveEditor.Tb.MoveSelectedLinesDown();
                    break;

                case "MoveLineUp":
                    ActiveEditor.Tb.MoveSelectedLinesUp();
                    break;

                case "Duplicate":
                {
                    var fctb = ActiveEditor.Tb;
                    fctb.Selection.Start = new Place(0, ActiveEditor.Tb.Selection.Start.iLine);
                    fctb.Selection.Expand();
                    object text = fctb.Selection.Text;
                    fctb.Selection.Start = new Place(0, ActiveEditor.Tb.Selection.Start.iLine);
                    fctb.InsertText(text + "\r\n");
                }
                    break;

                case "Join":
                {
                    var lines = ActiveEditor.Tb.SelectedText.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                    ActiveEditor.Tb.SelectedText = string.Join(" ", lines);
                }
                    break;

                case "Sort":
                {
                    var fctb = ActiveEditor.Tb;
                    if (fctb.SelectedText == null)
                    {
                        MessageBox.Show("No Text/Lines is/are Selected to Reverse");
                        return;
                    }
                    var lines = fctb.SelectedText.Split(new[] {Environment.NewLine},
                        StringSplitOptions.RemoveEmptyEntries);
                    Array.Reverse(lines);
                    var formedtext = lines.Aggregate<string, string>(null, (current, line) => current + (line + "\r\n"));
                    fctb.SelectedText = formedtext;
                }
                    break;
            }
        }

        private void IndentFunc(string str)
        {
            switch (str)
            {
                case "Increase":
                    ActiveEditor.Tb.IncreaseIndent();
                    break;

                case "Decrease":
                    ActiveEditor.Tb.DecreaseIndent();
                    break;

                case "Do":
                    if (ActiveEditor.Tb.Language == Language.Xml)
                        ActiveEditor.Tb.Text = PrettyXml(ActiveEditor.Tb.Text);
                    else
                        ActiveEditor.Tb.DoAutoIndent();
                    break;
            }
        }

        private static string PrettyXml(string xml)
        {
            var result = "";

            var mStream = new MemoryStream();
            var writer = new XmlTextWriter(mStream, Encoding.Unicode);
            var document = new XmlDocument();

            try
            {
                // Load the XmlDocument with the XML.
                document.LoadXml(xml);

                writer.Formatting = Formatting.Indented;

                // Write the XML into a formatting XmlTextWriter
                document.WriteContentTo(writer);
                writer.Flush();
                mStream.Flush();

                // Have to rewind the MemoryStream in order to read
                // its contents.
                mStream.Position = 0;

                // Read MemoryStream contents into a StreamReader.
                var sReader = new StreamReader(mStream);

                // Extract the text from the StreamReader.
                result = sReader.ReadToEnd();
            }
            catch (XmlException)
            {
            }

            mStream.Close();
            writer.Close();

            return result;
        }

        private void CodeFoldingFunc(string str)
        {
            if (str == "FoldAll")
                ActiveEditor.Tb.CollapseAllFoldingBlocks();
            else if (str == "FoldSelection")
                ActiveEditor.Tb.CollapseBlock(ActiveEditor.Tb.Selection.Start.iLine, ActiveEditor.Tb.Selection.End.iLine);
            else if (str == "UnFoldAll")
                ActiveEditor.Tb.ExpandAllFoldingBlocks();
        }

        private void BookmarkFunc(string func)
        {
            if (func == "Toggle")
                ActiveEditor.Tb.Bookmarks.Add(ActiveEditor.Tb.Selection.Start.iLine);
            else if (func == "Remove")
                ActiveEditor.Tb.Bookmarks.Remove(ActiveEditor.Tb.Selection.Start.iLine);
            else if (func == "ClearAll")
                ActiveEditor.Tb.Bookmarks.Clear();
            else if (func == "Manager")
            {
                var manager = new BookmarksInfos(ActiveEditor.Tb) {StartPosition = FormStartPosition.CenterParent};
                manager.ShowDialog(this);
            }
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
                ActiveEditor.Tb.Print(new PrintDialogSettings {ShowPrintPreviewDialog = true});
            else if (func == "Properties")
            {
                if (ActiveEditor.IsSaved)
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

    /*
     * version 2.8 , suppose
    /// <summary>
    /// ICommand
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Command Key eg : Macro
        /// </summary>
        string Key { get; }
        /// <summary>
        /// Possible Commands
        /// </summary>
        string[] Commands { get; }
        void ProcessCommand(string val);
    }
     */
}