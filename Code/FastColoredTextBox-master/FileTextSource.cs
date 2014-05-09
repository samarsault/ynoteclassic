using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
    /// <summary>
    ///     This class contains the source text (chars and styles).
    ///     It stores a text lines, the manager of commands, undo/redo stack, styles.
    /// </summary>
    public class FileTextSource : TextSource
    {
        private readonly Timer timer = new Timer();
        private Encoding fileEncoding;
        private FileStream fs;
        private List<int> sourceFileLinePositions = new List<int>();

        public FileTextSource(FastColoredTextBox currentTB)
            : base(currentTB)
        {
            timer.Interval = 10000;
            timer.Tick += timer_Tick;
            timer.Enabled = true;
        }

        public override Line this[int i]
        {
            get
            {
                if (lines[i] != null)
                    return lines[i];
                LoadLineFromSourceFile(i);

                return lines[i];
            }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        ///     Occurs when need to display line in the textbox
        /// </summary>
        public event EventHandler<LineNeededEventArgs> LineNeeded;

        /// <summary>
        ///     Occurs when need to save line in the file
        /// </summary>
        public event EventHandler<LinePushedEventArgs> LinePushed;

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            try
            {
                UnloadUnusedLines();
            }
            finally
            {
                timer.Enabled = true;
            }
        }

        private void UnloadUnusedLines()
        {
            const int margin = 2000;
            //   var iStartVisibleLine = CurrentTb.VisibleRange.Start.iLine;
            var iFinishVisibleLine = CurrentTb.VisibleRange.End.iLine;

            //var count = 0;
            for (var i = 0; i < Count; i++)
                if (lines[i] != null && !lines[i].IsChanged && Math.Abs(i - iFinishVisibleLine) > margin)
                {
                    lines[i] = null;
                    //   count++;
                }
#if debug
            Console.WriteLine("UnloadUnusedLines: " + count);
#endif
        }

        /// <summary>
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="enc"></param>
        public void OpenFile(string fileName, Encoding enc)
        {
            Clear();

            if (fs != null)
                fs.Dispose();

            //read lines of file
            fs = new FileStream(fileName, FileMode.Open);
            var length = fs.Length;
            //read signature
            enc = DefineEncoding(enc, fs);
            var shift = DefineShift(enc);
            //first line
            sourceFileLinePositions.Add((int) fs.Position);
            lines.Add(null);
            //other lines
            while (fs.Position < length)
            {
                var b = fs.ReadByte();
                if (b == 10) // char \n
                {
                    sourceFileLinePositions.Add((int) (fs.Position) + shift);
                    lines.Add(null);
                }
            }

            var temp = new Line[100];
            var c = lines.Count;
            lines.AddRange(temp);
            lines.TrimExcess();
            lines.RemoveRange(c, temp.Length);

            var temp2 = new int[100];
            c = lines.Count;
            sourceFileLinePositions.AddRange(temp2);
            sourceFileLinePositions.TrimExcess();
            sourceFileLinePositions.RemoveRange(c, temp.Length);

            fileEncoding = enc;

            OnLineInserted(0, Count);
            //load first lines for calc width of the text
            var linesCount = Math.Min(lines.Count, CurrentTb.ClientRectangle.Height/CurrentTb.CharHeight);
            for (var i = 0; i < linesCount; i++)
                LoadLineFromSourceFile(i);
            //
            NeedRecalc(new TextChangedEventArgs(0, linesCount - 1));
            if (CurrentTb.WordWrap)
                OnRecalcWordWrap(new TextChangedEventArgs(0, linesCount - 1));
        }

        private static int DefineShift(Encoding enc)
        {
            if (enc.IsSingleByte)
                return 0;

            if (enc.HeaderName == "unicodeFFFE")
                return 0; //UTF16 BE

            if (enc.HeaderName == "utf-16")
                return 1; //UTF16 LE

            if (enc.HeaderName == "utf-32BE")
                return 0; //UTF32 BE

            if (enc.HeaderName == "utf-32")
                return 3; //UTF32 LE

            return 0;
        }

        private static Encoding DefineEncoding(Encoding enc, Stream fs)
        {
            var bytesPerSignature = 0;
            var signature = new byte[4];
            var c = fs.Read(signature, 0, 4);
            if (signature[0] == 0xFF && signature[1] == 0xFE && signature[2] == 0x00 && signature[3] == 0x00 && c >= 4)
            {
                enc = Encoding.UTF32; //UTF32 LE
                bytesPerSignature = 4;
            }
            else if (signature[0] == 0x00 && signature[1] == 0x00 && signature[2] == 0xFE && signature[3] == 0xFF)
            {
                enc = new UTF32Encoding(true, true); //UTF32 BE
                bytesPerSignature = 4;
            }
            else if (signature[0] == 0xEF && signature[1] == 0xBB && signature[2] == 0xBF)
            {
                enc = Encoding.UTF8; //UTF8
                bytesPerSignature = 3;
            }
            else if (signature[0] == 0xFE && signature[1] == 0xFF)
            {
                enc = Encoding.BigEndianUnicode; //UTF16 BE
                bytesPerSignature = 2;
            }
            else if (signature[0] == 0xFF && signature[1] == 0xFE)
            {
                enc = Encoding.Unicode; //UTF16 LE
                bytesPerSignature = 2;
            }

            fs.Seek(bytesPerSignature, SeekOrigin.Begin);

            return enc;
        }

        public void CloseFile()
        {
            if (fs != null)
                try
                {
                    fs.Dispose();
                }
                catch
                {
                    ;
                }
            fs = null;
        }

        public override void SaveToFile(string fileName, Encoding enc)
        {
            //
            var newLinePos = new List<int>(Count);
            //create temp file
            var dir = Path.GetDirectoryName(fileName);
            var tempFileName = Path.Combine(dir, Path.GetFileNameWithoutExtension(fileName) + ".tmp");

            var sr = new StreamReader(fs, fileEncoding);
            using (var tempFs = new FileStream(tempFileName, FileMode.Create))
            using (var sw = new StreamWriter(tempFs, enc))
            {
                sw.Flush();

                for (var i = 0; i < Count; i++)
                {
                    newLinePos.Add((int) tempFs.Length);

                    var sourceLine = ReadLine(sr, i); //read line from source file

                    var lineIsChanged = lines[i] != null && lines[i].IsChanged;

                    var line = lineIsChanged ? lines[i].Text : sourceLine;

                    //call event handler
                    if (LinePushed != null)
                    {
                        var args = new LinePushedEventArgs(sourceLine, i, lineIsChanged ? line : null);
                        LinePushed(this, args);

                        if (args.SavedText != null)
                            line = args.SavedText;
                    }

                    //save line to file
                    if (i == Count - 1)
                        sw.Write(line);
                    else
                        sw.WriteLine(line);

                    sw.Flush();
                }
            }

            //clear lines buffer
            for (var i = 0; i < Count; i++)
                lines[i] = null;
            //deattach from source file
            sr.Dispose();
            fs.Dispose();
            //delete target file
            if (File.Exists(fileName))
                File.Delete(fileName);
            //rename temp file
            File.Move(tempFileName, fileName);

            //binding to new file
            sourceFileLinePositions = newLinePos;
            fs = new FileStream(fileName, FileMode.Open);
            fileEncoding = enc;
        }

        private string ReadLine(StreamReader sr, int i)
        {
            var filePos = sourceFileLinePositions[i];
            if (filePos < 0)
                return "";
            fs.Seek(filePos, SeekOrigin.Begin);
            sr.DiscardBufferedData();
            var line = sr.ReadLine();
            return line;
        }

        public override void ClearIsChanged()
        {
            foreach (var line in lines)
                if (line != null)
                    line.IsChanged = false;
        }

        private void LoadLineFromSourceFile(int i)
        {
            var line = CreateLine();
            fs.Seek(sourceFileLinePositions[i], SeekOrigin.Begin);
            var sr = new StreamReader(fs, fileEncoding);

            var s = sr.ReadLine() ?? "";

            //call event handler
            if (LineNeeded != null)
            {
                var args = new LineNeededEventArgs(s, i);
                LineNeeded(this, args);
                s = args.DisplayedLineText;
                if (s == null)
                    return;
            }

            foreach (var c in s)
                line.Add(new Char(c));
            lines[i] = line;

            if (CurrentTb.WordWrap)
                OnRecalcWordWrap(new TextChangedEventArgs(i, i));
        }

        public override void InsertLine(int index, Line line)
        {
            sourceFileLinePositions.Insert(index, -1);
            base.InsertLine(index, line);
        }

        public override void RemoveLine(int index, int count)
        {
            sourceFileLinePositions.RemoveRange(index, count);
            base.RemoveLine(index, count);
        }

        public override void Clear()
        {
            base.Clear();
        }

        public override int GetLineLength(int i)
        {
            return lines[i] == null ? 0 : base.lines[i].Count;
        }

        public override bool LineHasFoldingStartMarker(int iLine)
        {
            if (lines[iLine] == null)
                return false;
            return !string.IsNullOrEmpty(lines[iLine].FoldingStartMarker);
        }

        public override bool LineHasFoldingEndMarker(int iLine)
        {
            if (lines[iLine] == null)
                return false;
            return !string.IsNullOrEmpty(lines[iLine].FoldingEndMarker);
        }

        public override void Dispose()
        {
            if (fs != null)
                fs.Dispose();

            timer.Dispose();
        }

        internal void UnloadLine(int iLine)
        {
            if (lines[iLine] != null && !lines[iLine].IsChanged)
                lines[iLine] = null;
        }
    }

    public class LineNeededEventArgs : EventArgs
    {
        public LineNeededEventArgs(string sourceLineText, int displayedLineIndex)
        {
            SourceLineText = sourceLineText;
            DisplayedLineIndex = displayedLineIndex;
            DisplayedLineText = sourceLineText;
        }

        public string SourceLineText { get; private set; }

        public int DisplayedLineIndex { get; private set; }

        /// <summary>
        ///     This text will be displayed in textbox
        /// </summary>
        public string DisplayedLineText { get; set; }
    }

    public class LinePushedEventArgs : EventArgs
    {
        public LinePushedEventArgs(string sourceLineText, int displayedLineIndex, string displayedLineText)
        {
            SourceLineText = sourceLineText;
            DisplayedLineIndex = displayedLineIndex;
            DisplayedLineText = displayedLineText;
            SavedText = displayedLineText;
        }

        public string SourceLineText { get; private set; }

        public int DisplayedLineIndex { get; private set; }

        /// <summary>
        ///     This property contains only changed text.
        ///     If text of line is not changed, this property contains null.
        /// </summary>
        public string DisplayedLineText { get; private set; }

        /// <summary>
        ///     This text will be saved in the file
        /// </summary>
        public string SavedText { get; set; }
    }
}