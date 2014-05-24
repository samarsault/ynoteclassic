using System;
using System.Drawing;
using System.Xml;
using FastColoredTextBoxNS;

namespace SS.Ynote.Classic.Features.Syntax
{
    /// <summary>
    ///     Ynote Theme File Reader
    /// </summary>
    public static class YnoteThemeReader
    {
        public static void ApplyTheme(string source, SyntaxHighlighter highlighter, FastColoredTextBox tb)
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
                                var fontstyle = (FontStyle) Enum.Parse(typeof (FontStyle), reader["Font"]);
                                var color = reader["Color"];
                                InitStyle(name, fontstyle, GetColorFromHexVal(color), highlighter);
                                // if (reader.Read())
                                //     InitStyle(name, fontstyle, GetColorFromHexVal(color), highlighter);
                                break;

                            case "Key":
                                // Search for the attribute name on this current node.
                                InitKey(tb, reader["Name"], reader["Value"]);
                                // if (reader.Read())
                                //     KeyInit(tb, reader["Name"], reader["Value"]);
                                break;
                        }
                    }
            }
        }

        private static void InitKey(FastColoredTextBox tb, string name, string value)
        {
            var keyval = GetColorFromHexVal(value);
            switch (name)
            {
                case "Background":
                    tb.BackColor = keyval;
                    break;

                case "Foreground":
                    tb.ForeColor = keyval;
                    break;

                case "BracketStyle":
                    tb.BracketsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(60, GetColorFromHexVal(value))));
                    break;

                case "BracketStyle2":
                    tb.BracketsStyle2 = new MarkerStyle(new SolidBrush(Color.FromArgb(60, GetColorFromHexVal(value))));
                    break;

                case "Bookmark":
                    tb.BookmarkColor = keyval;
                    break;

                case "Caret":
                    tb.CaretColor = keyval;
                    break;

                case "CurrentLine":
                    tb.CurrentLineColor = keyval;
                    break;

                case "FoldingIndication":
                    tb.FoldingIndicatorColor = keyval;
                    break;

                case "LineNumber":
                    tb.LineNumberColor = keyval;
                    break;

                case "LineNumberPadding":
                    tb.IndentBackColor = keyval;
                    break;

                case "Selection":
                    tb.SelectionColor = keyval;
                    break;

                case "ServicesLine":
                    tb.ServiceLinesColor = keyval;
                    break;

                case "SameWords":
                    tb.SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, keyval)));
                    break;
            }
        }

        private static void InitStyle(string name, FontStyle style, Color color, SyntaxHighlighter sh)
        {
            if (sh == null)
                return;
            var tcstyle = new TextStyle(new SolidBrush(color), null, style);
            switch (name)
            {
                case "Comment":
                    sh.Comment = tcstyle;
                    break;

                case "String":
                    sh.String = tcstyle;
                    break;

                case "Number":
                    sh.Number = tcstyle;
                    break;

                case "Variable":
                    sh.Variable = tcstyle;
                    break;

                case "Keyword":
                    sh.Keyword = tcstyle;
                    break;

                case "Constant":
                    sh.Constant = tcstyle;
                    break;

                case "Storage":
                    sh.Storage = tcstyle;
                    break;

                case "TagBracket":
                    sh.TagBracket = tcstyle;
                    break;

                case "TagName":
                    sh.TagName = tcstyle;
                    break;

                case "ClassName":
                    sh.ClassName = tcstyle;
                    break;

                case "FunctionName":
                    sh.FunctionName = tcstyle;
                    break;
                case "FunctionArgument":
                    sh.FunctionArgument = tcstyle;
                    break;
                case "Punctuation":
                    sh.Punctuation = tcstyle;
                    break;
                case "AttributeName":
                    sh.AttributeName = tcstyle;
                    break;

                case "AttributeValue":
                    sh.AttributeValue = tcstyle;
                    break;

                case "CSSProperty":
                    sh.CSSProperty = tcstyle;
                    break;

                case "CSSSelector":
                    sh.CSSSelector = tcstyle;
                    break;

                case "CSSPropertyValue":
                    sh.CSSPropertyValue = tcstyle;
                    break;

                case "Preprocessor":
                    sh.Preprocessor = tcstyle;
                    break;

                case "LibraryClass":
                    sh.LibraryClass = tcstyle;
                    break;

                case "LibraryFunction":
                    sh.LibraryFunction = tcstyle;
                    break;
                case "DoctypeDeclaration":
                    sh.DoctypeDeclaration = tcstyle;
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
}