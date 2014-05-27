using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace SS.Ynote.Classic.Core.Search
{
    public partial class IncrementalSearcher : UserControl
    {
        private readonly Style _style;
        public FastColoredTextBox Tb;
        private bool _firstSearch = true;
        private Place _startPlace;

        public IncrementalSearcher()
        {
            InitializeComponent();
            _style = new MarkerStyle(new SolidBrush(Color.FromArgb(120, Color.Yellow)));
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
                var opt = cbMatchCase.Checked ? RegexOptions.None : RegexOptions.IgnoreCase;
                if (!cbRegex.Checked)
                    pattern = Regex.Escape(pattern);
                if (cbWholeWord.Checked)
                    pattern = "\\b" + pattern + "\\b";
                //
                var range = Tb.Selection.Clone();
                range.Normalize();
                //
                if (_firstSearch)
                {
                    _startPlace = range.Start;
                    _firstSearch = false;
                }
                //
                range.Start = range.End;
                range.End = range.Start >= _startPlace
                    ? new Place(Tb.GetLineLength(Tb.LinesCount - 1), Tb.LinesCount - 1)
                    : _startPlace;
                //

                HighlightAllMatches(Tb.Range, @pattern, !cbMatchCase.Checked);
                foreach (var r in range.GetRanges(pattern, opt))
                {
                    Tb.Selection = r;
                    Tb.DoSelectionVisible();
                    Tb.Invalidate();
                    HighlightAllMatches(Tb.Range, @pattern, !cbMatchCase.Checked);
                    return;
                }
                //
                if (range.Start >= _startPlace && _startPlace > Place.Empty)
                {
                    Tb.Selection.Start = new Place(0, 0);
                    FindNext(pattern);
                    return;
                }
                tbFind.BackColor = Color.LightCoral;
            }
            catch (Exception)
            {
                //pass;
            }
        }

        internal void FocusTextBox()
        {
            BeginInvoke((MethodInvoker) delegate
            {
                ResetSerach();
                tbFind.Focus();
                if (tbFind.Text != null)
                    tbFind.SelectAll();
            });
        }

        private void HighlightAllMatches(Range r, string pattern, bool ignorecase)
        {
            r.ClearStyle(_style);
            if (ignorecase)
                r.SetStyle(_style, pattern, RegexOptions.IgnoreCase);
            else
                r.SetStyle(_style, pattern);
        }

        private void ResetSerach()
        {
            _firstSearch = true;
        }

        private void cbMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            ResetSerach();
        }

        private void tbFind_TextChanged(object sender, EventArgs e)
        {
            BeginInvoke((MethodInvoker) delegate
            {
                ResetSerach();
                tbFind.BackColor = Color.White;
                FindNext(tbFind.Text);
            });
        }

        //private void FindForm_Load(object sender, EventArgs e)
        //{
        //    tbFind.Focus();
        //    ResetSerach();
        //}

        internal void Exit()
        {
            Tb.Range.ClearStyle(_style);
            Visible = false;
        }
    }

    /*
        /// <summary>
        ///     This style will drawing ellipse around of the word
        /// </summary>
        internal class HighlightMatchStyle : Style
        {
            private Color PenColor { get; set; }

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
    */
}