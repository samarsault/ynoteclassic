using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SS.Ynote.Classic.Features.Packages
{
    internal static class YnotePackage
    {
        internal static IDictionary<string, string> GenerateDictionary(string manifest)
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();
            var lines = File.ReadAllLines(manifest);
            foreach (
                var command in
                    lines.Select(
                        line =>
                            YnoteCommand.FromString(line.Replace("$ynotedata", Settings.SettingsDir)
                                .Replace("$ynotedir", Application.StartupPath))))
                dic.Add(command.Key, command.Value);
            //foreach (var line in lines)
            //{
            //    var command = Parse(line.Replace("$ynotedir", Application.StartupPath));
            //    dic.Add(command.Key, command.Value);
            //}
            return dic;
        }
    }
}