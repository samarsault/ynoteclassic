using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace SS.Ynote.Classic.Extensibility.Packages
{
    public partial class PackageDownloader : Form
    {
        private readonly string _file;
        private readonly string localFile;
        private WebClient client;

        public PackageDownloader(string file, string outfile)
        {
            InitializeComponent();
            _file = file;
            localFile = outfile;
        }

        private void BeginDownload()
        {
            client = new WebClient();
            client.DownloadProgressChanged += client_DownloadProgressChanged;
            client.DownloadFileCompleted += client_DownloadFileCompleted;
            client.DownloadFileAsync(new Uri(_file), localFile);
        }

        private void UpdateProgress(int i)
        {
            progressBar1.Value = i;
        }

        private void InstallPackage(string file)
        {
            // var installer = new PackageInstaller(file);
            // installer.ShowDialog(this);
            var info = new ProcessStartInfo(Application.StartupPath + @"\pkmgr.exe",file);
            info.Verb = "runas";
            Process.Start(info);
            DialogResult result=MessageBox.Show("Package Successfully Installed. Restart now to make changes ?","",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
                Application.Restart();
            Close();
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            UpdateProgress(e.ProgressPercentage);
            lblbytes.Text = string.Format("{0} / {1} bytes completed", e.BytesReceived, e.TotalBytesToReceive);
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
                Close();
            else
                InstallPackage(localFile);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client.CancelAsync();
        }

        protected override void OnShown(EventArgs e)
        {
            BeginDownload();
            base.OnShown(e);
        }
    }
}