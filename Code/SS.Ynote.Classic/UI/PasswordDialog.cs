using System;
using System.Windows.Forms;

namespace SS.Ynote.Classic.UI
{
    public partial class PasswordDialog : Form
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public PasswordDialog()
        {
            InitializeComponent();
        }

        public string Password
        {
            get { return tbPass.Text; }
        }

        /// <summary>
        ///     CbShowPassword
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbShowPass_CheckedChanged(object sender, EventArgs e)
        {
            tbPass.PasswordChar = cbShowPass.Checked ? '\0' : '●';
        }

        /// <summary>
        ///     OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            if (tbPass == null)
            {
                MessageBox.Show("Password Cannot Be Empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}