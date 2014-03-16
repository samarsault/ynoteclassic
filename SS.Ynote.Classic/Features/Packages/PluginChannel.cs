#region

using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

#endregion

namespace SS.Ynote.Classic.Features.Packages
{
    public static class PluginChannel
    {
        // <?xml version="1.0"?>
        //      <YnotePackages>
        //          <Package Version="1.0" Url="ZipManager.ypk" Name="ZipManager"></Package>
        //          <Packag
        //      </YnotePackages>
        public static IEnumerable<YnoteOnlinePackage> GetOnlinePackages(string channelUri)
        {
            IList<YnoteOnlinePackage> packages = new List<YnoteOnlinePackage>();
            using (var reader = XmlReader.Create(channelUri))
            {
                while (reader.Read())
                    if (reader.IsStartElement())
                        if (reader.Name == "Package")
                        {
                            var pack = new YnoteOnlinePackage
                            {
                                Name = reader["Name"],
                                Version = reader["Version"],
                                PackageUrl = reader["Url"],
                                Description = reader["Description"]
                            };
                            packages.Add(pack);
                        }
            }
            return packages;
        }
    }

    public class YnoteOnlinePackage
    {
        /// <summary>
        ///     Package Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Package Version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        ///     Package Url
        /// </summary>
        public string PackageUrl { get; set; }

        /// <summary>
        ///     Description
        /// </summary>
        public string Description { get; set; }

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