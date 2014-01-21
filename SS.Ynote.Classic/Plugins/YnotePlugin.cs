using System.Linq;
using System.Security.Policy;
using System.Xml;
using System.Collections.Generic;

namespace SS.Ynote.Classic.Plugins
{
    public class YnotePluginData
    {
        /// <summary>
        /// Dependencies
        /// </summary>
        public IList<string> Dependencies { get; set; }
        /// <summary>
        /// Plugin File
        /// </summary>
        public string PluginFile { get; set; }
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="file"></param>
        public YnotePluginData(string file)
        {
            Read(file);
            Dependencies = new List<string>();
        }

        public void Download()
        {
            var lst = new List<DownloadItem>();
            lst.Add(new DownloadItem(PluginFile, DownloadType.Plugin));
            lst.AddRange(Dependencies.Select(dependency => new DownloadItem(dependency, DownloadType.Dependency)));
            var downloader = new PluginDownloader(lst);
            downloader.ShowDialog();
        }
        /// <summary>
        /// Read .ynoteplugin file
        /// </summary>
        /// <param name="file"></param>
        private void Read(string file)
        {
            if (file == null) return;
            using (var reader = XmlReader.Create(file))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                        switch (reader.Name)
                        {
                            case "Plugin":
                                PluginFile = reader["File"];
                                break;
                            case "Dependency":
                                var dependency = (reader["Include"]);
                                Dependencies.Add(dependency);
                                if(reader.Read())
                                    Dependencies.Add(dependency);
                                break;
                        }
                }
            }
        }
    }
}
