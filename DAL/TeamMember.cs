using System.Text.RegularExpressions;

namespace DAL;

public class TeamMember
{
    private uint _id;
    private string _firstName = "";
    private string _lastName = "";
    private string _email = "";

    #region Regular Expressions
    private readonly static string _regFirstName = @"^[a-zA-Z]{3,10}$";
    private readonly static string _regLastName = @"^[a-zA-Z]{3,10}$";
    private readonly static string _regEmail = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
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
        set => _firstName = Regex.IsMatch(value, _regFirstName) ? value : throw new Exception("Invalid symbols");
    }
    public string LastName
    {
        get => _lastName;
        set => _lastName = Regex.IsMatch(value, _regLastName) ? value : throw new Exception("Invalid symbols");
    }
    public string Email
    {
        get => _email;
        set => _email = Regex.IsMatch(value, _regEmail) ? value : throw new Exception("Invalid symbols");
    }
    #endregion  
}