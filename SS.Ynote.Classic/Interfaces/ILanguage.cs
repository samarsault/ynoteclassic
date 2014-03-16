using System;
using FastColoredTextBoxNS;

namespace SS.Ynote.Classic.Interfaces
{
    // TODO: Implement ILanguage
    interface ILanguage
    {
        void Highlight(Range r);
        string[] Extensions { get; set; }
        AutocompleteItem[] AutoCompletions { get; set; }
        void DO()
        {
            if(bool){
                print("Hello, World!");
            }
        }
    }
}
