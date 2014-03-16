#region

using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;

#endregion

namespace SS.Ynote.Classic.Features.Packages
{
    // TODO : Check if working
    /// <summary>
    /// Plugin Downloader
    /// </summary>
    public partial class Downloader : Form
    {
        private readonly string _url;
        private string _downloadedto;
        private WebClient client;

        public Downloader(string url)
        {
            InitializeComponent();
            _url = url;
        }

        public void DownloadPlugin()
        {
            client = new WebClient();
            _downloadedto = Path.GetTempFileName() + ".ypk";
            client.DownloadProgressChanged += client_DownloadProgressChanged;
            client.DownloadFileCompleted += client_DownloadFileCompleted;
            client.DownloadFileAsync(new Uri(_url), _downloadedto);
            ShowDialog();
        }
        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled) return;
            Close();
            var package = new PluginInstaller(_downloadedto) {StartPosition = FormStartPosition.CenterParent};
            package.ShowDialog(this);
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label2.Text = string.Format("{0} bytes / {1} bytes Completed", e.BytesReceived, e.TotalBytesToReceive);
            label1.Text = "Downloading From : " + _url;
            Text = "Downloding Package : " + e.ProgressPercentage + " % Completed";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            client.CancelAsync();
        }
    }
}