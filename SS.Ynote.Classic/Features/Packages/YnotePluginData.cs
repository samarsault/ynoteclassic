#region

using System.Collections.Generic;

#endregion

namespace SS.Ynote.Classic.Features.Packages
{
    public class YnotePluginData
    {
        /// <summary>
        ///     Manifest File
        /// </summary>
        public string ManifestFile { get; set; }

        /// <summary>
        ///     Plugin File
        /// </summary>
        public string PluginFile { get; set; }

        /// <summary>
        ///     References
        /// </summary>
        public IEnumerable<string> References { get; set; }
    }
}