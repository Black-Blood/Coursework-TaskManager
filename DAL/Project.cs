namespace DAL;

public class Project
{
    public delegate void RegisterChanged();
    public event RegisterChanged? Notify;

    private readonly List<Task> _tasks = new();
    private readonly List<TeamMember> _teamMembers = new();
    private readonly List<ProjectDependency> _projectDependency = new();

    #region Create
    public void AddTask(Task newTask)
    {
        if (_tasks.Exists(task => task.ID == newTask.ID)) throw new Exception();

        _tasks.Add(newTask);

        Notify?.Invoke();
    }
    public void AddTeamMemberStatus(TeamMember newTeamMembers)
    {
        if (_teamMembers.Exists(taskStatus => taskStatus.ID == newTeamMembers.ID)) throw new Exception();

        _teamMembers.Add(newTeamMembers);

        Notify?.Invoke();
    }
    public void AddProjectDependency(uint taskID, uint teamMemberID)
    {
        if (_projectDependency.Exists(dependency => dependency.taskID == taskID && dependency.teamMemberID == teamMemberID))
            throw new Exception();

        _projectDependency.Add(new ProjectDependency(taskID, teamMemberID));

        Notify?.Invoke();
    }
    #endregion

    #region Read
    public List<Task> Tasks => _tasks;
    public List<TeamMember> TeamMembers => _teamMembers;
    public List<ProjectDependency> ProjectDependencys => _projectDependency;
    #endregion

    #region Update
    public void EditTask(Task updatedTask)
    {
        Task? task = _tasks.Find(task => task.ID == updatedTask.ID);

        if (task is null) throw new Exception();

        _tasks.Remove(task);
        _tasks.Add(updatedTask);

        Notify?.Invoke();
    }

    public void EditTeamMember(TeamMember updatedTaeamMember)
    {
        TeamMember? teamMembar = _teamMembers.Find(teamMember => teamMember.ID == updatedTaeamMember.ID);

        if (teamMembar is null) throw new Exception();

        _teamMembers.Remove(teamMembar);
        _teamMembers.Add(teamMembar);

        Notify?.Invoke();
    }

    #endregion

    #region Delete
    public void RemoveTask(Task task)
    {
        ProjectDependency? record = _projectDependency.Find(record => record.taskID == task.ID);
        if (record is not null) _projectDependency.Remove(record);

        _tasks.Remove(task);

        Notify?.Invoke();
    }
    public void RemoveMemberTask(TeamMember teamMember)
    {
        ProjectDependency? record = _projectDependency.Find(record => record.teamMemberID == teamMember.ID);
        if (record is not null) _projectDependency.Remove(record);

        _teamMembers.Remove(teamMember);

        Notify?.Invoke();
    }
    public void RemoveProjectDependency(uint taskID, uint teamMemberID)
    {
        if (!_projectDependency.Exists(dependency => dependency.taskID == taskID && dependency.teamMemberID == teamMemberID))
            throw new Exception();

        _projectDependency.Remove(new ProjectDependency(taskID, teamMemberID));

        Notify?.Invoke();
    }
    #endregion
}

public class ProjectDependency
{
    public uint taskID;
    public uint teamMemberID;

    public ProjectDependency(uint taskID, uint teamMemberID)
    {
        this.taskID = taskID;
        this.teamMemberID = teamMemberID;
    }
}