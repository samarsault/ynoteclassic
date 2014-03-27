//=====================================
//
// Helper Classes for Syntax Highlight using Xml Files
// The Ynote Classic Project
//
//=====================================

using FastColoredTextBoxNS;
using System.Text.RegularExpressions;

namespace SS.Ynote.Classic.Features.Syntax
{
    /// <summary>
    /// Syntax Rule
    /// </summary>
    public class SyntaxRule
    {
        /// <summary>
        /// The Style of the Rule eg. -> CommentStyle
        /// </summary>
        public Style Type;

        /// <summary>
        /// Regex To Highlight
        /// </summary>
        public string Regex;

        /// <summary>
        /// The Regex Options
        /// </summary>
        public RegexOptions Options;
    }

    /// <summary>
    /// Folding Rule
    /// </summary>
    public class FoldingRule
    {
        /// <summary>
        /// The Folding Start Marker
        /// </summary>
        public string FoldingStartMarker;

        /// <summary>
        /// The Folding End Marker
        /// </summary>
        public string FoldingEndMarker;

        /// <summary>
        /// The RegexOptions
        /// </summary>
        public RegexOptions Options;
    }
}