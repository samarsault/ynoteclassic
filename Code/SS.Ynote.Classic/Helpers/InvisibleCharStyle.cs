using FastColoredTextBoxNS;
using System.Drawing;

namespace SS.Ynote.Classic
{
    internal class InvisibleCharsRenderer : Style
    {
        private readonly Pen _pen;

        public InvisibleCharsRenderer(Pen pen)
        {
            _pen = pen;
        }

        public override void Draw(Graphics gr, Point position, Range range)
        {
            var tb = range.tb;
            using (Brush brush = new SolidBrush(_pen.Color))
                foreach (var place in range)
                {
                    switch (tb[place].c)
                    {
                        case ' ':
                            var point = tb.PlaceToPoint(place);
                            point.Offset(tb.CharWidth / 2, tb.CharHeight / 2);
                            gr.DrawLine(_pen, point.X, point.Y, point.X + 1, point.Y);
                            break;

                        case '\0':
                            point = tb.PlaceToPoint(place);
                            gr.DrawString("~", tb.Font, brush, point.X, point.Y);
                            break;
                    }

                    if (tb[place.iLine].Count - 1 == place.iChar)
                    {
                        var point = tb.PlaceToPoint(place);
                        point.Offset(tb.CharWidth, 0);
                        gr.DrawString("¶", tb.Font, brush, point);
                    }
                }
        }
    }
}