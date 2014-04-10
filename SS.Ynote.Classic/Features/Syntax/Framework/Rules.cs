using System.Text.RegularExpressions;
using FastColoredTextBoxNS;

namespace SS.Ynote.Classic.Features.Syntax
{
    /// <summary>
    ///     Syntax Rule
    /// </summary>
    public sealed class SyntaxRule
    {
        /// <summary>
        ///     The Regex Options
        /// </summary>
        public RegexOptions Options;

        /// <summary>
        ///     Regex To Highlight
        /// </summary>
        public string Regex;

        /// <summary>
        ///     The Style of the Rule eg. -> CommentStyle
        /// </summary>
        public Style Type;
    }

    /// <summary>
    ///     Folding Rule
    /// </summary>
    public sealed class FoldingRule
    {
        /// <summary>
        ///     The Folding End Marker
        /// </summary>
        public string FoldingEndMarker;

        /// <summary>
        ///     The Folding Start Marker
        /// </summary>
        public string FoldingStartMarker;

        /// <summary>
        ///     The RegexOptions
        /// </summary>
        public RegexOptions Options;
    }
}