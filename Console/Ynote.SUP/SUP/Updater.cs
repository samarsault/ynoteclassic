using System.Collections.Generic;
using System.Net;
using SUP.Host;

namespace SUP
{
    /// <summary>
    ///     Update Checker
    /// </summary>
    public class Updater
    {
        private readonly IUpdateHost _host;

        /// <summary>
        ///     Default Constructor
        /// </summary>
        /// <param name="host">The Host Application of the Update</param>
        public Updater(IUpdateHost host)
        {
            _host = host;
        }

        /// <summary>
        ///     Checks For Updates
        /// </summary>
        /// <returns></returns>
        public bool IsUpdateAvailable()
        {
            if (UrlExists(_host.UpdateFile))
            {
                if (GetUpdate().ListedUpdateFiles != null)
                    return true;
            }
            return false;
        }

        /// <summary>
        ///     Gets the Files which need Update
        /// </summary>
        /// <returns></returns>
        public Update GetUpdate()
        {
            Update update = Update.FromFile(_host.UpdateFile);
            IList<UpdateFile> updateFiles = new List<UpdateFile>();
            if (UrlExists(_host.UpdateFile))
            {
                if (update != null)
                {
                    foreach (UpdateFile file in update.ListedUpdateFiles)
                    {
                        file.OutPath = file.OutPath.Replace("$app_path", _host.RootDirectory);
                        var appFiles = _host.Files;
                        foreach (AppFile appfile in appFiles)
                        {
                            if (appfile.FileID == file.FileID)
                            {
                                if (file.VersionID > appfile.VersionID)
                                {
                                    updateFiles.Add(file);
                                }
                            }
                        }
                    }
                }
                update.ListedUpdateFiles = updateFiles;
            }
            return update;
        }

        /// <summary>
        ///     Checks whether a Url Exists
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool UrlExists(string url)
        {
            bool result = true;
            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Timeout = 1200; // miliseconds
            webRequest.Method = "HEAD";
            try
            {
                webRequest.GetResponse();
            }
            catch (WebException)
            {
                result = false;
            }

            return result;
        }
    }
}