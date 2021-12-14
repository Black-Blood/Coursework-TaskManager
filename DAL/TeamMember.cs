using System.Text.RegularExpressions;

namespace DAL;

public class TeamMember: StoreIndex
{
    private string _firstName = "";
    private string _lastName = "";
    private string _email = "";


    #region Regular Expressions
    public static string regFirstName = "";
    public static string regLastName = "";
    public static string regEmail = "";
    #endregion  


    #region Properties
    public string FirstName
    {
        get => _firstName;
        set => _firstName = Regex.IsMatch(value, regFirstName) ? value : throw new Exception();
    }

    public string Description
    {
        get => _lastName;
        set => _lastName = Regex.IsMatch(value, regLastName) ? value : throw new Exception();
    }
    
    public string Deadline
    {
        get => _email;
        set => _email = Regex.IsMatch(value, regEmail) ? value : throw new Exception();
    }
    #endregion  
}