using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using FastColoredTextBoxNS;
using SS.Ynote.Classic;
using SS.Ynote.Classic.Core.Extensibility;
using SS.Ynote.Classic.Core.RunScript;
using SS.Ynote.Classic.Core.Snippets;
using SS.Ynote.Classic.Core.Syntax;
using SS.Ynote.Classic.UI;
using WeifenLuo.WinFormsUI.Docking;

internal class SetSyntaxCommand : ICommand
{
    public string Key
    {
        get { return "SetSyntax"; }
    }

    public string[] Commands
    {
        get
        {
            return
                (from object language in Enum.GetValues(typeof (Language)) select language.ToString()).ToArray();
        }
    }

    public void ProcessCommand(string val, IYnote ynote)
    {
        var ae = ynote.Panel.ActiveDocument as Editor;
        if (ae == null) return;
        ae.Highlighter.HighlightSyntax(val.ToEnum<Language>(),
            new TextChangedEventArgs(ae.Tb.Range));
        ae.Tb.Language = val.ToEnum<Language>();
        ae.Syntax = null;
    }
}

internal class SetSyntaxFile : ICommand
{
    public string Key
    {
        get { return "SetSyntaxFile"; }
    }

    public string[] Commands
    {
        get
        {
            return Directory.GetFiles(YnoteSettings.SettingsDir + "Syntaxes", "*.ynotesyntax")
                .Select(Path.GetFileNameWithoutExtension)
                .ToArray();
        }
    }

    public void ProcessCommand(string val, IYnote ynote)
    {
        var ActiveEditor = ynote.Panel.ActiveDocument as Editor;
        foreach (var syntax in SyntaxHighlighter.LoadedSyntaxes.Where(syntax => syntax.SysPath ==
                                                                                string.Format(
                                                                                    "{0}\\Syntaxes\\{1}.xml",
                                                                                    YnoteSettings.SettingsDir,
                                                                                    val)))
        {
            ActiveEditor.Highlighter.HighlightSyntax(syntax, new TextChangedEventArgs(ActiveEditor.Tb.Range));
            ActiveEditor.Syntax = syntax;
        }
    }
}

internal class MacroCommand : ICommand
{
    public string Key
    {
        get { return "Macro"; }
    }

    public string[] Commands
    {
        get
        {
            return
                Directory.GetFiles(YnoteSettings.SettingsDir, "*.ynotemacro", SearchOption.AllDirectories)
                    .Select(directory => "Macro:" + Path.GetFileNameWithoutExtension(directory))
                    .ToArray();
        }
    }

    public void ProcessCommand(string val, IYnote ynote)
    {
        var edit = ynote.Panel.ActiveDocument as Editor;
        foreach (var file in Directory.GetFiles(YnoteSettings.SettingsDir, "*.ynotemacro", SearchOption.AllDirectories))
            if (Path.GetFileName(file) == val + ".ynotemacro")
                edit.Tb.MacrosManager.ExecuteMacros(file);
    }
}

internal class ScriptCommand : ICommand
{
    public string Key
    {
        get { return "Script"; }
    }

    public string[] Commands
    {
        get
        {
            return
                Directory.GetFiles(YnoteSettings.SettingsDir, "*.ys", SearchOption.AllDirectories)
                    .Select(Path.GetFileNameWithoutExtension)
                    .ToArray();
        }
    }

    public void ProcessCommand(string val, IYnote ynote)
    {
        foreach (var file in Directory.GetFiles(YnoteSettings.SettingsDir, "*.ys", SearchOption.AllDirectories))
            if (Path.GetFileName(file) == val + ".ys")
                YnoteScript.RunScript(ynote, file);
    }
}

internal class RunScriptCommand : ICommand
{
    public string Key
    {
        get { return "Run"; }
    }

    public string[] Commands
    {
        get
        {
            return
                Directory.GetFiles(YnoteSettings.SettingsDir + "RunScripts", "*.run")
                    .Select(Path.GetFileNameWithoutExtension)
                    .ToArray();
        }
    }

    public void ProcessCommand(string val, IYnote ynote)
    {
        var edit = ynote.Panel.ActiveDocument as Editor;
        var item = RunConfiguration.ToRunConfig(YnoteSettings.SettingsDir + @"RunScripts\" + val + ".run");
        if (item == null) return;
        if (edit != null) item.ProcessConfiguration(edit.Name);
        var temp = Path.GetTempFileName() + ".bat";
        File.WriteAllText(temp, item.ToBatch());
        var console = new Shell("cmd.exe", "/k " + temp);
        console.Show(ynote.Panel, DockState.DockBottom);
    }
}

internal class LineCommand : ICommand
{
    public string Key
    {
        get { return "Line"; }
    }

