using System.Text.RegularExpressions;

namespace FastColoredTextBoxNS
{
    internal class SyntaxHighlighter
    {
        //styles
        protected static readonly Platform platformType = PlatformType.GetOperationSystemPlatform();

        public static RegexOptions RegexCompiledOption
        {
            get
            {
                if (platformType == Platform.X86)
                    return RegexOptions.Compiled;
                return RegexOptions.None;
            }
        }

        public virtual void AutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            var tb = (sender as FastColoredTextBox);
            Language language = tb.Language;
            switch (language)
            {
                case Language.CSharp:
                    CSharpAutoIndentNeeded(sender, args);
                    break;

                case Language.VB:
                    VBAutoIndentNeeded(sender, args);
                    break;

                case Language.HTML:
                    HTMLAutoIndentNeeded(sender, args);
                    break;

                case Language.SQL:
                    SQLAutoIndentNeeded(sender, args);
                    break;

                case Language.PHP:
                    PHPAutoIndentNeeded(sender, args);
                    break;

                case Language.Javascript:
                    CSharpAutoIndentNeeded(sender, args);
                    break; //JS like C#
                default:
                    break;
            }
        }

        protected void PHPAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            /*
            FastColoredTextBox tb = sender as FastColoredTextBox;
            tb.CalcAutoIndentShiftByCodeFolding(sender, args);*/
            //block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\{.*\}[^""']*$"))
                return;
            //start of block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\{"))
            {
                args.ShiftNextLines = args.TabLength;
                return;
            }
            //end of block {}
            if (Regex.IsMatch(args.LineText, @"}[^""']*$"))
            {
                args.Shift = -args.TabLength;
                args.ShiftNextLines = -args.TabLength;
                return;
            }
            //is unclosed operator in previous line ?
            if (Regex.IsMatch(args.PrevLineText, @"^\s*(if|for|foreach|while|[\}\s]*else)\b[^{]*$"))
                if (!Regex.IsMatch(args.PrevLineText, @"(;\s*$)|(;\s*//)")) //operator is unclosed
                {
                    args.Shift = args.TabLength;
                }
        }

        protected void SQLAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            var tb = sender as FastColoredTextBox;
            tb.CalcAutoIndentShiftByCodeFolding(sender, args);
        }

        protected void HTMLAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            var tb = sender as FastColoredTextBox;
            tb.CalcAutoIndentShiftByCodeFolding(sender, args);
        }

        protected void VBAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            //end of block
            if (Regex.IsMatch(args.LineText, @"^\s*(End|EndIf|Next|Loop)\b", RegexOptions.IgnoreCase))
            {
                args.Shift = -args.TabLength;
                args.ShiftNextLines = -args.TabLength;
                return;
            }
            //start of declaration
            if (Regex.IsMatch(args.LineText,
                @"\b(Class|Property|Enum|Structure|Sub|Function|Namespace|Interface|Get)\b|(Set\s*\()",
                RegexOptions.IgnoreCase))
            {
                args.ShiftNextLines = args.TabLength;
                return;
            }
            // then ...
            if (Regex.IsMatch(args.LineText, @"\b(Then)\s*\S+", RegexOptions.IgnoreCase))
                return;
            //start of operator block
            if (Regex.IsMatch(args.LineText, @"^\s*(If|While|For|Do|Try|With|Using|Select)\b", RegexOptions.IgnoreCase))
            {
                args.ShiftNextLines = args.TabLength;
                return;
            }

            //Statements else, elseif, case etc
            if (Regex.IsMatch(args.LineText, @"^\s*(Else|ElseIf|Case|Catch|Finally)\b", RegexOptions.IgnoreCase))
            {
                args.Shift = -args.TabLength;
                return;
            }

            //Char _
            if (args.PrevLineText.TrimEnd().EndsWith("_"))
            {
                args.Shift = args.TabLength;
            }
        }

        protected void CSharpAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            //block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\{.*\}[^""']*$"))
                return;
            //start of block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\{"))
            {
                args.ShiftNextLines = args.TabLength;
                return;
            }
            //end of block {}
            if (Regex.IsMatch(args.LineText, @"}[^""']*$"))
            {
                args.Shift = -args.TabLength;
                args.ShiftNextLines = -args.TabLength;
                return;
            }
            //label
            if (Regex.IsMatch(args.LineText, @"^\s*\w+\s*:\s*($|//)") &&
                !Regex.IsMatch(args.LineText, @"^\s*default\s*:"))
            {
                args.Shift = -args.TabLength;
                return;
            }
            //some statements: case, default
            if (Regex.IsMatch(args.LineText, @"^\s*(case|default)\b.*:\s*($|//)"))
            {
                args.Shift = -args.TabLength / 2;
                return;
            }
            //is unclosed operator in previous line ?
            if (Regex.IsMatch(args.PrevLineText, @"^\s*(if|for|foreach|while|[\}\s]*else)\b[^{]*$"))
                if (!Regex.IsMatch(args.PrevLineText, @"(;\s*$)|(;\s*//)")) //operator is unclosed
                {
                    args.Shift = args.TabLength;
                }
        }
    }

    /// <summary>
    ///     Language
    /// </summary>
    public enum Language
    {
        Text,
        HTML,
        ASP,
        Javascript,
        CSS,
        PHP,
        SQL,
        Batch,
        INI,
        JSON,
        Actionscript,
        Antlr,
        Assembly,
        C,
        CPP,
        CoffeeScript,
        CSharp,
        D,
        Diff,
        FSharp,
        Haskell,
        Java,
        LaTeX,
        Lua,
        Lisp,
        Makefile,
        MATLAB,
        Objective_C,
        Pascal,
        Perl,
        PowerShell,
        Python,
        R,
        Ruby,
        Scala,
        Scheme,
        Shell,
        QBasic,
        VB,
        Xml,
        Yaml,
    }
}