using System.Collections.Generic;

namespace SUP.Host
{
    public interface IUpdateHost
    {
        /// <summary>
        ///     Name of the Host
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Gets the Current Version
        /// </summary>
        double CurrentVersion { get; }

        /// <summary>
        ///     The Update File
        /// </summary>
        string UpdateFile { get; }

        /// <summary>
        ///     Used to Download the Latest Files
        /// </summary>
        string LatestFilesUpdate { get; }

        /// <summary>
        ///     Gets the RootDirectory of the File
        /// </summary>
        string RootDirectory { get; }

        /// <summary>
        ///     Files of the Application
        /// </summary>
        IEnumerable<AppFile> Files { get; set; }
    }
}