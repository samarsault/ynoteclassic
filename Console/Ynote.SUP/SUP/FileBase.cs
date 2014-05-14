namespace SUP
{
    /// <summary>
    ///     Base of AppFile and UpdateFile
    /// </summary>
    public class FileBase
    {
        /// <summary>
        ///     Name of the File
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Version ID of the File
        /// </summary>
        public double VersionID { get; set; }

        /// <summary>
        ///     The ID of the File
        ///     Used as a Unique identifier to the file
        /// </summary>
        public int FileID { get; set; }
    }
}