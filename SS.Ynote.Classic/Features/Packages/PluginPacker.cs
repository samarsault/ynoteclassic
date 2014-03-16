#region

using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

#endregion

namespace SS.Ynote.Classic.Features.Packages
{
    public partial class PluginPacker : Form
    {
        public PluginPacker()
        {
            InitializeComponent();
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pluginfilebtn_Click(object sender, EventArgs e)
        {
            using (var o = new OpenFileDialog())
            {
                o.Filter = "DLLs (*.dll)|*.dll";
                o.ShowDialog();
                if (o.FileName == null) return;
                txtpluginfile.Text = o.FileName;
            }
        }

        private void outfilebtn_Click(object sender, EventArgs e)
        {
            using (var s = new SaveFileDialog())
            {
                s.Filter = "Ynote Plugin Package (*.ypk)|*.ypk";
                s.ShowDialog();
                if (s.FileName == null) return;
                txtoutfile.Text = s.FileName;
            }
        }

        private void btnaddref_Click(object sender, EventArgs e)
        {
            using (var o = new OpenFileDialog())
            {
                o.Filter = "DLLs (*.dll)|*.dll";
                o.ShowDialog();
                if (o.FileName == null) return;
                lstrefs.Items.Add(o.FileName);
            }
        }

        private void createpackagebtn_Click(object sender, EventArgs e)
        {
            var packagemaker = new YnotePackageMaker();
            string[] refs = lstrefs.Items
                .Cast<object>()
                .Select(x => x.ToString())
                .ToArray();
#if DEBUG
            foreach (var str in refs)
                Debug.WriteLine(str);
#endif
            if (packagemaker.MakePackage(txtoutfile.Text, txtpluginfile.Text, refs))
                MessageBox.Show("Plugin Package Successfully Created !", "Plugin Packer", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }
    }
}