#define DEVBUILD

using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using SS.Ynote.Classic.UI;

namespace SS.Ynote.Classic.Core.Snippets
{
    public class YnoteSnippet
    {
        public string File { get; private set; }

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

        /// <summary>
        ///     The Scope of the Snippet
        /// </summary>
        public string Scope { get; set; }

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
               <description></description>
               <tabTrigger></tabTrigger>
               <content></content>
           </YnoteSnippet>
        */

        public static YnoteSnippet Read(string snippetfile)
        {
            var snippet = new YnoteSnippet();
            snippet.File = snippetfile;
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
                            case "scope":
                                snippet.Scope = reader.ReadElementContentAsString();
                                break;
                        }
                    }
                }
            }
            return snippet;
        }

        public string GetSubstitutedContent(Editor edit)
        {
            string content = Content.Replace("$selection", edit.Tb.SelectedText)
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
            return content;
        }
    }
}