using System;
using System.Linq;
using System.Windows.Forms;

internal static class HelperMethods
{
    /// <summary>
    ///     Determines whether the source string contains the specified value.
    /// </summary>
    /// <param name="source">The String to search.</param>
    /// <param name="value">The search criteria.</param>
    /// <param name="comparisonOptions">The string comparison options to use.</param>
    /// <returns>
    ///     <c>true</c> if the source contains the specified value; otherwise, <c>false</c>.
    /// </returns>
    public static bool Contains(this string source, string value, StringComparison comparisonOptions)
    {
        return source.IndexOf(value, comparisonOptions) >= 0;
    }

    /// <summary>
    ///     Get Toolstripmenuitem by Name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static MenuItem GetMenuByName(this MenuItem parent, string name)
    {
        return parent.MenuItems.Cast<MenuItem>().FirstOrDefault(c => c.Text == name);
    }

    /// <summary>
    ///     Return string to Enum (extension)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="str"></param>
    /// <returns></returns>
    public static T ToEnum<T>(this string str)
    {
        return ((T)Enum.Parse(typeof(T), str));
    }

    /// <summary>
    ///     Return String To Int (extension)
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int ToInt(this string str)
    {
        return Convert.ToInt32(str);
    }
}