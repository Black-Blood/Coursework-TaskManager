using System.Text.RegularExpressions;

namespace DAL;

public class Task
{
    public static string regID = "";
    public static string regTitle = "";
    public static string regDescription = "";
    public static string regDeadline = "";

    private uint _id;
    private string _title = "";
    private string _description = "";
    private string _deadline = "";

    public uint ID
    {
        get => _id;
        set => _id = Regex.IsMatch(value.ToString(), regID) ? value : throw new Exception();
    }

    public string Title
    {
        get => _title;
        set => _title = Regex.IsMatch(value, regTitle) ? value : throw new Exception();
    }

    public string Description
    {
        get => _description;
        set => _description = Regex.IsMatch(value, regDescription) ? value : throw new Exception();
    }

}