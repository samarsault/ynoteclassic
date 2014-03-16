#region

using System;
using System.Windows.Forms;

#endregion

namespace SS.Ynote.Classic.Features.Packages
{
    public partial class PluginInstaller : Form
    {
        private readonly string _package;
        private readonly Timer _time;

        public PluginInstaller(string package)
        {
            InitializeComponent();
            _time = new Timer {Interval = 10};
            _time.Tick += _time_Tick;
            _package = package;
            _time.Start();
        }

        private void _time_Tick(object sender, EventArgs e)
        {
            _time.Stop();
            var loader = new YnotePackageLoader();
            if (loader.InstallPackage(_package))
            {
                progressBar1.Value = 100;
                var result = MessageBox.Show("Plugin Installed Successfully ? Restart now to make changes ?",
                    "Plugin Installer",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    Application.Restart();
            }
            else
                MessageBox.Show("There was an Error Installing the Plugin");
            Close();
        }
    }
}