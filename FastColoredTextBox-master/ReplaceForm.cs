using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace FastColoredTextBoxNS
{
    public partial class ReplaceForm : Form
    {
        readonly FastColoredTextBox tb;
        bool firstSearch = true;
        Place startPlace;

        public ReplaceForm(FastColoredTextBox tb)
        {
            InitializeComponent();
            this.tb = tb;
            cmbScope.SelectedIndex = 0;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btFindNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbScope.SelectedIndex == 0)
                {
                    if (!Find(tbFind.Text))
                        MessageBox.Show("Not found");
                }
                else if (cmbScope.SelectedIndex == 1)
                {
                    FindNextInSelection(tbFind.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private List<Range> FindAllInSelection(string pattern)
        {
            var opt = cbMatchCase.Checked ? RegexOptions.None : RegexOptions.IgnoreCase;
            if (!cbRegex.Checked)
                pattern = Regex.Escape(pattern);
            if (cbWholeWord.Checked)
                pattern = "\\b" + pattern + "\\b";
            var range = tb.Selection;
            range.Normalize();
            range.Start = range.End;
            range.End = new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1);
            //
            var list = new List<Range>();
            foreach (var r in range.GetRanges(pattern, opt))
                list.Add(r);
            return list;
        } 
        private List<Range> FindAll(string pattern)
        {
            RegexOptions opt = cbMatchCase.Checked ? RegexOptions.None : RegexOptions.IgnoreCase;
            if (!cbRegex.Checked)
                pattern = Regex.Escape(pattern);
            if (cbWholeWord.Checked)
                pattern = "\\b" + pattern + "\\b";
            //
            var range = tb.Selection.Clone();
            range.Normalize();
            range.Start = range.End;
            range.End = new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1);
            //
            var list = new List<Range>();
            foreach (var r in range.GetRangesByLines(pattern, opt))
                list.Add(r);

            return list;
        }

        private bool Find(string pattern)
        {
            RegexOptions opt = cbMatchCase.Checked ? RegexOptions.None : RegexOptions.IgnoreCase;
            if (!cbRegex.Checked)
                pattern = Regex.Escape(pattern);
            if (cbWholeWord.Checked)
                pattern = "\\b" + pattern + "\\b";
            //
            Range range = tb.Selection.Clone();
            range.Normalize();
            //
            if (firstSearch)
            {
                startPlace = range.Start;
                firstSearch = false;
            }
            //
            range.Start = range.End;
            range.End = range.Start >= startPlace ? new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1) : startPlace;
            //
            foreach (var r in range.GetRangesByLines(pattern, opt))
            {
                tb.Selection.Start = r.Start;
                tb.Selection.End = r.End;
                tb.DoSelectionVisible();
                tb.Invalidate();
                return true;
            }
            if (range.Start >= startPlace && startPlace > Place.Empty)
            {
                tb.Selection.Start = new Place(0, 0);
                return Find(pattern);
            }
            return false;
        }

        private void tbFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                btFindNext_Click(sender, null);
            if (e.KeyChar == '\x1b')
                Hide();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) // David
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ReplaceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            tb.Focus();
        }

        private void btReplace_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb.SelectionLength != 0)
                if (!tb.Selection.ReadOnly)
                    tb.InsertText(tbReplace.Text);
                btFindNext_Click(sender, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void ReplaceAllInSelection()
        {
            try
            {
                //search
                var ranges = FindAllInSelection(tbFind.Text);
                //check readonly
                var ro = false;
                foreach (var r in ranges)
                    if (r.ReadOnly)
                    {
                        ro = true;
                        break;
                    }
                //replace
                if (!ro)
                    if (ranges.Count > 0)
                    {
                        tb.TextSource.Manager.ExecuteCommand(new ReplaceTextCommand(tb.TextSource, ranges, tbReplace.Text));
                        tb.Selection.Start = new Place(0, 0);
                    }
                //
                tb.Invalidate();
                MessageBox.Show(ranges.Count + " occurrence(s) replaced");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            tb.Selection.EndUpdate();
        }
        private void btReplaceAll_Click(object sender, EventArgs e)
        {
            if(cmbScope.SelectedIndex == 0)
                ReplaceAll();
            else if(cmbScope.SelectedIndex == 1)
                ReplaceAllInSelection();
        }

        private void ReplaceAll()
        {
            try
            {
                tb.Selection.BeginUpdate();
                tb.Selection.Start = new Place(0, 0);
                //search
                var ranges = FindAll(tbFind.Text);
                //check readonly
                var ro = false;
                foreach (var r in ranges)
                    if (r.ReadOnly)
                    {
                        ro = true;
                        break;
                    }
                //replace
                if (!ro)
                    if (ranges.Count > 0)
                    {
                        tb.TextSource.Manager.ExecuteCommand(new ReplaceTextCommand(tb.TextSource, ranges, tbReplace.Text));
                        tb.Selection.Start = new Place(0, 0);
                    }
                //
                tb.Invalidate();
                MessageBox.Show(ranges.Count + " occurrence(s) replaced");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            tb.Selection.EndUpdate();
        }

        private Range _originalSelection;
        /// <summary>
        /// FindNext In Selection
        /// </summary>
        /// <param name="pattern"></param>
        private void FindNextInSelection(string pattern)
        {
            try
            {
                RegexOptions opt = cbMatchCase.Checked ? RegexOptions.None : RegexOptions.IgnoreCase;
                if (!cbRegex.Checked)
                    pattern = Regex.Escape(pattern);
                if (cbWholeWord.Checked)
                    pattern = "\\b" + pattern + "\\b";
                //
                var range = tb.Selection;
                //
                if (firstSearch)
                {
                    _originalSelection = range;
                    startPlace = range.Start;
                    firstSearch = false;
                }
                else
                {
                    range.Start = range.End;
                    range.End = range.Start >= startPlace ? new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1) : startPlace;
                }
                foreach (var r in range.GetRanges(pattern, opt))
                {
                    tb.Selection = r;
                    tb.DoSelectionVisible();
                    tb.Invalidate();
                    return;
                }
                //
                if (range.Start >= startPlace && startPlace > Place.Empty)
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
        protected override void OnActivated(EventArgs e)
        {
            tbFind.Focus();
            ResetSerach();
        }

        void ResetSerach()
        {
            firstSearch = true;
        }

        private void cbMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            ResetSerach();
        }
    }
}
