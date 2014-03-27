using System.Xml;

namespace SS.Ynote.Classic.Features.Project
{
    /// <summary>
    ///     Structure of a .ynoteproj file
    /// </summary>
    public class YnoteProject
    {
        public string BuildFile { get; set; }

        /// <summary>
        ///     Folders in Project
        /// </summary>
        public string Folder { get; set; }

        /// <summary>
        ///     Project Name
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        ///     Project File
        /// </summary>
        public string ProjectFile { get; set; }

        /// <summary>
        ///     Read Project File
        /// </summary>
        public static YnoteProject Read(string file)
        {
            var proj = new YnoteProject();
            using (var reader = XmlReader.Create(file))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                        switch (reader.Name)
                        {
                            case "Project":
                                proj.ProjectName = reader["Name"];
                                proj.ProjectFile = reader["File"];
                                break;

                            case "Folder":
                                proj.Folder = reader["Include"];
                                break;

                            case "Build":
                                proj.BuildFile = reader["File"];
                                break;
                        }
                }
            }
            return proj;
        }

        /// <summary>
        ///     Make Project File
        /// </summary>
        /// <param name="outfile"></param>
        public void MakeProjectFile(string outfile)
        {
            var xmlWriterSettings = new XmlWriterSettings { NewLineOnAttributes = true, Indent = true };
            using (var writer = XmlWriter.Create(outfile, xmlWriterSettings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("YnoteProject");
                writer.WriteStartElement("Project");
                writer.WriteAttributeString("Name", ProjectName);
                writer.WriteAttributeString("File", ProjectFile);
                writer.WriteEndElement();
                writer.WriteStartElement("Folder");
                writer.WriteAttributeString("Include", Folder);
                writer.WriteEndElement();
                if (BuildFile != string.Empty)
                {
                    writer.WriteStartElement("Build");
                    writer.WriteAttributeString("File", BuildFile);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                var doc = new XmlDocument();
                doc.Save(writer);
            }
        }
    }
}