using System;
using System.Collections.Generic;
using System.Xml;

namespace SS.Ynote.Classic.Plugins
{
    /// <summary>
    /// A Plugin Channel to retrieve info
    /// </summary>
    public class PluginChannel
    {
        /// <summary>
        /// List of Plugins
        /// </summary>
        public List<PluginItem> YnotePlugins { get; set; }
        /// <summary>
        /// Plugin Channel
        /// </summary>
        public PluginChannel()
        {
            YnotePlugins = new List<PluginItem>();
            Read("http://data.sscorps.tk/ynote/plugins.xml");
        }
        private void Read(string file)
        {
            if (file == null) return;
            using (var reader = XmlReader.Create(file))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                        if (reader.Name == "Plugin")
                        {
                            var dat = new PluginItem
                            {
                                Name = reader["Name"],
                                Description = reader["Description"],
                                Version = Convert.ToDouble(reader["Version"]),
                                DownloadLink = reader["Link"]
                            };
                            YnotePlugins.Add(dat);
                            break;
                        }
                }
            }
        }
    }
}
