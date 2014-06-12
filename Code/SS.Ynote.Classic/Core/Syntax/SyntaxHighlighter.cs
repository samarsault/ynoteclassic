using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using FastColoredTextBoxNS;
using SS.Ynote.Classic.Core.Settings;
using SS.Ynote.Classic.Core.Syntax.Framework;

namespace SS.Ynote.Classic.Core.Syntax
{
    /// <summary>
    ///     Predifined Syntax Highlighter
    /// </summary>
    public sealed class SyntaxHighlighter
    {
        #region Properties

        /// <summary>
        ///     Regex Compiled Options base on Platform x86 | x64
        /// </summary>
        private static RegexOptions RegexCompiledOption
        {
            get
            {
                return PlatformType.GetOperationSystemPlatform() == Platform.X86
                    ? RegexOptions.Compiled
                    : RegexOptions.None;
            }
        }

        /// <summary>
        ///     String style
        /// </summary>
        public Style String { get; set; }

        /// <summary>
        ///     Comment style
        /// </summary>
        public Style Comment { get; set; }

        /// <summary>
        ///     Number style
        /// </summary>
        public Style Number { get; set; }

        /// <summary>
        ///     Class name style
        /// </summary>
        public Style ClassName { get; set; }

        /// <summary>
        ///     Other Name Style
        /// </summary>
        public Style FunctionName { get; set; }

        /// <summary>
        ///     Function Argument Style
        /// </summary>
        public Style FunctionArgument { get; set; }

        /// <summary>
        ///     Char Style
        /// </summary>
        public Style Punctuation { get; set; }

        /// <summary>
        ///     Keyword style
        /// </summary>
        public Style Keyword { get; set; }


        /// <summary>
        ///     Constant
        /// </summary>
        public Style Constant { get; set; }

        /// <summary>
        ///     Storage
        /// </summary>
        public Style Storage { get; set; }

        /// <summary>
        ///     Variable
        /// </summary>
        public Style Variable { get; set; }

        /// <summary>
        ///     Preprocessor Style
        /// </summary>
        public Style Preprocessor { get; set; }

        /// <summary>
        ///     C# attribute style
        /// </summary>
        public Style AttributeName { get; set; }

        /// <summary>
        ///     HTML attribute value style
        /// </summary>
        public Style AttributeValue { get; set; }

        /// <summary>
        ///     HTML tag brackets style
        /// </summary>
        public Style TagBracket { get; set; }

        /// <summary>
        ///     HTML tag name style
        /// </summary>
        public Style TagName { get; set; }

        /// <summary>
        ///     CSS Selector Style
        /// </summary>
        public Style CSSSelector { get; set; }

        /// <summary>
        ///     CSS Property Style
        /// </summary>
        public Style CSSProperty { get; set; }

        /// <summary>
        ///     CSS Property Value Style
        /// </summary>
        public Style CSSPropertyValue { get; set; }

        /// <summary>
        ///     A Library Class
        /// </summary>
        public Style LibraryClass { get; set; }

        /// <summary>
        ///     A Library Function
        /// </summary>
        public Style LibraryFunction { get; set; }

