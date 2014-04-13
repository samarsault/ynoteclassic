#if TEST
// New Commander Preprocessor

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using FastColoredTextBoxNS;
using SS.Ynote.Classic.Features.Extensibility;
using SS.Ynote.Classic.Features.RunScript;
using SS.Ynote.Classic.UI;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic
{
    /// <summary>
    ///     ICommand
    /// </summary>
    internal interface ICommand
    {
        /// <summary>
        ///     Command Key eg : Macro
        /// </summary>
        string Key { get; }

        /// <summary>
        ///     Possible Commands
        /// </summary>
        string[] Commands { get; }

        /// <summary>
        ///     Processes Command
        /// </summary>
        /// <param name="val"></param>
        void ProcessCommand(string val, IYnote ynote);
    }

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
                    (from object language in Enum.GetValues(typeof (Language)) select "SetSyntax:" + language).ToArray();
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
            get { return new[] {"MoveUp", "MoveDown", "Join", "Sort", "Duplicate"}; }
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
            get { return new[] {"Indent:Increase", "Indent:Decrease", "Indent:Do"}; }
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
            get { return new[] {"Navigate:Back", "Navigate:Forward", "Navigate:GoLeftBracket", "Navigate:GoRightBracket"}; }
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
            get { throw new NotImplementedException(); }
        }

        public void ProcessCommand(string val, IYnote ynote)
        {
            throw new NotImplementedException();
        }
    }
}
#endif