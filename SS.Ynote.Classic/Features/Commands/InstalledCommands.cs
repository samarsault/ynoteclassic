using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using FastColoredTextBoxNS;
using SS.Ynote.Classic;
using SS.Ynote.Classic.Features.Extensibility;
using SS.Ynote.Classic.Features.RunScript;
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
                (from object language in Enum.GetValues(typeof(Language)) select "SetSyntax:" + language).ToArray();
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
            return Directory.GetFiles(SettingsBase.SettingsDir + "Syntaxes", "*.xml")
                  .Select(directory => "SetSyntaxFile:" + Path.GetFileNameWithoutExtension(directory))
                  .ToArray();
        }
    }

    public void ProcessCommand(string val, IYnote ynote)
    {
        var ActiveEditor = ynote.Panel.ActiveDocument as Editor;
        foreach (var syntax in SS.Ynote.Classic.Features.Syntax.SyntaxHighlighter.LoadedSyntaxes.Where(syntax => syntax.SysPath ==
                                                                                        string.Format(
                                                                                            "{0}\\Syntaxes\\{1}.xml",
                                                                                            SettingsBase.SettingsDir,
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
                Directory.GetFiles(SettingsBase.SettingsDir + "Macros", "*.ymc")
                    .Select(directory => "Macro:" + Path.GetFileNameWithoutExtension(directory))
                    .ToArray();
        }
    }

    public void ProcessCommand(string val, IYnote ynote)
    {
        var edit = ynote.Panel.ActiveDocument as Editor;
        edit.Tb.MacrosManager.ExecuteMacros(string.Format(@"{0}Macros\{1}.ymc", SettingsBase.SettingsDir,
            val));
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
                Directory.GetFiles(SettingsBase.SettingsDir + "Scripts", "*.ymc")
                    .Select(directory => "Script:" + Path.GetFileNameWithoutExtension(directory))
                    .ToArray();
        }
    }

    public void ProcessCommand(string val, IYnote ynote)
    {
        YnoteScript.RunScript(ynote, string.Format(@"{0}Scripts\{1}.ys", SettingsBase.SettingsDir, val));
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
                Directory.GetFiles(SettingsBase.SettingsDir + "RunScripts", "*.run")
                    .Select(directory => "Run:" + Path.GetFileNameWithoutExtension(directory))
                    .ToArray();
        }
    }

    public void ProcessCommand(string val, IYnote ynote)
    {
        var edit = ynote.Panel.ActiveDocument as Editor;
        var item = RunConfiguration.ToRunConfig(SettingsBase.SettingsDir + @"RunScripts\" + val + ".run");
        if (item == null) return;
        if (edit != null) item.ProcessConfiguration(edit.Name);
        var temp = Path.GetTempFileName() + ".bat";
        File.WriteAllText(temp, item.ToBatch());
        var console = new Cmd("cmd.exe", "/k " + temp);
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
        get { return new[] { "Line:MoveUp", "Line:MoveDown", "Line:Join", "Line:Sort", "Line:Duplicate" }; }
    }

    public void ProcessCommand(string val, IYnote ynote)
    {
        var activeEditor = ynote.Panel.ActiveDocument as Editor;
        switch (val)
        {
            case "MoveLineDown":
                activeEditor.Tb.MoveSelectedLinesDown();
                break;

            case "MoveLineUp":
                activeEditor.Tb.MoveSelectedLinesUp();
                break;

            case "Duplicate":
                activeEditor.Tb.DuplicateLine(activeEditor.Tb.Selection.Start.iLine);
                break;
            case "Join":
                var lns = activeEditor.Tb.SelectedText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                activeEditor.Tb.SelectedText = string.Join(" ", lns);
                break;
            case "Sort":
                var fctb = activeEditor.Tb;
                string[] lines;
                if (string.IsNullOrEmpty(fctb.SelectedText))
                    lines = fctb.Text.Split(new[] { Environment.NewLine },
                        StringSplitOptions.RemoveEmptyEntries);
                else
                    lines = fctb.SelectedText.Split(new[] { Environment.NewLine },
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
        get { return new[] { "Indent:Increase", "Indent:Decrease", "Indent:Do" }; }
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
        get { return new[] { "Navigate:Back", "Navigate:Forward", "Navigate:GoLeftBracket", "Navigate:GoRightBracket" }; }
    }

    public void ProcessCommand(string val, IYnote ynote)
    {
        var activeEditor = ynote.Panel.ActiveDocument as Editor;
        if (activeEditor == null) return;
        switch (val)
        {
            case "Back": activeEditor.Tb.NavigateBackward();
                break;
            case "Forward":
                activeEditor.Tb.NavigateForward();
                break;
            case "GoLeftBracket":
                activeEditor.Tb.GoLeftBracket('(', ')');
                break;

            case "GoRightBracket":
                activeEditor.Tb.GoRightBracket('(', ')');
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
        get { return new[] { "Selection:GoWordRight", "Selection:GoWordLeft", "Selection:Expand", "Selection:Readable", "Selection:Writeable" }; }
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
        get { return new[] { "CodeFolding:FoldAll", "CodeFolding:ExpandAll", "CodeFolding:FoldSelection" }; }
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
                "File:New",
                "File:Open",
                "File:Save",
                "File:Revert",
                "File:Properties",
                "File:Close",
                "File:CloseAll"
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
                using (var dlg = new System.Windows.Forms.OpenFileDialog())
                {
                    dlg.Filter = "All Files (*.*)|*.*";
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
        get { return new []{"Google:"}; }
    }

    public void ProcessCommand(string val, IYnote ynote)
    {
        System.Diagnostics.Process.Start("http://google.com/search?q=" + val);
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
        get { return new[] { "Wikipedia:" }; }
    }

    public void ProcessCommand(string val, IYnote ynote)
    {
        System.Diagnostics.Process.Start("http://en.wikipedia.org/w/index.php?search=" + val);
    }
}