    public string[] Commands
    {
        get { return new[] {"MoveUp", "MoveDown", "Join", "Sort", "Duplicate"}; }
    }

    public void ProcessCommand(string val, IYnote ynote)
    {
        var activeEditor = ynote.Panel.ActiveDocument as Editor;
        switch (val)
        {
            case "MoveDown":
                activeEditor.Tb.MoveSelectedLinesDown();
                break;

            case "MoveUp":
                activeEditor.Tb.MoveSelectedLinesUp();
                break;

            case "Duplicate":
                activeEditor.Tb.DuplicateLine(activeEditor.Tb.Selection.Start.iLine);
                break;

            case "Join":
                var lns = activeEditor.Tb.SelectedText.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                activeEditor.Tb.SelectedText = string.Join(" ", lns);
                break;

            case "Sort":
                var fctb = activeEditor.Tb;
                string[] lines;
                if (string.IsNullOrEmpty(fctb.SelectedText))
                    lines = fctb.Text.Split(new[] {Environment.NewLine},
                        StringSplitOptions.RemoveEmptyEntries);
                else
                    lines = fctb.SelectedText.Split(new[] {Environment.NewLine},
                        StringSplitOptions.RemoveEmptyEntries);
                Array.Reverse(lines);
                var formedtext = lines.Aggregate<string, string>(null, (current, line) => current + (line + "\r\n"));
                fctb.SelectedText = formedtext;
                break;
        }
    }
}

internal class IndentCommand : ICommand
{
    public string Key
    {
        get { return "Indent"; }
    }

    public string[] Commands
    {
        get { return new[] {"Increase", "Decrease", "Do"}; }
    }

    public void ProcessCommand(string value, IYnote ynote)
    {
        if (ynote.Panel.ActiveDocument == null || !(ynote.Panel.ActiveDocument is Editor)) return;
        var edit = ynote.Panel.ActiveDocument as Editor;
        switch (value)
        {
            case "Increase":
                edit.Tb.IncreaseIndent();
                break;

            case "Decrease":
                edit.Tb.DecreaseIndent();
                break;

            case "Do":
                if (edit.Tb.Language == Language.Xml)
                    edit.Tb.Text = PrettyXml(edit.Tb.Text);
                else
                    edit.Tb.DoAutoIndent();
                break;
        }
    }

    private static string PrettyXml(string xml)
    {
        var result = string.Empty;

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
}

internal class NavigateCommand : ICommand
{
    public string Key
    {
        get { return "Navigate"; }
    }

    public string[] Commands
    {
        get { return new[] {"Back", "Forward", "GoLeftBracket", "GoRightBracket"}; }
    }

    public void ProcessCommand(string val, IYnote ynote)
    {
        var activeEditor = ynote.Panel.ActiveDocument as Editor;
        if (activeEditor == null) return;
        switch (val)
        {
            case "Back":
                activeEditor.Tb.NavigateBackward();
                break;

            case "Forward":
                activeEditor.Tb.NavigateForward();
                break;

            case "GoLeftBracket":
                activeEditor.Tb.GoLeftBracket();
                break;

            case "GoRightBracket":
                activeEditor.Tb.GoRightBracket();
                break;
        }
    }
}

internal class SelectionCommand : ICommand
{
    public string Key
    {
        get { return "Selection"; }
    }

    public string[] Commands
    {
        get
        {
            return new[]
            {
                "GoWordRight", "GoWordLeft", "Expand", "Readable",
                "Writeable"
            };
        }
    }

    public void ProcessCommand(string val, IYnote ynote)
    {
        //TODO:Performance
        var tb = (ynote.Panel.ActiveDocument as Editor).Tb;
        if (tb != null)
        {
            if (val == "Readable")
                tb.Selection.ReadOnly = true;
            else if (val == "Writeable")
                tb.Selection.ReadOnly = false;
            else if (val == "GoWordRight")
                tb.Selection.GoWordRight(true);
            else if (val == "GoWordLeft")
                tb.Selection.GoWordLeft(true);
            else if (val == "Expand")
                tb.Selection.Expand();
        }
    }
}

internal class CodeFoldingCommand : ICommand
{
    public string Key
    {
        get { return "CodeFolding"; }
    }

