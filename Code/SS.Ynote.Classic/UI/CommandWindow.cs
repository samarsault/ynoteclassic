using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using AutocompleteMenuNS;

namespace SS.Ynote.Classic.UI
{
    public partial class CommandWindow : Form
    {
        /// <summary>
        ///     Delegate for Process Command Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ProcessCommandEventHandler(object sender, CommandWindowEventArgs e);

        /// <summary>
        ///     Occurs when users inputs a command and is given for processing
        /// </summary>
        public event ProcessCommandEventHandler ProcessCommand;

        public CommandWindow(IEnumerable<AutocompleteItem> itemlist)
        {
            InitializeComponent();
            SetAutoComplete(itemlist, completemenu, tbCmd);
        }

#if UNSUCCESSFUL
        private void BuildAutoComplete(IEnumerable<AutocompleteItem> itemlist)
        {
          /*  var items = new List<AutocompleteItem>();
            foreach(var doc in _ynote.Panel.Documents)
                items.Add(new FuzzyAutoCompleteItem((doc as DockContent).Text));*/
        }
#endif

        private static void SetAutoComplete(IEnumerable<AutocompleteItem> items, AutocompleteMenu completemenu,
            Control tb)
        {
            completemenu.SetAutocompleteMenu(tb, completemenu);
            completemenu.SetAutocompleteItems(items);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            completemenu.Show(tbCmd, true);
        }

        public virtual void OnProcessCommand(CommandWindowEventArgs e)
        {
            var handler = ProcessCommand;
            if (handler != null)
                handler(this, e);
        }

        private void tbCmd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
            if (e.KeyCode != Keys.Enter) return;
            OnProcessCommand(new CommandWindowEventArgs(tbCmd.Text));
            Close();
        }

        protected override void OnShown(EventArgs e)
        {
            completemenu.Show(tbCmd, true);
            base.OnShown(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            completemenu.Items = null;
            base.OnClosing(e);
        }
    }

    public class CommandWindowEventArgs : EventArgs
    {
        public CommandWindowEventArgs(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}