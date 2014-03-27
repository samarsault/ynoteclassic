using System.Collections.Generic;

namespace SS.Ynote.Classic.Features.Syntax
{
    public class SyntaxBase
    {
        public string SysPath { get; set; }

        /// <summary>
        /// Style Regex Dictionary
        /// </summary>
        public readonly IList<SyntaxRule> Rules;

        /// <summary>
        /// Folding Regexes
        /// </summary>
        public readonly IList<FoldingRule> FoldingRules;

        /// <summary>
        /// Left Bracket
        /// </summary>
        public char LeftBracket { get; set; }

        /// <summary>
        /// Left Bracket 2
        /// </summary>
        public char LeftBracket2 { get; set; }

        /// <summary>
        /// Right Bracket
        /// </summary>
        public char RightBracket { get; set; }

        /// <summary>
        /// Right Bracket 2
        /// </summary>
        public char RightBracket2 { get; set; }

        /// <summary>
        /// File Extensions
        /// </summary>
        public string[] Extensions { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SyntaxBase()
        {
            Rules = new List<SyntaxRule>();
            FoldingRules = new List<FoldingRule>();
        }

        /// <summary>
        /// Comment Prefix
        /// </summary>
        public string CommentPrefix { get; set; }
    }
}