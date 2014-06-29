using System;
using System.IO;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using SS.Ynote.Classic.Core.Settings;

namespace SS.Ynote.Classic.UI
{
    class MacroItem
    {
        internal string File;
        internal string Name;

        internal MacroItem(string name, string file)
        {
            Name = name;
            File = file;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public partial class MacroExecDialog : Form
    {
        private readonly FastColoredTextBox fctb;

        public MacroExecDialog(FastColoredTextBox tb)
        {
            InitializeComponent();
            foreach (
                var item in Directory.GetFiles(GlobalSettings.SettingsDir, "*.ynotemacro", SearchOption.AllDirectories))
            {
                var macroItem = new MacroItem(Path.GetFileNameWithoutExtension(item), item);
                cmbMacros.Items.Add(macroItem);
            }
            cmbMacros.SelectedIndex = 0;
            fctb = tb;
        }

        private void btnExec_Click(object sender, EventArgs e)
        {
            var macro = (cmbMacros.SelectedItem as MacroItem).File;
            var i = 0;
            while (i < times.Value)
            {
                fctb.MacrosManager.ExecuteMacros(macro);
                i++;
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}