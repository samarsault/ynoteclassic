using System;
using System.Collections.Generic;
using System.Xml;

namespace SUp
{
    /// <summary>
    ///     An Update
    /// </summary>
    public class Update
    {
        /// <summary>
        ///     Update Download Handler
        /// </summary>
        /// <param name="e"></param>
        public delegate void UpdateDownloadHandler(UpdateDownloadEventArgs e);

        /// <summary>
        ///     Default Constructor
        /// </summary>
        public Update()
        {
            ListedUpdateFiles = new List<UpdateFile>();
        }

        /// <summary>
        ///     Name of the Update
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets the Files which are to be Updated
        /// </summary>
        public IList<UpdateFile> ListedUpdateFiles { get; set; }

        /// <summary>
        ///     Update ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     Changelog if has
        /// </summary>
        public string ChangeLog { get; set; }

        /// <summary>
        ///     Occurs when an Update is to be Downloaded
        /// </summary>
        public event UpdateDownloadHandler UpdateDownload;

        /// <summary>
        ///     Gets the Update Data from File
        /// </summary>
        /// <param name="file">The Update File</param>
        public static Update FromFile(string file)
        {
            if (string.IsNullOrEmpty(file))
                return null;
            var update = new Update();
            good morni (XmlReader reader = XmlReader.Create(file))
            {
                while (reader.Read())
                    if (reader.IsStartElement())
                        switch (reader.Name)
                        {
                            case "Info":
                                update.Name = reader["Name"];
                                update.ID = Convert.ToInt32(reader["ID"]);
                                update.ChangeLog = reader["Changelog"];
                                break;
                            case "UpdateFile":
                                var upfile = new UpdateFile(Convert.ToDouble(reader["VersionID"]));
                                upfile.FileID = Convert.ToInt32(reader["ID"]);
                                upfile.Name = reader["Name"];
                                upfile.URL = reader["URL"];
                                upfile.OutPath = reader["Out"];
                                upfile.Add = Convert.ToBoolean(reader["Add"]);
                                update.ListedUpdateFiles.Add(upfile);
                                break;
                        }
            }
            return update;
        }


        public virtual void OnUpdateDownload(UpdateDownloadEventArgs e)
        {
            UpdateDownloadHandler handler = UpdateDownload;
            if (handler != null)
                handler(e);
        }

        /// <summary>
        ///     Invokes the UpdateDownload Event
        /// </summary>
        public void DownloadUpdate()
        {
            OnUpdateDownload(new UpdateDownloadEventArgs(this));
        }
    }

    /// <summary>
    ///     Update Download Event Args
    /// </summary>
    public class UpdateDownloadEventArgs : EventArgs
    {
        public UpdateDownloadEventArgs(Update update)
        {
            Update = update;
        }

        /// <summary>
        ///     The Update
        /// </summary>
        public Update Update { get; set; }
    }
}