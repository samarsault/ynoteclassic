using System;
using System.Collections.Generic;
using System.Xml;

namespace SUP
{
    /// <summary>
    ///     A File which is part of the Aplication
    ///     and is on the System
    /// </summary>
    public class AppFile : FileBase
    {
        /// <summary>
        ///     Default Constructor
        /// </summary>
        /// <param name="versionID">the Version ID</param>
        /// <param name="sysPath">Path of the File on the System</param>
        public AppFile(string sysPath, double versionID, int fileID)
        {
            FileSystemPath = sysPath;
            VersionID = versionID;
            FileID = fileID;
        }

        /// <summary>
        ///     Gets the FileSystemPath of the App
        /// </summary>
        public string FileSystemPath { get; set; }

        /// <summary>
        ///     Gets the list of appfiles from
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static IEnumerable<AppFile> GetAppFiles(string file)
        {
            if (string.IsNullOrEmpty(file))
                throw new NullReferenceException();
            var appfiles = new List<AppFile>();
            using (var reader = XmlReader.Create(file))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                        if (reader.Name == "File")
                        {
                            var appfile = new AppFile(reader["SysPath"], Convert.ToDouble(reader["VersionID"]),
                                Convert.ToInt32(reader["FileID"]));
                            appfile.Name = reader["Name"];
                            appfiles.Add(appfile);
                        }
                }
            }
            return appfiles;
        }
    }
}