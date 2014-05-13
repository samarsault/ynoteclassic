using System;
using AutocompleteMenuNS;

/// <summary>
///     Fuzzy AutoComplete Menu for Commander and File Switcher
/// </summary>
public class FuzzyAutoCompleteItem : AutocompleteItem
{
    public FuzzyAutoCompleteItem(string text)
        : base(text)
    {
    }

    public override CompareResult Compare(string fragmentText)
    {
        if (fragmentText == Text)
            return CompareResult.Visible;
        var lev = Levenshtein(Text, fragmentText);
        if (lev > 0.5)
            return CompareResult.VisibleAndSelected;
        if (lev > 0.125)
            return CompareResult.Visible;
        if (Text.StartsWith(fragmentText, StringComparison.InvariantCultureIgnoreCase) &&
            Text != fragmentText)
            return CompareResult.VisibleAndSelected;
        if (string.IsNullOrEmpty(fragmentText))
            return CompareResult.Visible;
        return CompareResult.Hidden;
    }

    public static float Levenshtein(string src, string dest)
    {
        int[,] d = new int[src.Length + 1, dest.Length + 1];
        int i, j, cost;
        char[] str1 = src.ToCharArray();
        char[] str2 = dest.ToCharArray();

        for (i = 0; i <= str1.Length; i++)
        {
            d[i, 0] = i;
        }
        for (j = 0; j <= str2.Length; j++)
        {
            d[0, j] = j;
        }
        for (i = 1; i <= str1.Length; i++)
        {
            for (j = 1; j <= str2.Length; j++)
            {
                if (str1[i - 1] == str2[j - 1])
                    cost = 0;
                else
                    cost = 1;

                d[i, j] =
                    Math.Min(
                        d[i - 1, j] + 1, // Deletion
                        Math.Min(
                            d[i, j - 1] + 1, // Insertion
                            d[i - 1, j - 1] + cost)); // Substitution

                if ((i > 1) && (j > 1) && (str1[i - 1] ==
                                           str2[j - 2]) && (str1[i - 2] == str2[j - 1]))
                {
                    d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + cost);
                }
            }
        }

        var dist = (float) d[str1.Length, str2.Length];

        return 1 - dist/Math.Max(str1.Length, str2.Length);
    }
}