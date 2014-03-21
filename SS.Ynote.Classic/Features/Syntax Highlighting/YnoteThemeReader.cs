#region

using System;
using System.Drawing;
using System.Xml;
using FastColoredTextBoxNS;

#endregion

namespace SS.Ynote.Classic
{
    /// <summary>
    ///     Ynote Theme File Reader
    /// </summary>
    public static class YnoteThemeReader
    {
        public static void ApplyTheme(string source, ISyntaxHighlighter highlighter, FastColoredTextBox tb)
        {
            using (var reader = XmlReader.Create(source))
            {
                while (reader.Read())
                    // Only detect start elements.
                    if (reader.IsStartElement())
                    {
                        // Get element name and switch on it.
                        switch (reader.Name)
                        {
                            case "Style":
                                // Search for the attribute name on this current node.
                                var name = reader["Name"];
                                var fontstyle = reader["Font"].ToEnum<FontStyle>();
                                var color = reader["Color"];
                                StyleInit(name, fontstyle, GetColorFromHexVal(color), highlighter);
                                if (reader.Read())
                                    StyleInit(name, fontstyle, GetColorFromHexVal(color), highlighter);
                                break;
                            case "Key":
                                // Search for the attribute name on this current node.
                                KeyInit(tb, reader["Name"], reader["Value"]);
                                if (reader.Read())
                                    KeyInit(tb, reader["Name"], reader["Value"]);
                                break;
                        }
                    }
            }
        }

        /*     static IEnumerable<Style> GetStyles(ISyntaxHighlighter h)
        {
            var lst = new List<Style>
            {
                h.CommentStyle,
                h.CommentTagStyle,
                h.FunctionsStyle,
                h.CharStyle,
                h.ClassNameStyle,
                h.ClassNameStyle2,
                h.KeywordStyle,
                h.KeywordStyle2,
                h.KeywordStyle3,
                h.TypesStyle,
                h.CSSPropertyStyle,
                h.CSSSelectorStyle,
                h.AttributeStyle,
                h.AttributeValueStyle,
                h.HtmlEntityStyle,
                h.TagBracketStyle,
                h.TagNameStyle,
                h.VariableStyle,
                h.PreprocessorStyle,
                h.StringStyle,
                h.NumberStyle
            };
            return lst;
        }
        */

        private static void KeyInit(FastColoredTextBox tb, string name, string value)
        {
            var keyval = GetColorFromHexVal(value);
            switch (name)
            {
                case "BackColor":
                    tb.BackColor = keyval;
                    break;
                case "TextColor":
                    tb.ForeColor = keyval;
                    break;
                case "BracketStyle":
                    tb.BracketsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(60, GetColorFromHexVal(value))));
                    break;
                case "BracketStyle2":
                    tb.BracketsStyle2 = new MarkerStyle(new SolidBrush(Color.FromArgb(60, GetColorFromHexVal(value))));
                    break;
                case "BookmarkColor":
                    tb.BookmarkColor = keyval;
                    break;
                case "CaretColor":
                    tb.CaretColor = keyval;
                    break;
                case "CurrentLineColor":
                    tb.CurrentLineColor = keyval;
                    break;
                case "FoldingIndicationColor":
                    tb.FoldingIndicatorColor = keyval;
                    break;
                case "LineNumberColor":
                    tb.LineNumberColor = keyval;
                    break;
                case "LineNumberPaddingColor":
                    tb.IndentBackColor = keyval;
                    break;
                case "SelectionColor":
                    tb.SelectionColor = keyval;
                    break;
                case "ServicesLineColor":
                    tb.ServiceLinesColor = keyval;
                    break;
            }
        }

        private static void StyleInit(string name, FontStyle style, Color color, ISyntaxHighlighter sh)
        {
            var tcstyle = new TextColorStyle(new SolidBrush(color), style);
            switch (name)
            {
                case "Comment":
                    sh.CommentStyle = tcstyle;
                    break;
                case "CommentTag":
                    sh.CommentTagStyle = tcstyle;
                    break;
                case "String":
                    sh.StringStyle = tcstyle;
                    break;
                case "Number":
                    sh.NumberStyle = tcstyle;
                    break;
                case "Variable":
                    sh.VariableStyle = tcstyle;
                    break;
                case "Keyword":
                    sh.KeywordStyle = tcstyle;
                    break;
                case "Keyword2":
                    sh.KeywordStyle2 = tcstyle;
                    break;
                case "Keyword3":
                    sh.KeywordStyle3 = tcstyle;
                    break;
                case "HtmlEntity":
                    sh.HtmlEntityStyle = tcstyle;
                    break;
                case "TagBracket":
                    sh.TagBracketStyle = tcstyle;
                    break;
                case "TagName":
                    sh.TagNameStyle = tcstyle;
                    break;
                case "Types":
                    sh.TypesStyle = tcstyle;
                    break;
                case "Functions":
                    sh.FunctionsStyle = tcstyle;
                    break;
                case "ClassName":
                    sh.ClassNameStyle = tcstyle;
                    break;
                case "ClassName2":
                    sh.ClassNameStyle2 = tcstyle;
                    break;
                case "Char":
                    sh.CharStyle = tcstyle;
                    break;
                case "Attribute":
                    sh.AttributeStyle = tcstyle;
                    break;
                case "AttributeValue":
                    sh.AttributeValueStyle = tcstyle;
                    break;
                case "CSSProperty":
                    sh.CSSPropertyStyle = tcstyle;
                    break;
                case "CSSSelector":
                    sh.CSSSelectorStyle = tcstyle;
                    break;
                case "CSSPropertyValue":
                    sh.CSSPropertyValueStyle = tcstyle;
                    break;
                case "Preprocessor":
                    sh.PreprocessorStyle = tcstyle;
                    break;
                case "Statements":
                    sh.StatementsStyle = tcstyle;
                    break;
                case "InBracket":
                    sh.InBracketStyle = tcstyle;
                    break;
            }
        }

        /// <summary>
        ///     Get Color from Hex
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private static Color GetColorFromHexVal(string hexString)
        {
            try
            {
                return ColorTranslator.FromHtml(hexString);
            }
            catch (Exception)
            {
                // System.Windows.Forms.MessageBox.Show("Invalid Hex Number : " + ex.Message);
                return default(Color);
            }
        }
    }

    public class TextColorStyle : TextStyle
    {
        public TextColorStyle(Brush tbBrush, FontStyle fstyle)
            : base(tbBrush, null, fstyle)
        {
        }
    }
}