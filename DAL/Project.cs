using System.Text.RegularExpressions;

namespace DAL;

public class Project
{
    public delegate void RegisterChanged();
    public event RegisterChanged? Notify;

    private readonly List<Task> _tasks = new();
    private readonly List<TeamMember> _teamMembers = new();
    private readonly List<TaskTeamMember> _taskTeamMembers = new();

    private string _name = @"^[a-zA-Z0-9 ]{3,20}$";
    private string _description = "^[~!@#$%^&*()_+'\"\\-/=a-zA-Z0-9]{3,50}$";

    #region Regular Expressions
    private readonly static string _regName = "";
    private readonly static string _regDescription = "";
    #endregion

    #region Properties
    public string Name
    {
        get => _name;
        set => _name = Regex.IsMatch(value, _regName) ? value : throw new Exception("Invalid symbols");
    }
    public string Description
    {
        get => _description;
        set => _description = Regex.IsMatch(value, _regDescription) ? value : throw new Exception("Invalid symbols");
    }
    #endregion


    #region Create
    public void AddTask(Task newTask)
    {
        if (_tasks.Exists(task => task.ID == newTask.ID)) throw new Exception("This task already exists");

        _tasks.Add(newTask);

        Notify?.Invoke();
    }
    public void AddTeamMember(TeamMember newTeamMembers)
    {
        if (_teamMembers.Exists(taskStatus => taskStatus.ID == newTeamMembers.ID)) throw new Exception("This team member already exists");

        _teamMembers.Add(newTeamMembers);

        Notify?.Invoke();
    }
    public void AddTaskTeamMember(uint taskID, uint teamMemberID)
    {
        if (_taskTeamMembers.Exists(dependency => dependency.TaskID == taskID && dependency.TeamMemberID == teamMemberID))
            throw new Exception("This 'task - team member' dependency already exists");

        _taskTeamMembers.Add(new TaskTeamMember(taskID, teamMemberID));

        Notify?.Invoke();
    }
    #endregion

    #region Read
    public List<Task> Tasks => _tasks;
    public List<TeamMember> TeamMembers => _teamMembers;
    public List<TaskTeamMember> TaskTeamMembers => _taskTeamMembers;
    #endregion

    #region Update
    public void UpdateTask(Task updatedTask)
    {
        Task? task = _tasks.Find(task => task.ID == updatedTask.ID);

        if (task is null) throw new Exception("This task doesn't exists");

        _tasks.Remove(task);
        _tasks.Add(updatedTask);

        Notify?.Invoke();
    }

    public void UpdateTeamMember(TeamMember updatedTaeamMember)
    {
        TeamMember? teamMembar = _teamMembers.Find(teamMember => teamMember.ID == updatedTaeamMember.ID);

        if (teamMembar is null) throw new Exception("This team member doesn't exists");

        _teamMembers.Remove(teamMembar);
        _teamMembers.Add(teamMembar);

        Notify?.Invoke();
    }
    #endregion

    #region Delete
    public void RemoveTask(Task task)
    {
        TaskTeamMember? record = _taskTeamMembers.Find(record => record.TaskID == task.ID);
        if (record is not null) _taskTeamMembers.Remove(record);

        _tasks.Remove(task);

        Notify?.Invoke();
    }
    public void RemoveMember(TeamMember teamMember)
    {
        TaskTeamMember? record = _taskTeamMembers.Find(record => record.TeamMemberID == teamMember.ID);
        if (record is not null) _taskTeamMembers.Remove(record);

        _teamMembers.Remove(teamMember);

        Notify?.Invoke();
    }
    public void RemoveTaskTeamMember(uint taskID, uint teamMemberID)
    {
        if (!_taskTeamMembers.Exists(dependency => dependency.TaskID == taskID && dependency.TeamMemberID == teamMemberID))
            throw new Exception("This 'task - team member' dependency doesn't exists");

        _taskTeamMembers.Remove(new TaskTeamMember(taskID, teamMemberID));

        Notify?.Invoke();
    }
    #endregion
}