using System.Text.RegularExpressions;

namespace DAL;

public class TaskStatus : StoreIndex
{
    private string _title = "";
    private string _description = "";


    #region Regular Expressions
    public static string regTitle = "";
    public static string regDescription = "";
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
    #endregion  
}