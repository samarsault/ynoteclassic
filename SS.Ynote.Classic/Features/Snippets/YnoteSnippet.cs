using FastColoredTextBoxNS;
using System.Collections.Generic;
using System.Xml;

namespace SS.Ynote.Classic.Features.Snippets
{
    public class YnoteSnippet
    {
        public string Value { get; set; }
        /// <summary>
        /// Get The AutoComplete Type
        /// </summary>
        public ApType AutoCompleteType { get; set; }

        static string GetSnippetFile(Language lang)
        {
            return string.Format(@"{0}Snippets\{1}.ynotesnippet", SettingsBase.SettingsDir, lang);
        }

        public static IEnumerable<YnoteSnippet> Read(Language lang)
        {
            return Read(GetSnippetFile(lang));
        }
        //<?xml version="1.0"?>
        //  <YnoteSnippet">
        //      <Snippet>for(int i=0;i &lt; ^;i++)</Snippet>
        //      <Keyword>Class</Keyword>
        //  </YnoteSnippet>
        private static IEnumerable<YnoteSnippet> Read(string file)
        {
            IList<YnoteSnippet> lst = new List<YnoteSnippet>();
            using (var reader = XmlReader.Create(file))
            {
                while (reader.Read())
                    if (reader.IsStartElement())
                    {
                        if (reader.Name == "Snippet")
                        {
                            if (reader.Read())
                            {
                                var snippet = new YnoteSnippet
                                {
                                    Value = reader.Value.Replace(@"\r\n", "\r\n"),
                                    AutoCompleteType = ApType.Snippet
                                };
                                lst.Add(snippet);
                            }
                        }
                        else if (reader.Name == "Keyword")
                        {
                            if (reader.Read())
                            {
                                var snippet = new YnoteSnippet
                                {
                                    Value = reader.Value,
                                    AutoCompleteType = ApType.Keyword
                                };
                                lst.Add(snippet);
                            }
                        }
                    }
            }
            return lst;
        }

 
    }

    /// <summary>
    /// Getsh the AutoCompletion Type i.e keyword or snippet
    /// which are processed in different manners
    /// </summary>
    public enum ApType
    {
        Keyword,
        Snippet
    }
}