#region

using System;
using System.Windows.Forms;

#endregion

namespace SS.Ynote.Classic.Features.Packages
{
    public partial class PluginManager : Form
    {
        public PluginManager()
        {
            InitializeComponent();
            PopulatePluginList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PopulatePluginList()
        {
            if (MainForm.Plugins == null) return;
            foreach (var plugin in MainForm.Plugins)
                listView1.Items.Add(new ListViewItem(new[] {plugin.Name, plugin.Version.ToString(), plugin.Description}));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Ynote Plugin Packages (*.ypk)|*.ypk";
                ofd.ShowDialog();
                if (ofd.FileName == "") return;
                var installer = new PluginInstaller(ofd.FileName) {StartPosition = FormStartPosition.CenterParent};
                installer.ShowDialog(this);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var packager = new PluginPacker {StartPosition = FormStartPosition.CenterParent};
            packager.ShowDialog(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var inp = new InputUrl())
            {
                inp.ShowDialog(this);
                if (inp.GeneratedPackage != null)
                    inp.GeneratedPackage.DownloadPackage();
            }
        }
    }
}