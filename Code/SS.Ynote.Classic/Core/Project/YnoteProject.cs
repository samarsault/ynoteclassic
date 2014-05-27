using System.Xml;

namespace SS.Ynote.Classic.Core.Project
{
    /// <summary>
    ///     Structure of a Ynote Project
    /// </summary>
    public class YnoteProject
    {
        /// <summary>
        ///     Checks whether the project has been saved
        /// </summary>
        public bool IsSaved
        {
            get { return FilePath != null; }
        }

        public string FilePath { get; set; }

        /// <summary>
        ///     Root Path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        ///     Name of the Project
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Files to Exclude
        /// </summary>
        public string[] ExcludeFileTypes { get; private set; }

        /// <summary>
        ///     Directory to Exclude
        /// </summary>
        public string[] ExcludeDirectories { get; private set; }

        /// <summary>
        ///     Loads a Project
        /// </summary>
        /// <returns></returns>
        public static YnoteProject Load(string file)
        {
            var project = new YnoteProject();
            project.FilePath = file;
            using (var reader = XmlReader.Create(file))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "Name":
                                project.Name = reader.ReadElementContentAsString();
                                break;
                            case "Path":
                                project.Path = reader.ReadElementContentAsString();
                                break;
                            case "ExcludeFiles":
                                project.ExcludeFileTypes = reader.ReadElementContentAsString().Split(',');
                                break;
                            case "ExcludeDirectory":
                                project.ExcludeDirectories = reader.ReadElementContentAsString().Split(',');
                                break;
                        }
                    }
                }
            }
            return project;
        }

        /// <summary>
        ///     Saves the Project
        /// </summary>
        public void Save(string file)
        {
            using (var writer = XmlWriter.Create(file, new XmlWriterSettings {Indent = true, NewLineChars = "\r\n"}))
            {
                writer.WriteStartElement("YnoteProject");
                writer.WriteElementString("Name", Name);
                writer.WriteElementString("Path", Path);
                if (ExcludeFileTypes != null)
                    writer.WriteElementString("ExcludeFiles", string.Join(",", ExcludeFileTypes));
                if (ExcludeDirectories != null)
                    writer.WriteElementString("ExcludeDirectory", string.Join(",", ExcludeDirectories));
                writer.WriteEndElement();
                writer.Close();
            }
        }
    }
}