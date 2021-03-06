using System.Text.RegularExpressions;

namespace DAL;

public class Task
{
    private uint _id;
    private string _title = "";
    private string _description = "";
    private DateTime _deadline = DateTime.Now;
    private TaskStatus _status = new();

    #region Regular Expressions
    private readonly static string regTitle = @"^[a-zA-Z0-9 ]{3,20}$";
    private readonly static string regDescription = "^.{3,50}$$";
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
        set => _title = Regex.IsMatch(value, regTitle) ? value : throw new Exception("Invalid symbols");
    }
    public string Description
    {
        get => _description;
        set => _description = Regex.IsMatch(value, regDescription) ? value : throw new Exception("Invalid symbols");
    }
    public DateTime Deadline
    {
        get => _deadline;
        set => _deadline = value;
    }
    public TaskStatus Status
    {
        get => _status;
        set => _status = value;
    }
    #endregion  
}