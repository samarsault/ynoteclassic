#region

using System.Collections.Generic;
using System.Text.RegularExpressions;
using FastColoredTextBoxNS;

#endregion

namespace SS.Ynote.Classic
{
    /// <summary>
    ///     Predifined Syntax Highlighter
    /// </summary>
    public sealed class SyntaxHighlighter : ISyntaxHighlighter
    {
        #region Properties

        /// <summary>
        ///     Regex Compiled Options base on Platform x86 | x64
        /// </summary>
        private static RegexOptions RegexCompiledOption
        {
            get
            {
                if (PlatformType.GetOperationSystemPlatform() == Platform.X86)
                    return RegexOptions.Compiled;
                return RegexOptions.None;
            }
        }

        /// <summary>
        ///     String style
        /// </summary>
        public Style StringStyle { private get; set; }

        /// <summary>
        ///     Comment style
        /// </summary>
        public Style CommentStyle { private get; set; }

        /// <summary>
        ///     Number style
        /// </summary>
        public Style NumberStyle { private get; set; }

        /// <summary>
        ///     C# attribute style
        /// </summary>
        public Style AttributeStyle { private get; set; }

        /// <summary>
        ///     Class name style
        /// </summary>
        public Style ClassNameStyle { private get; set; }

        /// <summary>
        ///     Char Style
        /// </summary>
        public Style CharStyle { private get; set; }

        /// <summary>
        ///     Keyword style
        /// </summary>
        public Style KeywordStyle { private get; set; }

        /// <summary>
        ///     Style of tags in comments of C#
        /// </summary>
        public Style CommentTagStyle { private get; set; }

        /// <summary>
        ///     HTML attribute value style
        /// </summary>
        public Style AttributeValueStyle { private get; set; }

        /// <summary>
        ///     HTML tag brackets style
        /// </summary>
        public Style TagBracketStyle { private get; set; }

        /// <summary>
        ///     HTML tag name style
        /// </summary>
        public Style TagNameStyle { private get; set; }

        /// <summary>
        ///     HTML Entity style
        /// </summary>
        public Style HtmlEntityStyle { private get; set; }

        /// <summary>
        ///     Preprocessor Style
        /// </summary>
        public Style PreprocessorStyle { private get; set; }

        /// <summary>
        ///     Variable style
        /// </summary>
        public Style VariableStyle { private get; set; }

        /// <summary>
        ///     Specific PHP keyword style
        /// </summary>
        public Style KeywordStyle2 { private get; set; }

        /// <summary>
        ///     Specific PHP keyword style
        /// </summary>
        public Style KeywordStyle3 { private get; set; }

        /// <summary>
        ///     SQL Statements style
        /// </summary>
        public Style StatementsStyle { private get; set; }

        /// <summary>
        ///     SQL Functions style
        /// </summary>
        public Style FunctionsStyle { private get; set; }

        /// <summary>
        ///     SQL Types style
        /// </summary>
        public Style TypesStyle { private get; set; }

        /// <summary>
        ///     CSS Selector Style
        /// </summary>
        public Style CSSSelectorStyle { private get; set; }

        /// <summary>
        ///     CSS Property Style
        /// </summary>
        public Style CSSPropertyStyle { private get; set; }

        /// <summary>
        ///     CSS Property Value Style
        /// </summary>
        public Style CSSPropertyValueStyle { private get; set; }

        /// <summary>
        ///     Python/Ruby Class Name Style
        /// </summary>
        public Style ClassNameStyle2 { private get; set; }

