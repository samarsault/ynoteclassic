using System;
using AutocompleteMenuNS;

/// <summary>
///     Fuzzy AutoComplete Menu for Commander and File Switcher
/// </summary>
public class CommandAutocompleteItem : AutocompleteItem
{
    public CommandAutocompleteItem(string text)
        : base(text)
    {
    }

    public override CompareResult Compare(string fragmentText)
    {
        if (fragmentText == Text)
            return CompareResult.VisibleAndSelected;
        if (Text.StartsWith(fragmentText, StringComparison.InvariantCultureIgnoreCase) &&
            Text != fragmentText)
            return CompareResult.VisibleAndSelected;
        var text = Text.Split(':')[1];
        if (text == fragmentText || text.StartsWith(fragmentText, StringComparison.InvariantCultureIgnoreCase))
            return CompareResult.VisibleAndSelected;
        return CompareResult.Hidden;
    }
}