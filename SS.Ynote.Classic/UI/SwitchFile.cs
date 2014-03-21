#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AutocompleteMenuNS;
using WeifenLuo.WinFormsUI.Docking;

#endregion

namespace SS.Ynote.Classic.UI
{
    public partial class SwitchFile : Form
    {
        private readonly IYnote ynote;

        public SwitchFile(IYnote note)
        {
            InitializeComponent();
            ynote = note;
            BuildAutoComplete();
        }

        void BuildAutoComplete()
        {
            var items = (from DockContent doc in ynote.Panel.Documents
                where doc.GetType() == typeof (Editor)
                select new AutocompleteItem(doc.Text)).ToList();
            SetAutoComplete(items, completemenu,textBox1);
        }

        static void SetAutoComplete(IEnumerable<AutocompleteItem> items, AutocompleteMenu completemenu, TextBox tb)
        {
            completemenu.SetAutocompleteMenu(tb, completemenu);
            completemenu.SetAutocompleteItems(items);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            completemenu.Show(textBox1, true);
        }

        private void DoKeyDownFunction(string text)
        {
            foreach (Editor edit in ynote.Panel.Documents)
                if (edit.Text == text)
                    edit.Show(ynote.Panel);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
            if (e.KeyCode != Keys.Enter) return;
            DoKeyDownFunction(textBox1.Text);
            if(!IsDisposed)
                Close();
        }

        protected override void OnShown(EventArgs e)
        {
            completemenu.Show(textBox1, true);
            base.OnShown(e);
        }
    }
}