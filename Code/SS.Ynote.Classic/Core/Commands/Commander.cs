using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using AutocompleteMenuNS;
using SS.Ynote.Classic.Core.Extensibility;

namespace SS.Ynote.Classic.Core
{
    public partial class Commander : Form
    {
        /// <summary>
        ///     Installed Commands
        /// </summary>
        private static IList<ICommand> Commands;

        /// <summary>
        ///     IYnote interface
        /// </summary>
        private readonly IYnote _ynote;

        internal ToolStripDropDownButton LangMenu;

        private bool _addedText;

        public Commander(IYnote host)
        {
            InitializeComponent();
            completemenu.AllowsTabKey = true;
            tbcommand.Focus();
            _ynote = host;
            LostFocus += (o, a) => Close();
            Commands = new List<ICommand>(GetCommands())
            {
                new SetSyntaxCommand(),
                new SetSyntaxFile(),
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
            BuildAutoComplete();
        }

        public bool IsSnippetMode { get; set; }

        private IEnumerable<ICommand> GetCommands()
        {
            var lst = new List<ICommand>();
            foreach (
                var file in Directory.GetFiles(
                    YnoteSettings.SettingsDir, "*.ynotecommand", SearchOption.AllDirectories)
                )
                lst.Add(GetCommand(file, _ynote));
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

        public void AddText(string text)
        {
            tbcommand.Text = text;
            tbcommand.Select(tbcommand.Text.Length, 0);
            _addedText = true;
            completemenu.Show(tbcommand, true);
        }

        private void BuildAutoComplete()
        {
            IList<AutocompleteItem> items = new List<AutocompleteItem>();
            foreach (var cmd in Commands)
                if (cmd == null)
                    return;
                else
                    foreach (var command in cmd.Commands)
                        items.Add(new CommandAutocompleteItem(cmd.Key + ":" + command));
            completemenu.SetAutocompleteMenu(tbcommand, completemenu);
            completemenu.SetAutocompleteItems(items);
        }

        protected override void OnShown(EventArgs e)
        {
            if (IsSnippetMode)
            {
                tbcommand.Text = "Snippet:";
                tbcommand.Select("Snippet:".Length, 0);
            }
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
                        command.ProcessCommand(c.Value, _ynote);
                if (c.Key == "SetSyntax" || c.Key == "SetSyntaxFile")
                    LangMenu.Text = c.Value;
                completemenu.Items = null;
                if (!IsDisposed)
                    Close();
            }
            catch
            {
                MessageBox.Show("Error Running Command!");
            }
        }

        public static void RunCommand(IYnote ynote, string commandstr)
        {
            var command = YnoteCommand.FromString(commandstr);
            foreach (var cmd in Commands)
                if (cmd.Key == command.Key)
                    cmd.ProcessCommand(command.Value, ynote);
        }
    }
}