using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using AutocompleteMenuNS;

/// <summary>
///     Fuzzy Auto Complete using LCS ( Longest Common Subsequence )
/// </summary>
public class FuzzyAutoCompleteItem : AutocompleteItem
{
    public FuzzyAutoCompleteItem(string text)
        : base(text)
    {
    }

    /// <summary>
    ///     LCS Algorithm
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <param name="index1"></param>
    /// <param name="index2"></param>
    /// <returns></returns>
    private static int LCS(string a, string b, int index1, int index2)
    {
        int max = 0;
        if (index1 == a.Length)
            return 0;
        if (index2 == b.Length)
            return 0;
        for (int i = index1; i < a.Length; i++)
        {
            int exist = b.IndexOf(a[i], index2);
            if (exist != -1)
            {
                // found char
                int x = 1 + LCS(a, b, i + 1, exist + 1);
                if (max < x)
                {
                    max = x;
                }
            }
        }
        return max;
    }

    public override CompareResult Compare(string fragmentText)
    {
        int max = 0;
        int index = 0;
        foreach (var item in Parent.Items)
        {
            int x = LCS(fragmentText.ToLower(), item.ToLower(), 0, 0);
            if (x > max)
            {
                max = x;
                index = Array.IndexOf(Parent.Items, item);
            }
        }
        if (Text == Parent.Items[index])
            return CompareResult.VisibleAndSelected;
        if (Text.Contains(fragmentText))
            return CompareResult.Visible;
        return CompareResult.Visible;
    }

}