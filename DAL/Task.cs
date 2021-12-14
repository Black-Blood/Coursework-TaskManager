using System.Text.RegularExpressions;

namespace DAL;

public class Task
{
    private uint _id;
    private string _title = "";
    private string _description = "";
    private string _deadline = "";
    private string _status = "";
    
    #region Regular Expressions
    public static string regTitle = "";
    public static string regDescription = "";
    public static string regDeadline = "";
    public static string regStatus = "";
    #endregion

    #region Properties
    public uint ID
    {
        get => _id;
        set => _id = value;
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
    public string Deadline
    {
        get => _deadline;
        set => _deadline = Regex.IsMatch(value, regDeadline) ? value : throw new Exception();
    }
    public string Status
    {
        get => _status;
        set => _status = Regex.IsMatch(value, regStatus) ? value : throw new Exception();
    }
    #endregion  
}