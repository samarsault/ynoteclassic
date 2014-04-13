using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AutocompleteMenuNS;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.UI
{
    public partial class SwitchFile : Form
    {
        private readonly IYnote _ynote;

        public SwitchFile(IYnote note)
        {
            InitializeComponent();
            _ynote = note;
            BuildAutoComplete();
        }

        private void BuildAutoComplete()
        {
            var items = (from DockContent doc in _ynote.Panel.Documents
                where doc is Editor
                select new AutocompleteItem(doc.Text)).ToList();
            SetAutoComplete(items, completemenu, textBox1);
        }

        private static void SetAutoComplete(IEnumerable<AutocompleteItem> items, AutocompleteMenu completemenu,
            Control tb)
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
            // updated : using LINQ
            foreach (var edit in _ynote.Panel.Documents.Cast<Editor>().Where(edit => edit.Text == text))
                edit.Show(_ynote.Panel, DockState.Document);
            // foreach (Editor edit in ynote.Panel.Documents)
            //     if (edit.Text == text)
            //         edit.Show(ynote.Panel, DockState.Document);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
            if (e.KeyCode != Keys.Enter) return;
            DoKeyDownFunction(textBox1.Text);
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