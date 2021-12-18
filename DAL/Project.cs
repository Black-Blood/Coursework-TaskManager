using System.Text.RegularExpressions;

namespace DAL;

public class Project
{
    public delegate void RegisterChanged();
    public event RegisterChanged? Notify;

    private readonly List<Task> _tasks = new();
    private readonly List<TeamMember> _teamMembers = new();
    private readonly List<TaskTeamMember> _taskTeamMembers = new();

    private string _name = "";
    private string _description = "";

    #region Regular Expressions
    public static string regName = ""; 
    public static string regDescription = "";
    #endregion

    #region Properties
    public string Name
    {
        get => _name;
        set => _name = Regex.IsMatch(value, regName) ? value : throw new Exception();
    }
    public string Description
    {
        get => _description;
        set => _description = Regex.IsMatch(value, regDescription) ? value : throw new Exception();
    }

    public List<Task> Tasks => _tasks;
    public List<TeamMember> TeamMembers => _teamMembers;
    public List<TaskTeamMember> TaskTeamMembers => _taskTeamMembers;
    #endregion
}