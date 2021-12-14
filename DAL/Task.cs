using System.Text.RegularExpressions;

namespace DAL;

public class Task : StoreIndex
{
    private string _title = "";
    private string _description = "";
    private string _deadline = "";

    
    #region Regular Expressions
    public static string regTitle = "";
    public static string regDescription = "";
    public static string regDeadline = "";
    #endregion  


    #region Properties
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
    #endregion  
}