    public string[] Commands
    {
        get { return new[] {"FoldAll", "ExpandAll", "FoldSelection"}; }
    }

    public void ProcessCommand(string val, IYnote ynote)
    {
        var tb = (ynote.Panel.ActiveDocument as Editor).Tb;
        if (val == "FoldAll")
            tb.CollapseAllFoldingBlocks();
        else if (val == "ExpandAll")
            tb.ExpandAllFoldingBlocks();
        else if (val == "FoldSelection")
            tb.CollapseBlock(tb.SelectionStart, tb.Selection.End.iLine);
    }
}

internal class FileCommand : ICommand
{
    public string Key
    {
        get { return "File"; }
    }

    public string[] Commands
    {
        get
        {
            return new[]
            {
                "New",
                "Open",
                "Save",
                "Revert",
                "Delete",
                "Properties",
                "Close",
                "CloseAll"
            };
        }
    }

    public void ProcessCommand(string val, IYnote ynote)
    {
        var edit = ynote.Panel.ActiveDocument as Editor;
        switch (val)
        {
            case "New":
                ynote.CreateNewDoc();
                break;

            case "Open":
                using (var dlg = new OpenFileDialog())
                {
                    dlg.Filter = "All Files (*.*)|*.*";
                    if (dlg.ShowDialog() == DialogResult.OK)
                        ynote.OpenFile(dlg.FileName);
                }
                break;

            case "Save":
                ynote.SaveEditor(ynote.Panel.ActiveDocument as Editor);
                break;

            case "Properties":
                if (edit.IsSaved)
                    NativeMethods.ShowFileProperties(edit.Name);
                break;

            case "Revert":
                if (edit == null || !edit.IsSaved) return;
                edit.Tb.OpenFile(edit.Name);
                edit.Text = Path.GetFileName(edit.Name);
                break;

            case "Close":
                edit.Close();
                break;

            case "CloseAll":
                foreach (Editor doc in ynote.Panel.Documents.OfType<Editor>())
                    doc.Close();
                break;
            case "Delete":
                var filename = edit.Name;
                if (!edit.IsSaved) return;
                var result = MessageBox.Show("Are you sure you want to Delete " + edit.Text + " ?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    File.Delete(filename);
                    edit.Close();
                }
                break;
        }
    }
}

internal class GoogleCommand : ICommand
{
    public string Key
    {
        get { return "Google"; }
    }

    public string[] Commands
    {
        get { return new[] {""}; }
    }

    public void ProcessCommand(string val, IYnote ynote)
    {
        Process.Start("http://google.com/search?q=" + val);
    }
}

internal class WikipediaCommand : ICommand
{
    public string Key
    {
        get { return "Wikipedia"; }
    }

    public string[] Commands
    {
        get { return new[] {""}; }
    }

    public void ProcessCommand(string val, IYnote ynote)
    {
        Process.Start("http://en.wikipedia.org/w/index.php?search=" + val);
    }
}

internal class SnippetCommand : ICommand
{
    public string Key
    {
        get { return "Snippet"; }
    }

    public string[] Commands
    {
        get { return GetCommands(); }
    }

    public void ProcessCommand(string val, IYnote ynote)
    {
        var edit = ynote.Panel.ActiveDocument as Editor;
        string file = Path.Combine(YnoteSettings.SettingsDir, val + ".ynotesnippet");
        foreach (
            var snippet in Directory.GetFiles(YnoteSettings.SettingsDir, "*.ynotesnippet", SearchOption.AllDirectories))
            if (Path.GetFileNameWithoutExtension(snippet) == "val")
                edit.InsertSnippet(YnoteSnippet.Read(file));
    }

    public string[] GetCommands()
    {
        int index = 0;
        if ((Application.OpenForms[index] as IYnote) == null)
            index++;
        IYnote ynote = Application.OpenForms[index] as IYnote;
        if (ynote.Panel.ActiveDocument == null
            || !(ynote.Panel.ActiveDocument is Editor))
            return null;
        var items = new List<string>();
        var lang = (ynote.Panel.ActiveDocument as Editor).Tb.Language;
        foreach (
            var file in Directory.GetFiles(YnoteSettings.SettingsDir, "*.ynotesnippet", SearchOption.AllDirectories))
            items.Add(Path.GetFileNameWithoutExtension(file));
        return items.ToArray();
    }
}