using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AutocompleteItem = AutocompleteMenuNS.AutocompleteItem;
using AutocompleteMenu = AutocompleteMenuNS.AutocompleteMenu;

namespace SS.Ynote.Classic.UI
{
    public partial class ClipboardHistory : Form
    {
        private readonly FastColoredTextBox fctb;

        public ClipboardHistory(IEnumerable<string> items, FastColoredTextBox tb)
        {
            InitializeComponent();
            fctb = tb;
            SetAutoComplete(items, completemenu, textBox1);
        }

        private static void SetAutoComplete(IEnumerable<string> items, AutocompleteMenu completemenu,
            Control tb)
        {
            completemenu.SetAutocompleteMenu(tb, completemenu);
            var lst = items.Select(item => new AutocompleteItem(item)).ToList();
            completemenu.SetAutocompleteItems(lst);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            completemenu.Show(textBox1, true);
        }

        private void DoKeyDownFunction()
        {
            fctb.InsertText(textBox1.Text);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
            if (e.KeyCode != Keys.Enter) return;
            DoKeyDownFunction();
            if (!IsDisposed)
                Close();
        }

        protected override void OnShown(EventArgs e)
        {
            completemenu.Show(textBox1, true);
            base.OnShown(e);
        }
    }
}