#define DEVBUILD

using System.IO;
using System.Windows.Forms;
using System.Xml;
using FastColoredTextBoxNS;
using SS.Ynote.Classic.UI;

namespace SS.Ynote.Classic.Features.Snippets
{
    public class YnoteSnippet
    {
        /// <summary>
        ///     Description of the Snippet
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Tab Trigger of the Snippet
        /// </summary>
        public string Tab { get; set; }

        /// <summary>
        ///     Content of the Snippet
        /// </summary>
        public string Content { get; set; }

        /* YnoteSnippet File Documentation
           ----------------------
           Functions
           ----------------------
           $file_name - File Name
           $file_name_extension - File Name with Extension
           $current_line - Text of Current line
           $selection - SelectedText
           ^ - Caret Position
        -----------------------
          Snippet File Structure
         ----------------------
          <?xml version="1.0"?>
           <YnoteSnippet Version="1.0">
               <name></name>
               <description></description>
               <tabTrigger></tabTrigger>
               <content></content>
           </YnoteSnippet>
        */

        public static string GetDirectory(Language lang)
        {
            return Settings.SettingsDir + "Snippets\\" + lang;
        }

        public static YnoteSnippet Read(string snippetfile)
        {
            var snippet = new YnoteSnippet();
            using (var reader = XmlReader.Create(snippetfile))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "description":
                                snippet.Description = reader.ReadElementContentAsString();
                                break;
                            case "tabTrigger":
                                snippet.Tab = reader.ReadElementContentAsString();
                                break;
                            case "content":
                                snippet.Content = reader.ReadElementContentAsString();
                                break;
                        }
                    }
                }
            }
            return snippet;
        }

        public void SubstituteContent(Editor edit)
        {
            Content = Content.Replace("$selection", edit.Tb.SelectedText)
                .Replace("$current_line", edit.Tb[edit.Tb.Selection.Start.iLine].Text)
                .Replace("$file_name_extension", edit.Text)
                .Replace("$file_name", Path.GetFileNameWithoutExtension(edit.Text))
                .Replace("$eol", "\r\n").Replace("$clipboard", Clipboard.GetText());
            if (Content.Contains("$choose_file"))
            {
                using (var dlg = new OpenFileDialog())
                {
                    var result = dlg.ShowDialog();
                    if (result == DialogResult.OK)
                        Content = Content.Replace("$choose_file", dlg.FileName);
                }
            }
        }
    }
}