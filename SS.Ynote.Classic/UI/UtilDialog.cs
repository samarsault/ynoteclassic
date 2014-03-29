#region

using FastColoredTextBoxNS;
using System;
using System.Windows.Forms;

#endregion

namespace SS.Ynote.Classic.UI
{
    /// <summary>
    ///     Insert Type
    /// </summary>
    public enum InsertType
    {
        Line,
        Column,
        Splitter,
        Width
    }

    /// <summary>
    ///     Utility Dialog
    /// </summary>
    public partial class UtilDialog : Form
    {
        private readonly FastColoredTextBox _fctb;
        private readonly InsertType _it;

        /// <summary>
        ///     Default Construtor
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="t"></param>
        public UtilDialog(FastColoredTextBox tb, InsertType t)
        {
            InitializeComponent();
            _it = t;
            _fctb = tb;
            Init(t);
            textBox1.KeyDown += ProcessKeyDown;
            numericTextBox1.KeyDown += ProcessKeyDown;
        }

        private int Lines
        {
            get { return numericTextBox1.IntValue; }
            set { numericTextBox1.Text = value.ToString(); }
        }

        private void ProcessKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter &&
                ((TextBox)(sender)).Text != null)
                Process();
        }

        private void Init(InsertType t)
        {
            switch (t)
            {
                case InsertType.Line:
                    Text = "Insert Lines";
                    label1.Text = "Number of Lines To Insert :";
                    button1.Text = "Insert";
                    numericTextBox1.Visible = true;
                    textBox1.Visible = false;
                    numericTextBox1.Focus();
                    break;

                case InsertType.Column:
                    Text = "Insert Column";
                    label1.Text = "No. of Columns to Insert :";
                    button1.Text = "Insert";
                    numericTextBox1.Visible = true;
                    textBox1.Visible = false;
                    numericTextBox1.Focus();
                    break;

                case InsertType.Splitter:
                    Text = "Split Settings";
                    label1.Text = "Seperator";
                    button1.Text = "Split";
                    numericTextBox1.Visible = false;
                    numericTextBox1.Text = _fctb.PreferredLineWidth.ToString();
                    textBox1.Visible = true;
                    textBox1.Focus();
                    break;

                case InsertType.Width:
                    Text = "Line Width";
                    label1.Text = "Width :";
                    button1.Text = "Create";
                    numericTextBox1.Visible = true;
                    textBox1.Visible = false;
                    numericTextBox1.Focus();
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process();
        }

        private void Process()
        {
            if (_it == InsertType.Line)
            {
                while (Lines > 0)
                {
                    _fctb.InsertText(Environment.NewLine);
                    Lines--;
                }
            }
            else if (_it == InsertType.Column)
            {
                while (Lines > 0)
                {
                    _fctb.InsertText(" ");
                    Lines--;
                }
            }
            else if (_it == InsertType.Splitter)
            {
                split(_fctb.Selection.Start.iLine);
            }
            else if (_it == InsertType.Width)
            {
                try
                {
                    _fctb.PreferredLineWidth = numericTextBox1.IntValue;
                }
                catch
                {
                }
            }
            Close();
        }

        private void split(int iLine)
        {
            _fctb.Selection.Start = new Place(0, iLine);
            _fctb.Selection.Expand();
            string[] words = _fctb.SelectedText.Split(textBox1.Text.ToCharArray());
            foreach (string word in words)
            {
                _fctb.InsertText(word + Environment.NewLine);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}