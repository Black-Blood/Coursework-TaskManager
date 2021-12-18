using DAL;

namespace BLL;

public class ProjectView
{
    private Project _project;

    internal ProjectView(Project project)
    {
        _project = project;
    }

    public string Name => _project.Name;
    public string Description => _project.Description;
}

public class TaskView
{
    private DAL.Task _task;

    internal TaskView(DAL.Task task)
    {
        _task = task;
    }

    public string ID => _task.ID.ToString();
    public string Title => _task.Title; 
    public string Description => _task.Description;
    public string Deadline => _task.Deadline.ToString();
    public string Status => _task.Status.CurrentStatus.ToString();
}

public class TeamMemberView
{
    private TeamMember _teamMember;

    internal TeamMemberView(TeamMember teamMember)
    {
        _teamMember = teamMember;
    }

    public string ID => _teamMember.ID.ToString();
    public string FirstName => _teamMember.FirstName;
    public string LastName => _teamMember.LastName;
    public string Email => _teamMember.Email;
}