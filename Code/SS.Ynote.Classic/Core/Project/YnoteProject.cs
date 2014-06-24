using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SS.Ynote.Classic.Core.Project
{
    /// <summary>
    ///     Structure of a Ynote Project
    /// </summary>
    public class YnoteProject
    {
        [JsonIgnore]
        public string LayoutFile { get { return string.Format(@"{0}\{1}.ynotelayout", System.IO.Path.GetDirectoryName(FilePath), Name); } }
        /// <summary>
        ///     Checks whether the project has been saved
        /// </summary>
        [JsonIgnore]
        public bool IsSaved
        {
            get { return FilePath != null; }
        }
        [JsonIgnore]
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
        public string[] ExcludeFileTypes { get;  set; }

        /// <summary>
        ///     Directory to Exclude
        /// </summary>
        public string[] ExcludeDirectories { get;  set; }
        /// <summary>
        /// Extra ( for plugins ) some arguments
        /// </summary>
        public string[] Arguments { get; set; }
        /// <summary>
        ///     Loads a Project
        /// </summary>
        /// <returns></returns>
        public static YnoteProject Load(string file)
        {
            if (!File.Exists(file))
            {
                MessageBox.Show("Cannot Read Project \n" + file, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            string json = File.ReadAllText(file);
            var proj =JsonConvert.DeserializeObject<YnoteProject>(json);
            proj.FilePath = file;
            return proj;
        }

        /// <summary>
        ///     Saves the Project
        /// </summary>
        public void Save(string file)
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(file,json);
        }
    }
}