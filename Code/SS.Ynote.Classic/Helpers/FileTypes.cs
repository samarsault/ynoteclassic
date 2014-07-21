using System.Xml;
using System.Linq;
using System.Collections.Generic;
using SS.Ynote.Classic.Core.Settings;
using SS.Ynote.Classic.Core.Syntax;

internal static class FileTypes
{
    internal static IDictionary<IEnumerable<string>, string> FileTypesDictionary { get; private set; }

    internal static void BuildDictionary()
    {
        FileTypesDictionary = new Dictionary<IEnumerable<string>, string>();
        using (var reader = XmlReader.Create(GlobalSettings.SettingsDir + "Extensions.xml"))
        {
            while (reader.Read())
            {
                if (reader.IsStartElement() && reader.Name == "Key")
                    FileTypesDictionary.Add(reader["Extensions"].Split('|'), reader["Language"]);
            }
        }
    }

    internal static string GetLanguage(IDictionary<IEnumerable<string>, string> dic, string extension)
    {
        string lang = "Text";
        foreach (var item in dic)
        {
            if (item.Key.Contains(extension))
            {
                lang = item.Value;
                break;
            }
            foreach (
                var syntax in
                    SyntaxHighlighter.LoadedSyntaxes.Where(syntax => syntax.Extensions.Contains(extension)))
                lang = syntax.Name;
        }
        return lang;
    }
}