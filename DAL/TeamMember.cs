using System.Text.RegularExpressions;

namespace DAL;

public class TeamMember
{
    private uint _id;
    private string _firstName = "";
    private string _lastName = "";
    private string _email = "";

    #region Regular Expressions
    public static string regFirstName = "";
    public static string regLastName = "";
    public static string regEmail = "";
    #endregion

    #region Properties
    public uint ID
    {
        get => _id;
        set => _id = value;
    }
    public string FirstName
    {
        get => _firstName;
        set => _firstName = Regex.IsMatch(value, regFirstName) ? value : throw new Exception();
    }
    public string LastName
    {
        get => _lastName;
        set => _lastName = Regex.IsMatch(value, regLastName) ? value : throw new Exception();
    }
    public string Email
    {
        get => _email;
        set => _email = Regex.IsMatch(value, regEmail) ? value : throw new Exception();
    }
    #endregion  
}