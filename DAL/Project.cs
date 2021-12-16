﻿namespace DAL;

public class Project
{
    public delegate void RegisterChanged();
    public event RegisterChanged? Notify;

    private readonly List<Task> _tasks = new();
    private readonly List<TeamMember> _teamMembers = new();
    private readonly List<TaskTeamMember> _taskTeamMembers = new();

    #region Create
    public void AddTask(Task newTask)
    {
        if (_tasks.Exists(task => task.ID == newTask.ID)) throw new Exception();

        _tasks.Add(newTask);

        Notify?.Invoke();
    }
    public void AddTeamMember(TeamMember newTeamMembers)
    {
        if (_teamMembers.Exists(taskStatus => taskStatus.ID == newTeamMembers.ID)) throw new Exception();

        _teamMembers.Add(newTeamMembers);

        Notify?.Invoke();
    }
    public void AddTaskTeamMember(uint taskID, uint teamMemberID)
    {
        if (_taskTeamMembers.Exists(dependency => dependency.taskID == taskID && dependency.teamMemberID == teamMemberID))
            throw new Exception();

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

        if (task is null) throw new Exception();

        _tasks.Remove(task);
        _tasks.Add(updatedTask);

        Notify?.Invoke();
    }

    public void UpdateTeamMember(TeamMember updatedTaeamMember)
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
        TaskTeamMember? record = _taskTeamMembers.Find(record => record.taskID == task.ID);
        if (record is not null) _taskTeamMembers.Remove(record);

        _tasks.Remove(task);

        Notify?.Invoke();
    }
    public void RemoveMember(TeamMember teamMember)
    {
        TaskTeamMember? record = _taskTeamMembers.Find(record => record.teamMemberID == teamMember.ID);
        if (record is not null) _taskTeamMembers.Remove(record);

        _teamMembers.Remove(teamMember);

        Notify?.Invoke();
    }
    public void RemoveTaskTeamMember(uint taskID, uint teamMemberID)
    {
        if (!_taskTeamMembers.Exists(dependency => dependency.taskID == taskID && dependency.teamMemberID == teamMemberID))
            throw new Exception();

        _taskTeamMembers.Remove(new TaskTeamMember(taskID, teamMemberID));

        Notify?.Invoke();
    }
    #endregion
}