        /// <summary>
        ///     Inside Bracket Style
        /// </summary>
        public Style InBracketStyle { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Highlight Syntax
        /// </summary>
        /// <param name="language"></param>
        /// <param name="range"></param>
        public void HighlightSyntax(Language language, TextChangedEventArgs range)
        {
            switch (language)
            {
                case Language.Actionscript:
                    ActionscriptSyntaxHighlight(range);
                    break;
                case Language.Assembly:
                    AsmSyntaxHighlight(range);
                    break;
                case Language.Antlr:
                    AntlrSyntaxHighlight(range);
                    break;
                case Language.Objective_C:
                    ObjCHighlight(range);
                    break;
                case Language.Batch:
                    BatchSyntaxHighlight(range);
                    break;
                case Language.C:
                    CppSyntaxHighlight(range);
                    break;
                case Language.CPP:
                    CppSyntaxHighlight(range);
                    break;
                case Language.CSS:
                    CSSHighlight(range);
                    break;
                case Language.CSharp:
                    CSharpSyntaxHighlight(range);
                    break;
                case Language.D:
                    DSyntaxHighlight(range);
                    break;
                case Language.Diff:
                    DiffSyntaxHighlight(range);
                    break;
                case Language.Java:
                    JavaSyntaxHighlight(range);
                    break;
                case Language.Lua:
                    LuaSyntaxHighlight(range);
                    break;
                case Language.Python:
                    PythonSyntaxHighlight(range);
                    break;
                case Language.QBasic:
                    QBHighlight(range);
                    break;
                case Language.Perl:
                    PerlSyntaxHighlight(range);
                    break;
                case Language.Ruby:
                    RubySyntaxHighlight(range);
                    break;
                case Language.Xml:
                    XmlSyntaxHighlight(range);
                    break;
                case Language.INI:
                    IniSyntaxHighlight(range);
                    break;
                case Language.Makefile:
                    MakeFileSyntaxHighlight(range);
                    break;
                case Language.JSON:
                    JsonSyntaxHighlight(range);
                    break;
                case Language.VB:
                    VBSyntaxHighlight(range);
                    break;
                case Language.HTML:
                    HighlightHtmlJSCSS(range);
                    break;
                case Language.Javascript:
                    JScriptSyntaxHighlight(range);
                    break;
                case Language.SQL:
                    SqlSyntaxHighlight(range);
                    break;
                case Language.Scheme:
                    SchemeSyntaxHighlight(range);
                    break;
                case Language.Shell:
                    ShellSyntaxHighlight(range);
                    break;
                case Language.PHP:
                    HighlightPHPHtml(range);
                    break;
                case Language.Lisp:
                    LispSyntaxHighlight(range);
                    break;
                case Language.FSharp:
                    FSharpSyntaxHighlight(range);
                    break;
                case Language.Pascal:
                    PascalSyntaxHighlight(range);
                    break;
                case Language.Scala:
                    ScalaSyntaxHighlight(range);
                    break;
                case Language.Yaml:
                    YamlSyntaxHighlight(range);
                    break;
            }
        }

        #endregion

        #region Private Methods

        private Regex _cSharpAttributeRegex,
            _cSharpClassNameRegex;

        private Regex _cSharpCommentRegex1,
            _cSharpCommentRegex2,
            _cSharpCommentRegex3;

        private Regex _cSharpKeywordRegex;

        private Regex _cSharpKeywordRegex2;

        private Regex _cSharpNumberRegex;
        private Regex _cSharpStringRegex;
        private Regex _cppClassNameRegex;
        private Regex _cppCommentRegex1;
        private Regex _cppCommentRegex2;
        private Regex _cppCommentRegex3;
        private Regex _cppFunctionsRegex;
        private Regex _cppKeywordRegex;
        private Regex _cppKeywordRegex2;
        private Regex _cppNumberRegex;
        private Regex _cppStringRegex;
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
        private Regex _jScriptKeywordRegex2;
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
        private Regex _javaStringRegex;
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

        private Regex _phpKeywordRegex1;

        private Regex _phpKeywordRegex2;

        private Regex _phpKeywordRegex3;

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

        /// <summary>
        ///     Init CSS Regex
        /// </summary>
        private void InitCssRegex()
        {
            _cssCommentRegex2 = new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            _cssCommentRegex3 = new Regex(@"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);
            _cssNumberRegex = new Regex(@"\d+[\.]?\d*(pt|px|\%|em)?");
            _cssPropertyRegex =
                new Regex(
                    @"(?<![\-\w])(animation|animation-name|animation-duration|animation-timing-function|animation-delay|animation-iteration-count|animation-direction|animation-play-state|appearance|backface-visibility|background|background|background-attachment|background-color|background-image|background-position|background-repeat|background-clip|background-origin|background-size|border|border|border-bottom|border-bottom-color|border-bottom-style|border-bottom-width|border-collapse|border-color|border-left|border-left-color|border-left-style|border-left-width|border-right|border-right-color|border-right-style|border-right-width|border-spacing|border-style|border-top|border-top-color|border-top-style|border-top-width|border-width|border-bottom-left-radius|border-bottom-right-radius|border-image|border-image-outset|border-image-repeat|border-image-slice|border-image-source|border-image-width|border-radius|border-top-left-radius|border-top-right-radius|box|box-align|box-direction|box-flex|box-flex-group|box-lines|box-ordinal-group|box-orient|box-pack|box-sizing|box-shadow|caption-side|clear|clip|color|column|column-count|column-fill|column-gap|column-rule|column-rule-color|column-rule-style|column-rule-width|column-span|column-width|columns|content|counter-increment|counter-reset|cursor|direction|display|empty-cells|float|font|font|font-family|font-size|font-style|font-variant|font-weight|@font-face|font-size-adjust|font-stretch|grid-columns|grid-rows|hanging-punctuation|height|icon|@keyframes|letter-spacing|line-height|list-style|list-style|list-style-image|list-style-position|list-style-type|margin|margin|margin-bottom|margin-left|margin-right|margin-top|max-height|max-width|min-height|min-width|nav|nav-down|nav-index|nav-left|nav-right|nav-up|opacity|outline|outline|outline-color|outline-offset|outline-style|outline-width|overflow|overflow-x|overflow-y|padding|padding|padding-bottom|padding-left|padding-right|padding-top|page-break|page-break-after|page-break-before|page-break-inside|perspective|perspective-origin|position|punctuation-trim|quotes|resize|rotation|rotation-point|table-layout|target|target|target-name|target-new|target-position|text|text-align|text-decoration|text-indent|text-justify|text-outline|text-overflow|text-shadow|text-transform|text-wrap|transform|transform|transform-origin|transform-style|transition|transition|transition-property|transition-duration|transition-timing-function|transition-delay|vertical-align|visibility|width|white-space|word-spacing|word-break|word-wrap|z-index|rgb|rgba|url|alpha|attr|counter|rect|hsl|hsla)(?![\-\w])|-moz-[a-zA-Z_\d]*(?![\-\w])|-webkit-[a-zA-Z_\d]*(?![\-\w])",
                    RegexOptions.IgnoreCase);
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
        private void CSSHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "/*";
            e.ChangedRange.ClearStyle(StringStyle, CSSPropertyStyle, CSSSelectorStyle, CSSPropertyValueStyle,
                NumberStyle);

            e.ChangedRange.tb.RightBracket = '}';
            e.ChangedRange.tb.LeftBracket2 = '(';
            e.ChangedRange.tb.RightBracket2 = ')';
            e.ChangedRange.tb.LeftBracket = '{';
            e.ChangedRange.tb.Range.ClearStyle(CommentTagStyle);
            if (_cssCommentRegex2 == null)
                InitCssRegex();
            e.ChangedRange.tb.Range.SetStyle(CommentTagStyle, _cssCommentRegex2);
            e.ChangedRange.tb.Range.SetStyle(CommentTagStyle, _cssCommentRegex3);
            e.ChangedRange.SetStyle(CSSPropertyStyle, _cssPropertyRegex);
            e.ChangedRange.SetStyle(CSSSelectorStyle, _cssSelectorRegex);
            e.ChangedRange.SetStyle(CSSPropertyValueStyle, _cssPropertyValueRegex);
            e.ChangedRange.SetStyle(NumberStyle, _cssNumberRegex);
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
            e.ChangedRange.ClearStyle(CommentStyle, StringStyle, KeywordStyle, KeywordStyle2, KeywordStyle3, CharStyle);
            e.ChangedRange.SetStyle(CommentStyle, @"rem .*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(StringStyle, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
            e.ChangedRange.SetStyle(KeywordStyle,
                @"\b(title|set|call|copy|exists|cut|cd|@|nul|choice|do|shift|sgn|errorlevel|con|prn|lpt1|echo|off|for|in|do|goto|if|then|else|not|end)\b",
                RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(KeywordStyle2, @":.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(KeywordStyle3, @"%.*?[^\\]%", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(CharStyle, @"\;|-|>|<|=|\+|\,|\$|\^|\[|\]|\$|:|\!");
        }

        private void InitJavaAsRegex()
        {
            _javaStringRegex = new Regex(@"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
            _javaCommentRegex1 = new Regex(@"//.*$", RegexOptions.Multiline);
            _javaCommentRegex2 = new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            _javaCommentRegex3 = new Regex(@"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);
            _javaNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            _javaAttributeRegex = new Regex(@"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline);
            _javaClassNameRegex = new Regex(@"\b(class|struct|enum|interface)\s+(?<range>\w+?)\b");
            _javaKeywordRegex =
                new Regex(
                    @"\b(abstract|break|case|catch|class|continue|default|delegate|do|else|extends|final|finally|for|if|implements|import|instanceof|interface|native|new|null|package|private|protected|public|static|super|switch|this|throw|throws|try|while)\b");
            _javaKeywordRegex2 =
                new Regex(
                    @"\b(void|volatile|double|transient|synchronized|short|long|float|char|boolean|byte|true|false|int|return)\b");
            _javaFunctionRegex = new Regex(@"\b(void|extends)\s+(?<range>\w+?)\b");
        }

        /// <summary>
        ///     Java/Actionscript Highlight
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
                InitJavaAsRegex();
            e.ChangedRange.tb.Range.ClearStyle(CommentStyle);
            e.ChangedRange.ClearStyle(PreprocessorStyle, StringStyle, NumberStyle, ClassNameStyle, ClassNameStyle2,
                KeywordStyle,
                KeywordStyle2, KeywordStyle3, CharStyle);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _javaCommentRegex1);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _javaCommentRegex2);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _javaCommentRegex3);
            //preprocessor
            e.ChangedRange.SetStyle(PreprocessorStyle, @"#.*$", RegexOptions.Multiline);
            //string
            e.ChangedRange.SetStyle(StringStyle, _javaStringRegex);
            //number highlighting
            e.ChangedRange.SetStyle(NumberStyle, _javaNumberRegex);
            //attribute highlighting
            e.ChangedRange.SetStyle(CommentStyle, _javaAttributeRegex);
            //class name highlighting
            e.ChangedRange.SetStyle(ClassNameStyle, _javaClassNameRegex);
            e.ChangedRange.SetStyle(ClassNameStyle2, _javaFunctionRegex);
            //keyword highlighting
            e.ChangedRange.SetStyle(KeywordStyle, _javaKeywordRegex);
            e.ChangedRange.SetStyle(KeywordStyle2, _javaKeywordRegex2);
            e.ChangedRange.SetStyle(KeywordStyle3,
                new Regex(
                    @"\b(AWTError|AWTEvent|AWTEventListener|AWTEventMulticaster|AWTException|AWTPermission|AbstractCollection|AbstractList|AbstractMap|AbstractMethodError|AbstractSequentialList|AbstractSet|AccessControlContext|AccessControlException|AccessController|AccessException|AccessibleObject|Acl|AclEntry|AclNotFoundException|ActionEvent|ActionListener|Activatable|ActivateFailedException|ActivationDesc|ActivationException|ActivationGroup|ActivationGroupDesc|ActivationGroupID|ActivationGroup_Stub|ActivationID|ActivationInstantiator|ActivationMonitor|ActivationSystem|Activator|ActiveEvent|Adjustable|AdjustmentEvent|AdjustmentListener|Adler32|AffineTransform|AffineTransformOp|AlgorithmParameterGenerator|AlgorithmParameterGeneratorSpi|AlgorithmParameterSpec|AlgorithmParameters|AlgorithmParametersSpi|AllPermission|AllPermissionCollection|AlphaComposite|AlphaCompositeContext|AlreadyBoundException|Annotation|Applet|AppletContext|AppletInitializer|AppletStub|Arc2D|ArcIterator|Area|AreaAveragingScaleFilter|AreaIterator|ArithmeticException|Array|Array|ArrayIndexOutOfBoundsException|ArrayList|ArrayStoreException|Arrays|AttributeEntry|AttributedCharacterIterator|AttributedString|Attributes|AudioClip|Authenticator|Autoscroll|BandCombineOp|BandedSampleModel|BasicPermission|BasicPermissionCollection|BasicStroke|BatchUpdateException|BeanContext|BeanContextChild|BeanContextChildComponentProxy|BeanContextChildSupport|BeanContextContainerProxy|BeanContextEvent|BeanContextMembershipEvent|BeanContextMembershipListener|BeanContextProxy|BeanContextServiceAvailableEvent|BeanContextServiceProvider|BeanContextServiceProviderBeanInfo|BeanContextServiceRevokedEvent|BeanContextServiceRevokedListener|BeanContextServices|BeanContextServicesListener|BeanContextServicesSupport|BeanContextSupport|BeanDescriptor|BeanInfo|Beans|BeansAppletContext|BeansAppletStub|BigDecimal|BigInteger|BindException|BitSet|Blob|Book|Boolean|BorderLayout|BreakIterator|BufferedImage|BufferedImageFilter|BufferedImageOp|BufferedInputStream|BufferedOutputStream|BufferedReader|BufferedWriter|Button|ButtonPeer|Byte|ByteArrayInputStream|ByteArrayOutputStream|ByteLookupTable|CMMException|CRC32|CRL|CRLException|Calendar|CallableStatement|Canvas|CanvasPeer|CardLayout|Certificate|Certificate|CertificateEncodingException|CertificateException|CertificateExpiredException|CertificateFactory|CertificateFactorySpi|CertificateNotYetValidException|CertificateParsingException|CharArrayReader|CharArrayWriter|CharConversionException|Character|CharacterBreakData|CharacterIterator|Checkbox|CheckboxGroup|CheckboxMenuItem|CheckboxMenuItemPeer|CheckboxPeer|CheckedInputStream|CheckedOutputStream|Checksum|Choice|ChoiceFormat|ChoicePeer|Class|ClassCastException|ClassCircularityError|ClassFormatError|ClassLoader|ClassNotFoundException|Clipboard|ClipboardOwner|Clob|CloneNotSupportedException|Cloneable|CodeSource|CollationElementIterator|CollationKey|CollationRules|Collator|Collection|Collections|Color|ColorConvertOp|ColorModel|ColorPaintContext|ColorSpace|CompactByteArray|CompactCharArray|CompactIntArray|CompactShortArray|CompactStringArray|Comparable|Comparator|Compiler|Component|ComponentAdapter|ComponentColorModel|ComponentEvent|ComponentListener|ComponentOrientation|ComponentPeer|ComponentSampleModel|Composite|CompositeContext|ConcurrentModificationException|Conditional|ConnectException|ConnectException|ConnectIOException|Connection|Constructor|Container|ContainerAdapter|ContainerEvent|ContainerListener|ContainerPeer|ContentHandler|ContentHandlerFactory|ContextualRenderedImageFactory|ConvolveOp|CropImageFilter|CubicCurve2D|CubicIterator|Cursor|Customizer|DGC|DSAKey|DSAKeyPairGenerator|DSAParameterSpec|DSAParams|DSAPrivateKey|DSAPrivateKeySpec|DSAPublicKey|DSAPublicKeySpec|DataBuffer|DataBufferByte|DataBufferInt|DataBufferShort|DataBufferUShort|DataFlavor|DataFormatException|DataInput|DataInputStream|DataOutput|DataOutputStream|DataTruncation|DatabaseMetaData|DatagramPacket|DatagramSocket|DatagramSocketImpl|Date|DateFormat|DateFormatSymbols|DateFormatZoneData|DateFormatZoneData_en|DecimalFormat|DecimalFormatSymbols|Deflater|DeflaterOutputStream|DesignMode|Dialog|DialogPeer|Dictionary|DigestException|DigestInputStream|DigestOutputStream|DigitList|Dimension|Dimension2D|DirectColorModel|DnDConstants|Double|DragGestureEvent|DragGestureListener|DragGestureRecognizer|DragSource|DragSourceContext|DragSourceContextPeer|DragSourceDragEvent|DragSourceDropEvent|DragSourceEvent|DragSourceListener|Driver|DriverInfo|DriverManager|DriverPropertyInfo|DropTarget|DropTargetContext|DropTargetContextPeer|DropTargetDragEvent|DropTargetDropEvent|DropTargetEvent|DropTargetListener|DropTargetPeer|EOFException|Ellipse2D|EllipseIterator|EmptyStackException|EncodedKeySpec|EntryPair|Enumeration|Error|Event|EventDispatchThread|EventListener|EventObject|EventQueue|EventQueueItem|EventSetDescriptor|Exception|ExceptionInInitializerError|ExportException|Externalizable|FDBigInt|FactoryURLClassLoader|FeatureDescriptor|Field|FieldPosition|File|FileDescriptor|FileDialog|FileDialogPeer|FileFilter|FileInputStream|FileNameMap|FileNotFoundException|FileOutputStream|FilePermission|FilePermissionCollection|FileReader|FileSystem|FileWriter|FilenameFilter|FilterInputStream|FilterOutputStream|FilterReader|FilterWriter|FilteredImageSource|FinalReference|Finalizer|FlatteningPathIterator|FlavorMap|Float|FloatingDecimal|FlowLayout|FocusAdapter|FocusEvent|FocusListener|FocusManager|Font|FontMetrics|FontPeer|FontRenderContext|Format|Frame|FramePeer|GZIPInputStream|GZIPOutputStream|GeneralPath|GeneralPathIterator|GeneralSecurityException|GenericBeanInfo|GlyphJustificationInfo|GlyphMetrics|GlyphVector|GradientPaint|GradientPaintContext|GraphicAttribute|Graphics|Graphics2D|GraphicsConfigTemplate|GraphicsConfiguration|GraphicsDevice|GraphicsEnvironment|GregorianCalendar|GridBagConstraints|GridBagLayout|GridBagLayoutInfo|GridLayout|Group|Guard|GuardedObject|HashMap|HashSet|Hashtable|HttpURLConnection|ICC_ColorSpace|ICC_Profile|ICC_ProfileGray|ICC_ProfileRGB|IOException|Identity|IdentityScope|IllegalAccessError|IllegalAccessException|IllegalArgumentException|IllegalComponentStateException|IllegalMonitorStateException|IllegalPathStateException|IllegalStateException|IllegalThreadStateException|Image|ImageConsumer|ImageFilter|ImageGraphicAttribute|ImageMediaEntry|ImageObserver|ImageProducer|ImagingOpException|IncompatibleClassChangeError|IndexColorModel|IndexOutOfBoundsException|IndexedPropertyDescriptor|InetAddress|InetAddressImpl|Inflater|InflaterInputStream|InheritableThreadLocal|InputContext|InputEvent|InputMethodEvent|InputMethodHighlight|InputMethodListener|InputMethodRequests|InputStream|InputStreamReader|InputSubset|Insets|InstantiationError|InstantiationException|IntHashtable|Integer|InternalError|InterruptedException|InterruptedIOException|IntrospectionException|Introspector|InvalidAlgorithmParameterException|InvalidClassException|InvalidDnDOperationException|InvalidKeyException|InvalidKeySpecException|InvalidObjectException|InvalidParameterException|InvalidParameterSpecException|InvocationEvent|InvocationTargetException|ItemEvent|ItemListener|ItemSelectable|Iterator|JarEntry|JarException|JarFile|JarInputStream|JarOutputStream|JarURLConnection|JarVerifier|Kernel|Key|KeyAdapter|KeyEvent|KeyException|KeyFactory|KeyFactorySpi|KeyListener|KeyManagementException|KeyPair|KeyPairGenerator|KeyPairGeneratorSpi|KeySpec|KeyStore|KeyStoreException|KeyStoreSpi|Label|LabelPeer|LastOwnerException|LayoutManager|LayoutManager2|Lease|LightweightDispatcher|LightweightPeer|LightweightPeer|Line2D|LineBreakData|LineBreakMeasurer|LineIterator|LineMetrics|LineNumberInputStream|LineNumberReader|LinkageError|LinkedList|List|List|ListIterator|ListPeer|ListResourceBundle|LoaderHandler|Locale|LocaleData|LocaleElements|LocaleElements_en|LocaleElements_en_US|LocateRegistry|LogStream|Long|LookupOp|LookupTable|MalformedURLException|Manifest|Map|MarshalException|MarshalledObject|Math|MediaEntry|MediaTracker|Member|MemoryImageSource|Menu|MenuBar|MenuBarPeer|MenuComponent|MenuComponentPeer|MenuContainer|MenuItem|MenuItemPeer|MenuPeer|MenuShortcut|MergeCollation|MessageDigest|MessageDigestSpi|MessageFormat|Method|MethodDescriptor|MimeType|MimeTypeParameterList|MimeTypeParseException|MissingResourceException|Modifier|MouseAdapter|MouseDragGestureRecognizer|MouseEvent|MouseListener|MouseMotionAdapter|MouseMotionListener|MultiPixelPackedSampleModel|MulticastSocket|MultipleMaster|Naming|NativeLibLoader|NegativeArraySizeException|NetPermission|NoClassDefFoundError|NoRouteToHostException|NoSuchAlgorithmException|NoSuchElementException|NoSuchFieldError|NoSuchFieldException|NoSuchMethodError|NoSuchMethodException|NoSuchObjectException|NoSuchProviderException|NoninvertibleTransformException|Normalizer|NotActiveException|NotBoundException|NotOwnerException|NotSerializableException|NullPointerException|Number|NumberFormat|NumberFormatException|ObjID|Object|ObjectInput|ObjectInputStream|ObjectInputStreamWithLoader|ObjectInputValidation|ObjectOutput|ObjectOutputStream|ObjectStreamClass|ObjectStreamConstants|ObjectStreamException|ObjectStreamField|Observable|Observer|OpenType|Operation|OptionalDataException|OutOfMemoryError|OutputStream|OutputStreamWriter|Owner|PKCS8EncodedKeySpec|Package|PackedColorModel|PageFormat|Pageable|Paint|PaintContext|PaintEvent|Panel|PanelPeer|Paper|ParameterBlock|ParameterDescriptor|ParseException|ParsePosition|PasswordAuthentication|PathIterator|PatternEntry|PeerFixer|Permission|Permission|PermissionCollection|Permissions|PermissionsEnumerator|PermissionsHash|PhantomReference|PipedInputStream|PipedOutputStream|PipedReader|PipedWriter|PixelGrabber|PixelInterleavedSampleModel|PlainDatagramSocketImpl|PlainSocketImpl|Point|Point2D|Policy|Polygon|PopupMenu|PopupMenuPeer|PreparedStatement|Principal|PrintGraphics|PrintJob|PrintStream|PrintWriter|Printable|PrinterAbortException|PrinterException|PrinterGraphics|PrinterIOException|PrinterJob|PrivateKey|PrivilegedAction|PrivilegedActionException|PrivilegedExceptionAction|Process|ProfileDataException|Properties|PropertyChangeEvent|PropertyChangeListener|PropertyChangeSupport|PropertyDescriptor|PropertyEditor|PropertyEditorManager|PropertyEditorSupport|PropertyPermission|PropertyPermissionCollection|PropertyResourceBundle|PropertyVetoException|ProtectionDomain|ProtocolException|Provider|ProviderException|PublicKey|PushbackInputStream|PushbackReader|QuadCurve2D|QuadIterator|Queue|RGBImageFilter|RMIClassLoader|RMIClientSocketFactory|RMIFailureHandler|RMISecurityException|RMISecurityManager|RMIServerSocketFactory|RMISocketFactory|RSAPrivateCrtKey|RSAPrivateCrtKeySpec|RSAPrivateKey|RSAPrivateKeySpec|RSAPublicKey|RSAPublicKeySpec|Random|RandomAccessFile|Raster|RasterFormatException|RasterOp|Reader|RectIterator|Rectangle|Rectangle2D|RectangularShape|Ref|Reference|ReferenceQueue|ReflectPermission|Registry|RegistryHandler|Remote|RemoteCall|RemoteException|RemoteObject|RemoteRef|RemoteServer|RemoteStub|RenderContext|RenderableImage|RenderableImageOp|RenderableImageProducer|RenderedImage|RenderedImageFactory|RenderingHints|ReplicateScaleFilter|RescaleOp|ResourceBundle|ResultSet|ResultSetMetaData|RoundRectIterator|RoundRectangle2D|RuleBasedCollator|Runnable|Runtime|RuntimeException|RuntimePermission|SQLData|SQLException|SQLInput|SQLOutput|SQLWarning|SampleModel|ScrollPane|ScrollPaneAdjustable|ScrollPanePeer|Scrollbar|ScrollbarPeer|SecureClassLoader|SecureRandom|SecureRandomSpi|Security|SecurityException|SecurityManager|SecurityPermission|SentenceBreakData|SequenceInputStream|Serializable|SerializablePermission|ServerCloneException|ServerError|ServerException|ServerNotActiveException|ServerRef|ServerRuntimeException|ServerSocket|Set|Shape|ShapeGraphicAttribute|Short|ShortLookupTable|Signature|SignatureException|SignatureSpi|SignedObject|Signer|SimpleBeanInfo|SimpleDateFormat|SimpleTextBoundary|SimpleTimeZone|SinglePixelPackedSampleModel|Skeleton|SkeletonMismatchException|SkeletonNotFoundException|Socket|SocketException|SocketImpl|SocketImplFactory|SocketInputStream|SocketOptions|SocketOutputStream|SocketPermission|SocketPermissionCollection|SocketSecurityException|SoftReference|SortedMap|SortedSet|SpecialMapping|Stack|StackOverflowError|Statement|StreamCorruptedException|StreamTokenizer|String|StringBuffer|StringBufferInputStream|StringCharacterIterator|StringIndexOutOfBoundsException|StringReader|StringSelection|StringTokenizer|StringWriter|Stroke|Struct|StubNotFoundException|SubList|SyncFailedException|System|SystemColor|SystemFlavorMap|TextArea|TextAreaPeer|TextAttribute|TextBoundaryData|TextComponent|TextComponentPeer|TextEvent|TextField|TextFieldPeer|TextHitInfo|TextJustifier|TextLayout|TextLine|TextListener|TextMeasurer|TexturePaint|TexturePaintContext|Thread|ThreadDeath|ThreadGroup|ThreadLocal|Throwable|TileObserver|Time|TimeZone|TimeZoneData|Timestamp|TooManyListenersException|Toolkit|Transferable|TransformAttribute|Transparency|TreeMap|TreeSet|Types|UID|URL|URLClassLoader|URLConnection|URLDecoder|URLEncoder|URLStreamHandler|URLStreamHandlerFactory|UTFDataFormatException|UnexpectedException|UnicastRemoteObject|UnicodeClassMapping|UnknownContentHandler|UnknownError|UnknownGroupException|UnknownHostException|UnknownHostException|UnknownObjectException|UnknownServiceException|UnmarshalException|UnrecoverableKeyException|Unreferenced|UnresolvedPermission|UnresolvedPermissionCollectio|UnsatisfiedLinkError|UnsupportedClassVersionError|UnsupportedEncodingException|UnsupportedFlavorException|UnsupportedOperationException|Utility|VMID|ValidationCallback|Vector|VerifyError|VetoableChangeListener|VetoableChangeSupport|VirtualMachineError|Visibility|Void|WeakHashMap|WeakReference|Win32FileSystem|Win32Process|Window|WindowAdapter|WindowEvent|WindowListener|WindowPeer|WordBreakData|WordBreakTable|WritableRaster|WritableRenderedImage|WriteAbortedException|Writer|X509CRL|X509CRLEntry|X509Certificate|X509EncodedKeySpec|X509Extension|ZipConstants|ZipEntry|ZipException|ZipFile|ZipInputStream|ZipOutputStream)\b"));
            //Char Highlighting
            e.ChangedRange.SetStyle(CharStyle, @"\;|-|>|<|=|\+|\,|\$|\^|\[|\]");
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers("{", "}");
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
        }

        /// <summary>
        ///     QBasic Syntax Highlight
        /// </summary>
        /// <param name="r"></param>
        private void QBHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.ClearStyle(CommentStyle, StringStyle, KeywordStyle, KeywordStyle2, VariableStyle, NumberStyle);
            e.ChangedRange.SetStyle(CommentStyle, @"\'.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(StringStyle, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
            e.ChangedRange.SetStyle(KeywordStyle,
                @"\b(ABS|AND|AS|ASC|ATN|BASE|BEEP|BLOAD|BSAVE|CDBL|CHR\$|CINT|CIRCLE|CLEAR|CLNG|CLS|COLOR|COM|COMMAND$|CONST|COS|CSNG|CSRLIN|CSRUN|CVD|CVDMBF|CVI|CVL|CVS|CVSMBF|DATA|DATE$|DEF|DEFDBL|DEFINT|DEFLNG|DEFSNG|DEFSTR|DIM|DOUBLE|DRAW|ENVIRON|ENVIRON$|EQV|ERASE|ERDEV|ERDEV$|ERL|ERR|ERROR|EXP|FIX|FN|FRE|HEX$|IMP|INSTR|INT|INTEGER|KEY|LBOUND|LCASE$|LEFT$|LEN|LET|LINE|LIST|LOCATE|LOG|LONG|LPOS|LTRIM$|MID$|MKD$|MKDMBF$|MKI$|MKL$|MKS$|MOD|NOT|OCT$|OFF|ON|OPEN|OPTION|OR|OUT|OUTPUT|PAINT|PALETTE|PCOPY|PEEK|PEN|PLAY|PMAP|POINT|POKE|POS|PRESET|PSET|RANDOMIZE|READ|REDIM|RESTORE|RIGHT$|RND|RTRIM$|SADD|SCREEN|SEEK|SEG|SETMEM|SGN|SIGNAL|SIN|SINGLE|SLEEP|SOUND|SPACE$|SPC|SQR|STATIC|STICK|STOP|STR$|STRIG|STRING|STRING$|SWAP|TAB|TAN|TIME$|TIMER|TROFF|TRON|TYPE|UBOUND|UCASE$|UEVENT|VAL|VARPTR|VARPTR$|VARSEG|VIEW|WIDTH|WINDOW|XOR)\b",
                RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(KeywordStyle2,
                @"\b(ABSOLUTE|ALIAS|BYVAL|CALL|CALLS|CASE|CDECL|CHAIN|COMMON|DO|DECLARE|ELSE|ELSEIF|END|ENDIF|EXIT|FOR|FUNCTION|GOSUB|GOTO|IF|INT86OLD|INT86XOLD|INTERUPT|INTERUPTX|IS|LOCAL|LOOP|MERGE|NEXT|RESUME|RETURN|RUN|SELECT|SHARED|SHELL|STEP|SUB|SYSTEM|THEN|TO|UNTIL|WEND|WHILE|ACCESS|ANY|APPEND|BINARY|CHDIR|CLOSE|EOF|FIELD|FILEATTR|FILES|FREEFILE|GET|INKEY$|INP|INPUT|IOCTL|IOCTL$|KILL|LINE|LOC|LOCK|LOF|LPRINT|LSET|MKDIR|NAME|OPEN|OUT|OUTPUT|PRINT|PUT|RANDOM|RESET|RMDIR|RSET|SEEK|UNLOCK|USING|WAIT|WIDTH|WRITE)\b",
                RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(VariableStyle, @"\$[a-zA-Z_\d]*\b", RegexCompiledOption);
            e.ChangedRange.SetStyle(NumberStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
        }

        /// <summary>
        ///     Init C++ Regex
        /// </summary>
        private void InitCppRegex()
        {
            _cppStringRegex = new Regex(
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
            _cppCommentRegex1 = new Regex(@"//.*$", RegexOptions.Multiline);
            _cppCommentRegex2 = new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            _cppCommentRegex3 = new Regex(@"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);
            _cppNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            _cppKeywordRegex =
                new Regex(
                    @"\b(abstract|class|interface|enum|event|delegate|break|base|case|break|default|typedef|as|catch|char|checked|const|continue|decimal|default|do|double|else|explicit|extern|finally|fixed|float|for|foreach|goto|if|implicit|in|int|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|struct|switch|this|throw|try|typeof|ulong|unchecked|unsafe|using|virtual|volatile|while|add|alias|ascending|descending|dynamic|from|get|global|group|join|let|orderby|partial|remove|select|set|value|var|where|yield)\b");
            _cppClassNameRegex = new Regex(@"\b(class|struct|enum|interface)\s+(?<range>\w+?)\b");
            _cppKeywordRegex2 = new Regex(@"\b(void|byte|bool|string|int|ulong|ushort|uint|true|false)\b");
            _cppFunctionsRegex = new Regex(@"\b(void|int|bool|string|uint|ushort|ulong|byte)\s+(?<range>\w+?)\b");
        }

        /// <summary>
        ///     C++ Syntax Highlight
        /// </summary>
        /// <param name="r"></param>
        private void CppSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "//";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.LeftBracket2 = '{';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.RightBracket2 = '}';
            e.ChangedRange.tb.Range.ClearStyle(CommentStyle);
            e.ChangedRange.ClearStyle(StringStyle, KeywordStyle, KeywordStyle2, NumberStyle, CharStyle, ClassNameStyle,
                ClassNameStyle2, PreprocessorStyle);
            if (_cppCommentRegex1 == null)
                InitCppRegex();
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _cppCommentRegex1);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _cppCommentRegex2);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _cppCommentRegex3);
            e.ChangedRange.SetStyle(StringStyle, _cppStringRegex);
            e.ChangedRange.SetStyle(PreprocessorStyle, "#.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(ClassNameStyle, _cppClassNameRegex);
            e.ChangedRange.SetStyle(ClassNameStyle2, _cppFunctionsRegex);
            e.ChangedRange.SetStyle(KeywordStyle, _cppKeywordRegex);
            e.ChangedRange.SetStyle(KeywordStyle2, _cppKeywordRegex2);
            e.ChangedRange.SetStyle(NumberStyle, _cppNumberRegex);
            e.ChangedRange.SetStyle(CharStyle, @"\;|-|>|<|=|\+|\,|\$|\^|\[|\]|\$|\!|\?");
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
            e.ChangedRange.tb.Range.ClearStyle(CommentStyle);
            //clear style of changed range
            e.ChangedRange.ClearStyle(KeywordStyle, TagBracketStyle, TagNameStyle, AttributeStyle,
                AttributeValueStyle,
                HtmlEntityStyle);
            //
            if (_htmlTagRegex == null)
                InitHtmlRegex();
            //comment highlighting
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _htmlCommentRegex1);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _htmlCommentRegex2);
            //tag brackets highlighting
            e.ChangedRange.SetStyle(TagBracketStyle, _htmlTagRegex);
            //tag name
            e.ChangedRange.SetStyle(TagNameStyle, _htmlTagNameRegex);
            //end of tag
            e.ChangedRange.SetStyle(TagNameStyle, _htmlEndTagRegex);
            //attributes
            e.ChangedRange.SetStyle(AttributeStyle, _htmlAttrRegex);
            //attribute values
            e.ChangedRange.SetStyle(AttributeValueStyle, _htmlAttrValRegex);
            //html entity
            e.ChangedRange.SetStyle(HtmlEntityStyle, _htmlEntityRegex);
            e.ChangedRange.SetStyle(TagBracketStyle, @"\?[a-zA-Z_\d]*\b", RegexOptions.IgnoreCase);

            XmlFold(e.ChangedRange.tb);
        }

        private static void XmlFold(FastColoredTextBox fctb)
        {
            try
            {
                fctb.Range.ClearFoldingMarkers();
                //
                var stack = new Stack<Tag>();
                int id = 0;
                var ranges =
                    fctb.Range.GetRanges(new Regex(@"<(?<range>/?\w+)\s[^>]*?[^/]>|<(?<range>/?\w+)\s*>",
                        RegexOptions.Singleline));
                //extract opening and closing tags (exclude open-close tags: <TAG/>)
                foreach (Range r in ranges)
                {
                    string tagName = r.Text;
                    int iLine = r.Start.iLine;
                    //if it is opening tag...
                    if (tagName[0] != '/')
                    {
                        // ...push into stack
                        var tag = new Tag {Name = tagName, Id = id++, StartLine = r.Start.iLine};
                        stack.Push(tag);
                        // if this line has no markers - set marker
                        if (string.IsNullOrEmpty(fctb[iLine].FoldingStartMarker))
                            fctb[iLine].FoldingStartMarker = tag.Marker;
                    }
                    else
                    {
                        //if it is closing tag - pop from stack
                        Tag tag = stack.Pop();
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

        private void PythonSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "#";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.tb.Range.ClearStyle(CommentStyle, CommentTagStyle);
            e.ChangedRange.ClearStyle(StringStyle, ClassNameStyle, ClassNameStyle2, KeywordStyle, KeywordStyle2,
                NumberStyle, CharStyle);
            e.ChangedRange.tb.Range.SetStyle(CommentTagStyle,
                "(\"\"\".*?\"\"\")|(.*\"\"\")|(\'\'\'.*?\'\'\')|(.*\'\'\')",
                RegexOptions.Singleline | RegexOptions.RightToLeft | RegexOptions.Multiline);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, "#.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(StringStyle, new Regex(
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
            e.ChangedRange.SetStyle(ClassNameStyle, @"\b(class)\s+(?<range>\w+?)\b");
            e.ChangedRange.SetStyle(ClassNameStyle2, @"\b(def)\s+(?<range>\w+?)\b");
            e.ChangedRange.SetStyle(KeywordStyle,
                @"\b(and|del|from|not|while|as|elif|global|or|with|assert|else|if|pass|yield|break|except|import|print|class|exec|nonlocal|in|raise|continue|finally|is|return|def|for|lambda|try)\b");
            e.ChangedRange.SetStyle(KeywordStyle2,
                @"\b(int|id|callable|dict|open|all|vars|object|iter|enumerate|sorted|property|super|classmethod|tuple|compile|basestring|map|range|ord|isinstance|long|float|format|str|type|hasattr|max|len|repr|getattr|list)\b");
            e.ChangedRange.SetStyle(NumberStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            e.ChangedRange.SetStyle(CharStyle, @"\!|\:|\+|=|\-|\*|\@");
            PythonFold(e.ChangedRange.tb);
        }

        private static void PythonFold(FastColoredTextBox fctb)
        {
            //delete all markers
            fctb.Range.ClearFoldingMarkers();

            int currentIndent = 0;
            int lastNonEmptyLine = 0;

            for (int i = 0; i < fctb.LinesCount; i++)
            {
                var line = fctb[i];
                int spacesCount = line.StartSpacesCount;
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
            e.ChangedRange.ClearStyle(CommentStyle, StringStyle, KeywordStyle, KeywordStyle2, NumberStyle);
            e.ChangedRange.SetStyle(CommentStyle, "//.*$");
            e.ChangedRange.SetStyle(CommentStyle, @"(\(\*.*?\*\))|(\(\*.*)");
            e.ChangedRange.SetStyle(StringStyle, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
            e.ChangedRange.SetStyle(KeywordStyle,
                @"\b(and|as|begin|do|done|val|stdin|downto|else|mutable|yield||end|exception|for|fun|function|in|if|let|match|module|not|open|of|prefix|raise|rec|struct|then|to|try|type|while|with|override|int|float|ushort|uint|long|byte|sbyte|bool|string|char)\b");
            e.ChangedRange.SetStyle(KeywordStyle2, @"\b(true|false)\b");
            e.ChangedRange.SetStyle(NumberStyle, @"\b\\d+[\\.]?\\d*([eE]\\-?\\d+)?[lLdDfF]?\b|\b0x[a-fA-F\\d]+\b");
        }

        /// <summary>
        ///     Highlight INI Syntax
        /// </summary>
        /// <param name="e"></param>
        private void IniSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = ";";
            e.ChangedRange.ClearStyle(CommentStyle, KeywordStyle2, StringStyle, NumberStyle, CharStyle);
            e.ChangedRange.SetStyle(CommentStyle, @"\;.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(KeywordStyle2, @"\[.*?[^\\]\]");
            e.ChangedRange.SetStyle(StringStyle, "\".*?[^\\\\]\"|\"\"|\'.*?[^\\\\]\'|\'\'");
            e.ChangedRange.SetStyle(NumberStyle, "\b\\d+[\\.]?\\d*([eE]\\-?\\d+)?[lLdDfF]?\b|\b0x[a-fA-F\\d]+\b");
            e.ChangedRange.SetStyle(CharStyle, @"\*|\,|\:,\?|\@|\!");
            e.ChangedRange.ClearFoldingMarkers();
            foreach (Range r in e.ChangedRange.GetRangesByLines(@"^\[\w+\]$", RegexOptions.None))
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
            e.ChangedRange.tb.Range.ClearStyle(CommentStyle);
            e.ChangedRange.ClearStyle(StringStyle, KeywordStyle, KeywordStyle2, KeywordStyle3, VariableStyle,
                NumberStyle, CharStyle);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, @"--.*$", RegexOptions.Multiline);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, @"\-\-\[\[.+?\-\-\]\]",
                RegexOptions.Singleline | RegexOptions.RightToLeft);
            e.ChangedRange.SetStyle(StringStyle, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
            e.ChangedRange.SetStyle(KeywordStyle,
                @"\b(and|break|do|else|elseif|end|for|function|if|in|local|not|or|repeat|return|then|until|while)\b");
            e.ChangedRange.SetStyle(KeywordStyle2,
                @"\b(false|nil|true|coroutine|debug|io|math|os|package|string|table)\b");
            e.ChangedRange.SetStyle(KeywordStyle3,
                @"\b(assert|collectgarbage|dofile|error|_G|getfenv|getmetatable|gcinfo|ipairs|loadfile|loadstring|module|next|pairs|pcall|print|rawequal|rawget|rawset|require|setfenv|setmetatable|tonumber|tostring|type|unpack|_VERSION|xpcall)\b");
            e.ChangedRange.SetStyle(VariableStyle, @"\.[a-zA-Z_\d]*\b", RegexCompiledOption);
            e.ChangedRange.SetStyle(NumberStyle, @"\b\\d+[\\.]?\\d*([eE]\\-?\\d+)?[lLdDfF]?\b|\b0x[a-fA-F\\d]+\b");
            e.ChangedRange.SetStyle(CharStyle, @"\[|\]|\*|\?|\(|\)|\^|\!|\;|\,|\.|\:");
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers(@"function", @"end function");
        }

        private void RubySyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "#";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.tb.Range.ClearStyle(CommentStyle);
            e.ChangedRange.ClearStyle(KeywordStyle, StringStyle, NumberStyle, ClassNameStyle, CharStyle,
                ClassNameStyle2, VariableStyle);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, @"#.*$", RegexOptions.Multiline);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, @"(=begin.*?=end)|(.*=end)",
                RegexOptions.IgnoreCase | RegexOptions.RightToLeft | RegexOptions.Singleline);
            e.ChangedRange.SetStyle(ClassNameStyle, @"\b(class)\s+(?<range>\w+?)\b");
            e.ChangedRange.SetStyle(ClassNameStyle2, @"\b(def)\s+(?<range>\w+?)\b");
            e.ChangedRange.SetStyle(StringStyle, new Regex(
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
            e.ChangedRange.SetStyle(VariableStyle, @"\$[a-zA-Z_\d]*\b", RegexCompiledOption);
            e.ChangedRange.SetStyle(KeywordStyle,
                @"\b(__FILE__|__LINE__|alias|and|begin|break|case|class|def|defined|do|else|elsif|end|ensure|for|foreach|if|in|module|next|not|or|redo|rescue|retry|return|super|then|undef|unless|until|when|while|yield)\b");
            e.ChangedRange.SetStyle(KeywordStyle2, @"\b(self|puts|true|false|nil)\b");
            e.ChangedRange.SetStyle(NumberStyle, @"\b\\d+[\\.]?\\d*([eE]\\-?\\d+)?[lLdDfF]?\b|\b0x[a-fA-F\\d]+\b");
            e.ChangedRange.SetStyle(CharStyle, @"\[|\]|\*|\?|\(|\)|\^|\!|\;|\,|\.|\:");
            PythonFold(e.ChangedRange.tb);
        }

        private void JsonSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "//";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '{';
            e.ChangedRange.tb.RightBracket2 = '}';
            e.ChangedRange.ClearStyle(CommentStyle, KeywordStyle3, StringStyle, NumberStyle, KeywordStyle);
            e.ChangedRange.SetStyle(CommentStyle, new Regex(@"//.*$"));
            e.ChangedRange.SetStyle(StringStyle, "\".*?[^\\\\]\"|\"\"|\'.*?[^\\\\]\'|\'\'");
            e.ChangedRange.SetStyle(KeywordStyle, "true|false", RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(NumberStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?\b");
            e.ChangedRange.SetStyle(CharStyle, "\\W|_");
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers("{", "}");
        }

        private void LispSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = ";";
            e.ChangedRange.ClearStyle(CommentStyle, StringStyle, KeywordStyle, NumberStyle, CharStyle, ClassNameStyle2);
            e.ChangedRange.SetStyle(CommentStyle, @"\;.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(StringStyle, @"""""|''|"".*?[^\\]""|'.*?[^\\]'", RegexCompiledOption);
            e.ChangedRange.SetStyle(KeywordStyle, @"\b(and|eval|else|nil|if|lambda|or|set|defun)\b");
            e.ChangedRange.SetStyle(NumberStyle, @"\b\d+[\.]?\d*\b", RegexCompiledOption);
            e.ChangedRange.SetStyle(CharStyle, @"\;|\!|#|\:|\*|\&|\@|\(|\)|\-|=|\?|\\");
            foreach (var range in e.ChangedRange.tb.GetRanges(@"\b(defun|DEFUN)\s+(?<range>\w+)\b"))
                e.ChangedRange.SetStyle(ClassNameStyle2, @"\b" + range.Text + @"\b");
        }

        /// <summary>
        ///     Perl Syntax Highlight
        /// </summary>
        /// <param name="r"></param>
        private void PerlSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "#";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.tb.Range.ClearStyle(CommentStyle);
            e.ChangedRange.ClearStyle(StringStyle, VariableStyle, KeywordStyle, KeywordStyle2, KeywordStyle3,
                NumberStyle);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, "#.*$", RegexOptions.Multiline);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, @"(=begin.*?=cut)|(.*=cut)",
                RegexOptions.IgnoreCase | RegexOptions.RightToLeft | RegexOptions.Singleline);
            e.ChangedRange.SetStyle(StringStyle, @"""""|''|"".*?[^\\]""|'.*?[^\\]'", RegexCompiledOption);
            e.ChangedRange.SetStyle(VariableStyle, @"\$[a-zA-Z_\d]*\b|%[a-zA-Z_\d]*\b", RegexCompiledOption);
            e.ChangedRange.SetStyle(KeywordStyle,
                @"\b(use|my|last|scalar|for|foreach|elsif|require|package|continue|goto|next|until|unless|while|our|sub|if|no|else|local|return)\b");
            e.ChangedRange.SetStyle(KeywordStyle2,
                @"\b(bless|close|closedir|die|eval|exit|grep|map|open|opendir|print|return|splice|split|sysopen|warn|do|each|values|BEGIN|CORE|DESTROY|END|STDERR|STDIN|STDOUT|abs|accept|alarm|and|atan2|bind|binmode|caller|chdir|chmod|chomp|chop|chown|chr|chroot|cmp|connect|cos|crypt|dbmclose|dbmopen|default|defined|delete|dump|endgrent|endhostent|endnetent|endprotoent|endpwent|endservent|eof|eq|exec|exists|exp|fcntl|fileno|flock|fork|formline|ge|getc|getgrent|getgrgid|getgrnam|gethostbyad|gethostbyna|gethostent|getlogin|getnetbyadd|getnetbynam|getnetent|getpeername|getpgrp|getppid|getpriority|getprotobyn|getprotobyn|getprotoent|getpwent|getpwnam|getpwuid|getservbyna|getservbypo|getservent|getsockname|getsockopt|glob|gmtime|gt|hex|import|index|int|ioctl|join|keys|kill|lc|lcfirst|le|length|link|listen|localtime|log|lstat|lt|m|mkdir|msgctl|msgget|msgrcv|msgsnd|ne|new|ne|not|oct|or|ord|pack|pipe|pop|pos|printf|q|qq|quotemeta|qw|qx|rand|read|readdir|readlink|readpipe|recv|ref|rename|reset|reverse|rewinddir|rindex|rmdir|s|seek|seekdir|select|semctl|semget|semop|send|setgrent|sethostent|setnetent|setpgrp|setpriority|setprotoent|setpwent|setservent|setsockopt|shift|shmctl|shmget|shmread|shmwrite|shutdown|sin|sleep|socket|socketpair|sort|split|sprintf|sqrt|srand|stat|study|substr|switch|symlink|syscall|sysread|system|system|syswrite|tell|telldir|time|times|tr|truncate|uc|ucfirst|umask|undef|unlink|unpack|unshift|untie|utime|vec|wait|waitpid|wantarray|write|x|xor|y)\b");
            e.ChangedRange.SetStyle(KeywordStyle3,
                @"\b(\-a|\-e|\-b|\-c|\-d|\-k|\-f|\-g|\-l|\-m|\-o|\-p|\-r|\-s|\-t|\-u|\-w|\-x|\-z)\b",
                RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(NumberStyle, @"\b\d+[\.]?\d*\b", RegexCompiledOption);
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
            _phpKeywordRegex1 =
                new Regex(
                    @"\b(die|echo|empty|define|exit|eval|include|include_once|isset|list|require|require_once|return|print|unset)\b",
                    RegexCompiledOption);
            _phpKeywordRegex2 =
                new Regex(
                    @"\b(abstract|and|array|as|break|true|false|case|catch|cfunction|class|clone|const|continue|declare|default|do|else|elseif|enddeclare|endfor|endforeach|endif|endswitch|endwhile|extends|final|for|foreach|function|global|goto|if|implements|instanceof|interface|namespace|new|or|private|protected|public|static|switch|throw|try|use|var|while|xor)\b",
                    RegexCompiledOption);
            _phpKeywordRegex3 = new Regex(@"__CLASS__|__DIR__|__FILE__|__LINE__|__FUNCTION__|__METHOD__|__NAMESPACE__",
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
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.tb.Range.ClearStyle(CommentStyle);
            //clear style of changed range
            e.ChangedRange.ClearStyle(NumberStyle, VariableStyle, KeywordStyle2, KeywordStyle, KeywordStyle3, CharStyle);
            //
            if (_phpStringRegex == null)
                InitPHPRegex();
            //comment highlighting
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _phpCommentRegex1);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _phpCommentRegex2);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _phpCommentRegex3);
            //string highlighting
            e.ChangedRange.SetStyle(StringStyle, _phpStringRegex);
            //number highlighting
            e.ChangedRange.SetStyle(NumberStyle, _phpNumberRegex);
            //var highlighting
            e.ChangedRange.SetStyle(VariableStyle, _phpVarRegex);
            //keyword highlighting
            e.ChangedRange.SetStyle(KeywordStyle2, _phpKeywordRegex1);
            e.ChangedRange.SetStyle(KeywordStyle, _phpKeywordRegex2);
            e.ChangedRange.SetStyle(KeywordStyle3, _phpKeywordRegex3);
            e.ChangedRange.SetStyle(CharStyle, @">|\*|-|=|\+|\!|<|\^|\?");
            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();
            //set folding markers
            e.ChangedRange.SetFoldingMarkers("{", "}"); //allow to collapse brackets block
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
        }

        private void HighlightPHPHtml(TextChangedEventArgs e)
        {
            HTMLSyntaxHighlight(e);
            foreach (Range r in e.ChangedRange.tb.GetRanges(@"(<\?.*?.*?\?>)", RegexOptions.Singleline))
            {
                //remove HTML highlighting from this fragment
                r.ClearStyle(StyleIndex.All);
                //do PHP highlighting
                PHPSyntaxHighlight(new TextChangedEventArgs(r));
            }
        }

        private void HighlightHtmlJSCSS(TextChangedEventArgs e)
        {
            HTMLSyntaxHighlight(e);
            foreach (Range r in e.ChangedRange.tb.GetRanges(@"(<script.*?>.*?</script>)", RegexOptions.Singleline))
            {
                //remove HTML highlighting from this fragment
                r.ClearStyle(CommentStyle, AttributeStyle, AttributeValueStyle, HtmlEntityStyle);
                //do Jscript highlighting
                JScriptSyntaxHighlight(new TextChangedEventArgs(r));
            }
            foreach (Range r in e.ChangedRange.tb.GetRanges(@"(<style.*?>.*?</style>)", RegexOptions.Singleline))
            {
                //remove HTML highlighting from this fragment
                r.ClearStyle(CommentStyle, AttributeStyle, AttributeValueStyle, HtmlEntityStyle);
                //do CSS highlighting
                CSSHighlight(new TextChangedEventArgs(r));
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
                    @"\b(abstract|as|base|using|break|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|else|enum|event|explicit|extern|finally|fixed|float|for|foreach|goto|if|implicit|in|interface|internal|is|this|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|struct|switch|throw|try|typeof|unchecked|unsafe|virtual|volatile|while|add|alias|ascending|descending|dynamic|from|get|global|group|into|join|let|orderby|partial|remove|select|set|value|var|where|yield)\b|#region\b|#endregion\b",
                    RegexCompiledOption);
            _cSharpKeywordRegex2 = new Regex(@"\b(void|bool|string|int|double|float|byte|uint|ushort|ulong|true|false)\b");
            _csharpFunctionRegex = new Regex(@"\b(void|int|bool|string|uint|ushort|ulong|byte)\s+(?<range>\w+?)\b");
        }

        private void CSharpSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "//";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket2 = ']';
            //clear style of changed range
            e.ChangedRange.tb.Range.ClearStyle(CommentStyle);
            e.ChangedRange.ClearStyle(StringStyle, NumberStyle, AttributeStyle, ClassNameStyle, ClassNameStyle2,
                KeywordStyle, KeywordStyle2);
            //
            if (_cSharpStringRegex == null)
                InitCSharpRegex();
            //comment highlighting
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _cSharpCommentRegex1);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _cSharpCommentRegex2);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _cSharpCommentRegex3);
            //string highlighting
            e.ChangedRange.SetStyle(StringStyle, _cSharpStringRegex);
            //number highlighting
            e.ChangedRange.SetStyle(NumberStyle, _cSharpNumberRegex);
            //attribute highlighting
            e.ChangedRange.SetStyle(AttributeStyle, _cSharpAttributeRegex);
            //class name highlighting
            e.ChangedRange.SetStyle(ClassNameStyle, _cSharpClassNameRegex);
            //funtion highlight
            e.ChangedRange.SetStyle(ClassNameStyle2, _csharpFunctionRegex);
            //keyword highlighting
            e.ChangedRange.SetStyle(KeywordStyle, _cSharpKeywordRegex);
            //keywordstyle2
            e.ChangedRange.SetStyle(KeywordStyle2, _cSharpKeywordRegex2);
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers("{", "}"); //allow to collapse brackets block
            e.ChangedRange.SetFoldingMarkers("#region", "#endregion");
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
                    @"\b(break|case|catch|const|continue|default|delete|do|escape|unescape|else|export|for|function|if|in|instanceof|new|null|return|switch|throw|try|var|void|while|with|typeof)\b",
                    RegexCompiledOption);
            _jScriptKeywordRegex2 =
                new Regex(
                @"\b(Array|Boolean|Date|Math|Object|String|eval|isFinite|isNaN|parseInt|parseFloat|this|document|NaN|navigator|prototype|true|false)\b");
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
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket2 = ']';
            //clear style of visible range
            e.ChangedRange.tb.Range.ClearStyle(CommentStyle);
            //clear style of changed range
            e.ChangedRange.ClearStyle(StringStyle, NumberStyle, KeywordStyle, KeywordStyle2, ClassNameStyle,
                ClassNameStyle2, CharStyle);

            if (_jScriptStringRegex == null)
                InitJScriptRegex();
            //comment highlighting
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _jScriptCommentRegex1);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _jScriptCommentRegex2);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _jScriptCommentRegex3);
            //string highlighting
            e.ChangedRange.SetStyle(StringStyle, _jScriptStringRegex);
            //number highlighting
            e.ChangedRange.SetStyle(NumberStyle, _jScriptNumberRegex);
            e.ChangedRange.SetStyle(ClassNameStyle, _jScriptFunctionRegex);
            e.ChangedRange.SetStyle(ClassNameStyle2, _jScriptFunctionRegex2);
            //keyword highlighting
            e.ChangedRange.SetStyle(KeywordStyle, _jScriptKeywordRegex);
            e.ChangedRange.SetStyle(KeywordStyle2, _jScriptKeywordRegex2);
            e.ChangedRange.SetStyle(InBracketStyle, @"\[(.*?)\]");
            e.ChangedRange.SetStyle(CharStyle, @"\;|\,|<|>|-|\$|=|\!|\.|\?|\*|\&|\#|\^");
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
            e.ChangedRange.tb.Range.ClearStyle(CommentStyle);
            //clear style of changed range
            e.ChangedRange.ClearStyle(CommentStyle, TagBracketStyle, TagNameStyle, AttributeStyle, AttributeValueStyle,
                HtmlEntityStyle);
            //
            if (_htmlTagRegex == null)
                InitHtmlRegex();
            //comment highlighting
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _htmlCommentRegex1);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _htmlCommentRegex2);
            //tag brackets highlighting
            e.ChangedRange.SetStyle(TagBracketStyle, _htmlTagRegex);
            //tag name
            e.ChangedRange.SetStyle(TagNameStyle, _htmlTagNameRegex);
            //end of tag
            e.ChangedRange.SetStyle(TagNameStyle, _htmlEndTagRegex);
            //attributes
            e.ChangedRange.SetStyle(AttributeStyle, _htmlAttrRegex);
            //attribute values
            e.ChangedRange.SetStyle(AttributeValueStyle, _htmlAttrValRegex);
            //html entity
            e.ChangedRange.SetStyle(HtmlEntityStyle, _htmlEntityRegex);

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
        /// <param name="range"></param>
        private void VBSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "'";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '\x0';
            e.ChangedRange.tb.RightBracket2 = '\x0';
            //clear style of changed range
            e.ChangedRange.ClearStyle(CommentStyle, StringStyle, CommentStyle, NumberStyle, ClassNameStyle, KeywordStyle);
            //
            if (_vbStringRegex == null)
                InitVBRegex();
            //string highlighting
            e.ChangedRange.SetStyle(StringStyle, _vbStringRegex);
            //comment highlighting
            e.ChangedRange.SetStyle(CommentStyle, _vbCommentRegex);
            //number highlighting
            e.ChangedRange.SetStyle(NumberStyle, _vbNumberRegex);
            //class name highlighting
            e.ChangedRange.SetStyle(ClassNameStyle, _vbClassNameRegex);
            //keyword highlighting
            e.ChangedRange.SetStyle(KeywordStyle, _vbKeywordRegex);

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
        /// <param name="range"></param>
        private void SqlSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "--";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '\x0';
            e.ChangedRange.tb.RightBracket2 = '\x0';
            //clear style of changed range
            e.ChangedRange.tb.Range.ClearStyle(CommentStyle);
            e.ChangedRange.ClearStyle(CommentStyle, StringStyle, NumberStyle, VariableStyle, StatementsStyle,
                KeywordStyle,
                FunctionsStyle, TypesStyle);
            if (_sqlStringRegex == null)
                InitSqlRegex();
            //comment highlighting
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _sqlCommentRegex1);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _sqlCommentRegex2);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _sqlCommentRegex3);
            //string highlighting
            e.ChangedRange.SetStyle(StringStyle, _sqlStringRegex);
            //number highlighting
            e.ChangedRange.SetStyle(NumberStyle, _sqlNumberRegex);
            //types highlighting
            e.ChangedRange.SetStyle(TypesStyle, _sqlTypesRegex);
            //var highlighting
            e.ChangedRange.SetStyle(VariableStyle, _sqlVarRegex);
            //statements
            e.ChangedRange.SetStyle(StatementsStyle, _sqlStatementsRegex);
            //keywords
            e.ChangedRange.SetStyle(KeywordStyle, _sqlKeywordsRegex);
            //functions
            e.ChangedRange.SetStyle(FunctionsStyle, _sqlFunctionsRegex);

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
        /// <param name="r"></param>
        private void DSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "//";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '{';
            e.ChangedRange.tb.RightBracket2 = '}';
            e.ChangedRange.tb.Range.ClearStyle(CommentStyle);
            e.ChangedRange.ClearStyle(StringStyle, KeywordStyle, KeywordStyle2, NumberStyle);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, @"//.*$");
            e.ChangedRange.tb.Range.SetStyle(CommentStyle,
                new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline | RegexCompiledOption));
            e.ChangedRange.tb.Range.SetStyle(CommentStyle,
                new Regex(@"(/\*.*?\*/)|(.*\*/)",
                    RegexOptions.Singleline | RegexOptions.RightToLeft | RegexCompiledOption));
            e.ChangedRange.SetStyle(StringStyle, @"""""|''|"".*?[^\\]""|'.*?[^\\]'", RegexCompiledOption);
            e.ChangedRange.SetStyle(KeywordStyle,
                @"\b(AL|AH|AX|EAX|BL|BH|BX|EBX|CL|CH|CX|ECX|DL|DH|DX|EDX|BP|EBP|module|this|SP|ESP|ref|DI|EDI|SI|ESI|ES|CS|SS|DS|GS|FS|CR0|CR2|CR3|CR4|DR0|DR1|DR2|DR3|DR6|DR7|TR3|TR4|TR5|TR6|TR7|ST|MM0|MM1|MM2|MM3|MM4|MM5|MM6|MM7|lock|rep|repe|repne|repnz|repz|offset|seg|__LOCAL_SIZE|near ptr|far ptr|byte ptr|short ptr|int ptr|word ptr|dword ptr|float ptr|double ptr|extended ptr|aaa|aad|aam|aas|adc|add|addpd|addps|addsd|addss|and|andnpd|andnps|andpd|andps|arpl|bound|bsf|bsr|bswap|bt|btc|alias|defined|import|btr|bts|call|cbw|cdq|clc|cld|clflush|cli|clts|cmc|cmova|cmovae|cmovb|cmovbe|cmovc|cmove|cmovg|cmovge|cmovl|cmovle|cmovna|cmovnae|cmovnb|cmovnbe|cmovnc|cmovne|cmovng|cmovnge|cmovnl|cmovnle|cmovno|cmovnp|cmovns|cmovnz|cmovo|cmovp|cmovpe|cmovpo|cmovs|cmovz|cmp|cmppd|cmpps|cmps|cmpsb|cmpsd|cmpss|cmpsw|cmpxch8b|cmpxchg|comisd|comiss|cpuid|cvtdq2pd|cvtdq2ps|cvtpd2dq|cvtpd2pi|cvtpd2ps|cvtpi2pd|cvtpi2ps|cvtps2dq|cvtps2pd|cvtps2pi|cvtsd2si|cvtsd2ss|cvtsi2sd|cvtsi2ss|cvtss2sd|cvtss2si|cvttpd2dq|cvttpd2pi|cvttps2dq|cvttps2pi|cvttsd2si|cvttss2si|cwd|cwde|da|daa|das|db|dd|de|dec|df|di|div|divpd|divps|divsd|divss|dl|dq|ds|dt|dw|emms|enter|f2xm1|fabs|fadd|faddp|fbld|fbstp|fchs|fclex|fcmovb|fcmovbe|fcmove|fcmovnb|fcmovnbe|fcmovne|fcmovnu|fcmovu|fcom|fcomi|fcomip|fcomp|fcompp|fcos|fdecstp|fdisi|fdiv|fdivp|fdivr|fdivrp|feni|ffree|fiadd|ficom|ficomp|fidiv|fidivr|fild|fimul|fincstp|finit|fist|fistp|fisub|fisubr|fld|fld1|fldcw|fldenv|fldl2e|fldl2t|fldlg2|fldln2|fldpi|fldz|fmul|fmulp|fnclex|fndisi|fneni|fninit|fnop|fnsave|fnstcw|fnstenv|fnstsw|fpatan|fprem|fprem1|fptan|frndint|frstor|fsave|fscale|fsetpm|fsin|fsincos|fsqrt|fst|fstcw|fstenv|fstp|fstsw|fsub|fsubp|fsubr|fsubrp|ftst|fucom|fucomi|fucomip|fucomp|fucompp|fwait|fxam|fxch|fxrstor|fxsave|fxtract|fyl2x|fyl2xp1|hlt|idiv|imul|in|inc|ins|insb|insd|insw|int|into|invd|invlpg|iret|iretd|ja|jae|jb|jbe|jc|jcxz|je|jecxz|jg|jge|jl|jle|jmp|jna|jnae|jnb|jnbe|jnc|jne|jng|jnge|jnl|jnle|jno|jnp|jns|jnz|jo|jp|jpe|jpo|js|jz|lahf|lar|ldmxcsr|lds|lea|leave|les|lfence|lfs|lgdt|lgs|lidt|lldt|lmsw|lock|lods|lodsb|lodsd|lodsw|loop|loope|loopne|loopnz|loopz|lsl|lss|ltr|maskmovdqu|maskmovq|maxpd|maxps|maxsd|maxss|mfence|minpd|minps|minsd|minss|mov|movapd|movaps|movd|movdq2q|movdqa|movdqu|movhlps|movhpd|movhps|movlhps|movlpd|movlps|movmskpd|movmskps|movntdq|movnti|movntpd|movntps|movntq|movq|movq2dq|movs|movsb|movsd|movss|movsw|movsx|movupd|movups|movzx|mul|mulpd|mulps|mulsd|mulss|neg|nop|not|or|orpd|orps|out|outs|outsb|outsd|outsw|packssdw|packsswb|packuswb|paddb|paddd|paddq|paddsb|paddsw|paddusb|paddusw|paddw|pand|pandn|pavgb|pavgw|pcmpeqb|pcmpeqd|pcmpeqw|pcmpgtb|pcmpgtd|pcmpgtw|pextrw|pinsrw|pmaddwd|pmaxsw|pmaxub|pminsw|pminub|pmovmskb|pmulhuw|pmulhw|pmullw|pmuludq|pop|popa|popad|popf|popfd|por|prefetchnta|prefetcht0|prefetcht1|prefetcht2|psadbw|pshufd|pshufhw|pshuflw|pshufw|pslld|pslldq|psllq|psllw|psrad|psraw|psrld|psrldq|psrlq|psrlw|psubb|psubd|psubq|psubsb|psubsw|psubusb|psubusw|psubw|punpckhbw|punpckhdq|punpckhqdq|punpckhwd|punpcklbw|punpckldq|punpcklqdq|punpcklwd|push|pusha|pushad|pushf|pushfd|pxor|rcl|rcpps|rcpss|rcr|rdmsr|rdpmc|rdtsc|rep|repe|repne|repnz|repz|ret|retf|rol|ror|rsm|rsqrtps|rsqrtss|sahf|sal|sar|sbb|scas|scasb|scasd|scasw|seta|setae|setb|setbe|setc|sete|setg|setge|setl|setle|setna|setnae|setnb|setnbe|setnc|setne|setng|setnge|setnl|setnle|setno|setnp|setns|setnz|seto|setp|setpe|setpo|sets|setz|sfence|sgdt|shl|shld|shr|shrd|shufpd|shufps|sidt|sldt|smsw|sqrtpd|sqrtps|sqrtsd|sqrtss|stc|std|sti|stmxcsr|stos|stosb|stosd|stosw|str|sub|subpd|subps|subsd|subss|sysenter|sysexit|test|ucomisd|ucomiss|ud2|unpckhpd|unpckhps|unpcklpd|unpcklps|verr|verw|wait|wbinvd|wrmsr|xadd|xchg|xlat|xlatb|xor|xorpd|xorps|pavgusb|pf2id|pfacc|pfadd|pfcmpeq|pfcmpge|pfcmpgt|pfmax|pfmin|pfmul|pfnacc|pfpnacc|pfrcp|pfrcpit1|pfrcpit2|pfrsqit1|pfrsqrt|pfsub|pfsubr|pi2fd|pmulhrw|pswapd|abstract|align|argc|argv|asm|assert|attribute|auto|bit|body|bool|break|byte|case|catch|cent|cfloat|char|class|complex|const|continue|creal|d_time|db|dd|de|debug|default|delete|deprecated|df|dg|di|do|double|ds|dynamic_cast|else|enum|envp|even|explicit|extended|extern|false|final|float|for|fp|friend|function|goto|idouble|if|ifloat|imaginary|in|inline|inout|instance|int|interface|ireal|long|mutable|naked|namespace|new|operator|out|override|private|protected|public|real|register|reinterpret_cast|return|short|signed|sizeof|static|struct|super|switch|synchronized|template|throw|true|try|typedef|typeid|typename|ubyte|ucent|uint|ulong|union|unsigned|ushort|using|version|virtual|void|volatile|wchar|while)\b");
            e.ChangedRange.SetStyle(KeywordStyle2,
                @"\b(ClassInfo|E|Exception|FileException|LN10|LN2|LOG10E|LOG2|LOG2E|LOG2T|LocalTimetoUTC|M_1_PI|M_2_PI|M_2_SQRT|Object|OutBuffer|OutOfMemory|PI|PI_2|PI_4|RegExp|SQRT1_2|SQRT2|SliceStream|Stream|StringException|Thread|ThreadError|TicksPerSecond|UTCtoLocalTime|acos|addExt|addRange|addRoot|align2|align4|alignSize|altsep|append|asin|atan|atan2|atof|atoi|bsf|bsr|bt|btc|btr|bts|capitalize|capwords|ceil|center|close|cmp|copyFrom|copysign|cos|cosh|count|create|curdir|data|defaultExt|digits|enable|eof|exec|exp|expandtabs|expml|fabs|fill0|find|floor|fncharmatch|fnmatch|frexp|fullCollect|genCollect|getAll|getAttributes|getbaseName|getDirName|getDrive|getExt|getSize|getState|getThis|getUTCtime|getc|getcw|hdl|hexdigits|hypot|icmp|inp|inpl|inpw|insert|isabs|isalnum|isalpha|isascii|iscntrl|isdigit|isfinite|isgraph|isinf|islower|isnan|isnormal|isprint|ispunct|isspace|issubnormal|isupper|isxdigit|join|ldexp|letters|linesep|ljustify|log|log10|loglp|lowercase|maketrans|match|minimize|modf|octdigits|open|outp|outpl|outpw|pardir|parse|pathsep|pause|pauseAll|position|pow|printf|rand|rand_seed|read|readBlock|readExact|readLine|readString|readStringW|readable|remove|removeRange|removeRoot|rename|replace|replaceOld|replaceSlice|reserve|resume|resumeAll|rfind|rint|rjustify|rndtol|run|scanf|search|seek|seekCur|seekEnd|seekSet|seekable|sep|setPriority|signbit|sin|sinh|size|split|splitlines|spread|sqrt|start|strip|stripl|stripr|tan|tanh|test|thread_hdl|toByte|toBytes|toDateString|toHash|toInt|toLong|toShort|toString|toTimeString|toUbyte|toUint|toUlong|toUshort|tolower|toupper|translate|ungetc|ungetcw|uppercase|vprintf|vscanf|wait|whitespace|write|writeBlock|writeExact|writeLine|writeLineW|writeString|writeStringW|writeable|yield|zfill)\b");
            e.ChangedRange.SetStyle(NumberStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?\b", RegexCompiledOption);
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers("{", "}");
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
        }

        private void PascalSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "{";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.tb.Range.ClearStyle(CommentStyle);
            e.ChangedRange.ClearStyle(StringStyle, KeywordStyle, KeywordStyle2, NumberStyle, CharStyle);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle,
                new Regex(@"\(\*(.*?)\*\)", RegexOptions.Singleline | RegexCompiledOption));
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, @"({.*?})|({.*)", RegexOptions.Singleline);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, @"({.*?})|(.*})",
                RegexOptions.Singleline | RegexOptions.RightToLeft);
            e.ChangedRange.SetStyle(StringStyle, new Regex(@"""""|''|"".*?[^\\]""|'.*?[^\\]'", RegexCompiledOption));
            e.ChangedRange.SetStyle(KeywordStyle,
                @"\b(and|array|as|asm|begin|case|class|const|constructor|destructor|dispinterfac|div|do|downto|else|end|except|exports|file|finalization|finally|for|function|goto|if|implementation|in|inherited|initialization|inline|interface|is|label|library|mod|nil|not|object|of|or|out|packed|procedure|program|property|raise|record|repeat|resourcestring|set|shl|shr|string|then|threadvar|to|try|type|unit|until|uses|var|while|with|xor)\b",
                RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(KeywordStyle2,
                @"\b(absolute|abstract|assembler|automated|cdecl|contains|default|dispid|dynamic|export|external|far|forward|implements|index|message|name|near|nodefault|overload|override|package|pascal|private|protected|public|published|read|readonly|register|reintroduce|requires|resident|safecall|stdcall|stored|virtual|write|writeonly)\b",
                RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(NumberStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?\b");
            e.ChangedRange.SetStyle(CharStyle, @"\:|\?|\*|%|=|\+|\!|\^|#|\,|\.|\\|@|\|");
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers("{", "}");
            e.ChangedRange.SetFoldingMarkers("Begin", "End", RegexOptions.IgnoreCase);
        }

        private void AsmSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = ";";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.ClearStyle(CommentStyle, StringStyle, KeywordStyle, KeywordStyle2, VariableStyle, NumberStyle,
                CharStyle);
            e.ChangedRange.SetStyle(CommentStyle, @"\;.*$");
            e.ChangedRange.SetStyle(StringStyle, new Regex(@"""""|''|"".*?[^\\]""|'.*?[^\\]'", RegexCompiledOption));
            e.ChangedRange.SetStyle(KeywordStyle,
                @"\b(add|addb|addc|addcb|and|andb|bbc|bbs|bc|be|bge|bgt|bh|ble|blt|bmov|bmovi|bnc|bne|bnh|bnst|bnv|bnvt|br|bst|bv|bvt|call|clr|clrb|clrc|clrvt|cmp|cmpb|cmpl|dbnz|dbnzw|dec|decb|di|div|divb|divu|divub|djnz|djnzw|dpst|ei|eq|ext|extb|ge|gt|idlpd|inc|incb|jbc|jbs|jc|je|jge|jgt|jh|jle|jlt|jnc|jne|jnh|jnst|jnv|jnvt|jst|jv|jvt|lcall|ld|ldb|ldbse|ldbze|le|ljmp|lt|mod|mul|mulb|mulu|mulub|ne|neg|negb|nop|norml|not|notb|nul|or|orb|pop|popa|popf|push|pusha|pushf|ret|rst|scall|setc|shl|shlb|shll|shr|shra|shrab|shral|shrb|shrl|sjmp|skip|st|stb|sub|subb|subc|subcb|tijmp|trap|xch|xchb|xor|xorb)\b",
                RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(KeywordStyle2,
                @"\b(at|byte|cseg|dcb|dcl|dcp|dcr|dcw|dsb|dseg|dsl|dsp|dsr|dsw|dword|else|end|endif|endm|entry|equ|exitm|exitrn|far|if|irp|irpc|kseg|local|long|macro|main|module|null|org|oseg|pointer|public|rel|rept|rseg|set|stack|stacksize|word)\b",
                RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(VariableStyle, new Regex(@"\$[a-zA-Z_\d]*\b", RegexCompiledOption));
            e.ChangedRange.SetStyle(NumberStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?\b");
            e.ChangedRange.SetStyle(CharStyle, @"\:|\?|\*|%|=|\+|\!|\^|#|\,|\.|\\|@|\|");
        }

        private void ScalaSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "//";
            var commentRegex1 = new Regex(@"//.*$", RegexOptions.Multiline);
            var commentRegex2 = new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            var commentRegex3 = new Regex(@"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);
            e.ChangedRange.tb.Range.ClearStyle(CommentStyle);
            e.ChangedRange.ClearStyle(StringStyle, KeywordStyle, KeywordStyle2, KeywordStyle3, NumberStyle, CharStyle);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, commentRegex1);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, commentRegex2);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, commentRegex3);
            e.ChangedRange.SetStyle(StringStyle, new Regex(@"""""|''|"".*?[^\\]""|'.*?[^\\]'"));
            e.ChangedRange.SetStyle(KeywordStyle3, new Regex(@"#[define|elif|else|endif|error|if|undef|warning]*\b"));
            e.ChangedRange.SetStyle(KeywordStyle,
                @"\b(abstract|boolean|break|byte|case|catch|char|class|continue|default|delegate|do|double|else|extends|false|final|finally|float|for|if|implements|import|instanceof|int|interface|long|native|new|null|package|private|protected|public|return|short|static|super|switch|synchronized|this|throw|throws|transient|true|try|void|volatile|while)\b");
            e.ChangedRange.SetStyle(KeywordStyle2,
                @"\b(def|object|val|var|trait|type|override|with|sealed|yield|match|implicit|requires)\b");
            e.ChangedRange.SetStyle(NumberStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?\b");
            e.ChangedRange.SetStyle(CharStyle, @"=|>|<|\[|\]|\(|\)|\?|\$|\!|\*|\^");
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers("{", "}");
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
        }

        private void AntlrSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(CommentStyle, StringStyle, KeywordStyle, KeywordStyle2);
            e.ChangedRange.SetStyle(CommentStyle, @"//.*$");
            e.ChangedRange.SetStyle(StringStyle, @"""""|''|"".*?[^\\]""|'.*?[^\\]'");
            e.ChangedRange.SetStyle(KeywordStyle,
                @"\b(tokens|header|lexclass|grammar|class|extends|Lexer|TreeParser|charVocabulary|Parser|protected|public|private|returns|throws|exception|catch|options)\b",
                RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(KeywordStyle2,
                @"ANTLRException|ANTLR_API|ANTLR_API|ANTLR_CXX_SUPPORTS_NAMESPACE|ANTLR_C_USING|ANTLR_REALLY_NO_STRCASECMP|ANTLR_SUPPORT_XML|ANTLR_USE_NAMESPACE|EOF_TYPE|HAS_NOT_CCTYPE_H|INVALID_TYPE|MIN_USER_TYPE|NEEDS_OPERATOR_LESS_THAN|NO_STATIC_CONSTS|NULL_TREE_LOOKAHEAD|AST|ASTArray|ASTFactory|ASTNULL|ASTNULLType|ASTPair|ASTRefCount|BaseAST|BitSet|CUSTOM_API|CharBuffer|CharInputBuffer|CharScanner|CharScannerLiteralsLess|CharStreamException|CharStreamIOException|CircularQueue|CommonAST|CommonASTWithHiddenTokens|CommonHiddenStreamToken|CommonToken|IOException|InputBuffer|LA|LLkParser|LT|LexerInputState|LoadAST|MismatchedCharException|MismatchedTokenException|NoViableAltException|NoViableAltForCharException|OS_NO_ALLOCATOR|Parser|ParserInputState|RecognitionException|RefCount|RefToken|SKIP|SemanticException|Token|TokenBuffer|TokenStream|TokenStreamBasicFilter|TokenStreamException|TokenStreamHiddenTokenFilter|TokenStreamIOException|TokenStreamRecognitionException|TokenStreamRetryException|TokenStreamSelector|Tracer|TreeParser|TreeParserInputState|TreeParserSharedInputState)\b",
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
                    @"\b(__asm|__based|__cdecl|__cplusplus|__emit|__export|__far|__fastcall|__fortran|__huge|__inline|__interrupt|__loadds|__near|__pascal|__saveregs|__segment|__segname|__self|__stdcall|__asm|__syscall|argc|argv|auto|break|case|char|const|continue|default|do|double|else|enum|envp|extern|float|for|goto|if|int|long|main|register|return|short|private|protected|public|signed|sizeof|static|struct|switch|typedef|union|unsigned|void|volatile|wchar_t|while|wmain|id|in|out|inout|bycopy|byref|self|super)\b");
            _objCClassNameRegex = new Regex(@"\b(class|struct|enum|interface)\s+(?<range>\w+?)\b");
            _objCStringRegex2 = new Regex(@"(?<=\<)(.*?)(?=\>)|\<|\>");
            _objCFunctionsRegex = new Regex(@"\b(int|void)\s+(?<range>\w+?)\b");
            _objCPreprocessorRegex = new Regex(@"#[a-zA-Z_\d]*\b|\bdefined\b", RegexCompiledOption);
            _objCKeywordRegex2 =
                new Regex(
                    @"\b(bool|catch|class|const_cast|delete|dynamic_cast|explicit|false|friend|inline|mutable|namespace|new|operator|reinterpret_cast|static_cast|template|this|throw|true|try|typeid|typename|using|virtual)\b|@[a-zA-Z_\d]*\b");
        }

        private void ObjCHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "//";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.tb.Range.ClearStyle(CommentStyle);
            e.ChangedRange.ClearStyle(StringStyle, KeywordStyle, KeywordStyle2, ClassNameStyle, ClassNameStyle2,
                PreprocessorStyle, NumberStyle);
            if (_objCCommentRegex1 == null)
                InitObjCRegex();
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _objCCommentRegex1);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _objCCommentRegex2);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, _objCCommentRegex3);
            e.ChangedRange.SetStyle(PreprocessorStyle, _objCPreprocessorRegex);
            e.ChangedRange.SetStyle(StringStyle, _objCStringRegex2);
            e.ChangedRange.SetStyle(StringStyle, _objCStringRegex);
            e.ChangedRange.SetStyle(ClassNameStyle, _objCClassNameRegex);
            e.ChangedRange.SetStyle(ClassNameStyle2, _objCFunctionsRegex);
            e.ChangedRange.SetStyle(KeywordStyle, _objCKeywordRegex);
            e.ChangedRange.SetStyle(KeywordStyle2, _objCKeywordRegex2);
            e.ChangedRange.SetStyle(NumberStyle, _objCNumberRegex);
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
            e.ChangedRange.tb.Range.ClearStyle(CommentStyle);
            e.ChangedRange.ClearStyle(StringStyle, KeywordStyle, KeywordStyle2, KeywordStyle3, NumberStyle);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, @"//.*$", RegexOptions.Multiline);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            e.ChangedRange.tb.Range.SetStyle(CommentStyle, @"(/\*.*?\*/)|(.*\*/)",
                RegexOptions.Singleline | RegexOptions.RightToLeft);
            e.ChangedRange.SetStyle(StringStyle, @"""""|''|"".*?[^\\]""|'.*?[^\\]'");
            e.ChangedRange.SetStyle(KeywordStyle,
                @"\b(break|case|continue|default|do|while|else|for|foreach|in|if|label|return|super|switch|throw|try|catch|finally|while|with|dynamic|final|internal|native|override|private|protected|public|static|class|const|extends|function|get|implements|interface|package|set|var|default|import|include|use|namespace|AS3|flash_proxy|object_proxy|false|null|this|true)\b");
            e.ChangedRange.SetStyle(KeywordStyle2,
                @"\b(Boolean|decodeURI|decodeURIComponent|encodeURI|encodeURIComponent|escape|int|isFinite|isNaN|isXMLName|Number|Object|parseFloat|parseInt|String|trace|uint|unescape|XML|XMLList|Infinity|NaN|undefined|void|null)\b");
            e.ChangedRange.SetStyle(KeywordStyle3,
                @"\b(abs|acceptAllClient|acceptAllServer|acceptClient|acceptDragDrop|acceptServer|acknowledge|acos|activate|add|addCallback|addChannel|addChild|addChildAt|addChildSet|addCuePoint|addData|addDragData|addEventListener|addFocusManager|addHaloColors|addHandler|addHeader|addItem|addItemAt|addListenerHandler|addLogger|addNamespace|addObject|addPage|addPopUp|addResponder|addSimpleHeader|addTarget|addToCreationQueue|addToFreeItemRenderers|adjustBrightness|adjustBrightness2|adjustFocusRect|adjustGutters|adjustMinMax|allowDomain|allowInsecureDomain|appendChild|appendText|apply|applyFilter|applyItemRendererProperties|applySelectionEffect|applySeriesSet|applySettings|applyValueToTarget|areInaccessibleObjectsUnderPoint|areSoundsInaccessible|asin|atan|atan2|attachAudio|attachCamera|attachNetStream|attachOverlay|attribute|attributes|begin|beginBitmapFill|beginFill|beginGradientFill|beginInterpolation|beginTween|bindProperty|bindSetter|bringToFront|browse|buildLabelCache|buildMinorTickCache|buildSubSeries|cacheDefaultValues|cacheIndexValues|cacheNamedValues|calculateDropIndex|calculateDropIndicatorY|calculatePreferredSizeFromData|calculateRowHeight|call|callLater|callProperty|canLoadWSDL|canWatch|cancel|captureStartValues|ceil|center|centerPopUp|channelConnectHandler|channelDisconnectHandler|channelFaultHandler|charAt|charCodeAt|chartStateChanged|checkCreate|checkDelete|checkUpdate|child|childIndex|children|childrenCreated|claimStyles|clear|clearHeaders|clearIndicators|clearResult|clearSelected|clearSeparators|clearStyle|clearStyleDeclaration|clickHandler|clone|cloneNode|close|collectTransitions|collectionChangeHandler|colorTransform|comments|commit|commitProperties|compare|completeHandler|compress|computeSpectrum|concat|configureScrollBars|conflict|connect|connectFailed|connectSuccess|connectTimeoutHandler|contains|containsPoint|containsRect|contentToGlobal|contentToLocal|copy|copyChannel|copyPixels|copySelectedItems|cos|count|create|createBorder|createBox|createChildren|createComponentFromDescriptor|createComponentsFromDescriptors|createCursor|createDataID|createElement|createErrorMessage|createEvent|createEventFromMessageFault|createFaultEvent|createGradientBox|createInstance|createInstances|createItem|createItemEditor|createMenu|createNavItem|createPopUp|createReferenceOnParentDocument|createRequestTimeoutErrorMessage|createTextNode|createToolTip|createTween|createUID|createUniqueName|createUpdateEvent|createXMLDocument|curveTo|customizeSeries|dataChanged|dataForFormat|dataToLocal|dateCompare|dateToString|deactivate|debug|decode|decodeXML|defaultCreateMask|defaultFilterFunction|defaultSettings|deleteItem|deleteProperty|deleteReferenceOnParentDocument|deltaTransformPoint|descendants|describeData|describeType|destroyItemEditor|destroyToolTip|determineTextFormatFromStyles|disableAutoUpdate|disablePolling|disconnect|disconnectFailed|disconnectSuccess|dispatchEvent|displayObjectToString|dispose|distance|doConversion|doDrag|doValidation|downArrowButton_buttonDownHandler|download|dragCompleteHandler|dragDropHandler|dragEnterHandler|dragExitHandler|dragOverHandler|dragScroll|draw|drawCaretIndicator|drawCircle|drawColumnBackground|drawEllipse|drawFocus|drawHeaderBackground|drawHighlightIndicator|drawHorizontalLine|drawItem|drawLinesAndColumnBackgrounds|drawRect|drawRoundRect|drawRoundRectComplex|drawRowBackground|drawRowBackgrounds|drawSelectionIndicator|drawSeparators|drawShadow|drawTileBackground|drawTileBackgrounds|drawVerticalLine|easeIn|easeInOut|easeNone|easeOut|effectEndHandler|effectFinished|effectStartHandler|effectStarted|elements|enableAutoUpdate|enablePolling|encodeDate|encodeValue|end|endEdit|endEffectsForTarget|endEffectsStarted|endFill|endInterpolation|endTween|enumerateFonts|equals|error|every|exec|executeBindings|executeChildBindings|exp|expandChildrenOf|expandItem|extractMinInterval|extractMinMax|fatal|fault|fill|fillRect|filter|filterCache|filterInstance|findAny|findDataPoints|findFirst|findFocusManagerComponent|findItem|findKey|findLast|findString|findText|finishEffect|finishKeySelection|finishPrint|finishRepeat|floodFill|floor|flush|focusInHandler|focusOutHandler|forEach|format|formatDataTip|formatDays|formatDecimal|formatForScreen|formatMilliseconds|formatMinutes|formatMonths|formatNegative|formatPrecision|formatRounding|formatRoundingWithPrecision|formatSeconds|formatThousands|formatToString|formatValue|formatYears|fromCharCode|generateFilterRect|getAffectedProperties|getAxis|getBoolean|getBounds|getCacheKey|getCamera|getChannel|getChannelSet|getCharBoundaries|getCharIndexAtPoint|getChildAt|getChildByName|getChildIndex|getChildren|getClassInfo|getClassStyleDeclarations|getColorBoundsRect|getColorName|getColorNames|getComplexProperty|getConflict|getContent|getCuePointByName|getCuePoints|getCurrent|getData|getDate|getDay|getDefinition|getDefinitionByName|getDescendants|getDestination|getDividerAt|getElementBounds|getEvents|getExplicitOrMeasuredHeight|getExplicitOrMeasuredWidth|getFeedback|getFirstCharInParagraph|getFocus|getFullURL|getFullYear|getGroupName|getHeader|getHeaderAt|getHours|getImageReference|getInstance|getItem|getItemAt|getItemIndex|getKeyProperty|getLabelEstimate|getLabels|getLevelString|getLineIndexAtPoint|getLineIndexOfChar|getLineLength|getLineMetrics|getLineOffset|getLineText|getLocal|getLogger|getMenuAt|getMessageResponder|getMicrophone|getMilliseconds|getMinutes|getMissingInterpolationValues|getMonth|getNamespaceForPrefix|getNextFocusManagerComponent|getNumber|getObject|getObjectsUnderPoint|getOperation|getOperationAsString|getParagraphLength|getParentItem|getPendingOperation|getPercentLoaded|getPixel|getPixel32|getPixels|getPort|getPrefixForNamespace|getProperties|getProperty|getProtocol|getRadioButtonAt|getRect|getRemote|getRenderDataForTransition|getRepeaterItem|getResourceBundle|getSOAPAction|getSWFRoot|getSecondAxis|getSeconds|getSelected|getSelectedText|getServerName|getServerNameWithPort|getServiceIdForMessage|getStackTrace|getStartValue|getString|getStringArray|getStyle|getStyleDeclaration|getSubscribeMessage|getTabAt|getText|getTextFormat|getTextRunInfo|getTextStyles|getThumbAt|getTime|getTimezoneOffset|getType|getUID|getUITextFormat|getUTCDate|getUTCDay|getUTCFullYear|getUTCHours|getUTCMilliseconds|getUTCMinutes|getUTCMonth|getUTCSeconds|getUnsubscribeMessage|getValue|getValueFromSource|getValueFromTarget|getViewIndex|globalToContent|globalToLocal|gotoAndPlay|gotoAndStop|guardMinMax|handleResults|hasChildNodes|hasChildren|hasComplexContent|hasDefinition|hasEventListener|hasFormat|hasGlyphs|hasIllegalCharacters|hasMetadata|hasOwnProperty|hasProperty|hasResponder|hasSimpleContent|hide|hideBuiltInItems|hideCursor|hideData|hideDropFeedback|hideFocus|hiliteSelectedNavItem|hitTest|hitTestObject|hitTestPoint|hitTestTextNearPos|horizontalGradientMatrix|identity|inScopeNamespaces|indexOf|indexToColumn|indexToItemRenderer|indexToRow|indicesToIndex|inflate|inflatePoint|info|initChannelSet|initEffect|initInstance|initListData|initMaskEffect|initProgressHandler|initProtoChain|initSecondaryMode|initializationComplete|initialize|initializeAccessibility|initializeInterpolationData|initializeRepeater|initializeRepeaterArrays|initialized|insert|insertBefore|insertChildAfter|insertChildBefore|internalConnect|internalDisconnect|internalSend|interpolate|intersection|intersects|invalidate|invalidateCache|invalidateChildOrder|invalidateData|invalidateDisplayList|invalidateFilter|invalidateList|invalidateMapping|invalidateProperties|invalidateSeries|invalidateSeriesStyles|invalidateSize|invalidateStacking|invalidateTransform|invalidateTransitions|invert|invertTransform|invoke|isAccessible|isAttribute|isBranch|isColorName|isCompensating|isCreate|isDebug|isDefaultPrevented|isDelete|isEmpty|isEmptyUpdate|isEnabled|isError|isFatal|isFocusInaccessible|isFontFaceEmbedded|isHttpURL|isHttpsURL|isInfo|isInheritingStyle|isInheritingTextFormatStyle|isInvalid|isItemHighlighted|isItemOpen|isItemSelected|isItemVisible|isOurFocus|isParentDisplayListInvalidatingStyle|isParentSizeInvalidatingStyle|isPrototypeOf|isRealValue|isSimple|isSizeInvalidatingStyle|isToggled|isTopLevel|isTopLevelWindow|isUpdate|isValidStyleValue|isWarn|isWatching|isWhitespace|itemRendererContains|itemRendererToIndex|itemRendererToIndices|join|keyDownHandler|keyUpHandler|lastIndexOf|layoutChrome|layoutEditor|legendDataChanged|length|lineGradientStyle|lineStyle|lineTo|load|loadBytes|loadPolicyFile|loadState|loadWSDL|localName|localToContent|localToData|localToGlobal|localeCompare|lock|log|logEvent|logout|makeListData|makeRowsAndColumns|map|mapCache|mappingChanged|match|max|measure|measureHTMLText|measureHeightOfItems|measureText|measureWidthOfItems|merge|min|mouseClickHandler|mouseDoubleClickHandler|mouseDownHandler|mouseEventToItemRenderer|mouseMoveHandler|mouseOutHandler|mouseOverHandler|mouseUpHandler|mouseWheelHandler|move|moveDivider|moveNext|movePrevious|moveSelectionHorizontally|moveSelectionVertically|moveTo|name|namespace|namespaceDeclarations|newInstance|nextFrame|nextName|nextNameIndex|nextPage|nextScene|nextValue|nodeKind|noise|normalize|notifyStyleChangeInChildren|numericCompare|offset|offsetPoint|onEffectEnd|onMoveTweenEnd|onMoveTweenUpdate|onScaleTweenEnd|onScaleTweenUpdate|onTweenEnd|onTweenUpdate|open|owns|paletteMap|parent|parentChanged|parse|parseCSS|parseDateString|parseNumberString|parseXML|pause|perlinNoise|pixelDissolve|pixelsToPercent|placeSortArrow|play|polar|pop|popUpMenu|qnameToString|qnamesEqual|random|readBoolean|readByte|readBytes|readDouble|readExternal|readFloat|readInt|readMultiByte|readObject|readShort|readUTF|readUTFBytes|readUnsignedByte|readUnsignedInt|readUnsignedShort|receive|receiveAudio|receiveVideo|reduceLabels|refresh|regenerateStyleCache|register|registerApplication|registerCacheHandler|registerColorName|registerDataTransform|registerEffects|registerFont|registerInheritingStyle|registerParentDisplayListInvalidatingStyle|registerParentSizeInvalidatingStyle|registerSizeInvalidatingStyle|release|releaseCollection|releaseItem|remove|removeAll|removeAllChildren|removeAllCuePoints|removeAllCursors|removeBusyCursor|removeChannel|removeChild|removeChildAt|removeCuePoint|removeCursor|removeEventListener|removeFocusManager|removeHeader|removeIndicators|removeItemAt|removeListenerHandler|removeLogger|removeNamespace|removeNode|removePopUp|removeTarget|replace|replacePort|replaceProtocol|replaceSelectedText|replaceText|replaceTokens|requestTimedOut|reset|resetNavItems|result|resultHandler|resume|resumeBackgroundProcessing|resumeEventHandling|reverse|revertChanges|rgbMultiply|rollOutHandler|rollOverHandler|rotate|rotatedGradientMatrix|round|rslCompleteHandler|rslErrorHandler|rslProgressHandler|save|saveStartValue|saveState|scale|scroll|scrollChildren|scrollHandler|scrollHorizontally|scrollPositionToIndex|scrollToIndex|scrollVertically|search|seek|seekPendingFailureHandler|seekPendingResultHandler|selectItem|send|setActualSize|setAdvancedAntiAliasingTable|setAxis|setBusyCursor|setChildIndex|setChildren|setClipboard|setColor|setCompositionString|setCredentials|setCuePoints|setCurrentState|setCursor|setDate|setDirty|setEmpty|setEnabled|setFocus|setFullYear|setHandler|setHours|setItemAt|setItemIcon|setKeyFrameInterval|setLocalName|setLoopBack|setLoopback|setMenuItemToggled|setMilliseconds|setMinutes|setMode|setMonth|setMotionLevel|setName|setNamespace|setPixel|setPixel32|setPixels|setProgress|setProperty|setPropertyIsEnumerable|setQuality|setRemoteCredentials|setRowCount|setRowHeight|setScrollBarProperties|setScrollProperties|setSecondAxis|setSeconds|setSelectColor|setSelected|setSelection|setSettings|setSilenceLevel|setSize|setStyle|setStyleDeclaration|setTextFormat|setThumbValueAt|setTime|setToggled|setTweenHandlers|setUTCDate|setUTCFullYear|setUTCHours|setUTCMilliseconds|setUTCMinutes|setUTCMonth|setUTCSeconds|setUseEchoSuppression|setVisible|settings|setupPropertyList|shift|show|showCursor|showDisplayForDownloading|showDisplayForInit|showDropFeedback|showFeedback|showFocus|showSettings|simpleType|sin|slice|some|sort|sortOn|splice|split|sqrt|stack|start|startDrag|startDragging|startEffect|status|statusHandler|stop|stopAll|stopDrag|stopDragging|stopImmediatePropagation|stopPropagation|stringCompare|stringToDate|stripNaNs|styleChanged|stylesInitialized|subscribe|substitute|substr|substring|subtract|suspendBackgroundProcessing|suspendEventHandling|swapChildren|swapChildrenAt|tan|test|text|textInput_changeHandler|threshold|toArray|toDateString|toExponential|toFixed|toLocaleDateString|toLocaleLowerCase|toLocaleString|toLocaleTimeString|toLocaleUpperCase|toLowerCase|toPrecision|toString|toTimeString|toUTCString|toUpperCase|toXMLString|togglePause|toolTipShowHandler|transform|transformCache|transformPoint|translate|trim|truncateToFit|tweenEventHandler|uncompress|union|unload|unlock|unregister|unregisterDataTransform|unshift|unsubscribe|unwatch|update|updateAfterEvent|updateBackground|updateData|updateDisplayList|updateFilter|updateList|updateMapping|updateNavItemIcon|updateNavItemLabel|updateProperties|updateStacking|updateTransform|upload|validate|validateAll|validateClient|validateCreditCard|validateCurrency|validateData|validateDate|validateDisplayList|validateEmail|validateNow|validateNumber|validatePhoneNumber|validateProperties|validateSize|validateSocialSecurity|validateString|validateTransform|validateZipCode|validationResultHandler|valueOf|verticalGradientMatrix|warn|watch|willTrigger|writeBoolean|writeByte|writeBytes|writeDouble|writeDynamicProperties|writeDynamicProperty|writeExternal|writeFloat|writeInt|writeMultiByte|writeObject|writeShort|writeUTF|writeUTFBytes|writeUnsignedInt)\b");
            e.ChangedRange.SetStyle(NumberStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers("{", "}");
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
        }

        private void SchemeSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = ";";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.ClearStyle(CommentStyle, StringStyle, NumberStyle, PreprocessorStyle, KeywordStyle,
                KeywordStyle2, KeywordStyle3);
            e.ChangedRange.SetStyle(CommentStyle, @"\;.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(StringStyle, new Regex(@"""""|''|"".*?[^\\]""|'.*?[^\\]'"));
            e.ChangedRange.SetStyle(PreprocessorStyle, @"#\:[a-zA-Z_\d]*\b");
            e.ChangedRange.SetStyle(KeywordStyle,
                @"\b(and|begin|case|cond|define|delay|do|else|if|lambda|let|letrec|or|quasiquote|quote|set!|unquote|unquote-splicing|apply|map|for-each|force|call-with-current-continuation)\b",
                RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(KeywordStyle2,
                @"\b(not|boolean||eqv|eq|equal|pair|null|list|symbol|number|complex|real|rational|integer|exact|inexact|zero|positive|negative|odd|even|char|char-alphabetic|char-numeric|char-whitespace|char-upper-case|char-lower-case|string|vector|procedure|input-port|output-port|eof-object|char-ready)\b",
                RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(KeywordStyle3,
                @"\b(abs|quotient|remainder|modulo|gcd|lcm|numerator|denominator|floor|ceiling|truncate|round|rationalize|exp|log|sin|cos|tan|asin|acos|atan|sqrt|expt|make-rectangular|make-polar|real-part|imag-part|magnitude|angle|call-with-input-file|call-with-output-file|current-input-port|current-output-port|with-input-from-file|with-output-to-file|open-input-file|open-output-file|close-input-port|close-output-port|read|read-char|peek-char|write|display|newline|write-char|load|transcript-on|transcript-of)\b",
                RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(NumberStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
        }

        private void ShellSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "#";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.LeftBracket2 = '[';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.RightBracket2 = ']';
            e.ChangedRange.ClearStyle(CommentStyle, StringStyle, KeywordStyle2, KeywordStyle2, VariableStyle,
                NumberStyle);
            e.ChangedRange.SetStyle(CommentStyle, @"#.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(StringStyle, new Regex(@"""""|''|"".*?[^\\]""|'.*?[^\\]'"));
            e.ChangedRange.SetStyle(KeywordStyle,
                @"\b(alias|bg|break|breaksw|case|cd|chdir|continue|default|dirs|echo|end|endif|eval|exec|exit|fg|foreach|glob|goto|hashstat|history|if|jobs|kill|limit|login|logout|nice|nohup|notify|onintr|popd|pushd|rehash|repeat|set|setenv|shift|source|stop|suspend|switch|time|type|umask|unalias|unhash|unlimit|unset|unsetenv|wait|while)\b");
            e.ChangedRange.SetStyle(KeywordStyle2,
                @"\b(admin|apropos|ar|as|at|atq|atrm|awk|banner|basename|batch|bc|bdiff|bfs|cal|calender|cancel|cat|cb|cc|cdc|cflow|chgrp|chkey|chmod|chown|clear|cmp|cof2elf|col|comb|comm|compress|cp|cpio|crontab|crypt|cscope|csh|csplit|ctags|ctrace|cu|cut|cxref|date|dbx|dc|dd|delta|deroff|df|diff|diff3|diffmk|dircmp|dirname|dis|download|dpost|dpost|du|ed|edit|egrep|env|eqn|ex|expr|exstr|face|factor|false|fgrep|file|find|finger|fmli|fmt|fmtmsg|fold|ftp|gcore|gencat|get|getopts|gettxt|gprof|grep|groups|head|help|hostid|hostname|iconv|id|install|ipcrm|ipcs|ismpx|join|jsh|jterm|jwin|keylogin|keylogout|ksh|layers|ld|ldd|lex|line|lint|ln|logname|lorder|lp|lpq|lpr|lprm|lprof|lpstat|lptest|ls|m4|mail|mailalias|mailx|make|makekey|man|mcs|mesg|mkdir|mkmsgs|more|mv|nawk|newform|newgrp|news|nl|nm|nroff|od|openwin|pack|page|passwd|paste|pcat|pg|pic|pr|printenv|printf|prof|prs|ps|ptx|pwd|rcp|red|regcmp|relogin|reset|rksh|rlogin|rm|rmdel|rmdir|rsh|ruptime|rwho|sact|sccs|sccsdiff|script|sdb|sdiff|sed|sh|shl|shutdown|size|sleep|soelim|sort|spell|split|srchtxt|strings|strip|stty|su|sum|tabs|tail|talk|tar|tbl|tee|telnet|test|timex|touch|tput|tr|troff|true|truss|tset|tsort|tty|uname|uncompress|unget|uniq|units|unpack|uptime|users|uucp|uudecode|uuencode|uuglist|uulog|uuname|uupick|uustat|uuto|uux|vacation|val|vc|vedit|vi|view|w|wall|wc|what|whatis|which|who|whoami|whois|write|xargs|yacc|zcat)\b");
            e.ChangedRange.SetStyle(VariableStyle, @"\b(HOME|PATH|TERM|USER|EXITIN|LOGNAME|MAIL|PWD|SHELL|TERMCAP)\b");
            e.ChangedRange.SetStyle(NumberStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
        }

        private void MakeFileSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(CommentStyle, StringStyle, KeywordStyle, KeywordStyle2, KeywordStyle3,
                VariableStyle,
                NumberStyle);
            e.ChangedRange.SetStyle(CommentStyle, @"#.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(StringStyle, @"""""|''|"".*?[^\\]""|'.*?[^\\]'");
            e.ChangedRange.SetStyle(KeywordStyle,
                @"\b(define|endef|ifdef|ifndef|ifeq|ifneq|else|endif|include|override|export|unexport|vpath)\b");
            e.ChangedRange.SetStyle(KeywordStyle3, @"^.*?(?=\=)", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(KeywordStyle2, @"^.*?(?=:)", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(VariableStyle, @"@[a-zA-Z_\d]*\b", RegexCompiledOption);
            e.ChangedRange.SetStyle(NumberStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
        }

        private void YamlSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(CommentStyle, StringStyle, KeywordStyle, NumberStyle);
            e.ChangedRange.SetStyle(CommentStyle, @"#.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(StringStyle, @"""""|''|"".*?[^\\]""|'.*?[^\\]'");
            e.ChangedRange.SetStyle(KeywordStyle, @"^.*?(?=:)", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(StringStyle, @"\:.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(NumberStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
        }

        private void DiffSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(CommentStyle, StringStyle, KeywordStyle, KeywordStyle2, KeywordStyle3);
            e.ChangedRange.SetStyle(CommentStyle, @"\-\-\-.*$|\+\+\+.*$|\*\*\*.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(StringStyle, @"\!.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(KeywordStyle, @"\+.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(KeywordStyle2, @"\-.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(KeywordStyle3, @"(?<=@)(.*?)(?=@)|@|@", RegexOptions.Multiline);
        }

        #endregion

        #region Performance Test

        /*
         #if DEBUG
         public void Dispose()
        {
            if(CommentStyle != null)CommentStyle.Dispose();
            if (CommentTagStyle != null) CommentTagStyle.Dispose();
            if (StringStyle != null) StringStyle.Dispose();
            if (NumberStyle != null) NumberStyle.Dispose();
            if(KeywordStyle !=null)KeywordStyle.Dispose();
            if (KeywordStyle2 != null) KeywordStyle2.Dispose();
            if (KeywordStyle3 != null) KeywordStyle3.Dispose();
            if (PreprocessorStyle != null) PreprocessorStyle.Dispose();
            if (ClassNameStyle != null) ClassNameStyle.Dispose();
            if (ClassNameStyle2 != null) ClassNameStyle2.Dispose();
            if (AttributeStyle != null) AttributeStyle.Dispose();
            if (AttributeValueStyle != null) AttributeValueStyle.Dispose();
            if (HtmlEntityStyle != null) HtmlEntityStyle.Dispose();
            if (TagNameStyle != null) TagNameStyle.Dispose();
            if (TagBracketStyle != null) TagBracketStyle.Dispose();
            if (StatementsStyle != null) StatementsStyle.Dispose();
            if (TypesStyle != null) TypesStyle.Dispose();
            if (FunctionsStyle != null) FunctionsStyle.Dispose();
            if (VariableStyle != null) VariableStyle.Dispose();
            if (CSSSelectorStyle != null) CSSSelectorStyle.Dispose();
            if (CSSPropertyStyle != null) CSSPropertyStyle.Dispose();
            if (CharStyle != null) CharStyle.Dispose();
        } 
         #endif
         */

        #endregion
    }

    public class Tag
    {
        public int Id;
        public string Name;
        public int StartLine;

        public string Marker
        {
            get { return Name + Id; }
        }
    }
}