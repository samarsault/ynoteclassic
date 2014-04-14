using System.Collections.Generic;

namespace SS.Ynote.Classic.Features.Syntax
{
    public class SyntaxBase
    {
        /// <summary>
        ///     Folding Regexes
        /// </summary>
        internal readonly IList<FoldingRule> FoldingRules;

        /// <summary>
        ///     Style Regex Dictionary
        /// </summary>
        internal readonly IList<SyntaxRule> Rules;

        /// <summary>
        ///     Default Constructor
        /// </summary>
        public SyntaxBase()
        {
            Rules = new List<SyntaxRule>();
            FoldingRules = new List<FoldingRule>();
        }

        internal string SysPath { get; set; }

        /// <summary>
        ///     Left Bracket
        /// </summary>
        internal char LeftBracket { get; set; }

        /// <summary>
        ///     Left Bracket 2
        /// </summary>
        internal char LeftBracket2 { get; set; }

        /// <summary>
        ///     Right Bracket
        /// </summary>
        internal char RightBracket { get; set; }

        /// <summary>
        ///     Right Bracket 2
        /// </summary>
        internal char RightBracket2 { get; set; }

        /// <summary>
        ///     File Extensions
        /// </summary>
        internal string[] Extensions { get; set; }

        /// <summary>
        ///     Comment Prefix
        /// </summary>
        internal string CommentPrefix { get; set; }
    }
}