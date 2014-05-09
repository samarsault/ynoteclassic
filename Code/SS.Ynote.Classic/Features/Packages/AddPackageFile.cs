using System;
using System.Windows.Forms;

namespace SS.Ynote.Classic.Features.Packages
{
    internal partial class AddPackageFile : Form
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public AddPackageFile()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Input
        /// </summary>
        public string Input
        {
            get { return tbFile.Text; }
        }

        /// <summary>
        ///     Output
        /// </summary>
        public string Output
        {
            get { return tbOutput.Text; }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbFile.Text) || string.IsNullOrEmpty(tbOutput.Text))
            {
                MessageBox.Show("Error : One of the fields are left empty.\r\nPlease fill them to proceed.",
                    "Ynote Classic", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                var ok = dlg.ShowDialog() == DialogResult.OK;
                if (ok)
                    tbFile.Text = dlg.FileName;
            }
        }
    }
}