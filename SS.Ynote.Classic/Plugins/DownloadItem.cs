using System.IO;

namespace SS.Ynote.Classic.Plugins
{
    public class DownloadItem
    {
        /// <summary>
        /// Whether file is successfully downloaded
        /// </summary>
        public bool SuccessfullyDownloaded { get; set; }
        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        public DownloadType DownloadType { get; set; }
        /// <summary>
        /// The Directory where the Plugins will be saved (temp)
        /// </summary>
        public string Directory { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public DownloadItem(string url, DownloadType t)
        {
            SuccessfullyDownloaded = false;
            Directory = Path.GetTempPath() + @"\YnotePlugins\"; ;
            Url = url;
            DownloadType = t;
        }
        public string GetSaveDirFromType(string filename)
        {
            var str = string.Empty;
            switch (DownloadType)
            {
                case DownloadType.Dependency:
                    str = Directory + filename;
                    break;
                case DownloadType.Plugin:
                    str = Directory + @"\Plugins\" + filename;
                    break;
            }
            return str;
        }
    }

    public enum DownloadType
    {
        Dependency,
        Plugin,
    }
}