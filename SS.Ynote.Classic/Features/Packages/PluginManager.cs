#region

using System;
using System.Net.NetworkInformation;
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
            var t = new Timer {Interval = 100};
            t.Tick += (sender, args) => PopulateOnlineList(t);
            t.Start();
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

        private void PopulateOnlineList(object sender)
        {
            //TODO:Update Online Package List to On Server Url
            if (NetworkInterface.GetIsNetworkAvailable())
                foreach (var package in PluginChannel.GetOnlinePackages(@"C:\Users\Lenovo\Desktop\Packages.xml"))
                    lstonlinepacks.Items.Add(new ListViewItem(new[] {package.Name, package.Version, package.Description}, 0) {Tag = package});
            if (sender.GetType() == typeof (Timer))
                ((Timer) (sender)).Stop();
            lbldownload.Hide();
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
            foreach (ListViewItem item in lstonlinepacks.CheckedItems)
            {
                var package = item.Tag as YnoteOnlinePackage;
                if (package != null) package.DownloadPackage();
            }
        }
    }
}