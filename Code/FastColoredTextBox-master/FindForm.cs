using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
    public partial class FindForm : Form
    {
        private bool _firstSearch = true;
        private Place _startPlace;
        private readonly FastColoredTextBox tb;

     /// <summary>
        ///     Find Form
        /// </summary>
        /// <param name="tb"></param>)
        public FindForm(FastColoredTextBox tb)
        {
            InitializeComponent();
            this.tb = tb;
            cmbFindIn.SelectedIndex = 0;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btFindNext_Click(object sender, EventArgs e)
        {
           if (cmbFindIn.SelectedIndex == 0)
               FindNext(tbFind.Text);
           else
               FindNextInSelection(tbFind.Text);
        }

        /// <summary>
        /// <summary>
        ///     Find Next
        /// </summary>
        /// <param name="pattern"></param>id FindNext(string pattern)
        public void FindNext(string pattern)
        {
            try
            {
                var opt = cbMatchCase.Checked ? RegexOptions.None : RegexOptions.IgnoreCase;
                if (!cbRegex.Checked)
                    pattern = Regex.Escape(pattern);
                if (cbWholeWord.Checked)
                    pattern = "\\b" + pattern + "\\b";
                //
                var range = tb.Selection.Clone();
                range.Normalize();
                //
                if (_firstSearch)
                {
                    _startPlace = range.Start;
                    _firstSearch = false;
                }
                //
                range.Start = range.End;
                range.End = range.Start >= _startPlace ? new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1) : _startPlace;
                //
                foreach (var r in range.GetRanges(pattern, opt))
                {
                    tb.Selection = r;
                    tb.DoSelectionVisible();
                    tb.Invalidate();
                    return;
                }
                //
                if (range.Start >= _startPlace && _startPlace > Place.Empty)
                {
                    tb.Selection.Start = new Place(0, 0);
                    FindNext(pattern);
                    return;
                }
                MessageBox.Show("Not found");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            }
        }

        private void FindForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            tb.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnActivated(EventArgs e)
        {
            tbFind.Focus();
            ResetSerach();
        }

        private void ResetSerach()
        {
            _firstSearch = true;
        }

        private void cbMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            ResetSerach();
        }

        private Range _originalSelection;

        private void FindNextInSelection(string pattern)
        {
            try
            {
                var opt = cbMatchCase.Checked ? RegexOptions.None : RegexOptions.IgnoreCase;
                if (!cbRegex.Checked)
                    pattern = Regex.Escape(pattern);
                if (cbWholeWord.Checked)
                    pattern = "\\b" + pattern + "\\b";
                //
                var range = tb.Selection;
                //
                if (_firstSearch)
                {
                    _originalSelection = range;
                    _startPlace = range.Start;
                    _firstSearch = false;
                }
                else
                {
                    range.Start = range.End;
                    range.End = range.Start >= _startPlace ? new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1) : _startPlace;
                }
                foreach (var r in range.GetRanges(pattern, opt))
                {
                    tb.Selection = r;
                    tb.DoSelectionVisible();
                    tb.Invalidate();
                    return;
                }
                //
                if (range.Start >= _startPlace && _startPlace > Place.Empty)
                {
                    tb.Selection = _originalSelection;
                    FindNextInSelection(pattern);
                    return;
                }
                MessageBox.Show("Not found");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private List<Range> FindAll(string pattern)
        {
            var opt = cbMatchCase.Checked ? RegexOptions.None : RegexOptions.IgnoreCase;
            if (!cbRegex.Checked)
                pattern = Regex.Escape(pattern);
            if (cbWholeWord.Checked)
                pattern = "\\b" + pattern + "\\b";
            //
            var range = tb.Selection.IsEmpty ? tb.Range.Clone() : tb.Selection.Clone();
            //
            var list = new List<Range>();
            foreach (var r in range.GetRanges(pattern, opt))
                list.Add(r);

            return list;
        }
        private void btHighlightAll_Click(object sender, EventArgs e)
        {
            string pattern = tbFind.Text;
            MessageBox.Show(FindAll(pattern).Count + " Occurrence(s) Found.");

        }
    }
}