using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using FastColoredTextBoxNS;
using AutocompleteItem = AutocompleteMenuNS.AutocompleteItem;
using MethodAutocompleteItem = AutocompleteMenuNS.MethodAutocompleteItem;
using SnippetAutocompleteItem = AutocompleteMenuNS.SnippetAutocompleteItem;
/// <summary>
/// Ynote Snippet
/// </summary>
public class YnoteSnippet
{
    /// <summary>
    /// Item List
    /// </summary>
    public List<SnippetDesc> Items { get; set; }
    /// <summary>
    /// Default Constructor
    /// </summary>
    public YnoteSnippet()
    {
        Items = new List<SnippetDesc>();
    }
    /// <summary>
    /// Reads Snippet.xml File
    /// </summary>
    /// <param name="file"></param>
    public void ReadSnippet(string file)
    {
        using (var reader = XmlReader.Create(file))
        {
            while (reader.Read())
                // Only detect start elements.
                if (reader.IsStartElement())
                    // Get element name and switch on it.
                    if (reader.Name == "Snippet")
                    {
                        var type = reader["Type"];
                        var value = reader["Value"];
                        var snipdesc = new SnippetDesc(type, value);
                        Items.Add(snipdesc);
                        if (reader.Read())
                        {
                            var desc = new SnippetDesc(type, value);
                            Items.Add(desc);
                        }

                    }
        }
    }

    public IDictionary<Language, string> GenerateSnippetFileDictionary()
    {
         return Enum.GetValues(typeof (Language)).Cast<Language>().ToDictionary(lang => lang, lang => string.Format(@"{0}\User\Snippets\{1}.ynotesnippet", Application.StartupPath, lang.ToString())); 
    }

}
/// <summary>
/// Snippet Desc
/// </summary>
public class SnippetDesc
{
    /// <summary>
    /// Autocomplete Type
    /// </summary>
    public string AutoCompleteType { get; set; }
    /// <summary>
    /// Text
    /// </summary>
    public string Text { get; set; }
    /// <summary>
    /// Snippet Desc
    /// </summary>
    public SnippetDesc(string t, string s)
    {
        AutoCompleteType = t;
        Text = s;
    }
    public AutocompleteItem Gen()
    {
        switch (AutoCompleteType)
        {
            case "CodeSnippet":
                return new SnippetAutocompleteItem(Text);
                break;
            case "Keyword":
                return new AutocompleteItem(Text);
                break;
            case "DotSnippet":
                return new MethodAutocompleteItem(Text);
                break;
        }
        return null;
    }
}