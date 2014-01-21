using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace SS.Ynote.Classic.Plugins
{
    public partial class PluginDownloader : Form
    {

        private readonly IList<DownloadItem> urls;
        private int index = 0;
        private WebClient client;

        /// <summary>
        /// Summary
        /// </summary>
        public PluginDownloader(List<DownloadItem> items)
        {
            InitializeComponent();
            urls = items;
            BeginDownloadFile(urls[index]);
        }
        public void BeginDownloadFile(DownloadItem item)
        {
            if (index != 0)
                index++;
            client = new WebClient();
            client.DownloadProgressChanged += client_ProgressChanged;
            client.DownloadFileCompleted += client_DownloadFileCompleted;
            var uri = new Uri(item.Url);
            label1.Text = "Downloading + " + Path.GetFileName(uri.LocalPath);
            urls.Remove(urls[index]);
            client.DownloadFileAsync(uri, urls[index].GetSaveDirFromType(Path.GetFileName(uri.LocalPath)));
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            lblprogress.Text = "Completed.";
            urls[index].SuccessfullyDownloaded = true;
            if (index + 1 == urls.Count)
            {
                BeginDownloadFile(urls[index]);
            }
            else
            {
                Show();
                InstallPlugin();
            }
        }

        void InstallPlugin()
        {
            label1.Text = "Installing..";
            foreach(var item in urls)
                if (item.SuccessfullyDownloaded)
                {
                    CopyDirectory(item.Directory, Application.StartupPath, true);
                }
            label1.Text = "Installed.";
            var result = MessageBox.Show("Restart for Plugins to take action?", "Plugin Manager",
                MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
                Application.Restart();
        }
        private static void CopyDirectory(string sourceDirName, string destDirName, bool copySubDirs)
        {
            var dir = new DirectoryInfo(sourceDirName);
            var dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
                throw new DirectoryNotFoundException(
                "Source directory does not exist or could not be found: "
                + sourceDirName);
            // If the destination directory does not exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }


            // Get the file contents of the directory to copy.
            var files = dir.GetFiles();

            foreach (var file in files)
            {
                // Create the path to the new copy of the file.
                var temppath = Path.Combine(destDirName, file.Name);

                // Copy the file.
                file.CopyTo(temppath, false);
            }

            // If copySubDirs is true, copy the subdirectories.
            if (copySubDirs)
            {

                foreach (DirectoryInfo subdir in dirs)
                {
                    // Create the subdirectory.
                    string temppath = Path.Combine(destDirName, subdir.Name);

                    // Copy the subdirectories.
                    CopyDirectory(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private void client_ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            lblprogress.Text = "{0}/{1} bytes downloaded" +
                               string.Format(e.BytesReceived.ToString(), e.TotalBytesToReceive.ToString());
            progressBar1.Value = e.ProgressPercentage;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            client.CancelAsync();
        }

        private void hidebutton_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
