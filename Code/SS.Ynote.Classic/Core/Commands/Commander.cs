using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using AutocompleteMenuNS;
using SS.Ynote.Classic.Core.Extensibility;
using SS.Ynote.Classic.Core.Settings;

namespace SS.Ynote.Classic.Core
{
    public partial class Commander : Form
    {
        /// <summary>
        ///     Installed Commands
        /// </summary>
        private static IList<ICommand> Commands;

        internal ToolStripDropDownButton LangMenu;

        public Commander()
        {
#if DEBUG
            var watch = new Stopwatch();
            watch.Start();
#endif
            InitializeComponent();
            completemenu.AllowsTabKey = true;
            tbcommand.Focus();
            LostFocus += (o, a) => Close();
            ReloadCommands();
            BuildAutoComplete();
#if DEBUG
            watch.Stop();
            Debug.WriteLine(watch.ElapsedMilliseconds + "ms Commander.ctor()");
#endif
        }

        private static void ReloadCommands()
        {
            Commands = new List<ICommand>(GetCommands())
            {
                new SetSyntaxCommand(),
                new MacroCommand(),
                new ScriptCommand(),
                new SnippetCommand(),
                new RunScriptCommand(),
                new LineCommand(),
                new SelectionCommand(),
                new IndentCommand(),
                new FileCommand(),
                new CodeFoldingCommand(),
                new NavigateCommand(),
                new GoogleCommand(),
                new WikipediaCommand(),
            };
        }

        private static IEnumerable<ICommand> GetCommands()
        {
            var lst = new List<ICommand>();
            foreach (
                var file in Directory.GetFiles(
                    GlobalSettings.SettingsDir, "*.ynotecommand", SearchOption.AllDirectories)
                )
                lst.Add(GetCommand(file, Globals.Ynote));
            return lst;
        }

        private static ICommand GetCommand(string script, IYnote ynote)
        {
            try
            {
                return YnoteScript.Get<ICommand>(ynote, script, "*.GetCommand");
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an Error running the script : \r\n" + ex.Message, "YnoteScript Host",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }
        private void BuildAutoComplete()
        {
            IList<AutocompleteItem> items = new List<AutocompleteItem>();
            foreach (var cmd in Commands)
                if (cmd == null)
                    return;
                else
                    foreach (var command in cmd.Commands)
                        items.Add(new FuzzyAutoCompleteItem(cmd.Key + ":" + command));
            completemenu.SetAutocompleteMenu(tbcommand, completemenu);
            completemenu.SetAutocompleteItems(items);
        }

        protected override void OnShown(EventArgs e)
        {
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
                RunCommand(YnoteCommand.FromString(tbcommand.Text));
            }
            else if (e.KeyCode == Keys.Escape)
            {
                completemenu.Items = null;
                Close();
            }
        }

        private void RunCommand(YnoteCommand c)
        {
            try
            {
                foreach (var command in Commands)
                    if (command.Key == c.Key)
                        command.ProcessCommand(c.Value, Globals.Ynote);
                if (c.Key == "SetSyntax" || c.Key == "SetSyntaxFile")
                    LangMenu.Text = c.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Running Command!\n" + ex.Message);
            }
            completemenu.Items = null;
            if (!IsDisposed)
                Close();
        }

        public static void RunCommand(IYnote ynote, string commandstr)
        {
            var command = YnoteCommand.FromString(commandstr);
            if (Commands == null)
                ReloadCommands();
            foreach (var cmd in Commands)
            {
                if (cmd.Key == command.Key)
                {
                    cmd.ProcessCommand(command.Value, ynote);
                    break;
                }
            }
        }
    }
}