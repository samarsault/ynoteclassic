using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.Core.Search
{
    public partial class FindInFiles : Form
    {
        public FindInFiles()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            var ynote = Application.OpenForms[1] as IYnote;
            if (ynote == null)
                ynote = Application.OpenForms[0] as IYnote;
            var results = new SearchResults(ynote);
            results.Show(ynote.Panel, DockState.DockBottom);
            results.FindAll(tbdir.Text, cbRegex.Checked, cbCase.Checked, tbFind.Text, tbFilter.Text, cbsubdir.Checked);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ynote = Application.OpenForms[1] as IYnote;
            if (ynote == null)
                ynote = Application.OpenForms[0] as IYnote;
            var results = new SearchResults(ynote);
            results.Show(ynote.Panel, DockState.DockBottom);
            results.ReplaceAll(tbdir.Text, tbFilter.Text, cbRegex.Checked, cbCase.Checked, tbFind.Text, tbReplace.Text,
                cbsubdir.Checked);
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var browserdlg = new FolderBrowserDialog())
            {
                var result = browserdlg.ShowDialog();
                if (result == DialogResult.OK)
                    tbdir.Text = browserdlg.SelectedPath;
            }
        }

        private void tbFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnFind_Click(null, e);
            }
            else if (e.KeyData == Keys.Escape)
            {
                Close();
            }
        }
    }
}