#region

using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

#endregion

namespace SS.Ynote.Classic.Features.Packages
{
    public class YnoteOnlinePackage
    {
        /// <summary>
        ///     Package Url
        /// </summary>
        public string PackageUrl { get; set; }

        /// <summary>
        ///     Downloads the Package
        /// </summary>
        public void DownloadPackage()
        {
            var downloader = new Downloader(PackageUrl) {StartPosition = FormStartPosition.CenterParent};
            downloader.DownloadPlugin();
        }
    }
}