using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SS.Ynote.Classic.Core.Extensibility;
using SS.Ynote.Classic.Core.Settings;

namespace SS.Ynote.Classic.Core.RunScript
{
    public class RunScript
    {
        /// <summary>
        ///     Series of Commands
        /// </summary>
        private IDictionary<string, string[]> Tasks = new Dictionary<string, string[]>();

        /// <summary>
        ///     Name of the Script
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Scope of the Script
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        ///     Local Path of the RunScript
        /// </summary>
        [JsonIgnore]
        public string LocalPath { get; set; }

        public static RunScript Get(string file)
        {
            string json = File.ReadAllText(file);
            var dic = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(json);
            var runsc = new RunScript();
            runsc.Tasks = dic;
            AddName(dic, runsc);
            runsc.LocalPath = file;
            return runsc;
        }

        private static void AddName(Dictionary<string, string[]> dic, RunScript script)
        {
            foreach (var item in dic)
            {
                if (item.Key == "Name")
                    script.Name = item.Value[0];
                if (item.Key == "Scope")
                    script.Scope = item.Value[0];
            }
        }

        public void Save(string file)
        {
            var serialized = JsonConvert.SerializeObject(Tasks, Formatting.Indented);
            File.WriteAllText(file, serialized);
        }

        /// <summary>
        ///     Runs the Script
        /// </summary>
        public void Run()
        {
            foreach (var task in Tasks)
            {
                if (task.Key != "Name")
                {
                    if (task.Key != "Scope")
                    {
                        string ys = GlobalSettings.SettingsDir + task.Key + ".runtask";
                        // expand all abbreviations eg - $source_path, $project_path
                        for (int i = 0; i < task.Value.Length; i++)
                        {
                            task.Value[i] = Globals.ExpandAbbr(task.Value[i], Globals.Ynote);
                        }
                        YnoteScript.InvokeScript(task.Value, ys, "*.RunTask");
                    }
                }
            }
        }

        public static IEnumerable<string> GetConfigurations()
        {
            return Directory.GetFiles(GlobalSettings.SettingsDir, "*.ynoterun", SearchOption.AllDirectories);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}