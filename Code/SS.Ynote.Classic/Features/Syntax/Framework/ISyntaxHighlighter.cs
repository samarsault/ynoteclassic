using FastColoredTextBoxNS;

namespace SS.Ynote.Classic.Features.Syntax
{
    public interface ISyntaxHighlighter
    {
        /// <summary>
        ///     String style
        /// </summary>
        Style StringStyle { get; set; }

        /// <summary>
        ///     Comment style
        /// </summary>
        Style CommentStyle { get; set; }

        /// <summary>
        ///     Number style
        /// </summary>
        Style NumberStyle { get; set; }

        /// <summary>
        ///     C# attribute style
        /// </summary>
        Style AttributeStyle { get; set; }

        /// <summary>
        ///     Class name style
        /// </summary>
        Style ClassNameStyle { get; set; }

        /// <summary>
        ///     Char Style
        /// </summary>
        Style CharStyle { get; set; }

        /// <summary>
        ///     Keyword style
        /// </summary>
        Style KeywordStyle { get; set; }

        /// <summary>
        ///     Style of tags in comments of C#
        /// </summary>
        Style CommentTagStyle { get; set; }

        /// <summary>
        ///     HTML attribute value style
        /// </summary>
        Style AttributeValueStyle { get; set; }

        /// <summary>
        ///     HTML tag brackets style
        /// </summary>
        Style TagBracketStyle { get; set; }

        /// <summary>
        ///     HTML tag name style
        /// </summary>
        Style TagNameStyle { get; set; }

        /// <summary>
        ///     HTML Entity style
        /// </summary>
        Style HtmlEntityStyle { get; set; }

        /// <summary>
        ///     Preprocessor Style
        /// </summary>
        Style PreprocessorStyle { get; set; }

        /// <summary>
        ///     Variable style
        /// </summary>
        Style VariableStyle { get; set; }

        /// <summary>
        ///     Specific PHP keyword style
        /// </summary>
        Style KeywordStyle2 { get; set; }

        /// <summary>
        ///     Specific PHP keyword style
        /// </summary>
        Style KeywordStyle3 { get; set; }

        /// <summary>
        ///     SQL Statements style
        /// </summary>
        Style StatementsStyle { get; set; }

        /// <summary>
        ///     SQL Functions style
        /// </summary>
        Style FunctionsStyle { get; set; }

        /// <summary>
        ///     SQL Types style
        /// </summary>
        Style TypesStyle { get; set; }

        /// <summary>
        ///     CSS Selector Style
        /// </summary>
        Style CSSSelectorStyle { get; set; }

        /// <summary>
        ///     CSS Property Style
        /// </summary>
        Style CSSPropertyStyle { get; set; }

        /// <summary>
        ///     CSS Property Value Style
        /// </summary>
        Style CSSPropertyValueStyle { get; set; }

        /// <summary>
        ///     Other Name Style
        /// </summary>
        Style ClassNameStyle2 { get; set; }

        /// <summary>
        ///     Highlight Syntax
        /// </summary>
        /// <param name="language"></param>
        /// <param name="range"></param>
        void HighlightSyntax(Language language, TextChangedEventArgs range);

        /// <summary>
        ///     Highlight Syntax with SyntaxBase file
        /// </summary>
        /// <param name="Syntax"></param>
        /// <param name="args"></param>
        void HighlightSyntax(SyntaxBase Syntax, TextChangedEventArgs args);

        /// <summary>
        ///     Loads All Syntaxes
        /// </summary>
        void LoadAllSyntaxes();
    }
}