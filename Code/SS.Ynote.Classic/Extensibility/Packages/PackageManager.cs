using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
using SS.Ynote.Classic.Core.Settings;

namespace SS.Ynote.Classic.Extensibility.Packages
{
    public partial class PackageManager : Form
    {
        private string[] packages;

        public PackageManager()
        {
            InitializeComponent();
            PopulatePackageList();
        }

        private void LoadPackages(string url)
        {
            var client = new WebClient();
            client.DownloadStringCompleted += (sender, args) => LoadInfo(args.Result);
            client.DownloadStringAsync(new Uri(url));
        }

        private void LoadInfo(string json)
        {
            lbld.Hide();
            packages = JsonConvert.DeserializeObject<string[]>(json);
            if (packages.Length == 0)
                return;
            foreach (var pack in packages)
                AddItem(pack);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PopulatePackageList()
        {
            foreach (var file in Directory.GetFiles(GlobalSettings.SettingsDir + "Packages", "*.ynotepackage"))
                lstPackages.Items.Add(new ListViewItem(new[] {Path.GetFileNameWithoutExtension(file), file}));
        }

        private void AddItem(string arg)
        {
            string[] args = arg.Split(',');
            if (args.Length < 3)
            {
                MessageBox.Show("Invalid Package Metadata!");
                return;
            }
            lstwebpackages.Items.Add(new ListViewItem(new[] {args[0], args[1]}) {Tag = args[2]});
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Ynote Packages (*.ynotepackage)|*.ynotepackage";
                ofd.ShowDialog();
                if (string.IsNullOrEmpty(ofd.FileName)) return;
                var installer = new PackageInstaller(ofd.FileName) {StartPosition = FormStartPosition.CenterParent};
                installer.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var packager = new PackageMaker {StartPosition = FormStartPosition.CenterParent};
            packager.ShowDialog(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (lstPackages.SelectedItems[0] == null)
                return;
            var package = lstPackages.SelectedItems[0].SubItems[1].Text;
            YnotePackageManager.UninstallPackage(package);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Ynote Packages (*.ynotepackage)|*.ynotepackage";
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

        protected override void OnShown(EventArgs e)
        {
            LoadPackages("https://raw.githubusercontent.com/samarjeet27/ynotepackages/master/packages.json");
            base.OnShown(e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var item = lstwebpackages.SelectedItems[0];
            string url = (string) item.Tag;

            var result = MessageBox.Show("Are you sure you want to download " + item.Text + " Package ?", "",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                var downloader = new PackageDownloader(url,
                    Path.Combine(Path.GetTempPath(), item.Text + ".ynotepackage"));
                downloader.ShowDialog(this);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}