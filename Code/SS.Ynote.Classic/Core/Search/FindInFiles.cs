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
        public string Directory { get; set; }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            var ynote = Globals.Ynote;
            var results = new SearchResults(ynote);
            results.Show(ynote.Panel, DockState.DockBottom);
            results.FindAll(tbdir.Text, cbRegex.Checked, cbCase.Checked, tbFind.Text, tbFilter.Text, cbsubdir.Checked);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var results = new SearchResults(Globals.Ynote);
            results.Show(Globals.Ynote.Panel, DockState.DockBottom);
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
        protected override void OnLoad(EventArgs e)
        {
            if (!string.IsNullOrEmpty(Directory))
                tbdir.Text = Directory;
            base.OnLoad(e);
        }
    }
}