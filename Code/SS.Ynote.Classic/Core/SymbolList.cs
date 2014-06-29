using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using AutocompleteMenuNS;
using Newtonsoft.Json;
using SS.Ynote.Classic.Core.Settings;
using SS.Ynote.Classic.UI;

namespace SS.Ynote.Classic.Core
{
    static class SymbolList
    {
        private static Dictionary<string, Regex> dic;

        public static IEnumerable<AutocompleteItem> GetPlaces(Editor edit)
        {
            if (dic == null)
            {
                string json = File.ReadAllText(Path.Combine(GlobalSettings.SettingsDir, "Symbols.json"));
                dic = JsonConvert.DeserializeObject<Dictionary<string, Regex>>(json);
            }
            var lst = new List<AutocompleteItem>();
            Regex re;
            dic.TryGetValue(edit.Tb.Language, out re);
            if (re == null)
                return null;
            var matches = re.Matches(edit.Tb.Text);
            foreach (Match match in matches)
            {
                var symbol = match.Value;
                symbol = symbol.Substring(symbol.LastIndexOf(' ')).Trim();
                lst.Add(new FuzzyAutoCompleteItem(symbol) {Tag = match.Index});
            }
            return lst;
        }
    }
}