using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using SS.Ynote.Classic.Features.Syntax;
using SS.Ynote.Classic.UI.DiffMergeStuffs;
using WeifenLuo.WinFormsUI.Docking;
using Line = SS.Ynote.Classic.UI.DiffMergeStuffs.Line;

namespace SS.Ynote.Classic.UI
{
    public partial class Diff : DockContent
    {
        private readonly Style _greenStyle;
        private readonly SyntaxHighlighter _highlighter;
        private readonly Style _redStyle;
        private int _updating;

        public Diff()
        {
            InitializeComponent();
            _greenStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Lime)));
            _redStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Red)));
            _highlighter = new SyntaxHighlighter();
            YnoteThemeReader.ApplyTheme(Settings.SettingsDir + @"\Themes\Default.ynotetheme", _highlighter, fctb1);
            YnoteThemeReader.ApplyTheme(Settings.SettingsDir + @"\Themes\Default.ynotetheme", _highlighter, fctb2);
        }

        public Diff(string firstfile, string secondfile)
        {
            InitializeComponent();
            _greenStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Lime)));
            _redStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Red)));
            tbFirstFile.Text = firstfile;
            tbSecondFile.Text = secondfile;
            _highlighter = new SyntaxHighlighter();
            YnoteThemeReader.ApplyTheme(Settings.SettingsDir + @"\Themes\Default.ynotetheme", _highlighter, fctb1);
            YnoteThemeReader.ApplyTheme(Settings.SettingsDir + @"\Themes\Default.ynotetheme", _highlighter, fctb2);
        }

        private void btSecond_Click(object sender, EventArgs e)
        {
            if (ofdFile.ShowDialog() == DialogResult.OK)
                tbSecondFile.Text = ofdFile.FileName;
        }

        private void btFirst_Click(object sender, EventArgs e)
        {
            if (ofdFile.ShowDialog() == DialogResult.OK)
                tbFirstFile.Text = ofdFile.FileName;
        }

        private void tb_VisibleRangeChanged(object sender, EventArgs e)
        {
            if (_updating > 0)
                return;

            var fastColoredTextBox = sender as FastColoredTextBox;
            if (fastColoredTextBox != null)
            {
                var vPos = fastColoredTextBox.VerticalScroll.Value;
                var curLine = fastColoredTextBox.Selection.Start.iLine;

                UpdateScroll(sender == fctb2 ? fctb1 : fctb2, vPos, curLine);
            }

            fctb1.Refresh();
            fctb2.Refresh();
        }

        private void UpdateScroll(FastColoredTextBox tb, int vPos, int curLine)
        {
            if (_updating > 0)
                return;
            //
            BeginUpdate();
            //
            if (vPos <= tb.VerticalScroll.Maximum)
            {
                tb.VerticalScroll.Value = vPos;
                tb.UpdateScrollbars();
            }

            if (curLine < tb.LinesCount)
                tb.Selection = new Range(tb, 0, curLine, 0, curLine);
            //
            EndUpdate();
        }

        private void EndUpdate()
        {
            _updating--;
        }

        private void BeginUpdate()
        {
            _updating++;
        }

        public void DoCompare()
        {
            fctb1.Clear();
            fctb2.Clear();
            if (FileTypes.FileTypesDictionary == null)
                FileTypes.BuildDictionary();
            Cursor = Cursors.WaitCursor;
            var source1 = Lines.Load(tbFirstFile.Text);
            var source2 = Lines.Load(tbSecondFile.Text);
            var lang =
                FileTypes.GetLanguage(FileTypes.FileTypesDictionary, Path.GetExtension(tbFirstFile.Text))
                    .Language;
            var lang2 =
                FileTypes.GetLanguage(FileTypes.FileTypesDictionary, Path.GetExtension(tbSecondFile.Text))
                    .Language;
            source1.Merge(source2);

            BeginUpdate();

            Process(source1);

            EndUpdate();

            Cursor = Cursors.Default;

            _highlighter.HighlightSyntax(lang, new TextChangedEventArgs(fctb1.Range));
            _highlighter.HighlightSyntax(lang2, new TextChangedEventArgs(fctb2.Range));
        }

        private void btCompare_Click(object sender, EventArgs e)
        {
            //TODO:Check if working
            BeginInvoke(new MethodInvoker(DoCompare));
        }

        private void Process(IEnumerable<Line> lines)
        {
            foreach (var line in lines)
            {
                switch (line.state)
                {
                    case DiffType.None:
                        fctb1.AppendText(line.line + Environment.NewLine);
                        fctb2.AppendText(line.line + Environment.NewLine);
                        break;

                    case DiffType.Inserted:
                        fctb1.AppendText(Environment.NewLine);
                        fctb2.AppendText(line.line + Environment.NewLine, _greenStyle);
                        break;

                    case DiffType.Deleted:
                        fctb1.AppendText(line.line + Environment.NewLine, _redStyle);
                        fctb2.AppendText(Environment.NewLine);
                        break;
                }
                if (line.subLines != null)
                    Process(line.subLines);
            }
        }
    }

    #region Merge stuffs

    namespace DiffMergeStuffs
    {
        public class SimpleDiff<T>
        {
            private readonly IList<T> _left;
            private readonly IList<T> _right;
            private Func<T, T, bool> _compareFunc;
            private int[,] _matrix;
            private bool _matrixCreated;
            private int _postSkip;
            private int _preSkip;

            public SimpleDiff(IList<T> left, IList<T> right)
            {
                _left = left;
                _right = right;

                InitializeCompareFunc();
            }

            /// <summary>
            ///     This is the sole public method and it initializes
            ///     the LCS matrix the first time it's called, and
            ///     proceeds to fire a series of LineUpdate events
            /// </summary>
            /// proceeds to fire a series of LineUpdate events
            /// </summary>
            public event EventHandler<DiffEventArgs<T>> LineUpdate;

            public void RunDiff()
            {
                if (!_matrixCreated)
                {
                    CalculatePreSkip();
                    CalculatePostSkip();
                    CreateLCSMatrix();
                }

                for (var i = 0; i < _preSkip; i++)
                {
                    FireLineUpdate(DiffType.None, i, -1);
                }

                var totalSkip = _preSkip + _postSkip;
                ShowDiff(_left.Count - totalSkip, _right.Count - totalSkip);

                var leftLen = _left.Count;
                for (var i = _postSkip; i > 0; i--)
                {
                    FireLineUpdate(DiffType.None, leftLen - 1, -1);
                }
            }

            /// <summary>
            ///     This method is an optimization that
            ///     skips matching elements at the end of the
            ///     two arrays being diff'ed.
            ///     Care's taken so that this will never
            ///     overlap with the pre-skip.
            /// </summary>
            /// overlap with the pre-skip.
            /// </summary>
            private void CalculatePostSkip()
            {
                var leftLen = _left.Count;
                var rightLen = _right.Count;
                while (_postSkip < leftLen && _postSkip < rightLen &&
                       _postSkip < (leftLen - _preSkip) &&
                       _compareFunc(_left[leftLen - _postSkip - 1], _right[rightLen - _postSkip - 1]))
                {
                    _preSkip++;
                }
            }

            /// <summary>
            ///     This method is an optimization that
            ///     skips matching elements at the start of
            ///     the arrays being diff'ed
            /// </summary>
            /// f
            /// the arrays being diff'ed
            /// </summary>
            private void CalculatePreSkip()
            {
                var leftLen = _left.Count;
                var rightLen = _right.Count;
                while (_preSkip < leftLen && _preSkip < rightLen &&
                       _compareFunc(_left[_preSkip], _right[_preSkip]))
                {
                    _preSkip++;
                }
            }

            private void ShowDiff(int leftIndex, int rightIndex)
            {
                if (leftIndex > 0 && rightIndex > 0 &&
                    _compareFunc(_left[_preSkip + leftIndex - 1], _right[_preSkip + rightIndex - 1]))
                {
                    ShowDiff(leftIndex - 1, rightIndex - 1);
                    FireLineUpdate(DiffType.None, _preSkip + leftIndex - 1, -1);
                }
                else
                {
                    if (rightIndex > 0 &&
                        (leftIndex == 0 ||
                         _matrix[leftIndex, rightIndex - 1] >= _matrix[leftIndex - 1, rightIndex]))
                    {
                        ShowDiff(leftIndex, rightIndex - 1);
                        FireLineUpdate(DiffType.Inserted, -1, _preSkip + rightIndex - 1);
                    }
                    else if (leftIndex > 0 &&
                             (rightIndex == 0 ||
                              _matrix[leftIndex, rightIndex - 1] < _matrix[leftIndex - 1, rightIndex]))
                    {
                        ShowDiff(leftIndex - 1, rightIndex);
                        FireLineUpdate(DiffType.Deleted, _preSkip + leftIndex - 1, -1);
                    }
                }
            }

            private void CreateLCSMatrix()
            {
                var totalSkip = _preSkip + _postSkip;
                if (totalSkip >= _left.Count || totalSkip >= _right.Count)
                    return;

                // We only create a matrix large enough for the
                // unskipped contents of the diff'ed arrays
                _matrix = new int[_left.Count - totalSkip + 1, _right.Count - totalSkip + 1];

                for (var i = 1; i <= _left.Count - totalSkip; i++)
                {
                    // Simple optimization to avoid this calculation
                    // inside the outer loop (may have got JIT optimized
                    // but my tests showed a minor improvement in speed)
                    var leftIndex = _preSkip + i - 1;

                    // Again, instead of calculating the adjusted index inside
                    // the loop, I initialize it under the assumption that
                    // incrementing will be a faster operation on most CPUs
                    // compared to addition. Again, this may have got JIT
                    // optimized but my tests showed a minor speed difference.
                    for (int j = 1, rightIndex = _preSkip + 1; j <= _right.Count - totalSkip; j++, rightIndex++)
                    {
                        _matrix[i, j] = _compareFunc(_left[leftIndex], _right[rightIndex - 1])
                            ? _matrix[i - 1, j - 1] + 1
                            : Math.Max(_matrix[i, j - 1], _matrix[i - 1, j]);
                    }
                }

                _matrixCreated = true;
            }

            private void FireLineUpdate(DiffType diffType, int leftIndex, int rightIndex)
            {
                var local = LineUpdate;

                if (local == null)
                    return;

                var lineValue = leftIndex >= 0 ? _left[leftIndex] : _right[rightIndex];

                local(this, new DiffEventArgs<T>(diffType, lineValue, leftIndex, rightIndex));
            }

            private void InitializeCompareFunc()
            {
                // Special case for String types
                if (typeof (T) == typeof (String))
                {
                    _compareFunc = StringCompare;
                }
                else
                {
                    _compareFunc = DefaultCompare;
                }
            }

            private static bool StringCompare(T left, T right)
            {
                return Equals(left, right);
            }

            private static bool DefaultCompare(T left, T right)
            {
                return left.Equals(right);
            }
        }

        [Flags]
        public enum DiffType
        {
            None = 0,
            Inserted = 1,
            Deleted = 2
        }

        public class Line
        {
            public readonly string line;
            public DiffType state;
            public Lines subLines;

            public Line(string line)
            {
                this.line = line;
            }

            /// <summary>
            ///     Equals
            /// </summary>
            /// ///
            /// <summary>
            ///     Equals
            /// </summary>
            public override bool Equals(object obj)
            {
                return Equals(line, ((Line) obj).line);
            }

            public static bool operator ==(Line line1, Line line2)
            {
                return Equals(line1.line, line2.line);
            }

            public static bool operator !=(Line line1, Line line2)
            {
                return !Equals(line1.line, line2.line);
            }

            public override string ToString()
            {
                return line;
            }
        }

        public class DiffEventArgs<T> : EventArgs
        {
            public DiffEventArgs(DiffType diffType, T lineValue, int leftIndex, int rightIndex)
            {
                DiffType = diffType;
                LineValue = lineValue;
                LeftIndex = leftIndex;
                RightIndex = rightIndex;
            }

            public DiffType DiffType { get; private set; }

            public T LineValue { get; private set; }

            private int LeftIndex { get; set; }

            //TODO: Check RIght Index
            private int RightIndex { get; set; }
        }

        /// <summary>
        ///     File as list of lines
        /// </summary>
        public class Lines : List<Line>, IEquatable<Lines>
        {
            //??? ?????? ????? ??? ???????? ?????, ??????????? ? ????? ??????, ?? ?????? ?????? ????????? ?????
            private readonly Line _fictiveLine = new Line("===fictive line===") {state = DiffType.Deleted};

            private Lines()
            {
            }

            private Lines(int capacity)
                : base(capacity)
            {
            }

            private Line this[int i]
            {
                get { return i == -1 ? _fictiveLine : base[i]; }

                /*
                            set
                            {
                                if (i == -1) fictiveLine = value;
     /// <summary>
        ///     Is lines equal?
        /// </summary>
            */
            }

            /// <summary>
            ///     Is lines equal?
            /// </summary>
            public bool Equals(Lines other)
            {
                if (Count != other.Count)
                    return false;
                for (var i = 0; i < Count; i++)
                    if (this[i] != other[i])
                        return false;
                return true;
            }

            public static Lines Load(string fileName)
            {
                return Load(fileName, Encoding.Default);
            }

            /// <summary>
            ///     Load from file
            /// </summary>
            private static Lines Load(string fileName, Encoding enc)
            {
                var lines = new Lines();
                lines.AddRange(File.ReadAllLines(fileName, enc ?? Encoding.Default).Select(line => new Line(line)));
                return lines;
            }

            /// <summary>
            ///     Merge lines
            /// </summary>
            public void Merge(Lines lines)
            {
                var diff = new SimpleDiff<Line>(this, lines);
                var iLine = -1;

                diff.LineUpdate += (o, e) =>
                {
                    if (e.DiffType == DiffType.Inserted)
                    {
                        if (this[iLine].subLines == null)
                            this[iLine].subLines = new Lines();
                        e.LineValue.state = DiffType.Inserted;
                        this[iLine].subLines.Add(e.LineValue);
                    }
                    else
                    {
                        iLine++;
                        this[iLine].state = e.DiffType;
                        if (iLine > 0 &&
                            this[iLine - 1].state == DiffType.Deleted &&
                            this[iLine - 1].subLines == null &&
                            e.DiffType == DiffType.None)
                            this[iLine - 1].subLines = new Lines();
                    }
                };
                //?????/// <summary>
                diff.RunDiff();
            }

            /// <summary>
            ///     Clone
            /// </summary>
            public Lines Clone()
            {
                var result = new Lines(Count);
                result.AddRange(this.Select(line => new Line(line.line)));
                //TODO:Recheck
                return result;
            }

            private IEnumerable<Line> Expand()
            {
                return Expand(-1, Count - 1);
            }

            /// <summary>
            ///     Transform tree to list
            /// </summary>
            private IEnumerable<Line> Expand(int from, int to)
            {
                var result = new Lines();
                for (var i = from; i <= to; i++)
                {
                    if (this[i].state != DiffType.Deleted)
                        result.Add(this[i]);
                    if (this[i].subLines != null)
                        result.AddRange(this[i].subLines.Expand());
                }

                return result;
            }
        }
    }

    /*
            /// <summary>
            ///     ??????, ?????????? ????????? ??????????? ??????
            /// </summary>
            public class ConflictedLine : Line
            {
                public readonly Lines version1;
                public readonly Lines version2;

                public ConflictedLine(Lines version1, Lines version2)
                    : base("?")
                {
                    this.version1 = version1;
                    this.version2 = version2;
                }
            }
    */
}

#endregion Merge stuffs