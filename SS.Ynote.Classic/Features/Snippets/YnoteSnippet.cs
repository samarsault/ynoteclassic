using System.Collections.Generic;
using System.Xml;
using FastColoredTextBoxNS;

namespace SS.Ynote.Classic.Features.Snippets
{
    public class YnoteSnippet
    {
        private string Value { get; set; }

        private static string GetSnippetFile(Language lang)
        {
            return string.Format(@"{0}Snippets\{1}.ynotesnippet", SettingsBase.SettingsDir, lang);
        }

        public static IEnumerable<YnoteSnippet> Read(Language lang)
        {
            return Read(GetSnippetFile(lang));
        }

        //<?xml version="1.0"?>
        //  <YnoteSnippets Language="C#">
        //      <Snippet>for(int i=0;i < ^;i++)</Snippet>
        //  </YnoteSnippets>
        public static IEnumerable<YnoteSnippet> Read(string file)
        {
            IList<YnoteSnippet> lst = new List<YnoteSnippet>();
            using (var reader = XmlReader.Create(file))
            {
                while (reader.Read())
                    if (reader.IsStartElement())
                        if (reader.Name == "Snippet")
                        {
                            var snipp = new YnoteSnippet
                            {
                                Value = reader.Value.Replace(@"\r\n", "\r\n")
                            };
                            lst.Add(snipp);
                            if (reader.Read())
                            {
                                var snippet = new YnoteSnippet
                                {
                                    Value = reader.Value.Replace(@"\r\n", "\r\n")
                                };
                                lst.Add(snippet);
                            }
                        }
            }
            return lst;
        }

        public SnippetAutocompleteItem ToAutoCompleteItem()
        {
            return new SnippetAutocompleteItem(Value);
        }
    }
}