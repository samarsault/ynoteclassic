using System;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
    public partial class GoToForm : Form
    {
        public GoToForm()
        {
            InitializeComponent();
        }

        public int SelectedLineNumber { get; set; }
        public int SelectedColumnNumber { get; set; }

        public int TotalLineCount { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            tbLineNumber.Text = SelectedLineNumber.ToString();

            label.Text = String.Format("Line number (1 - {0}):", TotalLineCount);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            tbLineNumber.Focus();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int enteredLine;
            if (int.TryParse(tbLineNumber.Text, out enteredLine))
            {
                enteredLine = Math.Min(enteredLine, TotalLineCount);
                enteredLine = Math.Max(1, enteredLine);
                SelectedLineNumber = enteredLine;
            }
            int columnNumber;
            if (int.TryParse(tbColumnNum.Text, out columnNumber))
            {
                columnNumber = Math.Min(columnNumber, TotalLineCount);
                columnNumber = Math.Max(1, columnNumber);
                SelectedColumnNumber = columnNumber;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}