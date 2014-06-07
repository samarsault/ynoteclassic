using System.IO;
using System.Xml;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

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
        ///     Loads a Project
        /// </summary>
        /// <returns></returns>
        public static YnoteProject Load(string file)
        {
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