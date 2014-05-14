namespace SUP
{
    /// <summary>
    ///     A File which has to be Updated
    /// </summary>
    public class UpdateFile : FileBase
    {
        /// <summary>
        ///     Default Constructor
        /// </summary>
        /// <param name="versionID"></param>
        public UpdateFile(double versionID)
        {
            VersionID = versionID;
        }

        /// <summary>
        ///     Download URL
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        ///     Where the file goes after being downloaded
        /// </summary>
        public string OutPath { get; set; }

        /// <summary>
        ///     Whether the fileID already exist or has
        ///     to be added
        /// </summary>
        public bool Add { get; set; }
    }
}