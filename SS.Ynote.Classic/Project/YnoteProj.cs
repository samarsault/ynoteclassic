//======================================
//
// Copyright (C) 2014 Samarjeet Singh
// The Ynote Classic Project
//
//======================================

using System.Xml;

namespace SS.Ynote.Classic.Project
{
    public class YnoteProject
    {
        public string BuildFile { get; set; }

        /// <summary>
        /// Folders in Project
        /// </summary>
        public string Folder { get; set; }

        /// <summary>
        /// Project Name
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Project File
        /// </summary>
        public string ProjectFile { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="file"></param>
        public YnoteProject(string file)
        {
            ProjectFile = file;
            ReadProjectFile();
        }

        /// <summary>
        /// Default Constructor without param
        /// </summary>
        public YnoteProject()
        {

        }

        private void ReadProjectFile()
        {
            ReadProjectFile(ProjectFile);
        }

        /// <summary>
        /// Read Project File
        /// </summary>
        public void ReadProjectFile(string file)
        {
            if (ProjectFile == null) return;
            using (var reader = XmlReader.Create(ProjectFile))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                        switch (reader.Name)
                        {
                            case "Project":
                                ProjectName = reader["Name"];
                                break;
                            case "Folder":
                                Folder = (reader["Include"]);
                                break;
                            case "Build":
                                BuildFile = reader["File"];
                                break;
                        }
                }
            }
        }

        /// <summary>
        /// Make Project File
        /// </summary>
        /// <param name="outfile"></param>
        public void MakeProjectFile(string outfile)
        {
            var xmlWriterSettings = new XmlWriterSettings {NewLineOnAttributes = true, Indent = true};
            using (var writer = XmlWriter.Create(outfile, xmlWriterSettings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("YnoteProject");
                writer.WriteStartElement("Project");
                writer.WriteAttributeString("Name", ProjectName);
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
