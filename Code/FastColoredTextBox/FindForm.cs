using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace FastColoredTextBoxNS
{
    public partial class FindForm : Form
    {
        private enum FindNextDirection
        {
            Next,
            Previous
        }

        FastColoredTextBox tb;


        private bool hasPreviousFindResult = false;

        public FindForm(FastColoredTextBox tb)
        {
            InitializeComponent();
            this.tb = tb;
            this.tbFind.TextChanged += TbFindOnTextChanged;
        }

        private void TbFindOnTextChanged(object sender, EventArgs eventArgs)
        {
            this.hasPreviousFindResult = false;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btFindNext_Click(object sender, EventArgs e)
        {
            this.Find(tbFind.Text, FindNextDirection.Next);
        }

        private void btFindPrevious_Click(object sender, EventArgs e)
        {
            this.Find(tbFind.Text, FindNextDirection.Previous);
        }

        private Place NextPlace(Place p)
        {
            int lineLength = tb.GetLineLength(p.iLine);
            if (p.iChar < lineLength - 1)
            {
                return new Place(p.iChar+1, p.iLine);
            }
            else
            {
                // place is at last character of the line
                if (p.iLine < tb.LinesCount - 1)
                {
                    // move to next line
                    return new Place(0, p.iLine + 1);
                }
                else
                {
                    // already at last line, move to first line
                    return new Place(0,0);
                }
                
            }
        }

        private Place PrevPlace(Place p)
        {
            if (p.iChar == 0)
            {
                // move to previous line
                if (p.iLine == 0)
                {
                    // already at first line, move to the last character at last line
                    return new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1);
                }
                else
                {
                    return new Place(tb.GetLineLength(p.iLine - 1) - 1, p.iLine - 1);
                }
            }
            else
            {
                return new Place(p.iChar - 1, p.iLine);
            }
        }

        private Place EndOfLine(Place p)
        {
            return new Place(tb.GetLineLength(p.iLine) - 1, p.iLine);
        }
        private Place StartOfLine(Place p)
        {
            return new Place(0, p.iLine);
        }

        public void FindNext(string pattern)
        {
            this.Find(pattern, FindNextDirection.Next);
        }

        public void FindPrevious(string pattern)
        {
            this.Find(pattern, FindNextDirection.Previous);
        }

        private void Find(string pattern, FindNextDirection direction)
        {
            string originalPattern = pattern;
            Place start = new Place(0,0);
            Place endOfDocument = new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1);
            try
            {
                // create Regex
                RegexOptions opt = cbMatchCase.Checked ? RegexOptions.None : RegexOptions.IgnoreCase;
                if (!cbRegex.Checked)
                    pattern = Regex.Escape(pattern);
                if (cbWholeWord.Checked)
                    pattern = "\\b" + pattern + "\\b";
                // the current position
                Range range = tb.Selection.Clone();
                range.Normalize();

                // remember the start position
                start = new Place(range.Start.iChar, range.Start.iLine);

                if (direction == FindNextDirection.Next)
                {
                    // search till the end of the document
                    if (this.hasPreviousFindResult)
                    {
                        // increase range.Start with one position (if we don't do this will keep finding the same string)
                        range.Start = NextPlace(start);
                    }
                    else
                    {
                        range.Start = start;
                    }
                    range.End = endOfDocument; // search until end of document
                }
                else // find previous
                {
                    // search backwards till start of document
                    range.Start = new Place(0, 0);
                    range.End = start;
                }

                Place foundMatchPlace;
                bool foundMatch = TryFindNext(pattern, opt, direction, range, out foundMatchPlace);
                if (foundMatch)
                {
                    this.hasPreviousFindResult = true;
                    return;
                }


                // Searching forwarded and started at (0,0) => we have found nothing...
                if (direction == FindNextDirection.Next && start == new Place(0, 0))
                {
                    // Only show message when we don't have a previous find.
                    if (!this.hasPreviousFindResult) MessageBox.Show(String.Format("Pattern {0} not found.", originalPattern));
                    this.hasPreviousFindResult = false;
                    return;
                }
                // Searching backward and started at end of document => we have found nothing
                if (direction == FindNextDirection.Previous && start == endOfDocument)
                {
                    // Only show message when we don't have a previous find.
                    if (!this.hasPreviousFindResult) MessageBox.Show(String.Format("Pattern {0} not found.", originalPattern));
                    this.hasPreviousFindResult = false;
                    return;
                }

                // we haven't searched the entire document

                // Change the search range depending on whether we are searching for the next or previous
                if (direction == FindNextDirection.Next)
                {
                    // search from (0,0) to the line-end of start
                    range.Start = new Place(0, 0);
                    range.End = EndOfLine(start);
                }
                else // find previous
                {
                    // search from document-end to line-start of start
                    range.Start = StartOfLine(start);
                    range.End = endOfDocument; // search until end of document
                }

                Place foundMatchPlace2;
                bool foundMatch2 = TryFindNext(pattern, opt, direction, range, out foundMatchPlace2);
                if (foundMatch2)
                {
                    this.hasPreviousFindResult = true;
                    return;
                }

                
                // Found nothing
                // Only show message when we don't have a previous find.
                if (!this.hasPreviousFindResult) MessageBox.Show(String.Format("Pattern {0} not found.", originalPattern));
                this.hasPreviousFindResult = false;

            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Exception while searching");
            }
        }

        private bool TryFindNext(string pattern, RegexOptions opt, FindNextDirection direction, Range range, out Place foundMatchPlace)
        {
            if (direction == FindNextDirection.Next)
            {
                foreach (var r in range.GetRangesByLines(pattern, opt))
                {
                    foundMatchPlace = r.Start;
                    tb.Selection = r;
                    tb.DoSelectionVisible();
                    tb.Invalidate();
                    return true; // always return on the first match
                }
            }
            else // find previous
            {
                foreach (var r in range.GetRangesByLinesReversed(pattern, opt))
                {
                    foundMatchPlace = r.Start;
                    tb.Selection = r;
                    tb.DoSelectionVisible();
                    tb.Invalidate();
                    return true; // always return on the first match
                }
            }
            foundMatchPlace = Place.Empty;
            return false;
        }

        private void tbFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                btFindNext.PerformClick();
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '\x1b')
            {
                Hide();
                e.Handled = true;
                return;
            }
        }

        private void FindForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            this.tb.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            if (keyData == Keys.Enter)
            {
                btFindNext.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnActivated(EventArgs e)
        {
            tbFind.Focus();
            ResetSearch();
        }

        void ResetSearch()
        {
            hasPreviousFindResult = false;
        }

        private void cbMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            ResetSearch();
        }
        private void btFindAll_Click(object sender, EventArgs e)
        {
            string re = tbFind.Text;
            if (cbWholeWord.Checked)
                re = "\b" + re + "\b";
            tb.AddMultipleSelections(re, !cbMatchCase.Checked);
            Close();
        }
    }
}
  