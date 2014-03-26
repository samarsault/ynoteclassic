#region

using System.Windows.Forms;

#endregion

namespace SS.Ynote.Classic.Features.Packages
{
    public class YnoteOnlinePackage
    {
        /// <summary>
        ///     Package Url
        /// </summary>
        public string PackageUrl { private get; set; }

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