        /// <summary>
        ///     DocType Declaration
        /// </summary>
        public Style DoctypeDeclaration { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Highlight Syntax using SyntaxBase
        /// </summary>
        /// <param name="syntax"></param>
        /// <param name="e"></param>
        private void HighlightSyntaxFromFile(SyntaxBase syntax, TextChangedEventArgs e)
        {
            e.ChangedRange.tb.Language = syntax.Name;
            e.ChangedRange.tb.LeftBracket = syntax.LeftBracket;
            e.ChangedRange.tb.RightBracket = syntax.RightBracket;
            e.ChangedRange.tb.LeftBracket2 = syntax.LeftBracket2;
            e.ChangedRange.tb.RightBracket2 = syntax.RightBracket2;
            e.ChangedRange.tb.CommentPrefix = syntax.CommentPrefix;
            foreach (var rule in syntax.Rules)
            {
                e.ChangedRange.ClearStyle(rule.Type);
                e.ChangedRange.SetStyle(rule.Type, rule.Regex, rule.Options);
            }
            e.ChangedRange.ClearFoldingMarkers();
            foreach (var folding in syntax.FoldingRules)
                e.ChangedRange.SetFoldingMarkers(folding.FoldingStartMarker, folding.FoldingEndMarker, folding.Options);
        }

        /// <summary>
        ///     Highlight Syntax
        /// </summary>
        /// <param name="language"></param>
        /// <param name="args"></param>
        public void HighlightSyntax(string language, TextChangedEventArgs args)
        {
            if (Scopes.Contains(language))
            {
                switch (language)
                {
                    case "Actionscript":
                        ActionscriptSyntaxHighlight(args);
                        break;

                    case "Antlr":
                        AntlrSyntaxHighlight(args);
                        break;

                    case "ASP":
                        HTMLSyntaxHighlight(args);
                        break;

                    case "Objective_C":
                        ObjectiveCHighlight(args);
                        break;

                    case "Batch":
                        BatchSyntaxHighlight(args);
                        break;

                    case "C":
                        CSyntaxHighlight(args);
                        break;

                    case "C++":
                        CppSyntaxHighlight(args);
                        break;

                    case "CSS":
                        CssHighlight(args);
                        break;

                    case "CSharp":
                        CSharpSyntaxHighlight(args);
                        break;

                    case "CoffeeScript":
                        CoffeeScriptSyntaxHighlight(args);
                        break;

                    case "D":
                        DSyntaxHighlight(args);
                        break;

                    case "Diff":
                        DiffSyntaxHighlight(args);
                        break;

                    case "Java":
                        JavaSyntaxHighlight(args);
                        break;

                    case "Lua":
                        LuaSyntaxHighlight(args);
                        break;

                    case "Python":
                        PythonSyntaxHighlight(args);
                        break;

                    case "QBasic":
                        QBasicHighlight(args);
                        break;

                    case "Perl":
                        PerlSyntaxHighlight(args);
                        break;

                    case "PowerShell":
                        PowerShellSyntaxHighlight(args);
                        break;

                    case "R":
                        RSyntaxHighlight(args);
                        break;

                    case "Ruby":
                        RubySyntaxHighlight(args);
                        break;

                    case "Xml":
                        XmlSyntaxHighlight(args);
                        break;

                    case "INI":
                        IniSyntaxHighlight(args);
                        break;

                    case "Makefile":
                        MakeFileSyntaxHighlight(args);
                        break;

                    case "JSON":
                        JsonSyntaxHighlight(args);
                        break;

                    case "VB":
                        VBSyntaxHighlight(args);
                        break;

                    case "HTML":
                        HtmlSyntaxHighlight(args);
                        break;

                    case "Javascript":
                        JScriptSyntaxHighlight(args);
                        break;

                    case "SQL":
                        SqlSyntaxHighlight(args);
                        break;

                    case "ShellScript":
                        ShellSyntaxHighlight(args);
                        break;

                    case "PHP":
                        HTMLPHPSyntaxHighlight(args);
                        break;

                    case "Lisp":
                        LispSyntaxHighlight(args);
                        break;

                    case "FSharp":
                        FSharpSyntaxHighlight(args);
                        break;

                    case "Pascal":
                        PascalSyntaxHighlight(args);
                        break;

                    case "Scala":
                        ScalaSyntaxHighlight(args);
                        break;

                    case "Yaml":
                        YamlSyntaxHighlight(args);
                        break;

                    case "LaTeX":
                        LaTeXSyntaxHighlight(args);
                        break;

                    case "Haskell":
                        HaskellSyntaxHighlight(args);
                        break;

                    case "MATLAB":
                        MATLABSyntaxHighlight(args);
                        break;
                    case "Tcl":
                        TclSyntaxHighlight(args);
                        break;
                    default:
                        foreach (var syntax in LoadedSyntaxes)
                            if (syntax.Name == language)
                                HighlightSyntaxFromFile(syntax, args);
                        break;
                }
            }
        }

        #endregion Public Methods

        #region From File

        /// <summary>
        /// List of Scopes Installed
        /// </summary>
        public static IList<string> Scopes;
        /// <summary>
        ///     Loaded Syntaxes
        /// </summary>
        internal static IList<SyntaxBase> LoadedSyntaxes = new List<SyntaxBase>();

        /// <summary>
        ///     Loads All Syntaxes from File
        /// </summary>
        public void LoadAllSyntaxes()
        {
            Scopes = new List<string>()
            {
                "Text",
                "HTML",
                "ASP",
                "Javascript",
                "CSS",
                "PHP",
                "SQL",
                "Batch",
                "INI",
                "JSON",
                "Actionscript",
                "Antlr",
                "C",
                "C++",
                "CoffeeScript",
                "CSharp",
                "D",
                "Diff",
                "FSharp",
                "Haskell",
                "Java",
                "LaTeX",
                "Lua",
                "Lisp",
                "Makefile",
                "MATLAB",
                "Objective_C",
                "Pascal",
                "Perl",
                "PowerShell",
                "Python",
                "R",
                "Ruby",
                "Scala",
                "ShellScript",
                "Tcl",
                "QBasic",
                "VB",
                "Xml",
                "Yaml",
            };
            foreach (
                var file in
                    Directory.GetFiles(string.Format(@"{0}\Syntaxes\", GlobalSettings.SettingsDir), "*.ynotesyntax"))
            {
                var synbase = GenerateBase(file);
                LoadedSyntaxes.Add(synbase);
                Scopes.Add(synbase.Name);
            }

        }

        /// <summary>
        ///     Generates a SyntaxBase
        /// </summary>
        /// <param name="descFile"></param>
        /// <returns></returns>
        private SyntaxBase GenerateBase(string descFile)
        {
            var synbase = new SyntaxBase {SysPath = descFile};
            using (var reader = XmlReader.Create(descFile))
            {
                while (reader.Read())
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "Syntax":
                                synbase.CommentPrefix = reader["CommentPrefix"];
                                synbase.Extensions = reader["Extensions"].Split('|');
                                break;

                            case "Rule":
                            {
                                var type = reader["Type"];
                                var options = reader["Options"];
                                var regex = reader["Regex"];
                                synbase.Rules.Add(InitRule(type, regex, options));
                                // if (reader.Read())
                                //     synbase.Rules.Add(InitRule(type, regex, options));
                            }
                                break;

                            case "Folding":
                                var start = reader["Start"];
                                var end = reader["End"];
                                var foldOptions = reader["Options"];
                                synbase.FoldingRules.Add(InitFoldingRule(start, end, foldOptions));
                                // if (reader.Read())
                                //     synbase.FoldingRules.Add(InitFoldingRule(start, end, foldOptions));
                                break;

                            case "Brackets":
                                synbase.LeftBracket = reader["Left"][0];
                                synbase.RightBracket = reader["Right"][0];
                                if (reader["Left2"] != null)
                                {
                                    synbase.LeftBracket2 = reader["Left2"][0];
                                    synbase.RightBracket2 = reader["Right2"][0];
                                }
                                break;
                        }
                    }
            }
            return synbase;
        }

        private static FoldingRule InitFoldingRule(string start, string end, string options)
        {
            var rule = new FoldingRule
            {
                FoldingStartMarker = start,
                FoldingEndMarker = end
            };
            if (options == null)
                rule.Options = RegexOptions.None;
            else
                rule.Options = (RegexOptions) Enum.Parse(typeof (RegexOptions), options);
            return rule;
        }

        private SyntaxRule InitRule(string type, string regex, string options)
        {
            var rule = new SyntaxRule {Type = GetStyleFromName(type), Regex = regex};
            if (options == null)
                rule.Options = RegexOptions.None;
            else
                rule.Options = (RegexOptions) Enum.Parse(typeof (RegexOptions), options);
            return rule;
        }

        private Style GetStyleFromName(string name)
        {
            switch (name)
            {
                case "Comment":
                    return Comment;

                case "String":
                    return String;

                case "Number":
                    return Number;

                case "Variable":
                    return Variable;

                case "Keyword":
                    return Keyword;

                case "Constant":
                    return Constant;

                case "Storage":
                    return Storage;

                case "TagBracket":
                    return TagBracket;

                case "TagName":
                    return TagName;

                case "ClassName":
                    return ClassName;

                case "FunctionName":
                    return FunctionName;

                case "FunctionArgument":
                    return FunctionArgument;

                case "Char":
                    return Punctuation;

                case "Attribute":
                    return AttributeName;

                case "AttributeValue":
                    return AttributeValue;

                case "CSSProperty":
                    return CSSProperty;

                case "CSSSelector":
                    return CSSSelector;

                case "CSSPropertyValue":
                    return CSSPropertyValue;

                case "Preprocessor":
                    return Preprocessor;

                case "LibraryClass":
                    return LibraryClass;

                case "LibraryFunction":
                    return LibraryFunction;
            }
            return null;
        }

        #endregion From File

        #region Private Variables / Methods

        private Regex _cClassNameRegex;
        private Regex _cCommentRegex1;
        private Regex _cCommentRegex2;
        private Regex _cFunctionsRegex;
        private Regex _cKeywordRegex;
        private Regex _cNumberRegex;

        private Regex _cSharpAttributeRegex,
            _cSharpClassNameRegex;

        private Regex _cSharpCommentRegex1,
            _cSharpCommentRegex2,
            _cSharpCommentRegex3;

        private Regex _cSharpKeywordRegex;

        private Regex _cSharpNumberRegex;
        private Regex _cSharpStorageRegex;
        private Regex _cSharpStringRegex;
        private Regex _cStorageRegex;
        private Regex _cStringRegex;
        private Regex _commentRegex3;
        private Regex _csharpFunctionRegex;

        /// <summary>
        ///     Regexes
        /// </summary>
        private Regex _cssCommentRegex2;

        private Regex _cssCommentRegex3;
        private Regex _cssNumberRegex;
        private Regex _cssPropertyRegex;
        private Regex _cssPropertyValueRegex;
        private Regex _cssSelectorRegex;

        private Regex _htmlAttrRegex,
            _htmlAttrValRegex,
            _htmlCommentRegex1,
            _htmlCommentRegex2;

        private Regex _htmlEndTagRegex;
        private Regex _htmlEntityRegex;
        private Regex _htmlTagNameRegex;
        private Regex _htmlTagRegex;

        private Regex _jScriptCommentRegex1,
            _jScriptCommentRegex2,
            _jScriptCommentRegex3;

        private Regex _jScriptFunctionRegex,
            _jScriptFunctionRegex2;

        private Regex _jScriptKeywordRegex;
        private Regex _jScriptLibraryClass;
        private Regex _jScriptNumberRegex;
        private Regex _jScriptStringRegex;

        private Regex _javaAttributeRegex,
            _javaClassNameRegex;

        private Regex _javaCommentRegex1,
            _javaCommentRegex2,
            _javaCommentRegex3;

        private Regex _javaFunctionRegex;

        private Regex _javaKeywordRegex;

        private Regex _javaKeywordRegex2;
        private Regex _javaNumberRegex;
        private Regex _javaStorageRegex;
        private Regex _javaStringRegex;
        private Regex _jscriptLibraryFunction;
        private Regex _objCClassNameRegex;

        private Regex _objCCommentRegex1,
            _objCCommentRegex2,
            _objCCommentRegex3;

        private Regex _objCFunctionsRegex;
        private Regex _objCKeywordRegex;
        private Regex _objCKeywordRegex2;
        private Regex _objCNumberRegex;
        private Regex _objCPreprocessorRegex;
        private Regex _objCStringRegex;
        private Regex _objCStringRegex2;

        private Regex _phpCommentRegex1;

        private Regex _phpCommentRegex2;

        private Regex _phpCommentRegex3;

        private Regex _phpKeywordRegex;

        private Regex _phpNumberRegex;
        private Regex _phpStringRegex;
        private Regex _phpVarRegex;

        private Regex _sqlCommentRegex1,
            _sqlCommentRegex2,
            _sqlCommentRegex3;

        private Regex _sqlFunctionsRegex;
        private Regex _sqlKeywordsRegex;
        private Regex _sqlNumberRegex;
        private Regex _sqlStatementsRegex;
        private Regex _sqlStringRegex;
        private Regex _sqlTypesRegex;
        private Regex _sqlVarRegex;
        private Regex _vbClassNameRegex;
        private Regex _vbCommentRegex;
        private Regex _vbKeywordRegex;
        private Regex _vbNumberRegex;
        private Regex _vbStringRegex;
        private Regex pyClassNameRegex;

        private Regex pyCommentRegex,
            pyCommentRegex2,
            pyCommentRegex3;

        private Regex pyFunctionRegex;

        private Regex pyKeywordRegex,
            pyLibClassName;

        private Regex pyLibFunctionRegex;
        private Regex pyNumberRegex;
        private Regex pyStringRegex;

        /// <summary>
        ///     Init CSS Regex
        /// </summary>
        private void InitCssRegex()
        {
            _cssCommentRegex2 = new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            _cssCommentRegex3 = new Regex(@"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);
            _cssNumberRegex = new Regex(@"\d+[\.]?\d*(pt|px|\%|em)?");
            _cssPropertyRegex = new Regex(@"\b.*:");
            _cssSelectorRegex =
                new Regex(
                    @"\b(a|abbr|acronym|address|applet|area|article|aside|audio|b|base|basefont|bdi|bdo|big|blockquote|body|br|button|canvas|caption|center|cite|code|col|colgroup|command|datalist|dd|del|details|dfn|dialog|dir|div|dl|dt|em|embed|fieldset|figcaption|figure|font|footer|form|frame|frameset|h1|h2|h3|h4|h5|h6|head|header|hgroup|hr|html|i|iframe|img|input|ins|kbd|keygen|label|legend|li|link|map|mark|menu|meta|meter|nav|noframes|noscript|object|ol|optgroup|option|output|p|param|pre|progress|q|rp|rt|ruby|s|samp|script|section|select|small|source|span|strike|strong|style|sub|summary|sup|table|tbody|td|textarea|tfoot|th|thead|time|title|tr|track|tt|u|ul|var|video|wbr)\b|[#@\.][\w\-]+\b",
                    RegexOptions.IgnoreCase);
            _cssPropertyValueRegex =
                new Regex(
                    @"\b(absolute|always|armenian|auto|avoid|baseline|bidi-override|blink|block|bold|bolder|both|bottom|capitalize|caption|center|circle|close-quote|collapse|crosshair|cursive|dashed|decimal|decimal-leading-zero|default|disc|dotted|double|e-resize|embed|even|fantasy|fixed|georgian|groove|help|hidden|hide|icon|inherit|inline|inline-block|inline-table|inset|inside|italic|justify|large|left|lighter|line-through|list-item|lower-alpha|lower-greek|lower-latin|lower-roman|lowercase|ltr|medium|menu|message-box|middle|monospace|move|n-resize|ne-resize|no-close-quote|no-open-quote|no-repeat|none|normal|nowrap|nw-resize|oblique|odd|open-quote|outset|outside|overline|pointer|pre|pre-line|pre-wrap|progress|relative|repeat|repeat-x|repeat-y|ridge|right|rtl|s-resize|sans-serif|scroll|se-resize|separate|serif|show|small|small-caps|small-caption|solid|square|static|status-bar|sub|super|sw-resize|table|table-caption|table-cell|table-column|table-column-group|table-footer-group|table-header-group|table-row|table-row-group|text|text-bottom|text-top|thick|thin|top|transparent|underline|upper-alpha|upper-latin|upper-roman|uppercase|visible|w-resize|wait|x-large|x-small|xx-large|xx-small|break-word|unrestricted|Andale|Arial|Helvetica|Verdana|Cambria|Calibri|Candara|Century|Courier|Consolata|DejaVu|Droid|Garamond|Gentium|Geneva|Georgia|Klavika|Liberation|Lucida|Monaco|Myriad|Palatino|Symbol|Tahoma|Times|Univers|Wingdings|Sans|Serif|Mono|Math|New|Heavy|Bd|Md|Regular|Rg|Light|Lt|Unicode|Share-Regular|Share-TechMono|Delicious|aliceblue|antiquewhite|aqua|aquamarine|azure|beige|bisque|black|blanchedalmond|blue|blueviolet|brown|burlywood|cadetblue|chartreuse|chocolate|coral|cornflowerblue|cornsilk|crimson|cyan|darkblue|darkcyan|darkgoldenrod|darkgray|darkgreen|darkgrey|darkkhaki|darkmagenta|darkolivegreen|darkorange|darkorchid|darkred|darksalmon|darkseagreen|darkslateblue|darkslategray|darkslategrey|darkturquoise|darkviolet|deeppink|deepskyblue|dimgray|dimgrey|dodgerblue|firebrick|floralwhite|forestgreen|fuchsia|gainsboro|ghostwhite|gold|goldenrod|gray|green|greenyellow|grey|honeydew|hotpink|indianred|indigo|ivory|khaki|lavender|lavenderblush|lawngreen|lemonchiffon|lightbluelightcorallightcyanlightgoldenrodyellowlightgray|lightgreen|lightgrey|lightpink|lightsalmon|lightseagreen|lightskyblue|lightslategray|lightslategrey|lightsteelblue|lightyellow|lime|limegreen|linen|magenta|maroon|mediumaquamarine|mediumblue|mediumorchid|mediumpurple|mediumseagreen|mediumslateblue|mediumspringgreenmediumturquoise|mediumvioletred|midnightblue|mintcream|mistyrose|moccasin|navajowhite|navy|oldlace|olive|olivedrab|orange|orangered|orchid|palegoldenrod|palegreen|paleturquoise|palevioletred|papayawhip|peachpuff|peru|pink|plum|powderblue|purple|red|rosybrown|royalblue|saddlebrown|salmon|sandybrown|seagreen|seashell|sienna|silver|skyblue|slateblue|slategray|slategrey|snow|springgreen|steelblue|tan|teal|thistle|tomato|turquoise|violet|wheat|white|whitesmoke|yellow|yellowgreen)\b",
                    RegexOptions.IgnoreCase);
        }

        /// <summary>
        ///     Highlight CSS Syntax
        /// </summary>
        /// <param name="e"></param>
        private void CssHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "/*";
            e.ChangedRange.ClearStyle(CSSProperty, CSSSelector, CSSPropertyValue,
                Number);
            e.ChangedRange.tb.Range.ClearStyle(Comment);
            e.ChangedRange.tb.RightBracket = '}';
            e.ChangedRange.tb.LeftBracket2 = '(';
            e.ChangedRange.tb.RightBracket2 = ')';
            e.ChangedRange.tb.LeftBracket = '{';
            if (_cssCommentRegex2 == null)
                InitCssRegex();
            e.ChangedRange.tb.Range.SetStyle(Comment, _cssCommentRegex2);
            e.ChangedRange.tb.Range.SetStyle(Comment, _cssCommentRegex3);
            e.ChangedRange.SetStyle(CSSProperty, _cssPropertyRegex);
            e.ChangedRange.SetStyle(CSSSelector, _cssSelectorRegex);
            e.ChangedRange.SetStyle(CSSPropertyValue, _cssPropertyValueRegex);
            e.ChangedRange.SetStyle(Number, _cssNumberRegex);
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers("{", "}");
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
        }

        private void BatchSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "rem";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.ClearStyle(Comment, String, Keyword, Constant, Punctuation);
            e.ChangedRange.SetStyle(Comment, @"rem .*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(String, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
            e.ChangedRange.SetStyle(Keyword,
                @"\b(title|set|call|copy|exists|cut|cd|@|nul|choice|do|shift|sgn|errorlevel|con|prn|lpt1|echo|off|for|in|do|goto|if|then|else|not|end)\b",
                RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(Constant, @"%.*?[^\\]%", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(Punctuation, @"\;|-|>|<|=|\+|\,|\$|\^|\[|\]|\$|:|\!");
        }

        private void InitJavaRegex()
        {
            _javaStringRegex = new Regex(@"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
            _javaCommentRegex1 = new Regex(@"//.*$", RegexOptions.Multiline);
            _javaCommentRegex2 = new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            _javaCommentRegex3 = new Regex(@"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);
            _javaNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            _javaAttributeRegex = new Regex(@"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline);
            _javaClassNameRegex = new Regex(@"\b(class|enum|interface)\s+(?<range>\w+?)\b");
            _javaKeywordRegex =
                new Regex(
                    @"\b(abstract|break|case|catch|class|continue|default|do|else|extends|final|synchronized|return|transient|return|finally|for|if|implements|import|instanceof|interface|native|new|volatile|package|private|protected|public|static|super|switch|this|throw|throws|try|while)\b");
            _javaStorageRegex =
                new Regex(
                    @"\b(void|double|short|long|float|char|boolean|byte|int|String)\b");
            _javaFunctionRegex = new Regex(@"\b(void|extends)\s+(?<range>\w+?)\b");
        }

        /// <summary>
        ///     Java Highlight
        /// </summary>
        /// <param name="e"></param>
        private void JavaSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "//";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '{';
            e.ChangedRange.tb.RightBracket2 = '}';
            if (_javaStringRegex == null)
                InitJavaRegex();
            e.ChangedRange.tb.Range.ClearStyle(Comment);
            e.ChangedRange.ClearStyle(Preprocessor, Comment, String, Number, ClassName, FunctionName, Keyword, Storage,
                Constant, LibraryClass, Punctuation);
            e.ChangedRange.tb.Range.SetStyle(Comment, _javaCommentRegex1);
            e.ChangedRange.tb.Range.SetStyle(Comment, _javaCommentRegex2);
            e.ChangedRange.tb.Range.SetStyle(Comment, _javaCommentRegex3);
            //preprocessor
            e.ChangedRange.SetStyle(Preprocessor, @"#.*$", RegexOptions.Multiline);
            //string
            e.ChangedRange.SetStyle(String, _javaStringRegex);
            //attribute highlighting
            e.ChangedRange.SetStyle(Comment, _javaAttributeRegex);
            //class name highlighting
            e.ChangedRange.SetStyle(ClassName, _javaClassNameRegex);
            e.ChangedRange.SetStyle(FunctionName, _javaFunctionRegex);
            //keyword highlighting
            e.ChangedRange.SetStyle(Keyword, _javaKeywordRegex);
            e.ChangedRange.SetStyle(Storage, _javaStorageRegex);
            e.ChangedRange.SetStyle(Constant, @"\b(true|false|null)\b");
            e.ChangedRange.SetStyle(LibraryClass,
                new Regex(
                    @"\b(AWTError|AWTEvent|AWTEventListener|AWTEventMulticaster|AWTException|AWTPermission|AbstractCollection|AbstractList|AbstractMap|AbstractMethodError|AbstractSequentialList|AbstractSet|AccessControlContext|AccessControlException|AccessController|AccessException|AccessibleObject|Acl|AclEntry|AclNotFoundException|ActionEvent|ActionListener|Activatable|ActivateFailedException|ActivationDesc|ActivationException|ActivationGroup|ActivationGroupDesc|ActivationGroupID|ActivationGroup_Stub|ActivationID|ActivationInstantiator|ActivationMonitor|ActivationSystem|Activator|ActiveEvent|Adjustable|AdjustmentEvent|AdjustmentListener|Adler32|AffineTransform|AffineTransformOp|AlgorithmParameterGenerator|AlgorithmParameterGeneratorSpi|AlgorithmParameterSpec|AlgorithmParameters|AlgorithmParametersSpi|AllPermission|AllPermissionCollection|AlphaComposite|AlphaCompositeContext|AlreadyBoundException|Annotation|Applet|AppletContext|AppletInitializer|AppletStub|Arc2D|ArcIterator|Area|AreaAveragingScaleFilter|AreaIterator|ArithmeticException|Array|Array|ArrayIndexOutOfBoundsException|ArrayList|ArrayStoreException|Arrays|AttributeEntry|AttributedCharacterIterator|AttributedString|Attributes|AudioClip|Authenticator|Autoscroll|BandCombineOp|BandedSampleModel|BasicPermission|BasicPermissionCollection|BasicStroke|BatchUpdateException|BeanContext|BeanContextChild|BeanContextChildComponentProxy|BeanContextChildSupport|BeanContextContainerProxy|BeanContextEvent|BeanContextMembershipEvent|BeanContextMembershipListener|BeanContextProxy|BeanContextServiceAvailableEvent|BeanContextServiceProvider|BeanContextServiceProviderBeanInfo|BeanContextServiceRevokedEvent|BeanContextServiceRevokedListener|BeanContextServices|BeanContextServicesListener|BeanContextServicesSupport|BeanContextSupport|BeanDescriptor|BeanInfo|Beans|BeansAppletContext|BeansAppletStub|BigDecimal|BigInteger|BindException|BitSet|Blob|Book|Boolean|BorderLayout|BreakIterator|BufferedImage|BufferedImageFilter|BufferedImageOp|BufferedInputStream|BufferedOutputStream|BufferedReader|BufferedWriter|Button|ButtonPeer|Byte|ByteArrayInputStream|ByteArrayOutputStream|ByteLookupTable|CMMException|CRC32|CRL|CRLException|Calendar|CallableStatement|Canvas|CanvasPeer|CardLayout|Certificate|Certificate|CertificateEncodingException|CertificateException|CertificateExpiredException|CertificateFactory|CertificateFactorySpi|CertificateNotYetValidException|CertificateParsingException|CharArrayReader|CharArrayWriter|CharConversionException|Character|CharacterBreakData|CharacterIterator|Checkbox|CheckboxGroup|CheckboxMenuItem|CheckboxMenuItemPeer|CheckboxPeer|CheckedInputStream|CheckedOutputStream|Checksum|Choice|ChoiceFormat|ChoicePeer|Class|ClassCastException|ClassCircularityError|ClassFormatError|ClassLoader|ClassNotFoundException|Clipboard|ClipboardOwner|Clob|CloneNotSupportedException|Cloneable|CodeSource|CollationElementIterator|CollationKey|CollationRules|Collator|Collection|Collections|Color|ColorConvertOp|ColorModel|ColorPaintContext|ColorSpace|CompactByteArray|CompactCharArray|CompactIntArray|CompactShortArray|CompactStringArray|Comparable|Comparator|Compiler|Component|ComponentAdapter|ComponentColorModel|ComponentEvent|ComponentListener|ComponentOrientation|ComponentPeer|ComponentSampleModel|Composite|CompositeContext|ConcurrentModificationException|Conditional|ConnectException|ConnectException|ConnectIOException|Connection|Constructor|Container|ContainerAdapter|ContainerEvent|ContainerListener|ContainerPeer|ContentHandler|ContentHandlerFactory|ContextualRenderedImageFactory|ConvolveOp|CropImageFilter|CubicCurve2D|CubicIterator|Cursor|Customizer|DGC|DSAKey|DSAKeyPairGenerator|DSAParameterSpec|DSAParams|DSAPrivateKey|DSAPrivateKeySpec|DSAPublicKey|DSAPublicKeySpec|DataBuffer|DataBufferByte|DataBufferInt|DataBufferShort|DataBufferUShort|DataFlavor|DataFormatException|DataInput|DataInputStream|DataOutput|DataOutputStream|DataTruncation|DatabaseMetaData|DatagramPacket|DatagramSocket|DatagramSocketImpl|Date|DateFormat|DateFormatSymbols|DateFormatZoneData|DateFormatZoneData_en|DecimalFormat|DecimalFormatSymbols|Deflater|DeflaterOutputStream|DesignMode|Dialog|DialogPeer|Dictionary|DigestException|DigestInputStream|DigestOutputStream|DigitList|Dimension|Dimension2D|DirectColorModel|DnDConstants|Double|DragGestureEvent|DragGestureListener|DragGestureRecognizer|DragSource|DragSourceContext|DragSourceContextPeer|DragSourceDragEvent|DragSourceDropEvent|DragSourceEvent|DragSourceListener|Driver|DriverInfo|DriverManager|DriverPropertyInfo|DropTarget|DropTargetContext|DropTargetContextPeer|DropTargetDragEvent|DropTargetDropEvent|DropTargetEvent|DropTargetListener|DropTargetPeer|EOFException|Ellipse2D|EllipseIterator|EmptyStackException|EncodedKeySpec|EntryPair|Enumeration|Error|Event|EventDispatchThread|EventListener|EventObject|EventQueue|EventQueueItem|EventSetDescriptor|Exception|ExceptionInInitializerError|ExportException|Externalizable|FDBigInt|FactoryURLClassLoader|FeatureDescriptor|Field|FieldPosition|File|FileDescriptor|FileDialog|FileDialogPeer|FileFilter|FileInputStream|FileNameMap|FileNotFoundException|FileOutputStream|FilePermission|FilePermissionCollection|FileReader|FileSystem|FileWriter|FilenameFilter|FilterInputStream|FilterOutputStream|FilterReader|FilterWriter|FilteredImageSource|FinalReference|Finalizer|FlatteningPathIterator|FlavorMap|Float|FloatingDecimal|FlowLayout|FocusAdapter|FocusEvent|FocusListener|FocusManager|Font|FontMetrics|FontPeer|FontRenderContext|Format|Frame|FramePeer|GZIPInputStream|GZIPOutputStream|GeneralPath|GeneralPathIterator|GeneralSecurityException|GenericBeanInfo|GlyphJustificationInfo|GlyphMetrics|GlyphVector|GradientPaint|GradientPaintContext|GraphicAttribute|Graphics|Graphics2D|GraphicsConfigTemplate|GraphicsConfiguration|GraphicsDevice|GraphicsEnvironment|GregorianCalendar|GridBagConstraints|GridBagLayout|GridBagLayoutInfo|GridLayout|Group|Guard|GuardedObject|HashMap|HashSet|Hashtable|HttpURLConnection|ICC_ColorSpace|ICC_Profile|ICC_ProfileGray|ICC_ProfileRGB|IOException|Identity|IdentityScope|IllegalAccessError|IllegalAccessException|IllegalArgumentException|IllegalComponentStateException|IllegalMonitorStateException|IllegalPathStateException|IllegalStateException|IllegalThreadStateException|Image|ImageConsumer|ImageFilter|ImageGraphicAttribute|ImageMediaEntry|ImageObserver|ImageProducer|ImagingOpException|IncompatibleClassChangeError|IndexColorModel|IndexOutOfBoundsException|IndexedPropertyDescriptor|InetAddress|InetAddressImpl|Inflater|InflaterInputStream|InheritableThreadLocal|InputContext|InputEvent|InputMethodEvent|InputMethodHighlight|InputMethodListener|InputMethodRequests|InputStream|InputStreamReader|InputSubset|Insets|InstantiationError|InstantiationException|IntHashtable|Integer|InternalError|InterruptedException|InterruptedIOException|IntrospectionException|Introspector|InvalidAlgorithmParameterException|InvalidClassException|InvalidDnDOperationException|InvalidKeyException|InvalidKeySpecException|InvalidObjectException|InvalidParameterException|InvalidParameterSpecException|InvocationEvent|InvocationTargetException|ItemEvent|ItemListener|ItemSelectable|Iterator|JarEntry|JarException|JarFile|JarInputStream|JarOutputStream|JarURLConnection|JarVerifier|Kernel|Key|KeyAdapter|KeyEvent|KeyException|KeyFactory|KeyFactorySpi|KeyListener|KeyManagementException|KeyPair|KeyPairGenerator|KeyPairGeneratorSpi|KeySpec|KeyStore|KeyStoreException|KeyStoreSpi|Label|LabelPeer|LastOwnerException|LayoutManager|LayoutManager2|Lease|LightweightDispatcher|LightweightPeer|LightweightPeer|Line2D|LineBreakData|LineBreakMeasurer|LineIterator|LineMetrics|LineNumberInputStream|LineNumberReader|LinkageError|LinkedList|List|List|ListIterator|ListPeer|ListResourceBundle|LoaderHandler|Locale|LocaleData|LocaleElements|LocaleElements_en|LocaleElements_en_US|LocateRegistry|LogStream|Long|LookupOp|LookupTable|MalformedURLException|Manifest|Map|MarshalException|MarshalledObject|Math|MediaEntry|MediaTracker|Member|MemoryImageSource|Menu|MenuBar|MenuBarPeer|MenuComponent|MenuComponentPeer|MenuContainer|MenuItem|MenuItemPeer|MenuPeer|MenuShortcut|MergeCollation|MessageDigest|MessageDigestSpi|MessageFormat|Method|MethodDescriptor|MimeType|MimeTypeParameterList|MimeTypeParseException|MissingResourceException|Modifier|MouseAdapter|MouseDragGestureRecognizer|MouseEvent|MouseListener|MouseMotionAdapter|MouseMotionListener|MultiPixelPackedSampleModel|MulticastSocket|MultipleMaster|Naming|NativeLibLoader|NegativeArraySizeException|NetPermission|NoClassDefFoundError|NoRouteToHostException|NoSuchAlgorithmException|NoSuchElementException|NoSuchFieldError|NoSuchFieldException|NoSuchMethodError|NoSuchMethodException|NoSuchObjectException|NoSuchProviderException|NoninvertibleTransformException|Normalizer|NotActiveException|NotBoundException|NotOwnerException|NotSerializableException|NullPointerException|Number|NumberFormat|NumberFormatException|ObjID|Object|ObjectInput|ObjectInputStream|ObjectInputStreamWithLoader|ObjectInputValidation|ObjectOutput|ObjectOutputStream|ObjectStreamClass|ObjectStreamConstants|ObjectStreamException|ObjectStreamField|Observable|Observer|OpenType|Operation|OptionalDataException|OutOfMemoryError|OutputStream|OutputStreamWriter|Owner|PKCS8EncodedKeySpec|Package|PackedColorModel|PageFormat|Pageable|Paint|PaintContext|PaintEvent|Panel|PanelPeer|Paper|ParameterBlock|ParameterDescriptor|ParseException|ParsePosition|PasswordAuthentication|PathIterator|PatternEntry|PeerFixer|Permission|Permission|PermissionCollection|Permissions|PermissionsEnumerator|PermissionsHash|PhantomReference|PipedInputStream|PipedOutputStream|PipedReader|PipedWriter|PixelGrabber|PixelInterleavedSampleModel|PlainDatagramSocketImpl|PlainSocketImpl|Point|Point2D|Policy|Polygon|PopupMenu|PopupMenuPeer|PreparedStatement|Principal|PrintGraphics|PrintJob|PrintStream|PrintWriter|Printable|PrinterAbortException|PrinterException|PrinterGraphics|PrinterIOException|PrinterJob|PrivateKey|PrivilegedAction|PrivilegedActionException|PrivilegedExceptionAction|Process|ProfileDataException|Properties|PropertyChangeEvent|PropertyChangeListener|PropertyChangeSupport|PropertyDescriptor|PropertyEditor|PropertyEditorManager|PropertyEditorSupport|PropertyPermission|PropertyPermissionCollection|PropertyResourceBundle|PropertyVetoException|ProtectionDomain|ProtocolException|Provider|ProviderException|PublicKey|PushbackInputStream|PushbackReader|QuadCurve2D|QuadIterator|Queue|RGBImageFilter|RMIClassLoader|RMIClientSocketFactory|RMIFailureHandler|RMISecurityException|RMISecurityManager|RMIServerSocketFactory|RMISocketFactory|RSAPrivateCrtKey|RSAPrivateCrtKeySpec|RSAPrivateKey|RSAPrivateKeySpec|RSAPublicKey|RSAPublicKeySpec|Random|RandomAccessFile|Raster|RasterFormatException|RasterOp|Reader|RectIterator|Rectangle|Rectangle2D|RectangularShape|Ref|Reference|ReferenceQueue|ReflectPermission|Registry|RegistryHandler|Remote|RemoteCall|RemoteException|RemoteObject|RemoteRef|RemoteServer|RemoteStub|RenderContext|RenderableImage|RenderableImageOp|RenderableImageProducer|RenderedImage|RenderedImageFactory|RenderingHints|ReplicateScaleFilter|RescaleOp|ResourceBundle|ResultSet|ResultSetMetaData|RoundRectIterator|RoundRectangle2D|RuleBasedCollator|Runnable|Runtime|RuntimeException|RuntimePermission|SQLData|SQLException|SQLInput|SQLOutput|SQLWarning|SampleModel|ScrollPane|ScrollPaneAdjustable|ScrollPanePeer|Scrollbar|ScrollbarPeer|SecureClassLoader|SecureRandom|SecureRandomSpi|Security|SecurityException|SecurityManager|SecurityPermission|SentenceBreakData|SequenceInputStream|Serializable|SerializablePermission|ServerCloneException|ServerError|ServerException|ServerNotActiveException|ServerRef|ServerRuntimeException|ServerSocket|Set|Shape|ShapeGraphicAttribute|Short|ShortLookupTable|Signature|SignatureException|SignatureSpi|SignedObject|Signer|SimpleBeanInfo|SimpleDateFormat|SimpleTextBoundary|SimpleTimeZone|SinglePixelPackedSampleModel|Skeleton|SkeletonMismatchException|SkeletonNotFoundException|Socket|SocketException|SocketImpl|SocketImplFactory|SocketInputStream|SocketOptions|SocketOutputStream|SocketPermission|SocketPermissionCollection|SocketSecurityException|SoftReference|SortedMap|SortedSet|SpecialMapping|Stack|StackOverflowError|Statement|StreamCorruptedException|StreamTokenizer|String|StringBuffer|StringBufferInputStream|StringCharacterIterator|StringIndexOutOfBoundsException|StringReader|StringSelection|StringTokenizer|StringWriter|Stroke|Struct|StubNotFoundException|SubList|SyncFailedException|System|SystemColor|SystemFlavorMap|TextArea|TextAreaPeer|TextAttribute|TextBoundaryData|TextComponent|TextComponentPeer|TextEvent|TextField|TextFieldPeer|TextHitInfo|TextJustifier|TextLayout|TextLine|TextListener|TextMeasurer|TexturePaint|TexturePaintContext|Thread|ThreadDeath|ThreadGroup|ThreadLocal|Throwable|TileObserver|Time|TimeZone|TimeZoneData|Timestamp|TooManyListenersException|Toolkit|Transferable|TransformAttribute|Transparency|TreeMap|TreeSet|Types|UID|URL|URLClassLoader|URLConnection|URLDecoder|URLEncoder|URLStreamHandler|URLStreamHandlerFactory|UTFDataFormatException|UnexpectedException|UnicastRemoteObject|UnicodeClassMapping|UnknownContentHandler|UnknownError|UnknownGroupException|UnknownHostException|UnknownHostException|UnknownObjectException|UnknownServiceException|UnmarshalException|UnrecoverableKeyException|Unreferenced|UnresolvedPermission|UnresolvedPermissionCollectio|UnsatisfiedLinkError|UnsupportedClassVersionError|UnsupportedEncodingException|UnsupportedFlavorException|UnsupportedOperationException|Utility|VMID|ValidationCallback|Vector|VerifyError|VetoableChangeListener|VetoableChangeSupport|VirtualMachineError|Visibility|Void|WeakHashMap|WeakReference|Win32FileSystem|Win32Process|Window|WindowAdapter|WindowEvent|WindowListener|WindowPeer|WordBreakData|WordBreakTable|WritableRaster|WritableRenderedImage|WriteAbortedException|Writer|X509CRL|X509CRLEntry|X509Certificate|X509EncodedKeySpec|X509Extension|ZipConstants|ZipEntry|ZipException|ZipFile|ZipInputStream|ZipOutputStream)\b"));
            //number highlighting
            e.ChangedRange.SetStyle(Number, _javaNumberRegex);
            e.ChangedRange.SetStyle(Punctuation, @"\;|-|>|<|=|\+|\,|\$|\^|\[|\]");
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers("{", "}");
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
        }

        /// <summary>
        ///     QBasic Syntax Highlight
        /// </summary>
        /// <param name="e"></param>
        private void QBasicHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.ClearStyle(Comment, String, Keyword, LibraryFunction, Variable, Number);
            e.ChangedRange.SetStyle(Comment, @"\'.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(String, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
            e.ChangedRange.SetStyle(LibraryFunction,
                @"\b(ABS|AND|AS|ASC|ATN|BASE|BEEP|BLOAD|BSAVE|CDBL|CHR\$|CINT|CIRCLE|CLEAR|CLNG|CLS|COLOR|COM|COMMAND$|CONST|COS|CSNG|CSRLIN|CSRUN|CVD|CVDMBF|CVI|CVL|CVS|CVSMBF|DATA|DATE$|DEF|DEFDBL|DEFINT|DEFLNG|DEFSNG|DEFSTR|DIM|DOUBLE|DRAW|ENVIRON|ENVIRON$|EQV|ERASE|ERDEV|ERDEV$|ERL|ERR|ERROR|EXP|FIX|FN|FRE|HEX$|IMP|INSTR|INT|INTEGER|KEY|LBOUND|LCASE$|LEFT$|LEN|LET|LINE|LIST|LOCATE|LOG|LONG|LPOS|LTRIM$|MID$|MKD$|MKDMBF$|MKI$|MKL$|MKS$|MOD|NOT|OCT$|OFF|ON|OPEN|OPTION|OR|OUT|OUTPUT|PAINT|PALETTE|PCOPY|PEEK|PEN|PLAY|PMAP|POINT|POKE|POS|PRESET|PSET|RANDOMIZE|READ|REDIM|RESTORE|RIGHT$|RND|RTRIM$|SADD|SCREEN|SEEK|SEG|SETMEM|SGN|SIGNAL|SIN|SINGLE|SLEEP|SOUND|SPACE$|SPC|SQR|STATIC|STICK|STOP|STR$|STRIG|STRING|STRING$|SWAP|TAB|TAN|TIME$|TIMER|TROFF|TRON|TYPE|UBOUND|UCASE$|UEVENT|VAL|VARPTR|VARPTR$|VARSEG|VIEW|WIDTH|WINDOW|XOR)\b",
                RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(Keyword,
                @"\b(ABSOLUTE|ALIAS|BYVAL|CALL|CALLS|CASE|CDECL|CHAIN|COMMON|DO|DECLARE|ELSE|ELSEIF|END|ENDIF|EXIT|FOR|FUNCTION|GOSUB|GOTO|IF|INT86OLD|INT86XOLD|INTERUPT|INTERUPTX|IS|LOCAL|LOOP|MERGE|NEXT|RESUME|RETURN|RUN|SELECT|SHARED|SHELL|STEP|SUB|SYSTEM|THEN|TO|UNTIL|WEND|WHILE|ACCESS|ANY|APPEND|BINARY|CHDIR|CLOSE|EOF|FIELD|FILEATTR|FILES|FREEFILE|GET|INKEY$|INP|INPUT|IOCTL|IOCTL$|KILL|LINE|LOC|LOCK|LOF|LPRINT|LSET|MKDIR|NAME|OPEN|OUT|OUTPUT|PRINT|PUT|RANDOM|RESET|RMDIR|RSET|SEEK|UNLOCK|USING|WAIT|WIDTH|WRITE)\b",
                RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(Variable, @"\$[a-zA-Z_\d]*\b", RegexCompiledOption);
            e.ChangedRange.SetStyle(Number, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
        }

        /// <summary>
        ///     Initializes C++ Regex
        /// </summary>
        private void InitCRegex()
        {
            _cStringRegex = new Regex(
                @"
                            # Character definitions:
                            '
                            (?> # disable backtracking
                              (?:
                                \\[^\r\n]|    # escaped meta char
                                [^'\r\n]      # any character except '
                              )*
                            )
                            '?
                            |
                            # Normal string & verbatim strings definitions:
                            (?<verbatimIdentifier>@)?         # this group matches if it is an verbatim string
                            ""
                            (?> # disable backtracking
                              (?:
                                # match and consume an escaped character including escaped double quote ("") char
                                (?(verbatimIdentifier)        # if it is a verbatim string ...
                                  """"|                         #   then: only match an escaped double quote ("") char
                                  \\.                         #   else: match an escaped sequence
                                )
                                | # OR

                                # match any char except double quote char ("")
                                [^""]
                              )*
                            )
                            ""
                        ",
                RegexOptions.ExplicitCapture | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace |
                RegexCompiledOption
                );
            _cCommentRegex1 = new Regex(@"//.*$", RegexOptions.Multiline);
            _cCommentRegex2 = new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            _commentRegex3 = new Regex(@"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);
            _cNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            _cStorageRegex =
                new Regex(
                    @"\b(asm|__asm__|auto|bool|_Bool|char|_Complex|double|enum|float|_Imaginary|int|class|long|short|signed|struct|typedef|union|unsigned|void)\b");
            _cClassNameRegex = new Regex(@"\b(class|struct|enum|interface)\s+(?<range>\w+?)\b");
            _cKeywordRegex =
                new Regex(
                    @"\b(break|case|continue|default|public|private|do|else|for|goto|if|_Pragma|return|switch|sizeof|while|const|extern|register|restrict|static|volatile|inline)\b");
            _cFunctionsRegex = new Regex(@"\b(void|int|bool|#[define])\s+(?<range>\w+?)\b");
        }

        private void CppSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "//";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.LeftBracket2 = '{';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.RightBracket2 = '}';
            e.ChangedRange.tb.Range.ClearStyle(Comment);
            e.ChangedRange.ClearStyle(String, Preprocessor, FunctionName, Variable, ClassName, Keyword, Variable,
                Constant, Storage, LibraryClass, Number, Punctuation);
            // initialize the regexes if they are null
            if (_cCommentRegex1 == null)
                InitCRegex();
            e.ChangedRange.SetStyle(String, _cStringRegex);
            e.ChangedRange.tb.Range.SetStyle(Comment, _cCommentRegex1);
            e.ChangedRange.tb.Range.SetStyle(Comment, _cCommentRegex2);
            e.ChangedRange.tb.Range.SetStyle(Comment, _commentRegex3);
            // string higlighting between <>
            e.ChangedRange.SetStyle(String, @"(?<=\<)(.*?)(?=\>)|\<|\>");
            // preprocessor highlighting
            e.ChangedRange.SetStyle(Preprocessor, @"#[a-zA-Z_\d]*\b");
            // class name highlight
            e.ChangedRange.SetStyle(ClassName, _cClassNameRegex);
            // function name highlight
            e.ChangedRange.SetStyle(FunctionName, _cFunctionsRegex);
            e.ChangedRange.SetStyle(Variable, @"\*[a-zA-Z_\d]*\b|\b(f|m)[A-Z]\w*\b");
            e.ChangedRange.SetStyle(Keyword,
                @"\b(break|case|continue|default|public|private|do|else|for|goto|if|_Pragma|return|switch|sizeof|while|const|extern|register|restrict|static|volatile|inline|extern|class|wchar_t|template|friend|explicit|virtual|private|protected|public|while|for|do|if|else|switch|catch|enumerate|and|and_eq|bitand|bitor|compl|not|not_eq|or|or_eq|typeid|xor|xor_eq|const_cast|dynamic_cast|reinterpret_cast|static_cast|catch|operator|try|throw|using|delete)\b");
            e.ChangedRange.SetStyle(Constant,
                @"\b(this|nullptr|NULL|true|false|TRUE|FALSE|noErr|kNilOptions|kInvalidID|kVariableLengthArray)\b");
            e.ChangedRange.SetStyle(Storage,
                @"\b(asm|__asm__|auto|bool|_Bool|char|_Complex|double|enum|float|_Imaginary|int|class|long|short|signed|struct|typedef|union|unsigned|void)\b");
            e.ChangedRange.SetStyle(LibraryClass,
                @"\b(u_char|u_short|u_int|u_long|ushort|uint|u_quad_t|quad_t|qaddr_t|caddr_t|daddr_t|dev_t|fixpt_t|blkcnt_t|blksize_t|gid_t|in_addr_t|in_port_t|ino_t|key_t|mode_t|nlink_t|id_t|pid_t|off_t|segsz_t|swblk_t|uid_t|id_t|clock_t|size_t|ssize_t|time_t|useconds_t|suseconds_t|pthread_attr_t|pthread_cond_t|pthread_condattr_t|pthread_mutex_t|pthread_mutexattr_t|pthread_once_t|pthread_rwlock_t|pthread_rwlockattr_t|pthread_t|pthread_key_t|int8_t|int16_t|int32_t|int64_t|uint8_t|uint16_t|uint32_t|uint64_t|int_least8_t|int_least16_t|int_least32_t|int_least64_t|uint_least8_t|uint_least16_t|uint_least32_t|uint_least64_t|int_fast8_t|int_fast16_t|int_fast32_t|int_fast64_t|uint_fast8_t|uint_fast16_t|uint_fast32_t|uint_fast64_t|intptr_t|uintptr_t|intmax_t|intmax_t|uintmax_t|uintmax_t|AbsoluteTime|Boolean|Byte|ByteCount|ByteOffset|BytePtr|CompTimeValue|ConstLogicalAddress|ConstStrFileNameParam|ConstStringPtr|Duration|Fixed|FixedPtr|Float32|Float32Point|Float64|Float80|Float96|FourCharCode|Fract|FractPtr|Handle|ItemCount|LogicalAddress|OptionBits|OSErr|OSStatus|OSType|OSTypePtr|PhysicalAddress|ProcessSerialNumber|ProcessSerialNumberPtr|ProcHandle|Ptr|ResType|ResTypePtr|ShortFixed|ShortFixedPtr|SignedByte|SInt16|SInt32|SInt64|SInt8|Size|StrFileName|StringHandle|StringPtr|TimeBase|TimeRecord|TimeScale|TimeValue|TimeValue64|UInt16|UInt32|UInt64|UInt8|UniChar|UniCharCount|UniCharCountPtr|UniCharPtr|UnicodeScalarValue|UniversalProcHandle|UniversalProcPtr|UnsignedFixed|UnsignedFixedPtr|UnsignedWide|UTF16Char|UTF32Char|UTF8Char)\b");
            e.ChangedRange.SetStyle(Number, _cNumberRegex);
            e.ChangedRange.SetStyle(Punctuation, @"\W_");
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers("{", "}", RegexOptions.IgnoreCase);
            e.ChangedRange.SetFoldingMarkers(@"#if", @"#end");
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/");
        }

        /// <summary>
        ///     C Syntax Highlight
        /// </summary>
        /// <param name="e"></param>
        private void CSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "//";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.LeftBracket2 = '{';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.RightBracket2 = '}';
            e.ChangedRange.tb.Range.ClearStyle(Comment);
            e.ChangedRange.ClearStyle(String, Preprocessor, FunctionName, Variable, ClassName, Keyword, Variable,
                Constant, Storage, LibraryClass, Number, Punctuation);
            // initialize the regexes if they are null
            if (_cCommentRegex1 == null)
                InitCRegex();
            e.ChangedRange.SetStyle(String, _cStringRegex);
            e.ChangedRange.tb.Range.SetStyle(Comment, _cCommentRegex1);
            e.ChangedRange.tb.Range.SetStyle(Comment, _cCommentRegex2);
            e.ChangedRange.tb.Range.SetStyle(Comment, _commentRegex3);
            // string higlighting between <>
            e.ChangedRange.SetStyle(String, @"(?<=\<)(.*?)(?=\>)|\<|\>");
            // preprocessor highlighting
            e.ChangedRange.SetStyle(Preprocessor, @"#[a-zA-Z_\d]*\b");
            // class name highlight
            e.ChangedRange.SetStyle(ClassName, _cClassNameRegex);
            // function name highlight
            e.ChangedRange.SetStyle(FunctionName, _cFunctionsRegex);
            e.ChangedRange.SetStyle(Variable, @"\*[a-zA-Z_\d]*\b");
            e.ChangedRange.SetStyle(Keyword, _cKeywordRegex);
            e.ChangedRange.SetStyle(Constant,
                @"\b(NULL|true|false|TRUE|FALSE|noErr|kNilOptions|kInvalidID|kVariableLengthArray)\b");
            e.ChangedRange.SetStyle(Storage, _cStorageRegex);
            e.ChangedRange.SetStyle(LibraryClass,
                @"\b(u_char|u_short|u_int|u_long|ushort|uint|u_quad_t|quad_t|qaddr_t|caddr_t|daddr_t|dev_t|fixpt_t|blkcnt_t|blksize_t|gid_t|in_addr_t|in_port_t|ino_t|key_t|mode_t|nlink_t|id_t|pid_t|off_t|segsz_t|swblk_t|uid_t|id_t|clock_t|size_t|ssize_t|time_t|useconds_t|suseconds_t|pthread_attr_t|pthread_cond_t|pthread_condattr_t|pthread_mutex_t|pthread_mutexattr_t|pthread_once_t|pthread_rwlock_t|pthread_rwlockattr_t|pthread_t|pthread_key_t|int8_t|int16_t|int32_t|int64_t|uint8_t|uint16_t|uint32_t|uint64_t|int_least8_t|int_least16_t|int_least32_t|int_least64_t|uint_least8_t|uint_least16_t|uint_least32_t|uint_least64_t|int_fast8_t|int_fast16_t|int_fast32_t|int_fast64_t|uint_fast8_t|uint_fast16_t|uint_fast32_t|uint_fast64_t|intptr_t|uintptr_t|intmax_t|intmax_t|uintmax_t|uintmax_t|AbsoluteTime|Boolean|Byte|ByteCount|ByteOffset|BytePtr|CompTimeValue|ConstLogicalAddress|ConstStrFileNameParam|ConstStringPtr|Duration|Fixed|FixedPtr|Float32|Float32Point|Float64|Float80|Float96|FourCharCode|Fract|FractPtr|Handle|ItemCount|LogicalAddress|OptionBits|OSErr|OSStatus|OSType|OSTypePtr|PhysicalAddress|ProcessSerialNumber|ProcessSerialNumberPtr|ProcHandle|Ptr|ResType|ResTypePtr|ShortFixed|ShortFixedPtr|SignedByte|SInt16|SInt32|SInt64|SInt8|Size|StrFileName|StringHandle|StringPtr|TimeBase|TimeRecord|TimeScale|TimeValue|TimeValue64|UInt16|UInt32|UInt64|UInt8|UniChar|UniCharCount|UniCharCountPtr|UniCharPtr|UnicodeScalarValue|UniversalProcHandle|UniversalProcPtr|UnsignedFixed|UnsignedFixedPtr|UnsignedWide|UTF16Char|UTF32Char|UTF8Char)\b");
            e.ChangedRange.SetStyle(Number, _cNumberRegex);
            e.ChangedRange.SetStyle(Punctuation, @"\;|\.|\!|>|<|\:|\?|\/|\+|\-|&|@|~");
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers("{", "}", RegexOptions.IgnoreCase);
            e.ChangedRange.SetFoldingMarkers(@"#if", @"#end");
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/");
        }

        private void InitHtmlRegex()
        {
            _htmlCommentRegex1 = new Regex(@"(<!--.*?-->)|(<!--.*)", RegexOptions.Singleline);
            _htmlCommentRegex2 = new Regex(@"(<!--.*?-->)|(.*-->)", RegexOptions.Singleline | RegexOptions.RightToLeft);
            _htmlTagRegex = new Regex(@"<|/>|</|>|\?");
            _htmlTagNameRegex = new Regex(@"<(?<range>[!\w:]+)");
            _htmlEndTagRegex = new Regex(@"</(?<range>[\w:]+)>");
            _htmlAttrRegex =
                new Regex(
                    @"(?<range>[\w\d\-]{1,20}?)='[^']*'|(?<range>[\w\d\-]{1,20})=""[^""]*""|(?<range>[\w\d\-]{1,20})=[\w\d\-]{1,20}");
            _htmlAttrValRegex =
                new Regex(
                    @"[\w\d\-]{1,20}?=(?<range>'[^']*')|[\w\d\-]{1,20}=(?<range>""[^""]*"")|[\w\d\-]{1,20}=(?<range>[\w\d\-]{1,20})");
            _htmlEntityRegex = new Regex(@"\&(amp|gt|lt|nbsp|quot|apos|copy|reg|#[0-9]{1,8}|#x[0-9a-f]{1,8});",
                RegexOptions.IgnoreCase);
        }

        /// <summary>
        ///     Highlights Xml code
        /// </summary>
        /// <param name="e"></param>
        private void XmlSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "<!--";
            e.ChangedRange.tb.LeftBracket = '<';
            e.ChangedRange.tb.RightBracket = '>';
            e.ChangedRange.tb.LeftBracket2 = '(';
            e.ChangedRange.tb.RightBracket2 = ')';
            e.ChangedRange.tb.Range.ClearStyle(Comment);
            //clear style of changed range
            e.ChangedRange.ClearStyle(Keyword, TagBracket, TagName, AttributeName,
                AttributeValue, DoctypeDeclaration);
            //
            if (_htmlTagRegex == null)
                InitHtmlRegex();
            //comment highlighting
            e.ChangedRange.tb.Range.SetStyle(Comment, _htmlCommentRegex1);
            e.ChangedRange.tb.Range.SetStyle(Comment, _htmlCommentRegex2);
            e.ChangedRange.SetStyle(DoctypeDeclaration, @"<!.*?>", RegexOptions.Singleline);
            //tag brackets highlighting
            e.ChangedRange.SetStyle(TagBracket, _htmlTagRegex);
            //tag name
            e.ChangedRange.SetStyle(TagName, _htmlTagNameRegex);
            //end of tag
            e.ChangedRange.SetStyle(TagName, _htmlEndTagRegex);
            //attributes
            e.ChangedRange.SetStyle(AttributeName, _htmlAttrRegex);
            //attribute values
            e.ChangedRange.SetStyle(AttributeValue, _htmlAttrValRegex);
            //html entity
            e.ChangedRange.SetStyle(Constant, _htmlEntityRegex);
            e.ChangedRange.SetStyle(DoctypeDeclaration, @"\?[a-zA-Z_\d]*\b", RegexOptions.IgnoreCase);

            XmlFold(e.ChangedRange.tb);
        }

        private static void XmlFold(FastColoredTextBox fctb)
        {
            try
            {
                fctb.Range.ClearFoldingMarkers();
                //
                var stack = new Stack<XmlTag>();
                var id = 0;
                var ranges =
                    fctb.Range.GetRanges(new Regex(@"<(?<range>/?\w+)\s[^>]*?[^/]>|<(?<range>/?\w+)\s*>",
                        RegexOptions.Singleline));
                //extract opening and closing tags (exclude open-close tags: <TAG/>)
                foreach (var r in ranges)
                {
                    var tagName = r.Text;
                    var iLine = r.Start.iLine;
                    //if it is opening tag...
                    if (tagName[0] != '/')
                    {
                        // ...push into stack
                        var tag = new XmlTag {Name = tagName, Id = id++, StartLine = r.Start.iLine};
                        stack.Push(tag);
                        // if this line has no markers - set marker
                        if (string.IsNullOrEmpty(fctb[iLine].FoldingStartMarker))
                            fctb[iLine].FoldingStartMarker = tag.Marker;
                    }
                    else
                    {
                        //if it is closing tag - pop from stack
                        var tag = stack.Pop();
                        //compare line number
                        if (iLine == tag.StartLine)
                        {
                            //remove marker, because same line can not be folding
                            if (fctb[iLine].FoldingStartMarker == tag.Marker) //was it our marker?
                                fctb[iLine].FoldingStartMarker = null;
                        }
                        else
                        {
                            //set end folding marker
                            if (string.IsNullOrEmpty(fctb[iLine].FoldingEndMarker))
                                fctb[iLine].FoldingEndMarker = tag.Marker;
                        }
                    }
                }
            }
            catch
            {
                //throw;
            }
        }

        private void InitPythonRegex()
        {
            pyCommentRegex = new Regex(@"#.*$", RegexOptions.Multiline);
            pyCommentRegex2 = new Regex(@"("""""".*?"""""")|('''.*?''')|("""""".*)|('''.*)",
                RegexOptions.Singleline | RegexCompiledOption);
            pyCommentRegex3 = new Regex(@"("""""".*?"""""")|('''.*?''')|(.*"""""")|(.*''')",
                RegexOptions.Singleline | RegexOptions.RightToLeft | RegexCompiledOption);

            pyStringRegex = new Regex(@"""""|''|"".*?[^\\]""|'.*?[^\\]'", RegexCompiledOption);
            pyKeywordRegex =
                new Regex(
                    @"\b(and|del|from|not|while|as|elif|global|or|with|assert|else|if|pass|yield|break|except|import|print|exec|in|raise|continue|finally|is|return|for|try)\b");
            pyLibFunctionRegex =
                new Regex(
                    @"\b(dir|id|callable|dict|open|all|vars|iter|enumerate|sorted|super|classmethod|tuple|compile|basestring|map|self|range|lambda|ord|isinstance|long|float|format|str|type|hasattr|max|len|repr|getattr|list)\b");
            pyLibClassName =
                new Regex(
                    @"\b(self|bool|buffer|set|slice|unicode|property|staticmethod|enumerate|object|open|dict|int|tuple|basestring|long|float|type|list|RuntimeError)\b");
            pyClassNameRegex = new Regex(@"\b(class)\s+(?<range>\w+?)\b");
            pyFunctionRegex = new Regex(@"\b(def)\s+(?<range>\w+?)\b");
            pyNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
        }

        private void PythonSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "#";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket2 = ']';
            if (pyStringRegex == null)
                InitPythonRegex();
            e.ChangedRange.tb.Range.ClearStyle(Comment, String);
            e.ChangedRange.ClearStyle(String, ClassName, FunctionName, Keyword, Storage, Constant, LibraryClass,
                LibraryFunction, Number);
            e.ChangedRange.tb.Range.SetStyle(Comment, pyCommentRegex);
            e.ChangedRange.tb.Range.SetStyle(String, pyStringRegex);
            e.ChangedRange.tb.Range.SetStyle(String, pyCommentRegex2);
            e.ChangedRange.tb.Range.SetStyle(String, pyCommentRegex3);
            e.ChangedRange.SetStyle(ClassName, pyClassNameRegex);
            e.ChangedRange.SetStyle(FunctionName, pyFunctionRegex);
            e.ChangedRange.SetStyle(Keyword, pyKeywordRegex);
            e.ChangedRange.SetStyle(Storage, @"\b(def|global|class)\b");
            e.ChangedRange.SetStyle(Constant, @"\b(True|False|None|NotImplemented)\b");
            e.ChangedRange.SetStyle(LibraryClass, pyLibClassName);
            e.ChangedRange.SetStyle(LibraryFunction, pyLibFunctionRegex);
            e.ChangedRange.SetStyle(Number, pyNumberRegex);
            PythonFold(e.ChangedRange.tb);
        }

        private static void PythonFold(FastColoredTextBox fctb)
        {
            //delete all markers
            fctb.Range.ClearFoldingMarkers();

            var currentIndent = 0;
            var lastNonEmptyLine = 0;

            for (var i = 0; i < fctb.LinesCount; i++)
            {
                var line = fctb[i];
                var spacesCount = line.StartSpacesCount;
                if (spacesCount == line.Count) //empty line
                    continue;

                if (currentIndent < spacesCount)
                    //append start folding marker
                    fctb[lastNonEmptyLine].FoldingStartMarker = "m" + currentIndent;
                else if (currentIndent > spacesCount)
                    //append end folding marker
                    fctb[lastNonEmptyLine].FoldingEndMarker = "m" + spacesCount;

                currentIndent = spacesCount;
                lastNonEmptyLine = i;
            }
        }

        private void FSharpSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "//";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.ClearStyle(Comment, String, Keyword, Constant, Number);
            e.ChangedRange.SetStyle(Comment, @"//.*$");
            e.ChangedRange.SetStyle(Comment, @"(\(\*.*?\*\))|(\(\*.*)");
            e.ChangedRange.SetStyle(String, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
            e.ChangedRange.SetStyle(Keyword,
                @"\b(and|as|begin|do|done|val|stdin|downto|else|mutable|yield|end|exception|for|fun|function|in|if|let|match|module|not|open|of|prefix|raise|rec|struct|then|to|try|type|while|with|override|int|float|ushort|uint|long|byte|sbyte|bool|string|char)\b");
            e.ChangedRange.SetStyle(Constant, @"\b(true|false|null)\b");
            e.ChangedRange.SetStyle(Number, @"\b\\d+[\\.]?\\d*([eE]\\-?\\d+)?[lLdDfF]?\b|\b0x[a-fA-F\\d]+\b");
        }

        /// <summary>
        ///     Highlight INI Syntax
        /// </summary>
        /// <param name="e"></param>
        private void IniSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = ";";
            e.ChangedRange.ClearStyle(Comment, Keyword, String, Number, Punctuation);
            e.ChangedRange.SetStyle(Comment, @"\;.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(Keyword, @"\[.*?[^\\]\]");
            e.ChangedRange.SetStyle(String, "\".*?[^\\\\]\"|\"\"|\'.*?[^\\\\]\'|\'\'");
            e.ChangedRange.SetStyle(Number, "\b\\d+[\\.]?\\d*([eE]\\-?\\d+)?[lLdDfF]?\b|\b0x[a-fA-F\\d]+\b");
            e.ChangedRange.SetStyle(Punctuation, @"\*|\,|\:,\?|\@|\!");
            e.ChangedRange.ClearFoldingMarkers();
            foreach (var r in e.ChangedRange.GetRangesByLines(@"^\[\w+\]$", RegexOptions.None))
            {
                if (r.Start.iLine > 0) r.tb[r.Start.iLine - 1].FoldingEndMarker = "section";
                r.tb[r.Start.iLine].FoldingStartMarker = "section";
            }
        }

        /// <summary>
        ///     Lua Syntax Highlight
        /// </summary>
        /// <param name="e"></param>
        private void LuaSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "--";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.tb.Range.ClearStyle(Comment);
            e.ChangedRange.ClearStyle(String, Keyword, Constant, LibraryClass, LibraryFunction, Variable,
                Number, Punctuation);
            e.ChangedRange.tb.Range.SetStyle(Comment, @"--.*$", RegexOptions.Multiline);
            e.ChangedRange.tb.Range.SetStyle(Comment, @"\-\-\[\[.+?\-\-\]\]",
                RegexOptions.Singleline | RegexOptions.RightToLeft);
            e.ChangedRange.SetStyle(String, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
            e.ChangedRange.SetStyle(Keyword,
                @"\b(and|or|not|break|do|else|for|if|elseif|return|then|repeat|while|until|end|function|local|in)\b");
            e.ChangedRange.SetStyle(Constant, @"\b(false|nil|true|_G|_VERSION)\b");
            e.ChangedRange.SetStyle(LibraryClass, @"\b(coroutine|debug|io|math|os|package|string|table|char|byte)\b");
            e.ChangedRange.SetStyle(LibraryFunction,
                @"\b(assert|collectgarbage|dofile|error|getfenv|getmetatable|ipairs|loadfile|loadstring|module|next|pairs|pcall|print|rawequal|rawget|rawset|require|select|setfenv|setmetatable|tonumber|tostring|type|unpack|xpcall)\b");
            e.ChangedRange.SetStyle(Variable, @"\.[a-zA-Z_\d]*\b", RegexCompiledOption);
            e.ChangedRange.SetStyle(Number, @"\b\\d+[\\.]?\\d*([eE]\\-?\\d+)?[lLdDfF]?\b|\b0x[a-fA-F\\d]+\b");
            e.ChangedRange.SetStyle(Punctuation, @"\[|\]|\*|\?|\(|\)|\^|\!|\;|\,|\.|\:");
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers(@"function", @"end");
        }

        private void RubySyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "#";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.tb.Range.ClearStyle(Comment);
            e.ChangedRange.ClearStyle(Keyword, String, Number, ClassName, Punctuation,
                FunctionName, Variable, LibraryClass);
            // comment highlighting
            e.ChangedRange.tb.Range.SetStyle(Comment, @"#.*$", RegexOptions.Multiline);
            e.ChangedRange.tb.Range.SetStyle(Comment, @"(=begin.*?=end)|(.*=end)",
                RegexOptions.IgnoreCase | RegexOptions.RightToLeft | RegexOptions.Singleline);
            e.ChangedRange.SetStyle(ClassName, @"\b(class)\s+(?<range>\w+?)\b");
            e.ChangedRange.SetStyle(FunctionName, @"\b(def)\s+(?<range>\w+?)\b");
            e.ChangedRange.SetStyle(String, new Regex(
                @"
                            # Character definitions:
                            '
                            (?> # disable backtracking
                              (?:
                                \\[^\r\n]|    # escaped meta char
                                [^'\r\n]      # any character except '
                              )*
                            )
                            '?
                            |
                            # Normal string & verbatim strings definitions:
                            (?<verbatimIdentifier>@)?         # this group matches if it is an verbatim string
                            ""
                            (?> # disable backtracking
                              (?:
                                # match and consume an escaped character including escaped double quote ("") char
                                (?(verbatimIdentifier)        # if it is a verbatim string ...
                                  """"|                         #   then: only match an escaped double quote ("") char
                                  \\.                         #   else: match an escaped sequence
                                )
                                | # OR

                                # match any char except double quote char ("")
                                [^""]
                              )*
                            )
                            ""
                        ",
                RegexOptions.ExplicitCapture | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace |
                RegexCompiledOption
                ));
            e.ChangedRange.SetStyle(Variable, @"\$[a-zA-Z_\d]*\b|@[a-zA-Z_\d]*\b|\b(__(FILE|LINE)__|self)\b",
                RegexCompiledOption);
            e.ChangedRange.SetStyle(Keyword,
                @"\b(require|require_relative|gem|BEGIN|begin|case|class|else|elsif|END|end|ensure|for|if|in|module|rescue|then|unless|until|do|when|while|and|or|not|alias|alias_method|break|next|redo|retry|return|super|undef|yield|initialize|new|loop|include|extend|prepend|raise|attr_reader|attr_writer|attr_accessor|attr|catch|throw|private|module_function|public|protected|def)\b");
            e.ChangedRange.SetStyle(Constant, @"\b(puts|true|false|nil)\b|\:[a-zA-Z_\d]*\b|@[a-zA-Z_\d]*\b");
            e.ChangedRange.SetStyle(LibraryClass, @"\b[a-zA-Z_\d]*\.");
            e.ChangedRange.SetStyle(Number, @"\b\\d+[\\.]?\\d*([eE]\\-?\\d+)?[lLdDfF]?\b|\b0x[a-fA-F\\d]+\b");
            e.ChangedRange.SetStyle(Punctuation, @"\[|\]|\*|\?|\(|\)|\^|\!|\;|\,|\.|\:");
            PythonFold(e.ChangedRange.tb);
        }

        private void JsonSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "//";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '{';
            e.ChangedRange.tb.RightBracket2 = '}';
            e.ChangedRange.ClearStyle(Comment, String, Number, Constant, Punctuation);
            e.ChangedRange.SetStyle(Comment, new Regex(@"//.*$", RegexOptions.Multiline));
            e.ChangedRange.SetStyle(String, "\".*?[^\\\\]\"|\"\"|\'.*?[^\\\\]\'|\'\'");
            e.ChangedRange.SetStyle(Constant, @"\b(true|false|null)\b", RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(Number, @"\b\d+[\.]?\d*([eE]\-?\d+)?\b");
            e.ChangedRange.SetStyle(Punctuation, "\\W|_");
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers("{", "}");
        }

        private void LispSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = ";";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.ClearStyle(Comment, String, Keyword, Constant, Storage, LibraryFunction, FunctionName, Number,
                Punctuation);
            e.ChangedRange.SetStyle(Comment, @"\;.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(String, @"""""|''|"".*?[^\\]""|'.*?[^\\]'", RegexCompiledOption);
            e.ChangedRange.SetStyle(FunctionName, @"\b(defun|defmethod|defmacro|defvar|defconst)\s+(?<range>\w+)\b");
            e.ChangedRange.SetStyle(Keyword, @"\b(case|do|let|loop|if|else|when|eq|neq|and|or)\b");
            e.ChangedRange.SetStyle(Storage, @"\b(defun|defmethod|defmacro|defvar|defconst)\b");
            e.ChangedRange.SetStyle(Constant, @"\b(null|nil)\b");
            e.ChangedRange.SetStyle(LibraryFunction,
                @"\b(cons|car|cdr|cond|lambda|format|setq|setf|quote|eval|append|list|listp|memberp|t|load|progn)\b");
            e.ChangedRange.SetStyle(Number, @"\b\d+[\.]?\d*\b", RegexCompiledOption);
            e.ChangedRange.SetStyle(Punctuation, @"\;|\!|#|\:|\*|\&|\@|\(|\)|\-|=|\?|\\");
            foreach (var range in e.ChangedRange.tb.GetRanges(@"\b(defun)\s+(?<range>\w+)\b"))
                e.ChangedRange.SetStyle(FunctionName, @"\b" + range.Text + @"\b");
        }

        /// <summary>
        ///     Perl Syntax Highlight
        /// </summary>
        /// <param name="e"></param>
        private void PerlSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "#";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.tb.Range.ClearStyle(Comment);
            e.ChangedRange.ClearStyle(String, Variable, Keyword, Storage, Constant, LibraryFunction, Number);
            e.ChangedRange.SetStyle(Punctuation, @"=>");
            e.ChangedRange.tb.AddStyle(Comment);
            e.ChangedRange.tb.Range.SetStyle(Comment, @"#.*$", RegexOptions.Multiline);
            e.ChangedRange.tb.Range.SetStyle(Comment, @"(=begin.*?=cut)|(=begin.*)",
                RegexOptions.Singleline | RegexCompiledOption);
            e.ChangedRange.tb.Range.SetStyle(Comment, @"(=begin.*?=cut)|(.*=cut)",
                RegexOptions.Singleline | RegexOptions.RightToLeft | RegexCompiledOption);
            e.ChangedRange.SetStyle(String, @"""""|''|"".*?[^\\]""|'.*?[^\\]'", RegexCompiledOption);
            e.ChangedRange.SetStyle(Variable, @"\$[a-zA-Z_\d]*\b|%[a-zA-Z_\d]*\b", RegexCompiledOption);
            e.ChangedRange.SetStyle(Keyword,
                @"\b(sub|continue|die|do|else|elsif|exit|for|foreach|goto|if|last|next|redo|return|select|unless|until|wait|while|switch|case|package|require|use|eval|and|or|xor|as)\b");
            e.ChangedRange.SetStyle(Storage, @"\b(my|our|local)\b");
            e.ChangedRange.SetStyle(Constant, @"\b(__FILE__|__LINE__|__PACKAGE__)\b");
            e.ChangedRange.SetStyle(LibraryFunction,
                @"\b(ARGV|DATA|ENV|SIG|STDERR|STDIN|STDOUT|atan2|bind|binmode|bless|caller|chdir|chmod|chomp|chop|chown|chr|chroot|close|closedir|cmp|connect|cos|crypt|dbmclose|dbmopen|defined|delete|dump|each|endgrent|endhostent|endnetent|endprotoent|endpwent|endservent|eof|eq|exec|exists|exp|fcntl|fileno|flock|fork|format|formline|ge|getc|getgrent|getgrgid|getgrnam|gethostbyaddr|gethostbyname|gethostent|getlogin|getnetbyaddr|getnetbyname|getnetent|getpeername|getpgrp|getppid|getpriority|getprotobyname|getprotobynumber|getprotoent|getpwent|getpwnam|getpwuid|getservbyname|getservbyport|getservent|getsockname|getsockopt|glob|gmtime|grep|gt|hex|import|index|int|ioctl|join|keys|kill|lc|lcfirst|le|length|link|listen|localtime|log|lstat|lt|m|map|mkdir|msgctl|msgget|msgrcv|msgsnd|ne|no|oct|open|opendir|ord|pack|pipe|pop|pos|print|printf|push|q|qq|quotemeta|qw|qx|rand|read|readdir|readlink|recv|ref|rename|reset|reverse|rewinddir|rindex|rmdir|s|scalar|seek|seekdir|semctl|semget|semop|send|setgrent|sethostent|setnetent|setpgrp|setpriority|setprotoent|setpwent|setservent|setsockopt|shift|shmctl|shmget|shmread|shmwrite|shutdown|sin|sleep|socket|socketpair|sort|splice|split|sprintf|sqrt|srand|stat|study|substr|symlink|syscall|sysopen|sysread|system|syswrite|tell|telldir|tie|tied|time|times|tr|truncate|uc|ucfirst|umask|undef|unlink|unpack|unshift|untie|utime|values|vec|waitpid|wantarray|warn|write|y|q|qw|qq|qx)\b");
            e.ChangedRange.SetStyle(Number, @"\b\d+[\.]?\d*\b", RegexCompiledOption);
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers("=begin", "=cut");
            e.ChangedRange.SetFoldingMarkers("{", "}");
        }

        private void InitPHPRegex()
        {
            _phpStringRegex = new Regex(
                @"
                            # Character definitions:
                            '
                            (?> # disable backtracking
                              (?:
                                \\[^\r\n]|    # escaped meta char
                                [^'\r\n]      # any character except '
                              )*
                            )
                            '?
                            |
                            # Normal string & verbatim strings definitions:
                            (?<verbatimIdentifier>@)?         # this group matches if it is an verbatim string
                            ""
                            (?> # disable backtracking
                              (?:
                                # match and consume an escaped character including escaped double quote ("") char
                                (?(verbatimIdentifier)        # if it is a verbatim string ...
                                  """"|                         #   then: only match an escaped double quote ("") char
                                  \\.                         #   else: match an escaped sequence
                                )
                                | # OR

                                # match any char except double quote char ("")
                                [^""]
                              )*
                            )
                            ""
                        ",
                RegexOptions.ExplicitCapture | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace |
                RegexCompiledOption
                );
            _phpNumberRegex = new Regex(@"\b\d+[\.]?\d*\b", RegexCompiledOption);
            _phpCommentRegex1 = new Regex(@"(//|#).*$", RegexOptions.Multiline | RegexCompiledOption);
            _phpCommentRegex2 = new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline | RegexCompiledOption);
            _phpCommentRegex3 = new Regex(@"(/\*.*?\*/)|(.*\*/)",
                RegexOptions.Singleline | RegexOptions.RightToLeft | RegexCompiledOption);
            _phpVarRegex = new Regex(@"\$[a-zA-Z_\d]*\b", RegexCompiledOption);
            _phpKeywordRegex =
                new Regex(
                    @"\b(die|exit|include|include_once|namespace|require|require_once|return|abstract|and|as|break|case|catch|const|continue|declare|default|do|else|elseif|enddeclare|endfor|endforeach|endif|endswitch|endwhile|extends|final|for|foreach|global|if|implements|or|private|protected|public|static|switch|throw|try|use|while|xor)\b",
                    RegexCompiledOption);
        }

        /// <summary>
        ///     Highlights PHP code
        /// </summary>
        /// <param name="e"></param>
        private void PHPSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "//";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '{';
            e.ChangedRange.tb.RightBracket2 = '}';
            e.ChangedRange.tb.Range.ClearStyle(Comment);
            //clear style of changed range
            e.ChangedRange.ClearStyle(Number, Variable, Constant, Keyword, Storage, Punctuation, LibraryFunction);
            //
            if (_phpStringRegex == null)
                InitPHPRegex();
            e.ChangedRange.tb.Range.ClearStyle(Comment);
            //comment highlighting
            e.ChangedRange.tb.Range.SetStyle(Comment, _phpCommentRegex1);
            e.ChangedRange.tb.Range.SetStyle(Comment, _phpCommentRegex2);
            e.ChangedRange.tb.Range.SetStyle(Comment, _phpCommentRegex3);
            //string highlighting
            e.ChangedRange.SetStyle(String, _phpStringRegex);
            //var highlighting
            e.ChangedRange.SetStyle(Variable, _phpVarRegex);
            //keyword highlighting
            e.ChangedRange.SetStyle(Keyword, _phpKeywordRegex);
            e.ChangedRange.SetStyle(Constant,
                @"\b(true|false|null|__CLASS__|__FILE__|__LINE__|__NAMESPACE__|__FUNCTION__|__METHOD__)\b");
            e.ChangedRange.SetStyle(Storage, @"\b(function|class|var|interface|int|string|array|parent|self)\b");
            e.ChangedRange.SetStyle(LibraryFunction,
                @"\b(print|echo|fopen|substr|abs|pow|sqrt|floor|random|isset|unset|array|list)\b");
            e.ChangedRange.SetStyle(Punctuation, @">|\*|-|=|\+|\!|<|\^|\?");
            //number highlighting
            e.ChangedRange.SetStyle(Number, _phpNumberRegex);
            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();
            //set folding markers
            e.ChangedRange.SetFoldingMarkers("{", "}"); //allow to collapse brackets block
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
            // TODO : Add Class Name and Function Name Highlighting
        }

        private void HTMLPHPSyntaxHighlight(TextChangedEventArgs e)
        {
            HTMLSyntaxHighlight(e);
            foreach (var r in e.ChangedRange.tb.GetRanges(@"(<\?.*?.*?\?>)|(<\?.*?.*)", RegexOptions.Singleline))
            {
                //remove HTML highlighting from this fragment
                r.ClearStyle(TagName, TagBracket, AttributeName, AttributeValue, Comment, Constant);
                //do PHP highlighting
                PHPSyntaxHighlight(new TextChangedEventArgs(r));
            }
        }

        private void HtmlSyntaxHighlight(TextChangedEventArgs e)
        {
            HTMLSyntaxHighlight(e);
            foreach (var range in e.ChangedRange.tb.GetRanges(@"(<style.*?>.*?</style>)", RegexOptions.Singleline))
            {
                //remove HTML and JS from this fragment
                range.ClearStyle(Comment, TagBracket, AttributeName, AttributeValue, Keyword, Number, Constant, Storage,
                    FunctionName, ClassName, LibraryClass, LibraryFunction, Punctuation);
                //do CSS highlighting
                CssHighlight(new TextChangedEventArgs(range));
            }
            foreach (var r in e.ChangedRange.tb.GetRanges(@"(<script.*?>.*?</script>)", RegexOptions.Singleline))
            {
                //remove HTML and CSS highlighting from this fragment
                r.ClearStyle(Comment, TagBracket, AttributeName, AttributeValue, Number, Constant, CSSProperty,
                    CSSPropertyValue, CSSSelector);
                //do javascript highlighting
                JScriptSyntaxHighlight(new TextChangedEventArgs(r));
            }
        }

        private void InitCSharpRegex()
        {
            _cSharpStringRegex = new Regex(
                @"
                            # Character definitions:
                            '
                            (?> # disable backtracking
                              (?:
                                \\[^\r\n]|    # escaped meta char
                                [^'\r\n]      # any character except '
                              )*
                            )
                            '?
                            |
                            # Normal string & verbatim strings definitions:
                            (?<verbatimIdentifier>@)?         # this group matches if it is an verbatim string
                            ""
                            (?> # disable backtracking
                              (?:
                                # match and consume an escaped character including escaped double quote ("") char
                                (?(verbatimIdentifier)        # if it is a verbatim string ...
                                  """"|                         #   then: only match an escaped double quote ("") char
                                  \\.                         #   else: match an escaped sequence
                                )
                                | # OR

                                # match any char except double quote char ("")
                                [^""]
                              )*
                            )
                            ""
                        ",
                RegexOptions.ExplicitCapture | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace |
                RegexCompiledOption
                );
            _cSharpCommentRegex1 = new Regex(@"//.*$", RegexOptions.Multiline | RegexCompiledOption);
            _cSharpCommentRegex2 = new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline | RegexCompiledOption);
            _cSharpCommentRegex3 = new Regex(@"(/\*.*?\*/)|(.*\*/)",
                RegexOptions.Singleline | RegexOptions.RightToLeft | RegexCompiledOption);
            _cSharpNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b",
                RegexCompiledOption);
            _cSharpAttributeRegex = new Regex(@"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline | RegexCompiledOption);
            _cSharpClassNameRegex = new Regex(@"\b(class|struct|enum|interface)\s+(?<range>\w+?)\b", RegexCompiledOption);
            _cSharpKeywordRegex =
                new Regex(
                    @"\b(abstract|as|base|using|break|case|catch|char|checked|const|continue|decimal|default|delegate|do|else|enum|event|explicit|extern|finally|fixed|float|for|foreach|goto|if|implicit|in|interface|internal|is|this|lock|long|namespace|new|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|struct|switch|throw|try|typeof|unchecked|unsafe|virtual|volatile|while|add|alias|ascending|descending|dynamic|from|get|global|group|into|join|let|orderby|partial|remove|select|set|value|var|where|yield)\b",
                    RegexCompiledOption);
            _cSharpStorageRegex =
                new Regex(@"\b(class|void|bool|string|int|double|float|byte|uint|ushort|ulong)\b");
            _csharpFunctionRegex = new Regex(@"\b(void|int|bool|string|uint|ushort|ulong|byte)\s+(?<range>\w+?)\b");
        }

        private void CSharpSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "//";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '{';
            e.ChangedRange.tb.RightBracket2 = '}';
            //clear style of changed range
            e.ChangedRange.tb.Range.ClearStyle(Comment);
            e.ChangedRange.ClearStyle(String, Number, ClassName, AttributeName, FunctionName, Preprocessor, Keyword,
                Storage, Constant);
            //
            if (_cSharpStringRegex == null)
                InitCSharpRegex();
            //string highlighting
            e.ChangedRange.SetStyle(String, _cSharpStringRegex);
            //comment highlighting
            e.ChangedRange.tb.Range.SetStyle(Comment, _cSharpCommentRegex1);
            e.ChangedRange.tb.Range.SetStyle(Comment, _cSharpCommentRegex2);
            e.ChangedRange.tb.Range.SetStyle(Comment, _cSharpCommentRegex3);
            //attribute highlighting
            e.ChangedRange.SetStyle(AttributeName, _cSharpAttributeRegex);
            //class name highlighting
            e.ChangedRange.SetStyle(ClassName, _cSharpClassNameRegex);
            //funtion highlight
            e.ChangedRange.SetStyle(FunctionName, _csharpFunctionRegex);
            e.ChangedRange.SetStyle(Preprocessor, @"#[a-zA-Z_\d]*\b");
            //keyword highlighting
            e.ChangedRange.SetStyle(Keyword, _cSharpKeywordRegex);
            //Constant
            e.ChangedRange.SetStyle(Storage, _cSharpStorageRegex);
            e.ChangedRange.SetStyle(Constant, @"\b(true|false|null)\b");
            //number highlighting
            e.ChangedRange.SetStyle(Number, _cSharpNumberRegex);
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers("{", "}"); //allow to collapse brackets block
            e.ChangedRange.SetFoldingMarkers(@"#region\b", @"#endregion\b");
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
        }

        private void InitJScriptRegex()
        {
            _jScriptStringRegex = new Regex(
                @"
                            # Character definitions:
                            '
                            (?> # disable backtracking
                              (?:
                                \\[^\r\n]|    # escaped meta char
                                [^'\r\n]      # any character except '
                              )*
                            )
                            '?
                            |
                            # Normal string & verbatim strings definitions:
                            (?<verbatimIdentifier>@)?         # this group matches if it is an verbatim string
                            ""
                            (?> # disable backtracking
                              (?:
                                # match and consume an escaped character including escaped double quote ("") char
                                (?(verbatimIdentifier)        # if it is a verbatim string ...
                                  """"|                         #   then: only match an escaped double quote ("") char
                                  \\.                         #   else: match an escaped sequence
                                )
                                | # OR

                                # match any char except double quote char ("")
                                [^""]
                              )*
                            )
                            ""
                        ",
                RegexOptions.ExplicitCapture | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace |
                RegexCompiledOption
                );
            _jScriptCommentRegex1 = new Regex(@"//.*$", RegexOptions.Multiline | RegexCompiledOption);
            _jScriptCommentRegex2 = new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline | RegexCompiledOption);
            _jScriptCommentRegex3 = new Regex(@"(/\*.*?\*/)|(.*\*/)",
                RegexOptions.Singleline | RegexOptions.RightToLeft | RegexCompiledOption);
            _jScriptNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b",
                RegexCompiledOption);
            _jScriptKeywordRegex =
                new Regex(
                    @"\b(break|case|catch|const|continue|default|delete|do|escape|unescape|else|export|for|if|in|instanceof|new|return|switch|throw|try|void|while|with|typeof)\b",
                    RegexCompiledOption);
            _jScriptLibraryClass =
                new Regex(
                    @"\b(Array|Boolean|Date|Math|Object|String|document|NaN|prototype|window|navigator|Audio|Sound)\b");
            _jscriptLibraryFunction =
                new Regex(
                    @"\b(isFinite|isNaN|parseInt|parseFloat|length|documentElement|getElementsByTagName|getElementsByID|decodeURI|decodeURIComponent|encodeURI|eval|addEventListener|alert|atob|blur|btoa|clearInterval|clearTimeout|close|confirm|dispatchEvent|doNotTrack|find|focus|getComputedStyle|getMatchedCSSRules|getSelection|matchMedia|moveBy|moveTo|open|openDatabase|print|prompt|removeEventListener|resizeBy|resizeTo|scroll|scrollBy|scrollTo|setInterval|setTimeout|showModalDialog|stop)\b");
            _jScriptFunctionRegex = new Regex(@"\b(function)\s+(?<range>\w+?)\b");
            _jScriptFunctionRegex2 = new Regex(@"\b(var)\s+(?<range>\w+?)\b");
        }

        /// <summary>
        ///     Highlights JavaScript code
        /// </summary>
        /// <param name="e"></param>
        private void JScriptSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "//";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '{';
            e.ChangedRange.tb.RightBracket2 = '}';
            //clear style of visible range
            e.ChangedRange.tb.Range.ClearStyle(Comment);
            //clear style of changed range
            e.ChangedRange.ClearStyle(String, Number, Keyword, Storage, LibraryClass, ClassName,
                FunctionName, Punctuation, LibraryFunction);

            if (_jScriptStringRegex == null)
                InitJScriptRegex();
            //string highlighting
            e.ChangedRange.SetStyle(String, _jScriptStringRegex);
            e.ChangedRange.SetStyle(String, @"//|/.*?[^\\]/");
            //comment highlighting
            e.ChangedRange.tb.Range.SetStyle(Comment, _jScriptCommentRegex1);
            e.ChangedRange.tb.Range.SetStyle(Comment, _jScriptCommentRegex2);
            e.ChangedRange.tb.Range.SetStyle(Comment, _jScriptCommentRegex3);
            //number highlighting
            e.ChangedRange.SetStyle(Number, _jScriptNumberRegex);
            e.ChangedRange.SetStyle(ClassName, _jScriptFunctionRegex);
            e.ChangedRange.SetStyle(FunctionName, _jScriptFunctionRegex2);
            //keyword highlighting
            e.ChangedRange.SetStyle(Keyword, _jScriptKeywordRegex);
            // e.ChangedRange.SetStyle(FunctionArgument, new Regex(@"\((?<range>[!\w:]+)"));
            // e.ChangedRange.SetStyle(FunctionArgument, new Regex(@"</(?<range>[\w:]+)\)"));
            e.ChangedRange.SetStyle(Storage, @"\b(var|function)\b");
            e.ChangedRange.SetStyle(LibraryFunction, _jscriptLibraryFunction);
            e.ChangedRange.SetStyle(LibraryClass, _jScriptLibraryClass);
            e.ChangedRange.SetStyle(Constant, @"\b(true|false|null|this)\b");
            e.ChangedRange.SetStyle(Punctuation, @"\;|\,|<|>|-|\$|=|\!|\.|\?|\*|\&|\#|\^");
            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();
            //set folding markers
            e.ChangedRange.SetFoldingMarkers("{", "}"); //allow to collapse brackets block
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
        }

        /// <summary>
        ///     Highlights HTML code
        /// </summary>
        /// <param name="e"></param>
        private void HTMLSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "<!--";
            e.ChangedRange.tb.LeftBracket = '<';
            e.ChangedRange.tb.RightBracket = '>';
            e.ChangedRange.tb.LeftBracket2 = '(';
            e.ChangedRange.tb.RightBracket2 = ')';
            e.ChangedRange.tb.Range.ClearStyle(Comment);
            //clear style of changed range
            e.ChangedRange.ClearStyle(Comment, TagBracket, TagName, AttributeName, AttributeValue, Constant,
                DoctypeDeclaration);
            //
            if (_htmlTagRegex == null)
                InitHtmlRegex();
            //comment highlighting
            e.ChangedRange.tb.Range.SetStyle(Comment, _htmlCommentRegex1);
            e.ChangedRange.tb.Range.SetStyle(Comment, _htmlCommentRegex2);
            //tag brackets highlighting
            e.ChangedRange.SetStyle(TagBracket, _htmlTagRegex);
            //tag name
            e.ChangedRange.SetStyle(TagName, _htmlTagNameRegex);
            //end of tag
            e.ChangedRange.SetStyle(TagName, _htmlEndTagRegex);
            //attributes
            e.ChangedRange.SetStyle(AttributeName, _htmlAttrRegex);
            //attribute values
            e.ChangedRange.SetStyle(AttributeValue, _htmlAttrValRegex);
            e.ChangedRange.SetStyle(DoctypeDeclaration, @"<!.*?>", RegexOptions.Singleline);
            //html entity
            e.ChangedRange.SetStyle(Constant, _htmlEntityRegex);

            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();
            //set folding markers
            e.ChangedRange.SetFoldingMarkers("<head", "</head>", RegexOptions.IgnoreCase);
            e.ChangedRange.SetFoldingMarkers("<body", "</body>", RegexOptions.IgnoreCase);
            e.ChangedRange.SetFoldingMarkers("<table", "</table>", RegexOptions.IgnoreCase);
            e.ChangedRange.SetFoldingMarkers("<form", "</form>", RegexOptions.IgnoreCase);
            e.ChangedRange.SetFoldingMarkers("<div", "</div>", RegexOptions.IgnoreCase);
            e.ChangedRange.SetFoldingMarkers("<script", "</script>", RegexOptions.IgnoreCase);
            e.ChangedRange.SetFoldingMarkers("<tr", "</tr>", RegexOptions.IgnoreCase);
            e.ChangedRange.SetFoldingMarkers("<style", "</style>", RegexOptions.IgnoreCase);
        }

        private void InitVBRegex()
        {
            _vbStringRegex = new Regex(@"""""|"".*?[^\\]""", RegexCompiledOption);
            _vbCommentRegex = new Regex(@"'.*$", RegexOptions.Multiline | RegexCompiledOption);
            _vbNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?\b", RegexCompiledOption);
            _vbClassNameRegex = new Regex(@"\b(Class|Structure|Enum|Interface)[ ]+(?<range>\w+?)\b",
                RegexOptions.IgnoreCase | RegexCompiledOption);
            _vbKeywordRegex =
                new Regex(
                    @"\b(AddHandler|AddressOf|Alias|And|AndAlso|As|Boolean|ByRef|Byte|ByVal|Call|Case|Catch|CBool|CByte|CChar|CDate|CDbl|CDec|Char|CInt|Class|CLng|CObj|Const|Continue|CSByte|CShort|CSng|CStr|CType|CUInt|CULng|CUShort|Date|Decimal|Declare|Default|Delegate|Dim|DirectCast|Do|Double|Each|Else|ElseIf|End|EndIf|Enum|Erase|Error|Event|Exit|False|Finally|For|Friend|Function|Get|GetType|GetXMLNamespace|Global|GoSub|GoTo|Handles|If|Implements|Imports|In|Inherits|Integer|Interface|Is|IsNot|Let|Lib|Like|Long|Loop|Me|Mod|Module|MustInherit|MustOverride|Mybase|MyClass|Namespace|Narrowing|New|Next|Not|Nothing|NotInheritable|NotOverridable|Object|Of|On|Operator|Option|Optional|Or|OrElse|Overloads|Overridable|Overrides|ParamArray|Partial|Private|Property|Protected|Public|RaiseEvent|ReadOnly|ReDim|REM|RemoveHandler|Resume|Return|SByte|Select|Set|Shadows|Shared|Short|Single|Static|Step|Stop|String|Structure|Sub|SyncLock|Then|Throw|To|True|Try|TryCast|TypeOf|UInteger|ULong|UShort|Using|Variant|Wend|When|While|Widening|With|WithEvents|WriteOnly|Xor|Region)\b|(#Const|#Else|#ElseIf|#End|#If|#Region)\b",
                    RegexOptions.IgnoreCase | RegexCompiledOption);
        }

        /// <summary>
        ///     Highlights VB code
        /// </summary>
        /// <param name="e"></param>
        private void VBSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "'";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '\x0';
            e.ChangedRange.tb.RightBracket2 = '\x0';
            //clear style of changed range
            e.ChangedRange.ClearStyle(Comment, String, Comment, Number, ClassName, Keyword);
            //
            if (_vbStringRegex == null)
                InitVBRegex();
            //string highlighting
            e.ChangedRange.SetStyle(String, _vbStringRegex);
            //comment highlighting
            e.ChangedRange.SetStyle(Comment, _vbCommentRegex);
            //number highlighting
            e.ChangedRange.SetStyle(Number, _vbNumberRegex);
            //class name highlighting
            e.ChangedRange.SetStyle(ClassName, _vbClassNameRegex);
            //keyword highlighting
            e.ChangedRange.SetStyle(Keyword, _vbKeywordRegex);

            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();
            //set folding markers
            e.ChangedRange.SetFoldingMarkers(@"#Region\b", @"#End\s+Region\b", RegexOptions.IgnoreCase);
            e.ChangedRange.SetFoldingMarkers(@"\b(Class|Property|Enum|Structure|Interface)[ \t]+\S+",
                @"\bEnd (Class|Property|Enum|Structure|Interface)\b", RegexOptions.IgnoreCase);
            e.ChangedRange.SetFoldingMarkers(@"^\s*(?<range>While)[ \t]+\S+", @"^\s*(?<range>End While)\b",
                RegexOptions.Multiline | RegexOptions.IgnoreCase);
            e.ChangedRange.SetFoldingMarkers(@"\b(Sub|Function)[ \t]+[^\s']+", @"\bEnd (Sub|Function)\b",
                RegexOptions.IgnoreCase);
            //this declared separately because Sub and Function can be unclosed
            e.ChangedRange.SetFoldingMarkers(@"(\r|\n|^)[ \t]*(?<range>Get|Set)[ \t]*(\r|\n|$)", @"\bEnd (Get|Set)\b",
                RegexOptions.IgnoreCase);
            e.ChangedRange.SetFoldingMarkers(@"^\s*(?<range>For|For\s+Each)\b", @"^\s*(?<range>Next)\b",
                RegexOptions.Multiline | RegexOptions.IgnoreCase);
            e.ChangedRange.SetFoldingMarkers(@"^\s*(?<range>Do)\b", @"^\s*(?<range>Loop)\b",
                RegexOptions.Multiline | RegexOptions.IgnoreCase);
        }

        private void InitSqlRegex()
        {
            _sqlStringRegex = new Regex(@"""""|''|"".*?[^\\]""|'.*?[^\\]'", RegexCompiledOption);
            _sqlNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?\b", RegexCompiledOption);
            _sqlCommentRegex1 = new Regex(@"--.*$", RegexOptions.Multiline | RegexCompiledOption);
            _sqlCommentRegex2 = new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline | RegexCompiledOption);
            _sqlCommentRegex3 = new Regex(@"(/\*.*?\*/)|(.*\*/)",
                RegexOptions.Singleline | RegexOptions.RightToLeft | RegexCompiledOption);
            _sqlVarRegex = new Regex(@"@[a-zA-Z_\d]*\b", RegexCompiledOption);
            _sqlStatementsRegex =
                new Regex(
                    @"\b(ALTER APPLICATION ROLE|ALTER ASSEMBLY|ALTER ASYMMETRIC KEY|ALTER AUTHORIZATION|ALTER BROKER PRIORITY|ALTER CERTIFICATE|ALTER CREDENTIAL|ALTER CRYPTOGRAPHIC PROVIDER|ALTER DATABASE|ALTER DATABASE AUDIT SPECIFICATION|ALTER DATABASE ENCRYPTION KEY|ALTER ENDPOINT|ALTER EVENT SESSION|ALTER FULLTEXT CATALOG|ALTER FULLTEXT INDEX|ALTER FULLTEXT STOPLIST|ALTER FUNCTION|ALTER INDEX|ALTER LOGIN|ALTER MASTER KEY|ALTER MESSAGE TYPE|ALTER PARTITION FUNCTION|ALTER PARTITION SCHEME|ALTER PROCEDURE|ALTER QUEUE|ALTER REMOTE SERVICE BINDING|ALTER RESOURCE GOVERNOR|ALTER RESOURCE POOL|ALTER ROLE|ALTER ROUTE|ALTER SCHEMA|ALTER SERVER AUDIT|ALTER SERVER AUDIT SPECIFICATION|ALTER SERVICE|ALTER SERVICE MASTER KEY|ALTER SYMMETRIC KEY|ALTER TABLE|ALTER TRIGGER|ALTER USER|ALTER VIEW|ALTER WORKLOAD GROUP|ALTER XML SCHEMA COLLECTION|BULK INSERT|CREATE AGGREGATE|CREATE APPLICATION ROLE|CREATE ASSEMBLY|CREATE ASYMMETRIC KEY|CREATE BROKER PRIORITY|CREATE CERTIFICATE|CREATE CONTRACT|CREATE CREDENTIAL|CREATE CRYPTOGRAPHIC PROVIDER|CREATE DATABASE|CREATE DATABASE AUDIT SPECIFICATION|CREATE DATABASE ENCRYPTION KEY|CREATE DEFAULT|CREATE ENDPOINT|CREATE EVENT NOTIFICATION|CREATE EVENT SESSION|CREATE FULLTEXT CATALOG|CREATE FULLTEXT INDEX|CREATE FULLTEXT STOPLIST|CREATE FUNCTION|CREATE INDEX|CREATE LOGIN|CREATE MASTER KEY|CREATE MESSAGE TYPE|CREATE PARTITION FUNCTION|CREATE PARTITION SCHEME|CREATE PROCEDURE|CREATE QUEUE|CREATE REMOTE SERVICE BINDING|CREATE RESOURCE POOL|CREATE ROLE|CREATE ROUTE|CREATE RULE|CREATE SCHEMA|CREATE SERVER AUDIT|CREATE SERVER AUDIT SPECIFICATION|CREATE SERVICE|CREATE SPATIAL INDEX|CREATE STATISTICS|CREATE SYMMETRIC KEY|CREATE SYNONYM|CREATE TABLE|CREATE TRIGGER|CREATE TYPE|CREATE USER|CREATE VIEW|CREATE WORKLOAD GROUP|CREATE XML INDEX|CREATE XML SCHEMA COLLECTION|DELETE|DISABLE TRIGGER|DROP AGGREGATE|DROP APPLICATION ROLE|DROP ASSEMBLY|DROP ASYMMETRIC KEY|DROP BROKER PRIORITY|DROP CERTIFICATE|DROP CONTRACT|DROP CREDENTIAL|DROP CRYPTOGRAPHIC PROVIDER|DROP DATABASE|DROP DATABASE AUDIT SPECIFICATION|DROP DATABASE ENCRYPTION KEY|DROP DEFAULT|DROP ENDPOINT|DROP EVENT NOTIFICATION|DROP EVENT SESSION|DROP FULLTEXT CATALOG|DROP FULLTEXT INDEX|DROP FULLTEXT STOPLIST|DROP FUNCTION|DROP INDEX|DROP LOGIN|DROP MASTER KEY|DROP MESSAGE TYPE|DROP PARTITION FUNCTION|DROP PARTITION SCHEME|DROP PROCEDURE|DROP QUEUE|DROP REMOTE SERVICE BINDING|DROP RESOURCE POOL|DROP ROLE|DROP ROUTE|DROP RULE|DROP SCHEMA|DROP SERVER AUDIT|DROP SERVER AUDIT SPECIFICATION|DROP SERVICE|DROP SIGNATURE|DROP STATISTICS|DROP SYMMETRIC KEY|DROP SYNONYM|DROP TABLE|DROP TRIGGER|DROP TYPE|DROP USER|DROP VIEW|DROP WORKLOAD GROUP|DROP XML SCHEMA COLLECTION|ENABLE TRIGGER|EXEC|EXECUTE|FROM|INSERT|MERGE|OPTION|OUTPUT|SELECT|TOP|TRUNCATE TABLE|UPDATE|UPDATE STATISTICS|WHERE|WITH)\b",
                    RegexOptions.IgnoreCase | RegexCompiledOption);
            _sqlKeywordsRegex =
                new Regex(
                    @"\b(ADD|ALL|AND|ANY|AS|ASC|AUTHORIZATION|BACKUP|BEGIN|BETWEEN|BREAK|BROWSE|BY|CASCADE|CHECK|CHECKPOINT|CLOSE|CLUSTERED|COLLATE|COLUMN|COMMIT|COMPUTE|CONSTRAINT|CONTAINS|CONTINUE|CROSS|CURRENT|CURRENT_DATE|CURRENT_TIME|CURSOR|DATABASE|DBCC|DEALLOCATE|DECLARE|DEFAULT|DENY|DESC|DISK|DISTINCT|DISTRIBUTED|DOUBLE|DUMP|ELSE|END|ERRLVL|ESCAPE|EXCEPT|EXISTS|EXIT|EXTERNAL|FETCH|FILE|FILLFACTOR|FOR|FOREIGN|FREETEXT|FULL|FUNCTION|GOTO|GRANT|GROUP|HAVING|HOLDLOCK|IDENTITY|IDENTITY_INSERT|IDENTITYCOL|IF|IN|INDEX|INNER|INTERSECT|INTO|IS|JOIN|KEY|KILL|LIKE|LINENO|LOAD|NATIONAL|NOCHECK|NONCLUSTERED|NOT|NULL|OF|OFF|OFFSETS|ON|OPEN|OR|ORDER|OUTER|OVER|PERCENT|PIVOT|PLAN|PRECISION|PRIMARY|PRINT|PROC|PROCEDURE|PUBLIC|RAISERROR|READ|READTEXT|RECONFIGURE|REFERENCES|REPLICATION|RESTORE|RESTRICT|RETURN|REVERT|REVOKE|ROLLBACK|ROWCOUNT|ROWGUIDCOL|RULE|SAVE|SCHEMA|SECURITYAUDIT|SET|SHUTDOWN|SOME|STATISTICS|TABLE|TABLESAMPLE|TEXTSIZE|THEN|TO|TRAN|TRANSACTION|TRIGGER|TSEQUAL|UNION|UNIQUE|UNPIVOT|UPDATETEXT|USE|USER|VALUES|VARYING|VIEW|WAITFOR|WHEN|WHILE|WRITETEXT)\b",
                    RegexOptions.IgnoreCase | RegexCompiledOption);
            _sqlFunctionsRegex =
                new Regex(
                    @"(@@CONNECTIONS|@@CPU_BUSY|@@CURSOR_ROWS|@@DATEFIRST|@@DATEFIRST|@@DBTS|@@ERROR|@@FETCH_STATUS|@@IDENTITY|@@IDLE|@@IO_BUSY|@@LANGID|@@LANGUAGE|@@LOCK_TIMEOUT|@@MAX_CONNECTIONS|@@MAX_PRECISION|@@NESTLEVEL|@@OPTIONS|@@PACKET_ERRORS|@@PROCID|@@REMSERVER|@@ROWCOUNT|@@SERVERNAME|@@SERVICENAME|@@SPID|@@TEXTSIZE|@@TRANCOUNT|@@VERSION)\b|\b(ABS|ACOS|APP_NAME|ASCII|ASIN|ASSEMBLYPROPERTY|AsymKey_ID|ASYMKEY_ID|asymkeyproperty|ASYMKEYPROPERTY|ATAN|ATN2|AVG|CASE|CAST|CEILING|Cert_ID|Cert_ID|CertProperty|CHAR|CHARINDEX|CHECKSUM_AGG|COALESCE|COL_LENGTH|COL_NAME|COLLATIONPROPERTY|COLLATIONPROPERTY|COLUMNPROPERTY|COLUMNS_UPDATED|COLUMNS_UPDATED|CONTAINSTABLE|CONVERT|COS|COT|COUNT|COUNT_BIG|CRYPT_GEN_RANDOM|CURRENT_TIMESTAMP|CURRENT_TIMESTAMP|CURRENT_USER|CURRENT_USER|CURSOR_STATUS|DATABASE_PRINCIPAL_ID|DATABASEe_PRINCIPAL_ID|DATABASEPROPERTY|DATABASEPROPERTYEX|DATALENGTH|DATALENGTH|DATEADD|DATEDIFF|DATENAME|DATEPART|DAY|DB_ID|DB_NAME|DECRYPTBYASYMKEY|DECRYPTBYCERT|DECRYPTBYKEY|DECRYPTBYKEYAUTOASYMKEY|DECRYPTBYKEYAUTOCERT|DECRYPTBYPASSPHRASE|DEGREES|DENSE_RANK|DIFFERENCE|ENCRYPTBYASYMKEY|ENCRYPTBYCERT|ENCRYPTBYKEY|ENCRYPTBYPASSPHRASE|ERROR_LINE|ERROR_MESSAGE|ERROR_NUMBER|ERROR_PROCEDURE|ERROR_SEVERITY|ERROR_STATE|EVENTDATA|EXP|FILE_ID|FILE_IDEX|FILE_NAME|FILEGROUP_ID|FILEGROUP_NAME|FILEGROUPPROPERTY|FILEPROPERTY|FLOOR|fn_helpcollations|fn_listextendedproperty|fn_servershareddrives|fn_virtualfilestats|fn_virtualfilestats|FORMATMESSAGE|FREETEXTTABLE|FULLTEXTCATALOGPROPERTY|FULLTEXTSERVICEPROPERTY|GETANSINULL|GETDATE|GETUTCDATE|GROUPING|HAS_PERMS_BY_NAME|HOST_ID|HOST_NAME|IDENT_CURRENT|IDENT_CURRENT|IDENT_INCR|IDENT_INCR|IDENT_SEED|IDENTITY\(|INDEX_COL|INDEXKEY_PROPERTY|INDEXPROPERTY|IS_MEMBER|IS_OBJECTSIGNED|IS_SRVROLEMEMBER|ISDATE|ISDATE|ISNULL|ISNUMERIC|Key_GUID|Key_GUID|Key_ID|Key_ID|KEY_NAME|KEY_NAME|LEFT|LEN|LOG|LOG10|LOWER|LTRIM|MAX|MIN|MONTH|NCHAR|NEWID|NTILE|NULLIF|OBJECT_DEFINITION|OBJECT_ID|OBJECT_NAME|OBJECT_SCHEMA_NAME|OBJECTPROPERTY|OBJECTPROPERTYEX|OPENDATASOURCE|OPENQUERY|OPENROWSET|OPENXML|ORIGINAL_LOGIN|ORIGINAL_LOGIN|PARSENAME|PATINDEX|PATINDEX|PERMISSIONS|PI|POWER|PUBLISHINGSERVERNAME|PWDCOMPARE|PWDENCRYPT|QUOTENAME|RADIANS|RAND|RANK|REPLACE|REPLICATE|REVERSE|RIGHT|ROUND|ROW_NUMBER|ROWCOUNT_BIG|RTRIM|SCHEMA_ID|SCHEMA_ID|SCHEMA_NAME|SCHEMA_NAME|SCOPE_IDENTITY|SERVERPROPERTY|SESSION_USER|SESSION_USER|SESSIONPROPERTY|SETUSER|SIGN|SignByAsymKey|SignByCert|SIN|SOUNDEX|SPACE|SQL_VARIANT_PROPERTY|SQRT|SQUARE|STATS_DATE|STDEV|STDEVP|STR|STUFF|SUBSTRING|SUM|SUSER_ID|SUSER_NAME|SUSER_SID|SUSER_SNAME|SWITCHOFFSET|SYMKEYPROPERTY|symkeyproperty|sys\.dm_db_index_physical_stats|sys\.fn_builtin_permissions|sys\.fn_my_permissions|SYSDATETIME|SYSDATETIMEOFFSET|SYSTEM_USER|SYSTEM_USER|SYSUTCDATETIME|TAN|TERTIARY_WEIGHTS|TEXTPTR|TODATETIMEOFFSET|TRIGGER_NESTLEVEL|TYPE_ID|TYPE_NAME|TYPEPROPERTY|UNICODE|UPDATE\(|UPPER|USER_ID|USER_NAME|USER_NAME|VAR|VARP|VerifySignedByAsymKey|VerifySignedByCert|XACT_STATE|YEAR)\b",
                    RegexOptions.IgnoreCase | RegexCompiledOption);
            _sqlTypesRegex =
                new Regex(
                    @"\b(BIGINT|NUMERIC|BIT|SMALLINT|DECIMAL|SMALLMONEY|INT|TINYINT|MONEY|FLOAT|REAL|DATE|DATETIMEOFFSET|DATETIME2|SMALLDATETIME|DATETIME|TIME|CHAR|VARCHAR|TEXT|NCHAR|NVARCHAR|NTEXT|BINARY|VARBINARY|IMAGE|TIMESTAMP|HIERARCHYID|TABLE|UNIQUEIDENTIFIER|SQL_VARIANT|XML)\b",
                    RegexOptions.IgnoreCase | RegexCompiledOption);
        }

        /// <summary>
        ///     Highlights SQL code
        /// </summary>
        /// <param name="e"></param>
        private void SqlSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "--";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '\x0';
            e.ChangedRange.tb.RightBracket2 = '\x0';
            //clear style of changed range
            e.ChangedRange.tb.Range.ClearStyle(Comment);
            e.ChangedRange.ClearStyle(Comment, String, Number, Variable, Keyword, LibraryFunction, LibraryClass);
            if (_sqlStringRegex == null)
                InitSqlRegex();
            e.ChangedRange.tb.Range.ClearStyle(Comment);
            //string highlighting
            e.ChangedRange.SetStyle(String, _sqlStringRegex);
            //comment highlighting
            e.ChangedRange.tb.Range.SetStyle(Comment, _sqlCommentRegex1);
            e.ChangedRange.tb.Range.SetStyle(Comment, _sqlCommentRegex2);
            e.ChangedRange.tb.Range.SetStyle(Comment, _sqlCommentRegex3);
            //number highlighting
            e.ChangedRange.SetStyle(Number, _sqlNumberRegex);
            //types highlighting
            e.ChangedRange.SetStyle(LibraryClass, _sqlTypesRegex);
            //var highlighting
            e.ChangedRange.SetStyle(Variable, _sqlVarRegex);
            //statements
            e.ChangedRange.SetStyle(Keyword, _sqlStatementsRegex);
            //keywords
            e.ChangedRange.SetStyle(Keyword, _sqlKeywordsRegex);
            //functions
            e.ChangedRange.SetStyle(LibraryFunction, _sqlFunctionsRegex);

            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();
            //set folding markers
            e.ChangedRange.SetFoldingMarkers(@"\bBEGIN\b", @"\bEND\b", RegexOptions.IgnoreCase);
            //allow to collapse BEGIN..END blocks
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
        }

        /// <summary>
        ///     D Syntax Highlight
        /// </summary>
        /// <param name="e"></param>
        private void DSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "//";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '{';
            e.ChangedRange.tb.RightBracket2 = '}';
            e.ChangedRange.tb.Range.ClearStyle(Comment);
            e.ChangedRange.ClearStyle(String, Keyword, FunctionName, Constant, Storage, LibraryClass, Number);
            e.ChangedRange.tb.Range.SetStyle(Comment, @"//.*$");
            e.ChangedRange.tb.Range.SetStyle(Comment,
                new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline | RegexCompiledOption));
            e.ChangedRange.tb.Range.SetStyle(Comment,
                new Regex(@"(/\*.*?\*/)|(.*\*/)",
                    RegexOptions.Singleline | RegexOptions.RightToLeft | RegexCompiledOption));
            e.ChangedRange.SetStyle(String, @"""""|''|"".*?[^\\]""|'.*?[^\\]'", RegexCompiledOption);
            e.ChangedRange.SetStyle(FunctionName, @"\b(void|int)\s+(?<range>\w+?)\b");
            e.ChangedRange.SetStyle(Keyword,
                @"\b(module|auto|class|interface|enum|union|template|struct|pragma|import|deprecated|unittest|debug|package|extern|new|delete|typeof|typeid|cast|align|is|this|super|alias|typedef|asm|mixin|delegate|out|body|scope|version|public|private|protected|static|lazy|final|native|synchronized|abstract|with|iftype|goto|threadsafe|transient|export|return|break|case|continue|default|do|while|for|foreach|switch|if|else)\b");
            e.ChangedRange.SetStyle(Storage,
                @"\b(void|boolean|byte|char|short|int|float|long|double|ushort|int|uint|long|ulong|float|void|byte|ubyte|double|bit|char|wchar|ucent|cent|short|bool|dchar|real|ireal|ifloat|idouble|creal|cfloat|cdouble)\b");
            e.ChangedRange.SetStyle(Constant, @"\b(__FILE__|__LINE__|__DATE__|__TIME__|__TIMESTAMP__|null|true|false)\b");
            e.ChangedRange.SetStyle(LibraryClass, @"\b\p{Lu}[a-zA-Z_\d]*\b");
            e.ChangedRange.SetStyle(Number, @"\b\d+[\.]?\d*([eE]\-?\d+)?\b", RegexCompiledOption);
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers("{", "}");
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
        }

        private void PascalSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "//";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.tb.Range.ClearStyle(Comment);
            e.ChangedRange.ClearStyle(String, Keyword, Storage, FunctionName, Number, Punctuation);
            // highlight pascal single line comment
            e.ChangedRange.tb.Range.SetStyle(Comment, @"//.*$", RegexOptions.Multiline);
            // single line comment2 
            e.ChangedRange.tb.Range.SetStyle(Comment,
                new Regex(@"\(\*(.*?)\*\)", RegexOptions.Singleline | RegexCompiledOption));
            e.ChangedRange.tb.Range.SetStyle(Comment, @"({.*?})|({.*)", RegexOptions.Singleline);
            e.ChangedRange.tb.Range.SetStyle(Comment, @"({.*?})|(.*})",
                RegexOptions.Singleline | RegexOptions.RightToLeft);
            e.ChangedRange.SetStyle(String, new Regex(@"""""|''|"".*?[^\\]""|'.*?[^\\]'", RegexCompiledOption));
            e.ChangedRange.SetStyle(Keyword,
                @"\b(absolute|abstract|all|and|and_then|array|as|asm|attribute|begin|bindable|case|class|const|constructor|destructor|div|do|do|else|end|except|export|exports|external|far|file|finalization|finally|for|forward|goto|if|implementation|import|in|inherited|initialization|interface|interrupt|is|label|library|mod|module|name|near|nil|not|object|of|only|operator|or|or_else|otherwise|packed|pow|private|program|property|protected|public|published|qualified|record|repeat|resident|restricted|segment|set|shl|shr|then|to|try|type|unit|until|uses|value|var|view|virtual|while|with|xor)\b",
                RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(Storage, @"\b(function|procedure)\b", RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(FunctionName, @"\b(function|procedure)\s+(?<range>\w+?)\b", RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(Number, @"\b\d+[\.]?\d*([eE]\-?\d+)?\b");
            e.ChangedRange.SetStyle(Punctuation, @"\:|\?|\*|%|=|\+|\!|\^|#|\,|\.|\\|@|\|");
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers("{", "}");
            e.ChangedRange.SetFoldingMarkers(@"\bbegin\b", @"\bend\b", RegexOptions.IgnoreCase);
        }

        private void ScalaSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '{';
            e.ChangedRange.tb.RightBracket2 = '}';
            e.ChangedRange.tb.CommentPrefix = "//";
            e.ChangedRange.tb.Range.ClearStyle(Comment);
            e.ChangedRange.ClearStyle(String, Preprocessor, Storage, Keyword, Constant, Number, Punctuation,
                FunctionName, FunctionArgument);
            e.ChangedRange.tb.Range.SetStyle(Comment, new Regex(@"//.*$", RegexOptions.Multiline));
            e.ChangedRange.tb.Range.SetStyle(Comment, new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline));
            e.ChangedRange.tb.Range.SetStyle(Comment,
                new Regex(@"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft));
            e.ChangedRange.SetStyle(String, new Regex(@"""""|''|"".*?[^\\]""|'.*?[^\\]'"));
            e.ChangedRange.SetStyle(Preprocessor, new Regex(@"#[define|elif|else|endif|error|if|undef|warning]*\b"));
            e.ChangedRange.SetStyle(FunctionArgument, new Regex(@"\b[a-zA-Z_\d]*:"));
            e.ChangedRange.SetStyle(Keyword,
                @"\b(class|object|trait|extends|synchronized|@volatile|abstract|final|lazy|sealed|implicit|override|@transient|@native|else|if|do|while|for|yield|match|case|catch|finally|try|this|super|self|return|see|note|example|usecase|author|version|since|todo|deprecated|migration|define|inheritdoc|param|throws)\b");
            e.ChangedRange.SetStyle(Storage,
                new Regex(@"\b(def|Unit|Boolean|Byte|Char|Short|Int|Float|Long|Double|String|Symbol)\b"));
            e.ChangedRange.SetStyle(FunctionName, @"\b(def|class|object|trait)\s+(?<range>\w+?)\b");
            e.ChangedRange.SetStyle(Constant, @"\b(false|null|true|Nil|None)\b");
            e.ChangedRange.SetStyle(Number, @"\b\d+[\.]?\d*([eE]\-?\d+)?\b");
            e.ChangedRange.SetStyle(Punctuation, @"=|>|<|\[|\]|\(|\)|\?|\$|\!|\*|\^|\+");
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers("{", "}");
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
        }

        private void AntlrSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(Comment, String, Keyword, LibraryClass);
            e.ChangedRange.SetStyle(Comment, @"//.*$");
            e.ChangedRange.SetStyle(String, @"""""|''|"".*?[^\\]""|'.*?[^\\]'");
            e.ChangedRange.SetStyle(Keyword,
                @"\b(tokens|header|lexclass|grammar|class|extends|Lexer|TreeParser|charVocabulary|Parser|protected|public|private|returns|throws|exception|catch|options)\b",
                RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(LibraryClass,
                @"ANTLRException|ANTLR_API|ANTLR_API|ANTLR_CXX_SUPPORTS_NAMESPACE|ANTLR_C_USING|ANTLR_REALLY_NO_STRCASECMP|ANTLR_SUPPORT_XML|ANTLR_USE_NAMESPACE|EOF_TYPE|HAS_NOT_CCTYPE_H|INVALID_TYPE|MIN_USER_TYPE|NEEDS_OPERATOR_LESS_THAN|NO_STATIC_CONSTS|NULL_TREE_LOOKAHEAD|AST|ASTArray|ASTFactory|ASTNULL|ASTNULLType|ASTPair|ASTRefCount|BaseAST|BitSet|CUSTOM_API|CharBuffer|CharInputBuffer|CharScanner|CharScannerLiteralsLess|CharStreamException|CharStreamIOException|CircularQueue|CommonAST|CommonASTWithHiddenTokens|CommonHiddenStreamToken|CommonToken|IOException|InputBuffer|LA|LLkParser|LT|LexerInputState|LoadAST|MismatchedCharException|MismatchedTokenException|NoViableAltException|NoViableAltForCharException|OS_NO_ALLOCATOR|Parser|ParserInputState|RecognitionException|RefCount|RefToken|SKIP|SemanticException|Token|TokenBuffer|TokenStream|TokenStreamBasicFilter|TokenStreamException|TokenStreamHiddenTokenFilter|TokenStreamIOException|TokenStreamRecognitionException|TokenStreamRexception|TokenStreamSelector|Tracer|TreeParser|TreeParserInputState|TreeParserSharedInputState)\b",
                RegexOptions.IgnoreCase);
        }

        private void InitObjCRegex()
        {
            _objCStringRegex = new Regex(
                @"
                            # Character definitions:
                            '
                            (?> # disable backtracking
                              (?:
                                \\[^\r\n]|    # escaped meta char
                                [^'\r\n]      # any character except '
                              )*
                            )
                            '?
                            |
                            # Normal string & verbatim strings definitions:
                            (?<verbatimIdentifier>@)?         # this group matches if it is an verbatim string
                            ""
                            (?> # disable backtracking
                              (?:
                                # match and consume an escaped character including escaped double quote ("") char
                                (?(verbatimIdentifier)        # if it is a verbatim string ...
                                  """"|                         #   then: only match an escaped double quote ("") char
                                  \\.                         #   else: match an escaped sequence
                                )
                                | # OR

                                # match any char except double quote char ("")
                                [^""]
                              )*
                            )
                            ""
                        ",
                RegexOptions.ExplicitCapture | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace |
                RegexCompiledOption
                );
            _objCCommentRegex1 = new Regex(@"//.*$", RegexOptions.Multiline);
            _objCCommentRegex2 = new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            _objCCommentRegex3 = new Regex(@"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);
            _objCNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            _objCKeywordRegex =
                new Regex(
                    @"\b(while|for|do|if|else|switch|enumerate|return|NS_DURING|NS_HANDLER|NS_ENDHANDLER)\b|(@)(synthesize|dynamic|try|catch|finally|throw|synchronized|required|optional)\b");
            _objCClassNameRegex = new Regex(@"\b(class|struct|enum|interface)\s+(?<range>\w+?)\b");
            _objCStringRegex2 = new Regex(@"(?<=\<)(.*?)(?=\>)|\<|\>");
            _objCFunctionsRegex = new Regex(@"\b(int|void)\s+(?<range>\w+?)\b");
            _objCPreprocessorRegex = new Regex(@"#[a-zA-Z_\d]*\b", RegexCompiledOption);
        }

        private void ObjectiveCHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "//";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.tb.Range.ClearStyle(Comment);
            e.ChangedRange.ClearStyle(String, Keyword, Constant, ClassName, FunctionName,
                Preprocessor, Number);
            if (_objCCommentRegex1 == null)
                InitObjCRegex();
            e.ChangedRange.tb.Range.SetStyle(Comment, _objCCommentRegex1);
            e.ChangedRange.tb.Range.SetStyle(Comment, _objCCommentRegex2);
            e.ChangedRange.tb.Range.SetStyle(Comment, _objCCommentRegex3);
            // preprocessor match
            e.ChangedRange.SetStyle(Preprocessor, _objCPreprocessorRegex);
            // string highlight
            e.ChangedRange.SetStyle(String, _objCStringRegex2);
            e.ChangedRange.SetStyle(String, _objCStringRegex);
            // class name
            e.ChangedRange.SetStyle(ClassName, _objCClassNameRegex);
            e.ChangedRange.SetStyle(FunctionName, _objCFunctionsRegex);
            // keyword
            e.ChangedRange.SetStyle(Keyword, _objCKeywordRegex);
            e.ChangedRange.SetStyle(Storage,
                @"(@)(implementation|class|interface|protocol)\b|\b(id|IBOutlet|IBAction|BOOL|SEL|id|void|unichar|IMP|Class|instancetype)\b");
            // todo word boundaries
            e.ChangedRange.SetStyle(Constant, @"\b(NULL|NIL|SELF|TRUE|YES|FALSE|NO|FIRST|LAST|SIZE|ALL|ANY|SOME|NONE)\b",
                RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(LibraryClass,
                @"\bNS(RuleEditor|G(arbageCollector|radient)|MapTable|HashTable|Co(ndition|llectionView(Item))|T(oolbarItemGroup|extInputClient|r(eeNode|ackingArea))|InvocationOperation|Operation(Queue)|D(ictionaryController|ockTile)|P(ointer(Functions|Array)|athC(o(ntrol(Delegate)|mponentCell)|ell(Delegate))|r(intPanelAccessorizing|edicateEditor(RowTemplate)))|ViewController|FastEnumeration|Animat(ionContext|ablePropertyContainer))\b");
            e.ChangedRange.SetStyle(LibraryFunction,
                @"\b(NS(Rect(ToCGRect|FromCGRect)|MakeCollectable|S(tringFromProtocol|ize(ToCGSize|FromCGSize))|Draw(NinePartImage|ThreePartImage)|P(oint(ToCGPoint|FromCGPoint)|rotocolFromString)|EventMaskFromType|Value))\b");
            e.ChangedRange.SetStyle(Number, _objCNumberRegex);
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers("{", "}");
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
        }

        private void ActionscriptSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "//";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.tb.Range.ClearStyle(Comment);
            e.ChangedRange.ClearStyle(String, Keyword, LibraryClass, LibraryFunction, Number);
            e.ChangedRange.tb.Range.SetStyle(Comment, @"//.*$", RegexOptions.Multiline);
            e.ChangedRange.tb.Range.SetStyle(Comment, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            e.ChangedRange.tb.Range.SetStyle(Comment, @"(/\*.*?\*/)|(.*\*/)",
                RegexOptions.Singleline | RegexOptions.RightToLeft);
            e.ChangedRange.SetStyle(String, @"""""|''|"".*?[^\\]""|'.*?[^\\]'");
            e.ChangedRange.SetStyle(Keyword,
                @"\b(break|case|continue|default|do|while|else|for|foreach|on|in|if|label|return|super|switch|throw|try|catch|finally|while|with|dynamic|final|internal|native|override|private|protected|public|static|class|const|extends|function|get|implements|interface|package|set|var|default|import|include|use|namespace|AS3|flash_proxy|object_proxy|this)\b");
            e.ChangedRange.SetStyle(Constant, @"\b(true|false|null)\b");
            e.ChangedRange.SetStyle(LibraryClass,
                @"\b(Boolean|escape|int|Number|Object|String|uint|unescape|XML|XMLList|Infinity|NaN|undefined|void)\b");
            e.ChangedRange.SetStyle(LibraryFunction,
                @"\b(abs|acceptAllClient|acceptAllServer|acceptClient|isFinite|isXMLName|parseFloat|parseInt|decodeURI|decodeURIComponent|encodeURI|encodeURIComponent|trace|isNaN|acceptDragDrop|acceptServer|acknowledge|acos|activate|add|addCallback|addChannel|addChild|addChildAt|addChildSet|addCuePoint|addData|addDragData|addEventListener|addFocusManager|addHaloColors|addHandler|addHeader|addItem|addItemAt|addListenerHandler|addLogger|addNamespace|addObject|addPage|addPopUp|addResponder|addSimpleHeader|addTarget|addToCreationQueue|addToFreeItemRenderers|adjustBrightness|adjustBrightness2|adjustFocusRect|adjustGutters|adjustMinMax|allowDomain|allowInsecureDomain|appendChild|appendText|apply|applyFilter|applyItemRendererProperties|applySelectionEffect|applySeriesSet|applySettings|applyValueToTarget|areInaccessibleObjectsUnderPoint|areSoundsInaccessible|asin|atan|atan2|attachAudio|attachCamera|attachNetStream|attachOverlay|attribute|attributes|begin|beginBitmapFill|beginFill|beginGradientFill|beginInterpolation|beginTween|bindProperty|bindSetter|bringToFront|browse|buildLabelCache|buildMinorTickCache|buildSubSeries|cacheDefaultValues|cacheIndexValues|cacheNamedValues|calculateDropIndex|calculateDropIndicatorY|calculatePreferredSizeFromData|calculateRowHeight|call|callLater|callProperty|canLoadWSDL|canWatch|cancel|captureStartValues|ceil|center|centerPopUp|channelConnectHandler|channelDisconnectHandler|channelFaultHandler|charAt|charCodeAt|chartStateChanged|checkCreate|checkDelete|checkUpdate|child|childIndex|children|childrenCreated|claimStyles|clear|clearHeaders|clearIndicators|clearResult|clearSelected|clearSeparators|clearStyle|clearStyleDeclaration|clickHandler|clone|cloneNode|close|collectTransitions|collectionChangeHandler|colorTransform|comments|commit|commitProperties|compare|completeHandler|compress|computeSpectrum|concat|configureScrollBars|conflict|connect|connectFailed|connectSuccess|connectTimeoutHandler|contains|containsPoint|containsRect|contentToGlobal|contentToLocal|copy|copyChannel|copyPixels|copySelectedItems|cos|count|create|createBorder|createBox|createChildren|createComponentFromDescriptor|createComponentsFromDescriptors|createCursor|createDataID|createElement|createErrorMessage|createEvent|createEventFromMessageFault|createFaultEvent|createGradientBox|createInstance|createInstances|createItem|createItemEditor|createMenu|createNavItem|createPopUp|createReferenceOnParentDocument|createRequestTimeoutErrorMessage|createTextNode|createToolTip|createTween|createUID|createUniqueName|createUpdateEvent|createXMLDocument|curveTo|customizeSeries|dataChanged|dataForFormat|dataToLocal|dateCompare|dateToString|deactivate|debug|decode|decodeXML|defaultCreateMask|defaultFilterFunction|defaultSettings|deleteItem|deleteProperty|deleteReferenceOnParentDocument|deltaTransformPoint|descendants|describeData|describeType|destroyItemEditor|destroyToolTip|determineTextFormatFromStyles|disableAutoUpdate|disablePolling|disconnect|disconnectFailed|disconnectSuccess|dispatchEvent|displayObjectToString|dispose|distance|doConversion|doDrag|doValidation|downArrowButton_buttonDownHandler|download|dragCompleteHandler|dragDropHandler|dragEnterHandler|dragExitHandler|dragOverHandler|dragScroll|draw|drawCaretIndicator|drawCircle|drawColumnBackground|drawEllipse|drawFocus|drawHeaderBackground|drawHighlightIndicator|drawHorizontalLine|drawItem|drawLinesAndColumnBackgrounds|drawRect|drawRoundRect|drawRoundRectComplex|drawRowBackground|drawRowBackgrounds|drawSelectionIndicator|drawSeparators|drawShadow|drawTileBackground|drawTileBackgrounds|drawVerticalLine|easeIn|easeInOut|easeNone|easeOut|effectEndHandler|effectFinished|effectStartHandler|effectStarted|elements|enableAutoUpdate|enablePolling|encodeDate|encodeValue|end|endEdit|endEffectsForTarget|endEffectsStarted|endFill|endInterpolation|endTween|enumerateFonts|equals|error|every|exec|executeBindings|executeChildBindings|exp|expandChildrenOf|expandItem|extractMinInterval|extractMinMax|fatal|fault|fill|fillRect|filter|filterCache|filterInstance|findAny|findDataPoints|findFirst|findFocusManagerComponent|findItem|findKey|findLast|findString|findText|finishEffect|finishKeySelection|finishPrint|finishRepeat|floodFill|floor|flush|focusInHandler|focusOutHandler|forEach|format|formatDataTip|formatDays|formatDecimal|formatForScreen|formatMilliseconds|formatMinutes|formatMonths|formatNegative|formatPrecision|formatRounding|formatRoundingWithPrecision|formatSeconds|formatThousands|formatToString|formatValue|formatYears|fromCharCode|generateFilterRect|getAffectedProperties|getAxis|getBoolean|getBounds|getCacheKey|getCamera|getChannel|getChannelSet|getCharBoundaries|getCharIndexAtPoint|getChildAt|getChildByName|getChildIndex|getChildren|getClassInfo|getClassStyleDeclarations|getColorBoundsRect|getColorName|getColorNames|getComplexProperty|getConflict|getContent|getCuePointByName|getCuePoints|getCurrent|getData|getDate|getDay|getDefinition|getDefinitionByName|getDescendants|getDestination|getDividerAt|getElementBounds|getEvents|getExplicitOrMeasuredHeight|getExplicitOrMeasuredWidth|getFeedback|getFirstCharInParagraph|getFocus|getFullURL|getFullYear|getGroupName|getHeader|getHeaderAt|getHours|getImageReference|getInstance|getItem|getItemAt|getItemIndex|getKeyProperty|getLabelEstimate|getLabels|getLevelString|getLineIndexAtPoint|getLineIndexOfChar|getLineLength|getLineMetrics|getLineOffset|getLineText|getLocal|getLogger|getMenuAt|getMessageResponder|getMicrophone|getMilliseconds|getMinutes|getMissingInterpolationValues|getMonth|getNamespaceForPrefix|getNextFocusManagerComponent|getNumber|getObject|getObjectsUnderPoint|getOperation|getOperationAsString|getParagraphLength|getParentItem|getPendingOperation|getPercentLoaded|getPixel|getPixel32|getPixels|getPort|getPrefixForNamespace|getProperties|getProperty|getProtocol|getRadioButtonAt|getRect|getRemote|getRenderDataForTransition|getRepeaterItem|getResourceBundle|getSOAPAction|getSWFRoot|getSecondAxis|getSeconds|getSelected|getSelectedText|getServerName|getServerNameWithPort|getServiceIdForMessage|getStackTrace|getStartValue|getString|getStringArray|getStyle|getStyleDeclaration|getSubscribeMessage|getTabAt|getText|getTextFormat|getTextRunInfo|getTextStyles|getThumbAt|getTime|getTimezoneOffset|getType|getUID|getUITextFormat|getUTCDate|getUTCDay|getUTCFullYear|getUTCHours|getUTCMilliseconds|getUTCMinutes|getUTCMonth|getUTCSeconds|getUnsubscribeMessage|getValue|getValueFromSource|getValueFromTarget|getViewIndex|globalToContent|globalToLocal|gotoAndPlay|gotoAndStop|guardMinMax|handleResults|hasChildNodes|hasChildren|hasComplexContent|hasDefinition|hasEventListener|hasFormat|hasGlyphs|hasIllegalCharacters|hasMetadata|hasOwnProperty|hasProperty|hasResponder|hasSimpleContent|hide|hideBuiltInItems|hideCursor|hideData|hideDropFeedback|hideFocus|hiliteSelectedNavItem|hitTest|hitTestObject|hitTestPoint|hitTestTextNearPos|horizontalGradientMatrix|identity|inScopeNamespaces|indexOf|indexToColumn|indexToItemRenderer|indexToRow|indicesToIndex|inflate|inflatePoint|info|initChannelSet|initEffect|initInstance|initListData|initMaskEffect|initProgressHandler|initProtoChain|initSecondaryMode|initializationComplete|initialize|initializeAccessibility|initializeInterpolationData|initializeRepeater|initializeRepeaterArrays|initialized|insert|insertBefore|insertChildAfter|insertChildBefore|internalConnect|internalDisconnect|internalSend|interpolate|intersection|intersects|invalidate|invalidateCache|invalidateChildOrder|invalidateData|invalidateDisplayList|invalidateFilter|invalidateList|invalidateMapping|invalidateProperties|invalidateSeries|invalidateSeriesStyles|invalidateSize|invalidateStacking|invalidateTransform|invalidateTransitions|invert|invertTransform|invoke|isAccessible|isAttribute|isBranch|isColorName|isCompensating|isCreate|isDebug|isDefaultPrevented|isDelete|isEmpty|isEmptyUpdate|isEnabled|isError|isFatal|isFocusInaccessible|isFontFaceEmbedded|isHttpURL|isHttpsURL|isInfo|isInheritingStyle|isInheritingTextFormatStyle|isInvalid|isItemHighlighted|isItemOpen|isItemSelected|isItemVisible|isOurFocus|isParentDisplayListInvalidatingStyle|isParentSizeInvalidatingStyle|isPrototypeOf|isRealValue|isSimple|isSizeInvalidatingStyle|isToggled|isTopLevel|isTopLevelWindow|isUpdate|isValidStyleValue|isWarn|isWatching|isWhitespace|itemRendererContains|itemRendererToIndex|itemRendererToIndices|join|keyDownHandler|keyUpHandler|lastIndexOf|layoutChrome|layoutEditor|legendDataChanged|length|lineGradientStyle|lineStyle|lineTo|load|loadBytes|loadPolicyFile|loadState|loadWSDL|localName|localToContent|localToData|localToGlobal|localeCompare|lock|log|logEvent|logout|makeListData|makeRowsAndColumns|map|mapCache|mappingChanged|match|max|measure|measureHTMLText|measureHeightOfItems|measureText|measureWidthOfItems|merge|min|mouseClickHandler|mouseDoubleClickHandler|mouseDownHandler|mouseEventToItemRenderer|mouseMoveHandler|mouseOutHandler|mouseOverHandler|mouseUpHandler|mouseWheelHandler|move|moveDivider|moveNext|movePrevious|moveSelectionHorizontally|moveSelectionVertically|moveTo|name|namespace|namespaceDeclarations|newInstance|nextFrame|nextName|nextNameIndex|nextPage|nextScene|nextValue|nodeKind|noise|normalize|notifyStyleChangeInChildren|numericCompare|offset|offsetPoint|onEffectEnd|onMoveTweenEnd|onMoveTweenUpdate|onScaleTweenEnd|onScaleTweenUpdate|onTweenEnd|onTweenUpdate|open|owns|paletteMap|parent|parentChanged|parse|parseCSS|parseDateString|parseNumberString|parseXML|pause|perlinNoise|pixelDissolve|pixelsToPercent|placeSortArrow|play|polar|pop|popUpMenu|qnameToString|qnamesEqual|random|readBoolean|readByte|readBytes|readDouble|readExternal|readFloat|readInt|readMultiByte|readObject|readShort|readUTF|readUTFBytes|readUnsignedByte|readUnsignedInt|readUnsignedShort|receive|receiveAudio|receiveVideo|reduceLabels|refresh|regenerateStyleCache|register|registerApplication|registerCacheHandler|registerColorName|registerDataTransform|registerEffects|registerFont|registerInheritingStyle|registerParentDisplayListInvalidatingStyle|registerParentSizeInvalidatingStyle|registerSizeInvalidatingStyle|release|releaseCollection|releaseItem|remove|removeAll|removeAllChildren|removeAllCuePoints|removeAllCursors|removeBusyCursor|removeChannel|removeChild|removeChildAt|removeCuePoint|removeCursor|removeEventListener|removeFocusManager|removeHeader|removeIndicators|removeItemAt|removeListenerHandler|removeLogger|removeNamespace|removeNode|removePopUp|removeTarget|replace|replacePort|replaceProtocol|replaceSelectedText|replaceText|replaceTokens|requestTimedOut|reset|resetNavItems|result|resultHandler|resume|resumeBackgroundProcessing|resumeEventHandling|reverse|revertChanges|rgbMultiply|rollOutHandler|rollOverHandler|rotate|rotatedGradientMatrix|round|rslCompleteHandler|rslErrorHandler|rslProgressHandler|save|saveStartValue|saveState|scale|scroll|scrollChildren|scrollHandler|scrollHorizontally|scrollPositionToIndex|scrollToIndex|scrollVertically|search|seek|seekPendingFailureHandler|seekPendingResultHandler|selectItem|send|setActualSize|setAdvancedAntiAliasingTable|setAxis|setBusyCursor|setChildIndex|setChildren|setClipboard|setColor|setCompositionString|setCredentials|setCuePoints|setCurrentState|setCursor|setDate|setDirty|setEmpty|setEnabled|setFocus|setFullYear|setHandler|setHours|setItemAt|setItemIcon|setKeyFrameInterval|setLocalName|setLoopBack|setLoopback|setMenuItemToggled|setMilliseconds|setMinutes|setMode|setMonth|setMotionLevel|setName|setNamespace|setPixel|setPixel32|setPixels|setProgress|setProperty|setPropertyIsEnumerable|setQuality|setRemoteCredentials|setRowCount|setRowHeight|setScrollBarProperties|setScrollProperties|setSecondAxis|setSeconds|setSelectColor|setSelected|setSelection|setSettings|setSilenceLevel|setSize|setStyle|setStyleDeclaration|setTextFormat|setThumbValueAt|setTime|setToggled|setTweenHandlers|setUTCDate|setUTCFullYear|setUTCHours|setUTCMilliseconds|setUTCMinutes|setUTCMonth|setUTCSeconds|setUseEchoSuppression|setVisible|settings|setupPropertyList|shift|show|showCursor|showDisplayForDownloading|showDisplayForInit|showDropFeedback|showFeedback|showFocus|showSettings|simpleType|sin|slice|some|sort|sortOn|splice|split|sqrt|stack|start|startDrag|startDragging|startEffect|status|statusHandler|stop|stopAll|stopDrag|stopDragging|stopImmediatePropagation|stopPropagation|stringCompare|stringToDate|stripNaNs|styleChanged|stylesInitialized|subscribe|substitute|substr|substring|subtract|suspendBackgroundProcessing|suspendEventHandling|swapChildren|swapChildrenAt|tan|test|text|textInput_changeHandler|threshold|toArray|toDateString|toExponential|toFixed|toLocaleDateString|toLocaleLowerCase|toLocaleString|toLocaleTimeString|toLocaleUpperCase|toLowerCase|toPrecision|toString|toTimeString|toUTCString|toUpperCase|toXMLString|togglePause|toolTipShowHandler|transform|transformCache|transformPoint|translate|trim|truncateToFit|tweenEventHandler|uncompress|union|unload|unlock|unregister|unregisterDataTransform|unshift|unsubscribe|unwatch|update|updateAfterEvent|updateBackground|updateData|updateDisplayList|updateFilter|updateList|updateMapping|updateNavItemIcon|updateNavItemLabel|updateProperties|updateStacking|updateTransform|upload|validate|validateAll|validateClient|validateCreditCard|validateCurrency|validateData|validateDate|validateDisplayList|validateEmail|validateNow|validateNumber|validatePhoneNumber|validateProperties|validateSize|validateSocialSecurity|validateString|validateTransform|validateZipCode|validationResultHandler|valueOf|verticalGradientMatrix|warn|watch|willTrigger|writeBoolean|writeByte|writeBytes|writeDouble|writeDynamicProperties|writeDynamicProperty|writeExternal|writeFloat|writeInt|writeMultiByte|writeObject|writeShort|writeUTF|writeUTFBytes|writeUnsignedInt)\b");
            e.ChangedRange.SetStyle(Number, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers("{", "}");
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
        }

        private void ShellSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "#";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.ClearStyle(Comment, String, Keyword, Storage, FunctionName, Variable, Number, LibraryFunction);
            e.ChangedRange.SetStyle(Comment, @"#.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(String, new Regex(@"""""|''|"".*?[^\\]""|'.*?[^\\]'"));
            e.ChangedRange.SetStyle(FunctionName, @"\b(function)\s+(?<range>\w+?)\b");
            e.ChangedRange.SetStyle(Keyword,
                @"\b(if|then|else|elif|fi|for|in|do|done|select|case|continue|esac|while|until|return)\b");
            e.ChangedRange.SetStyle(Storage, @"\bfunction\b");
            e.ChangedRange.SetStyle(LibraryFunction,
                @"\b(alias|bg|bind|break|builtin|caller|cd|command|compgen|complete|dirs|disown|echo|enable|eval|exec|exit|false|fc|fg|getopts|hash|help|history|jobs|kill|let|logout|popd|printf|pushd|pwd|read|readonly|set|shift|shopt|source|suspend|test|times|trap|true|type|ulimit|umask|unalias|unset|wait)\b");
            e.ChangedRange.SetStyle(Variable, new Regex(@"\$[a-zA-Z_\d]*\b", RegexCompiledOption));
            e.ChangedRange.SetStyle(Number, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
        }

        private void MakeFileSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "#";
            e.ChangedRange.ClearStyle(Comment, String, Keyword, Constant, Variable, Number);
            e.ChangedRange.SetStyle(Comment, @"#.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(String, @"""""|''|"".*?[^\\]""|'.*?[^\\]'");
            e.ChangedRange.SetStyle(Keyword,
                @"\b(define|endef|ifdef|ifndef|ifeq|ifneq|else|endif|include|override|export|unexport|vpath)\b");
            e.ChangedRange.SetStyle(Constant, @"^.*?(?=\=)|^.*?(?=:)", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(Variable, @"@[a-zA-Z_\d]*\b", RegexCompiledOption);
            e.ChangedRange.SetStyle(Number, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
        }

        private void YamlSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "#";
            e.ChangedRange.ClearStyle(Comment, String, Keyword, Number);
            e.ChangedRange.SetStyle(Comment, @"#.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(String, @"""""|''|"".*?[^\\]""|'.*?[^\\]'");
            e.ChangedRange.SetStyle(Keyword, @"^.*?(?=:)", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(String, @"\:.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(Number, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
        }

        private void DiffSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(Comment, String, Keyword, Constant, Storage);
            e.ChangedRange.SetStyle(Comment, @"\-\-\-.*$|\+\+\+.*$|\*\*\*.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(String, @"\!.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(Keyword, @"\+.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(Constant, @"\-.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(Storage, @"(?<=@)(.*?)(?=@)|@|@", RegexOptions.Multiline);
        }

        private void PowerShellSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "#";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.ClearStyle(Comment, String, Keyword,Variable, LibraryFunction, Number);
            e.ChangedRange.SetStyle(Comment, @"#.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(String, @"""""|''|"".*?[^\\]""|'.*?[^\\]'");
            e.ChangedRange.SetStyle(Variable, @"\$[a-zA-Z_\d]*\b", RegexCompiledOption);
            e.ChangedRange.SetStyle(Keyword,
                @"\b(at|break|continue|do|else|elseif|filter|for|foreach|if|in|return|until|where|while|function)\b");
            e.ChangedRange.SetStyle(LibraryFunction,
                @"\b(cat|cd|chdir|cls|copy|date|del|diff|dir|echo|erase|exit|fc|find|findstr|format|get|goto|h|history|select|kill|label|lp|ls|md|mkdir|mode|mount|move|new|param|path|pause|popd|print|prompt|ps|pushd|pwd|r|rd|rm|recover|rem|ren|rename|replace|restore|rmdir|set|setlocal|shift|sleep|sort|start|subst|tee|throw|time|title|trap|tree|type|ver|verify|vol|write|xcopy)\b");
            e.ChangedRange.SetStyle(Number, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
        }

        private void RSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "#";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.ClearStyle(Comment, String, Keyword, Constant, LibraryFunction, FunctionArgument, Number);
            e.ChangedRange.SetStyle(Comment, @"#.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(String, @"""""|''|"".*?[^\\]""|'.*?[^\\]'");
            e.ChangedRange.SetStyle(Keyword,
                @"\b(function|if|break|next|repeat|else|for|return|switch|while|in|invisible)\b");
            e.ChangedRange.SetStyle(FunctionArgument, @"\b.*=");
            e.ChangedRange.SetStyle(Constant,
                @"\b(logical|numeric|character|complex|matrix|array|list|factor|T|F|TRUE|FALSE|NULL|NA|Inf|NaN)\b");
            e.ChangedRange.SetStyle(LibraryFunction,
                @"\b(real|print|abs|matrix|pi|letters|LETTERS|month\.name|month\.abb)\b");
            e.ChangedRange.SetStyle(Number, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers(@"{", @"}");
        }

        private void LaTeXSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '{';
            e.ChangedRange.tb.RightBracket2 = '}';
            e.ChangedRange.ClearStyle(Comment, String, Keyword, Number);
            e.ChangedRange.SetStyle(Comment, @"%.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(String, @"""""|''|"".*?[^\\]""|'.*?[^\\]'");
            e.ChangedRange.SetStyle(Keyword, @"\\[a-zA-Z_\d]*\b");
            e.ChangedRange.SetStyle(Number, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
        }

        private void HaskellSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "--";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.tb.Range.ClearStyle(Comment);
            e.ChangedRange.ClearStyle(Comment, String, Keyword, Constant, Number);
            e.ChangedRange.SetStyle(String, @"""""|''|"".*?[^\\]""|'.*?[^\\]'");
            e.ChangedRange.SetStyle(Comment, @"--.*$", RegexOptions.Multiline);
            e.ChangedRange.tb.Range.SetStyle(Comment,
                new Regex(@"({\-.*?\-})|({\-.*)", RegexOptions.Singleline | RegexCompiledOption));
            e.ChangedRange.tb.Range.SetStyle(Comment, @"({\-.*?\-})|(.*\-})",
                RegexOptions.RightToLeft | RegexOptions.Singleline | RegexCompiledOption);
            e.ChangedRange.SetStyle(Keyword,
                @"\b(class|instance|qualified|as|hiding|deriving|where|data|type|case|of|let|in|newtype|default|infix|do|if|then|else|module)\b");
            e.ChangedRange.SetStyle(Constant,
                @"\b(Just|Nothing|Left|Right|True|False|LT|EQ|GT|LANGUAGE|UNPACK|INLINE)\b");
            e.ChangedRange.SetStyle(LibraryClass,
                @"\b(Integer|Int|Maybe|Either|Bool|Float|Double|Char|String|Ordering|ShowS|ReadS|FilePath|IO|Monad|Functor|Eq|Ord|Read|Show|Num|(Frac|Ra)tional|Enum|Bounded|Real(Frac|Float)?|Integral|Floating)\b");
            e.ChangedRange.SetStyle(LibraryFunction,
                @"\b(abs|acos|acosh|all|and|any|appendFile|applyM|asTypeOf|asin|asinh|atan|atan2|atanh|break|catch|ceiling|compare|concat|concatMap|const|cos|cosh|curry|cycle|decodeFloat|div|divMod|drop|dropWhile|elem|encodeFloat|enumFrom|enumFromThen|enumFromThenTo|enumFromTo|error|even|exp|exponent|fail|filter|flip|floatDigits|floatRadix|floatRange|floor|fmap|foldl|foldl1|foldr|foldr1|fromEnum|fromInteger|fromIntegral|fromRational|fst|gcd|getChar|getContents|getLine|head|id|init|interact|ioError|isDenormalized|isIEEE|isInfinite|isNaN|isNegativeZero|iterate|last|lcm|length|lex|lines|log|logBase|lookup|map|mapM|mapM_|max|maxBound|maximum|maybe|min|minBound|minimum|mod|negate|not|notElem|null|odd|or|otherwise|pi|pred|print|product|properFraction|putChar|putStr|putStrLn|quot|quotRem|read|readFile|readIO|readList|readLn|readParen|reads|readsPrec|realToFrac|recip|rem|repeat|replicate|return|reverse|round|scaleFloat|scanl|scanl1|scanr|scanr1|seq|sequence|sequence_|show|showChar|showList|showParen|showString|shows|showsPrec|significand|signum|sin|sinh|snd|span|splitAt|sqrt|subtract|succ|sum|tail|take|takeWhile|tan|tanh|toEnum|toInteger|toRational|truncate|uncurry|undefined|unlines|until|unwords|unzip|unzip3|userError|words|writeFile|zip|zip3|zipWith|zipWith3)\b");
            e.ChangedRange.SetStyle(Number, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
        }

        private void MATLABSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "%";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.ClearStyle(Comment, String, Keyword, Constant, Number);
            e.ChangedRange.SetStyle(Comment, @"%.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(String, @"""""|''|"".*?[^\\]""|'.*?[^\\]'");
            e.ChangedRange.SetStyle(Keyword,
                @"\b(function|global|persistent|break|case|catch|else|elseif|end|for|if|otherwise|return|switch|try|while)\b");
            e.ChangedRange.SetStyle(Number, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers(@"\bfunction\b", @"\bend\b");
        }

        private void CoffeeScriptSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "#";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.tb.Range.ClearStyle(Comment);
            e.ChangedRange.ClearStyle(Constant, String, ClassName, FunctionName, Keyword, Constant, LibraryClass,
                LibraryFunction, Number);
            e.ChangedRange.SetStyle(Comment, "#.*$", RegexOptions.Multiline);
            e.ChangedRange.tb.Range.SetStyle(Comment,
                new Regex(@"(###.*?\###)|(###.*)", RegexOptions.Singleline | RegexCompiledOption));
            e.ChangedRange.tb.Range.SetStyle(Comment, new Regex(@"(###.*?###)|(.*###)"));
            e.ChangedRange.SetStyle(String, @"""""|''|"".*?[^\\]""|'.*?[^\\]'", RegexCompiledOption);
            e.ChangedRange.SetStyle(ClassName, @"\b(class)\s+(?<range>\w+?)\b");
            e.ChangedRange.SetStyle(FunctionName, @".*(?==)=|.*(?=:):");
            e.ChangedRange.SetStyle(Keyword,
                @"\b(instanceof|new|delete|debugger|typeof|and|or|is|isnt|not|super|class|this|extends|break|by|catch|continue|else|finally|for|in|of|if|return|switch|then|throw|try|unless|when|while|until|loop|do)\b");
            e.ChangedRange.SetStyle(Constant, @"\b(true|on|yes|false|off|no|null)\b");
            e.ChangedRange.SetStyle(LibraryClass,
                @"\b(Array|ArrayBuffer|Blob|Boolean|Date|document|Function||Int(8|16|32|64)Array|Math|Map|Number|Object|Proxy|RegExp|Set|String|WeakMap|window|Uint(8|16|32|64)Array|XMLHttpRequest)\b");
            e.ChangedRange.SetStyle(LibraryFunction,
                @"\b(debug|warn|info|log|error|time|timeEnd|assert|decodeURI|encodeURI|eval|parse(Float|Int)|require|apply|call|concat|every|filter|forEach|from|hasOwnProperty|indexOf|isPrototypeOf|join|lastIndexOf|map|of|pop|propertyIsEnumerable|push|reduce|reverse|shift|slice|some|sort|splice|unshift|valueOf|create|definePropert(ies|y)|freeze|getOwnProperty(Descriptors|Names)|getProperty(Descriptor|Names)|getPrototypeOf|is(Extensible|Frozen|Sealed)|keys|preventExtensions|seal
)\b");
            e.ChangedRange.SetStyle(Number, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
        }

        /// <summary>
        ///     Highlight TCL Syntax
        /// </summary>
        /// <param name="e"></param>
        private void TclSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "#";
            e.ChangedRange.tb.LeftBracket = '{';
            e.ChangedRange.tb.RightBracket = '}';
            e.ChangedRange.tb.LeftBracket2 = '(';
            e.ChangedRange.tb.RightBracket2 = ')';
            e.ChangedRange.ClearStyle(Comment, String, Keyword, Variable, Number);
            e.ChangedRange.SetStyle(Comment, @"#.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(String, @"""""|''|"".*?[^\\]""|'.*?[^\\]'", RegexCompiledOption);
            e.ChangedRange.SetStyle(Variable, @"\$[a-zA-Z_\d]*\b", RegexCompiledOption);
            e.ChangedRange.SetStyle(Keyword,
                @"\b(while|for|catch|return|break|continue|switch|exit|foreach|proc|if|then|else|elseif|after|append|array|auto_execok|auto_import|auto_load|auto_mkindex|auto_mkindex_old|auto_qualify|auto_reset|bgerror|binary|cd|clock|close|concat|dde|encoding|eof|error|eval|exec|expr|fblocked|fconfigure|fcopy|file|fileevent|filename|flush|format|gets|glob|global|history|http|incr|info|interp|join|lappend|library|lindex|linsert|list|llength|load|lrange|lreplace|lsearch|lset|lsort|memory|msgcat|namespace|open|package|parray|pid|pkg::create|pkg_mkIndex|proc|puts|pwd|re_syntax|read|registry|rename|resource|scan|seek|set|socket|SafeBase|source|split|string|subst|Tcl|tcl_endOfWord|tcl_findLibrary|tcl_startOfNextWord|tcl_startOfPreviousWord|tcl_wordBreakAfter|tcl_wordBreakBefore|tcltest|tclvars|tell|time|trace|unknown|unset|update|uplevel|upvar|variable|vwait)\b");
            e.ChangedRange.SetStyle(Number, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
        }

        #endregion
    }

    #region Code Folding

    /// <summary>
    ///     Structure Xml Tag
    /// </summary>
    internal class XmlTag
    {
        /// <summary>
        ///     XmlTag id
        /// </summary>
        internal int Id;

        /// <summary>
        ///     XmlTag Name
        /// </summary>
        internal string Name;

        /// <summary>
        ///     XmlTag StartLine
        /// </summary>
        internal int StartLine;

        /// <summary>
        ///     XmlTag Marker
        /// </summary>
        internal string Marker
        {
            get { return Name + Id; }
        }
    }

    #endregion
}