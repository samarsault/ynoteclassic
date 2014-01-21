using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace SS.Ynote.Classic.UI
{
    /// <summary>
    /// Insert Type
    /// </summary>
    public enum InsertType
    {
        Line,
        Column,
        Macro,
        Splitter,
        Width
    }
    /// <summary>
    /// Utility Dialog
    /// </summary>
    public partial class UtilDialog : Form
    {
        readonly FastColoredTextBox fctb;
        readonly InsertType it;
        /// <summary>
        /// Default Construtor
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="t"></param>
        public UtilDialog(FastColoredTextBox tb, InsertType t)
        {
            InitializeComponent();
            it = t;
            fctb = tb;
            Init(t);
        }

        void Init(InsertType t)
        {
            if (t == InsertType.Line)
            {
                Text = "Insert Lines";
                label1.Text = "Number of Lines To Insert :";
                button1.Text = "Insert";
                numericTextBox1.Visible = true;
                textBox1.Visible = false;
                numericTextBox1.Focus();
            }
            else if (t == InsertType.Column)
            {
                Text = "Insert Column";
                label1.Text = "No. of Columns to Insert :";
                button1.Text = "Insert";
                numericTextBox1.Visible = true;
                textBox1.Visible = false;
                numericTextBox1.Focus();
            }
            else if (t == InsertType.Macro)
            {
                Text = "Run Macro Multiple Times";
                label1.Text = "No. of Times to Run :";
                button1.Text = "Run";
                numericTextBox1.Visible = true;
                textBox1.Visible = false;
            }
            else if (t == InsertType.Splitter)
            {
                Text = "Split Settings";
                label1.Text = "Seperator";
                button1.Text = "Split";
                numericTextBox1.Visible = false;
                numericTextBox1.Text = fctb.PreferredLineWidth.ToString();
                textBox1.Visible = true;
                textBox1.Focus();
            }
            else if (t == InsertType.Width)
            {
                Text = "Line Width";
                label1.Text = "Width :";
                button1.Text = "Create";
                numericTextBox1.Visible = true;
                textBox1.Visible = false;
                numericTextBox1.Focus();
            }
        }
        public int Lines
        {
            get { return numericTextBox1.IntValue; }
            set { numericTextBox1.Text = value.ToString(); }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (it == InsertType.Line)
            {
                while (Lines > 0)
                {
                    fctb.InsertText(Environment.NewLine);
                    Lines--;
                }
            }
            else if (it == InsertType.Column)
            {
                while (Lines > 0)
                {
                    fctb.InsertText(" ");
                    Lines--;
                }
            }
            else if (it == InsertType.Macro)
            {
                while (Lines > 0)
                {
                    fctb.MacrosManager.ExecuteMacros();
                    Lines--;
                }
            }
            else if (it == InsertType.Splitter)
            {
                split(fctb.Selection.Start.iLine);
            }
            else if (it == InsertType.Width)
            {
                try
                {
                    fctb.PreferredLineWidth = numericTextBox1.IntValue;
                }
                catch
                {
                    
                }
            }
            Close();
        }
        void split(int iLine)
        {
            fctb.Selection.Start = new Place(0, iLine);
            fctb.Selection.Expand();
                string[] words = fctb.SelectedText.Split(textBox1.Text.ToCharArray());
                foreach (string word in words)
                {
                    fctb.InsertText(word + Environment.NewLine);
                }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
