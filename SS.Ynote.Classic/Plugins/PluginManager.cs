using System.Collections;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Windows.Forms;

namespace SS.Ynote.Classic.Plugins
{
    public partial class PluginManager : Form
    {
        public PluginManager()
        {
            InitializeComponent();
            lstplugins.FullRowSelect = true;
            StartPosition = FormStartPosition.CenterScreen;
            LoadPluginList();
        }
        #region Old Junk
        static void UninstallPlugin()
        {
            /*
            using (OpenFileDialog o = new OpenFileDialog())
            {
                o.Filter = "Ynote Plugin Definitions (*.ynoteplugin)|*.ynoteplugin";
                o.ShowDialog();
                if (!string.IsNullOrEmpty(o.FileName))
                {
                    var Reader = new PluginDefReader(o.FileName);
                    if (Reader.UninstallPlugin())
                    {
                        DialogResult result = MessageBox.Show("Plugin Successfully Uninstalled. Plugin will take effect after restart. Restarting now", "Ynote Classic", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Restart();
                    }
                    else
                    {
                        MessageBox.Show("Error Uninstalling Plugin!", "Ynote Classic", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            */
        }
       static void ImportPlugin()
        {
           /*
            using (OpenFileDialog o = new OpenFileDialog())
            {
                o.Filter = "Ynote Plugin Definitions (*.ynoteplugin)|*.ynoteplugin";
                o.ShowDialog();
                if (!string.IsNullOrEmpty(o.FileName))
                {
                    var Reader = new PluginDefReader(o.FileName);
                    if (Reader.InstallPlugin())
                    {
                        DialogResult result = MessageBox.Show("Plugin Successfully Installed. Plugin will take effect after restart. Restart Now?", "Ynote Classic", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                            Application.Restart();
                    }
                    else
                    {
                        MessageBox.Show("Error Installing Plugin!", "Ynote Classic", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
           */
        } 

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://ynoteplugins.codeplex.com");
        }
        private IYnotePlugin[] GetYnotePlugins()
        {
            var dircatalog = new DirectoryCatalog(Application.StartupPath + "\\Plugins");
            var container = new CompositionContainer(dircatalog);
            return container.GetExportedValues<IYnotePlugin>().ToArray();
        }
        private IFileTypePlugin[] GetFileTypePlugins()
        {
            var dircatalog = new DirectoryCatalog(Application.StartupPath + "\\Plugins");
            var container = new CompositionContainer(dircatalog);
            return container.GetExportedValues<IFileTypePlugin>().ToArray();
        }
        void LoadPluginList() 
        {
        }

        void PopulatePluginList()
        {
            var channel = new PluginChannel();
            foreach (var plugin in channel.YnotePlugins)
            {
                var item = new ListViewItem(new[] { plugin.Name, plugin.Version.ToString() });
                item.Tag = plugin;
            }
        }
        private void button1_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            Close();
        }
        #endregion

        private void button2_Click(object sender, System.EventArgs e)
        {
            foreach (ListViewItem item in lstremoteplugins.CheckedItems)
            {
                var plugin = (PluginItem) (item.Tag);
                var dat = new YnotePluginData(plugin.DownloadLink);
            }
        }


    }
}
