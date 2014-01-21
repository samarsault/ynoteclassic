using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms;
using AutocompleteMenuNS;
using SS.Ynote.Classic.Properties;

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
            var items = new List<AutocompleteItem>();
            foreach (Editor doc in ynote.Panel.Documents)
                items.Add(new AutocompleteItem(doc.Text));
            completemenu.SetAutocompleteMenu(textBox1, completemenu);
            completemenu.SetAutocompleteItems(items);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            completemenu.Show(textBox1, true);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            foreach (Editor edit in ynote.Panel.Documents)
                if (edit.Text == textBox1.Text)
                    edit.Show(ynote.Panel);
            Close();
        }
        protected override void OnShown(EventArgs e)
        {
            completemenu.Show(textBox1, true);
            base.OnShown(e);
        }
    }
       
}
