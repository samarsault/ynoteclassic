#region Using Directives

using FastColoredTextBoxNS;

#endregion

public interface ISyntaxHighlighter
{
    /// <summary>
    ///     String style
    /// </summary>
    Style StringStyle { set; }

    /// <summary>
    ///     Comment style
    /// </summary>
    Style CommentStyle { set; }

    /// <summary>
    ///     Number style
    /// </summary>
    Style NumberStyle { set; }

    /// <summary>
    ///     C# attribute style
    /// </summary>
    Style AttributeStyle { set; }

    /// <summary>
    ///     Class name style
    /// </summary>
    Style ClassNameStyle { set; }

    /// <summary>
    ///     Char Style
    /// </summary>
    Style CharStyle { set; }

    /// <summary>
    ///     Keyword style
    /// </summary>
    Style KeywordStyle { set; }

    /// <summary>
    ///     Style of tags in comments of C#
    /// </summary>
    Style CommentTagStyle { set; }

    /// <summary>
    ///     HTML attribute value style
    /// </summary>
    Style AttributeValueStyle { set; }

    /// <summary>
    ///     HTML tag brackets style
    /// </summary>
    Style TagBracketStyle { set; }

    /// <summary>
    ///     HTML tag name style
    /// </summary>
    Style TagNameStyle { set; }

    /// <summary>
    ///     HTML Entity style
    /// </summary>
    Style HtmlEntityStyle { set; }

    /// <summary>
    ///     Preprocessor Style
    /// </summary>
    Style PreprocessorStyle { set; }

    /// <summary>
    ///     Variable style
    /// </summary>
    Style VariableStyle { set; }

    /// <summary>
    ///     Specific PHP keyword style
    /// </summary>
    Style KeywordStyle2 { set; }

    /// <summary>
    ///     Specific PHP keyword style
    /// </summary>
    Style KeywordStyle3 { set; }

    /// <summary>
    ///     SQL Statements style
    /// </summary>
    Style StatementsStyle { set; }

    /// <summary>
    ///     SQL Functions style
    /// </summary>
    Style FunctionsStyle { set; }

    /// <summary>
    ///     SQL Types style
    /// </summary>
    Style TypesStyle { set; }

    /// <summary>
    ///     CSS Selector Style
    /// </summary>
    Style CSSSelectorStyle { set; }

    /// <summary>
    ///     CSS Property Style
    /// </summary>
    Style CSSPropertyStyle { set; }

    /// <summary>
    ///     CSS Property Value Style
    /// </summary>
    Style CSSPropertyValueStyle { set; }

    /// <summary>
    ///     Other Name Style
    /// </summary>
    Style ClassNameStyle2 { set; }
    /// <summary>
    ///     Highlight Syntax
    /// </summary>
    /// <param name="language"></param>
    /// <param name="range"></param>
    void HighlightSyntax(Language language, TextChangedEventArgs range);
}