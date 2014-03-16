#region

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

#endregion

namespace SS.Ynote.Classic
{
    public class RunConfiguration
    {
        public string Name { get; private set; }
        public string Process { get; private set; }
        public string Arguments { get; private set; }
        public string CmdDir { get; set; }

        public static IEnumerable<string> GetConfigurations()
        {
            return Directory.GetFiles(SettingsBase.SettingsDir + @"RunScripts\").ToList();
        }

        public static RunConfiguration ToRunConfig(string file)
        {
            using (var reader = XmlReader.Create(file))
                while (reader.Read())
                    if (reader.IsStartElement())
                        if (reader.Name == "Config")
                        {
                            var config = new RunConfiguration
                            {
                                Name = reader["Name"],
                                Arguments = reader["Args"],
                                CmdDir = reader["CmdDir"],
                                Process = reader["Process"]
                            };
                            return config;
                        }
            return null;
        }

        public string GetPath()
        {
            return string.Format(@"{0}\User\RunScripts\{1}.run", Application.StartupPath, Name);
        }

        public void ProcessConfiguration(string filename)
        {
            Arguments = Arguments.Replace("$source", filename);
        }

        public void EditConfig(string proc, string args, string dir, string name)
        {
            var str =
                string.Format(
                    "<?xml version=\"1.0\"?>\r\n\t<YnoteRun>\r\n\t\t<Config Name=\"{3}\" Process=\"{0}\" Args=\"{1}\" Directory=\"{2}\"/>\r\n\t</YnoteRun>",
                    proc, args, dir, name);
            File.WriteAllText(GetPath(), str);
        }

        public string ToBatch()
        {
            if (CmdDir != "")
                return string.Format("@echo off\r\necho {0} Run Script\r\ncd {1}\r\n{2} {3}", Name, CmdDir, Process,
                    Arguments);
            return string.Format("@echo off\r\necho {0} Run Script\r\n{1} {2}", Name, Process, Arguments);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}