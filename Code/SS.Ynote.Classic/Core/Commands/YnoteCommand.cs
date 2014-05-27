using System;
using System.Windows.Forms;

public class YnoteCommand
{
    /// <summary>
    ///     Key of the Command
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    ///     Value of the Command
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    ///     Creates a YnoteCommand from String
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public static YnoteCommand FromString(string command)
    {
        return Parse(command);
    }

    private static YnoteCommand Parse(string command)
    {
        try
        {
            var cmd = new YnoteCommand();
            var l = command.IndexOf(":");
            if (l > 0)
                cmd.Key = command.Substring(0, l);
            var result = command.Substring(command.LastIndexOf(':') + 1);
            cmd.Value = result;
            return cmd;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Parse Error : " + ex.Message, "Error");
            return null;
        }
    }
}