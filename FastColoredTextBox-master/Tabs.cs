// Thanks to : http://github.com/rickass/FastColoredTextBox

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using FastColoredTextBoxNS;

public class TabCharStyle : TextStyle
        {
            /// <summary>
            /// Draw the \t (tab character) using a special character.
            /// TODO: the \t character still occupies just one character instead of a variable width depending on the tab stops.
            /// </summary>
            public bool SpecialTabDraw { get; set; }


            public TabCharStyle(Brush foreBrush, Brush backgroundBrush, FontStyle fontStyle, bool specialTabDraw)
                : base(foreBrush, backgroundBrush, fontStyle)
            {
                SpecialTabDraw = specialTabDraw;
            }

            public override void Draw(Graphics gr, Point position, Range range)
            {
                if (!SpecialTabDraw)
                {
                    base.Draw(gr, position, range);
                    return;
                }

                // TODO: Can range span multiple lines? I don't think so...
                var llll = range.tb[range.Start.iLine]; // text on the line
                string beforeRangeText = llll.Text.Substring(0, range.Start.iChar); // all text before the range
                string rangeText = range.Text; // text within the range
                
                // Calculate where previous range ended
                int beforeRangeSize = TextSizeCalculator.TextWidth(beforeRangeText, range.tb.TabLength);
                int rangeSize = TextSizeCalculator.TextWidth(beforeRangeSize, rangeText, range.tb.TabLength) - beforeRangeSize;


                //draw background
                if (BackgroundBrush != null)
                {
                    // position.X should be at the correct location
                    //int backgroundWidth = (range.End.iChar - range.Start.iChar)*range.tb.CharWidth; // fixed character width
                    int backgroundWidth = rangeSize * range.tb.CharWidth;
                    gr.FillRectangle(BackgroundBrush, position.X, position.Y, backgroundWidth, range.tb.CharHeight);
                }
                //draw chars
                Font f = new Font(range.tb.Font, FontStyle);
                //Font fHalfSize = new Font(range.tb.Font.FontFamily, f.SizeInPoints/2, FontStyle);
                Line line = range.tb[range.Start.iLine];
                float dx = range.tb.CharWidth;
                float y = position.Y + range.tb.LineInterval / 2;
                float x = position.X - range.tb.CharWidth / 3;

                if (ForeBrush == null)
                    ForeBrush = new SolidBrush(range.tb.ForeColor);

                //IME mode
                if (range.tb.ImeAllowed)
                    for (int i = range.Start.iChar; i < range.End.iChar; i++)
                    {
                        SizeF size = FastColoredTextBox.GetCharSize(f, line[i].c);

                        var gs = gr.Save();
                        float k = size.Width > range.tb.CharWidth + 1 ? range.tb.CharWidth/size.Width : 1;
                        gr.TranslateTransform(x, y + (1 - k)*range.tb.CharHeight/2);
                        gr.ScaleTransform(k, (float) Math.Sqrt(k));
                        char c = line[i].c;
                        if (c == '\t')
                        {
                            // draw the rightwards arrow character (http://www.fileformat.info/info/unicode/char/2192/index.htm)
                            gr.DrawString("\u2192", f, ForeBrush, x, y, stringFormat);
                        }
                        else
                        {
                            gr.DrawString(c.ToString(), f, ForeBrush, 0, 0, stringFormat);
                        }
                        gr.Restore(gs);
                        /*
                        if(size.Width>range.tb.CharWidth*1.5f)
                            gr.DrawString(line[i].c.ToString(), fHalfSize, foreBrush, x, y+range.tb.CharHeight/4, stringFormat);
                        else
                            gr.DrawString(line[i].c.ToString(), f, foreBrush, x, y, stringFormat);
                         * */
                        x += dx;
                    }
                else
                {
                    //classic mode 
                    int currentSize = beforeRangeSize;
                    for (int i = range.Start.iChar; i < range.End.iChar; i++)
                    {
                        //draw char
                        char c = line[i].c;
                        if (c == '\t')
                        {
                            int tabWidth = TextSizeCalculator.TabWidth(currentSize, range.tb.TabLength);
                            // How do we print tabs?
                            // draw the rightwards arrow character (http://www.fileformat.info/info/unicode/char/2192/index.htm)
                            //gr.DrawString("\u2192", f, ForeBrush, x, y, stringFormat);
                            // or draw an arrow via DrawLine?
                            var pen = new Pen(Color.FromArgb(255, 0, 0, 255), 1);
                            pen.EndCap = LineCap.ArrowAnchor;
                            gr.DrawLine(pen, x, y + (range.tb.CharHeight / 2), x + (tabWidth * dx), y + (range.tb.CharHeight / 2));

                            // move to next position
                            // x += dx; // tab has width 1

                            x += tabWidth * dx; // tab width has variable width
                            currentSize += tabWidth;
                        }
                        else
                        {
                            currentSize++;
                            gr.DrawString(c.ToString(), f, ForeBrush, x, y, stringFormat);
                            x += dx;
                        }
                    }
                }
                //
                f.Dispose();
            }
        }
    public static class TextSizeCalculator
    {

        /// <summary>
        /// Converts the prevLine.Length to TextWidth and finds the corresponding Place in the currentLine.
        /// 
        /// So first the prevLine is converted from tabs to spaces, then we have an index. 
        /// Then we convert currentLine from tabs to spaces and find a Place that matches.
        /// </summary>
        /// <param name="prevLine"></param>
        /// <param name="currentLine"></param>
        /// <param name="tabLength"></param>
        /// <returns></returns>
        public static int AdjustedCharWidthOffset(string prevLine, string currentLine, int tabLength)
        {
            int prevCharWidthOffset = TextWidth(prevLine, tabLength); // width of the text in charwidth multiples
            int offset = CharIndexAtCharWidthPoint(currentLine, tabLength, prevCharWidthOffset);
            return offset;
        }

        /// <summary>
        /// 
        /// 
        /// charPositionOffset is in multiples of the CharWidth.
        /// 
        /// Given string "a\t" (a followed by TAB, with tablenth = 4).
        /// When charIndex = 0, return 0
        /// When charIndex = 1, return 1,
        /// When charIndex = 2 or 3 the index is within the TAB, either go to the first char on the left/right.
        /// </summary>
        /// <returns></returns>
        public static int CharIndexAtCharWidthPoint(string text, int tabLength, int charPositionOffset)
        {
            return CharIndexAtPoint(text, tabLength, 1, charPositionOffset);
        }

        public static int CharIndexAtPoint(string text, int tabLength, int charWidth, int xPos)
        {
            if (xPos <= 0) return 0;
            int drawingWidth = 0;
            int prevDrawingWidth = 0;
            int size = 0;
            int prevSize = 0;

            for (int i = 0; i < text.Length; i++)
            {
                prevDrawingWidth = drawingWidth;
                prevSize = size;
                if (text[i] != '\t')
                {
                    drawingWidth += charWidth;
                    size++;
                }
                else
                {
                    int tabWidth = TabWidth(size, tabLength);
                    drawingWidth += tabWidth*charWidth;
                    size += tabWidth;
                }

                if (xPos <= drawingWidth)
                {
                    // we have gone past the character
                    double d = ((double)(drawingWidth - prevDrawingWidth)) / 2.0;
                    int diff = (int)Math.Round(d);
                    //if ((2*diff == charWidth))
                    //{
                    //    // current character is exactly one character wide
                    //    return i;
                    //}
                    //else 
                    if (xPos < prevDrawingWidth + diff)
                    {
                        return i;
                    }
                    else
                    {
                        // index could be placed after last character when (i+1) >= text.Length
                        return i + 1;
                    }
                }
            }
            return text.Length;
        }

        public static int TabWidth(int preceedingTextLength, int tabLength)
        {
            int tabFiller = tabLength - (preceedingTextLength % tabLength);

            return tabFiller;
        }

        public static int TextWidth(string text, int tabLength)
        {
            int size = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != '\t')
                {
                    size++;
                }
                else
                {
                    size += TabWidth(size, tabLength);
                }
            }
            return size;
        }

        public static int TextWidth(int preceedingTextLength, string text, int tabLength)
        {
            int size = preceedingTextLength;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != '\t')
                {
                    size++;
                }
                else
                {
                    size += TabWidth(size, tabLength);
                }
            }
            return size;
        }
    }