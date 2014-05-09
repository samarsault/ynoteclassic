using System.Text.RegularExpressions;
using FastColoredTextBoxNS;

namespace SS.Ynote.Classic.Features.Syntax
{
    /// <summary>
    ///     Syntax Rule
    /// </summary>
    internal sealed class SyntaxRule
    {
        /// <summary>
        ///     The Regex Options
        /// </summary>
        internal RegexOptions Options;

        /// <summary>
        ///     Regex To Highlight
        /// </summary>
        internal string Regex;

        /// <summary>
        ///     The Style of the Rule eg. -> CommentStyle
        /// </summary>
        internal Style Type;
    }

    /// <summary>
    ///     Folding Rule
    /// </summary>
    internal sealed class FoldingRule
    {
        /// <summary>
        ///     The Folding End Marker
        /// </summary>
        internal string FoldingEndMarker;

        /// <summary>
        ///     The Folding Start Marker
        /// </summary>
        internal string FoldingStartMarker;

        /// <summary>
        ///     The RegexOptions
        /// </summary>
        internal RegexOptions Options;
    }
}