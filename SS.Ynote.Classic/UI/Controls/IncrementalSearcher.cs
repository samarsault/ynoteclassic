using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace SS.Ynote.Classic
{
    public partial class IncrementalSearcher : UserControl
    {
        private readonly Style style;
        public FastColoredTextBox tb;
        private bool firstSearch = true;
        private Place startPlace;
        
        public IncrementalSearcher()
        {
            InitializeComponent();
            style = new TextStyle(Brushes.Red, Brushes.Yellow, FontStyle.Regular);
            tbFind.Focus();
        }


        private void tbFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                FindNext(tbFind.Text);
            else if (e.KeyCode == Keys.Escape)
                Exit();
        }

        private void FindNext(string pattern)
        {
            try
            {
                tbFind.BackColor = Color.White;
                RegexOptions opt = cbMatchCase.Checked ? RegexOptions.None : RegexOptions.IgnoreCase;
                if (!cbRegex.Checked)
                    pattern = Regex.Escape(pattern);
                if (cbWholeWord.Checked)
                    pattern = "\\b" + pattern + "\\b";
                //
                var range = tb.Selection.Clone();
                range.Normalize();
                //
                if (firstSearch)
                {
                    startPlace = range.Start;
                    firstSearch = false;
                }
                //
                range.Start = range.End;
                if (range.Start >= startPlace)
                    range.End = new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1);
                else
                    range.End = startPlace;
                //
                foreach (Range r in range.GetRanges(pattern, opt))
                {
                    tb.Selection = r;
                    tb.DoSelectionVisible();
                    tb.Invalidate();
                    if (highlightall.Checked | cbMatchCase.Checked)
                        HighlightAllMatches(tb.Range, @pattern, false);
                    else if (highlightall.Checked)
                        HighlightAllMatches(tb.Range, @pattern, true);
                    return;
                }
                //
                if (range.Start >= startPlace && startPlace > Place.Empty)
                {
                    tb.Selection.Start = new Place(0, 0);
                    FindNext(pattern);
                    return;
                }
                tbFind.BackColor = Color.LightCoral;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HighlightAllMatches(Range r, string pattern, bool ignorecase)
        {
            r.ClearStyle(style);
            if (ignorecase)
                r.SetStyle(style, pattern, RegexOptions.IgnoreCase);
            else
                r.SetStyle(style, pattern);
        }

        private void tbFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                FindNext(tbFind.Text);

                e.Handled = true;
                return;
            }
            if (e.KeyChar == '\x1b')
            {
                Hide();
                e.Handled = true;
            }
        }

        private void ResetSerach()
        {
            firstSearch = true;
        }

        private void cbMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            ResetSerach();
        }

        private void tbFind_TextChanged(object sender, EventArgs e)
        {
            FindNext(tbFind.Text);
        }
      //private void FindForm_Load(object sender, EventArgs e)
      //{
      //    tbFind.Focus();
      //    ResetSerach();
      //}

        private void Exit()
        {
            tb.Range.ClearStyle(style);
            Visible = false;
        }

        private void highlightall_CheckedChanged(object sender, EventArgs e)
        {
            if (highlightall.Checked)
                HighlightAllMatches(tb.Range, tbFind.Text, !cbMatchCase.Checked);
        }
    }

    /// <summary>
    ///     This style will drawing ellipse around of the word
    /// </summary>
    internal class HighlightMatchStyle : Style
    {
        public Color PenColor { get; set; }

        public override void Draw(Graphics gr, Point position, Range range)
        {
            //get size of rectangle
            Size size = GetSizeOfRange(range);
            //create rectangle
            var rect = new Rectangle(position, size);
            //inflate it
            rect.Inflate(2, 2);
            //get rounded rectangle
            GraphicsPath path = GetRoundedRectangle(rect, 7);
            //draw rounded rectangle
            gr.DrawPath(new Pen(PenColor), path);
        }
    }
}