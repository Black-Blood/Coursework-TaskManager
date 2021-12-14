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
        if (_tasks.Exists(task => task.ID == newTask.ID)) throw new Exception("This task already exists!");

        _tasks.Add(newTask);
    }
    public void AddTeamMemberStatus(TeamMember newTeamMembers)
    {
        if (_teamMembers.Exists(taskStatus => taskStatus.ID == newTeamMembers.ID)) throw new Exception("This task status already exists!");

        _teamMembers.Add(newTeamMembers);
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

        if (task is null) throw new Exception("This task dosn't exists!");

        _tasks.Remove(task);
        _tasks.Add(updatedTask);
    }

    public void EditTeamMember(TeamMember updatedTaeamMember)
    {
        TeamMember? teamMembar = _teamMembers.Find(teamMember => teamMember.ID == updatedTaeamMember.ID);

        if (teamMembar is null) throw new Exception("This team membar dosn't exists!");

        _teamMembers.Remove(teamMembar);
        _teamMembers.Add(teamMembar);
    }

    #endregion

    #region Delete
    public void RemoveTask(Task task)
    {
        ProjectDependency? record = _projectDependency.Find(record => record.taskID == task.ID);
        if (record is not null) _projectDependency.Remove(record);

        _tasks.Remove(task);
    }
    public void RemoveMemberTask(TeamMember teamMember)
    {
        ProjectDependency? record = _projectDependency.Find(record => record.teamMemberID == teamMember.ID);
        if (record is not null) _projectDependency.Remove(record);

        _teamMembers.Remove(teamMember);
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