using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace SUP.Tester
{
    public partial class UpdateWindow : Form
    {
        private readonly Update _update;
        private int currentdownloadindex;

        public UpdateWindow(Update update)
        {
            InitializeComponent();
            _update = update;
            updateInfolbl.Text = update.Name;
            TerminateYnoteProcess();
            PopulateUpdateFiles();
        }

        private void TerminateYnoteProcess()
        {
            foreach (var process in Process.GetProcesses())
            {
                if (process.ProcessName == "SS.Ynote.Classic.exe")
                    process.Kill();
            }
        }

        private void PopulateUpdateFiles()
        {
            foreach (UpdateFile file in _update.ListedUpdateFiles)
            {
                var item = new ListViewItem(new[] {file.Name, file.VersionID.ToString()}) {Tag = file};
                listView1.Items.Add(item);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
                item.Checked = true;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
                item.Checked = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_update.ChangeLog == null)
                MessageBox.Show("This update has no ChangeLog");
            else
                new ChangeLogViewer(_update.ChangeLog).ShowDialog();
        }

        private void StartDownload()
        {
            labeldowninfo.Visible = true;
            progress.Visible = true;
            var webclient = new WebClient();
            webclient.DownloadProgressChanged += webclient_DownloadProgressChanged;
            webclient.DownloadFileCompleted += webclient_DownloadFileCompleted;
            UpdateFile file = _update.ListedUpdateFiles[currentdownloadindex];
            webclient.DownloadFileAsync(new Uri(file.URL), file.OutPath);
        }

        private void webclient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progress.Value = e.ProgressPercentage;
            labeldowninfo.Text = e.ProgressPercentage + " % " + e.BytesReceived + " byte(s) / " + e.TotalBytesToReceive +
                                 " byte(s)";
        }

        private void webclient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            currentdownloadindex++;
            if (currentdownloadindex != _update.ListedUpdateFiles.Count - 1)
                StartDownload();
            else
            {
                MessageBox.Show("Download Complete!");
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartDownload();
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}