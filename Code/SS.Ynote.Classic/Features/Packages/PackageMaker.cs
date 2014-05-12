using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace SS.Ynote.Classic.Features.Packages
{
    public partial class PackageMaker : Form
    {
        public PackageMaker()
        {
            InitializeComponent();
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void outfilebtn_Click(object sender, EventArgs e)
        {
            using (var s = new SaveFileDialog())
            {
                s.Filter = "Ynote Package File (*.ypk)|*.ypk";
                s.ShowDialog();
                if (s.FileName == null) return;
                txtoutfile.Text = s.FileName;
            }
        }

        private void createpackagebtn_Click(object sender, EventArgs e)
        {
            var dic = lstfiles.Items.Cast<ListViewItem>()
                .ToDictionary(item => item.SubItems[0].Text, item => item.SubItems[1].Text);
            //  foreach(ListViewItem item in lstfiles.Items)
            //      dic.Add(item.SubItems[0].Text, item.SubItems[1].Text);
            if (!YnotePackageMaker.MakePackage(txtoutfile.Text, dic)) return;
            MessageBox.Show("Package Successfully Created !", "Plugin Packer", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lstfiles.Items.Remove(lstfiles.SelectedItems[0]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var dlg = new AddPackageFile())
            {
                var show = dlg.ShowDialog();
                if (show == DialogResult.OK)
                    lstfiles.Items.Add(new ListViewItem(new[] { dlg.Input, dlg.Output }));
            }
        }

        private void PackageMaker_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start("https://ynoteclassic.codeplex.com/wikipage?title=Ynote Packages");
        }
    }
}