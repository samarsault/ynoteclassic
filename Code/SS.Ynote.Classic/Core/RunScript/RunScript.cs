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
        ///     Local Path of the RunScript
        /// </summary>
        public string LocalPath { get; set; }

        public static RunScript Get(string file)
        {
            string json = File.ReadAllText(file);
            var dic = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(json);
            var runsc = new RunScript();
            runsc.Tasks = dic;
            runsc.LocalPath = file;
            return runsc;
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
                string ys = GlobalSettings.SettingsDir + task.Key + ".runtask";
                // expand all abbreviations eg - $source_path, $project_path
                for (int i = 0; i < task.Value.Length; i++)
                {
                    task.Value[i] = Globals.ExpandAbbr(task.Value[i], Globals.Ynote);
                }
                YnoteScript.InvokeScript(ys, "*.RunTask", task.Value, Globals.Ynote);
            }
        }
    }
}