using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using SS.Ynote.Classic.Core.Settings;

namespace SS.Ynote.Classic.Extensibility.Packages
{
    internal static class YnotePackage
    {
        internal static IDictionary<string, string> GenerateDictionary(string manifest)
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();
            var lines = File.ReadAllLines(manifest);
            foreach (var line in lines)
            {
                var command = YnoteCommand.FromString(line);
                command.Value = command.Value.Replace("$ynotedata", GlobalSettings.SettingsDir);
                if (command.Value.IndexOf("$ynotedir") != -1)
                    command.Value = command.Value.Replace("$ynotedir", Application.StartupPath);
                dic.Add(command.Key, command.Value);
            }
            return dic;
        }
    }
}