using System.Windows.Forms;

public class YnoteCommand
{
    public YnoteCommand(string key, string value)
    {
        Key = key;
        Value = value;
    }

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
            var l = command.Split(':');
            return new YnoteCommand(l[0], l[1]);
        }
        catch
        {
            MessageBox.Show("Parse Error : ", "Error");
            return null;
        }
    }
}