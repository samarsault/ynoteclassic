using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SS.Ynote.Classic.Features.Packages
{
    public partial class PackageManager : Form
    {
        public PackageManager()
        {
            InitializeComponent();
            PopulatePackageList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PopulatePackageList()
        {
            foreach (var file in Directory.GetFiles(SettingsBase.SettingsDir + "Packages"))
                listView1.Items.Add(new ListViewItem(new[] { Path.GetFileNameWithoutExtension(file), file }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Ynote Packages (*.ypk)|*.ypk";
                ofd.ShowDialog();
                if (string.IsNullOrEmpty(ofd.FileName)) return;
                var installer = new PackageInstaller(ofd.FileName) { StartPosition = FormStartPosition.CenterParent };
                installer.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var packager = new PackageMaker { StartPosition = FormStartPosition.CenterParent };
            packager.ShowDialog(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems[0] == null)
                return;
            var package = listView1.SelectedItems[0].SubItems[1].Text;
            YnotePackageManager.UninstallPackage(package);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Ynote Packages (*.ypk)|*.ypk";
                if (dlg.ShowDialog() != DialogResult.OK) return;
                var info = new ProcessStartInfo(Application.StartupPath + "\\pkmgr.exe")
                {
                    Verb = "runas",
                    Arguments = dlg.FileName
                };
                Process.Start(info);
            }
        }
        private void PackageManager_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start("http://ynoteclassic.codeplex.com/wikipage?title=Ynote%20Packages");
        }
    }
}