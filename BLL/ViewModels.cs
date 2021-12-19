using DAL;

namespace BLL;

public class ProjectView
{
    private readonly Project _project;

    internal ProjectView(Project project)
    {
        _project = project;
    }

    public string Name => _project.Name;
    public string Description => _project.Description;

    public string Progress => CalculateProgress();
    
    private string CalculateProgress()
    {
        int totalCountOfTasks = _project.Tasks.Count;
        int doneTasks = 0;

        if(totalCountOfTasks == 0) return "0%";

        foreach (var task in _project.Tasks)
            if(task.Status.CurrentStatus == DAL.TaskStatus.StatusType.Done)
                doneTasks++;

        return (doneTasks / totalCountOfTasks * 100).ToString() + "%";

    }
}

public class TaskView
{
    private readonly DAL.Task _task;

    internal TaskView(DAL.Task task)
    {
        _task = task;
    }

    public string ID => _task.ID.ToString();
    public string Title => _task.Title; 
    public string Description => _task.Description;
    public string Deadline => _task.Deadline.ToString() + " " + CheckDateTime();
    public string Status => _task.Status.CurrentStatus.ToString();

    private string CheckDateTime()
    {
        if (DateTime.Compare(_task.Deadline, DateTime.Now) <= 0)
            return "(Active)";

        return "(Pass)";
    }
}

public class TeamMemberView
{
    private readonly TeamMember _teamMember;

    internal TeamMemberView(TeamMember teamMember)
    {
        _teamMember = teamMember;
    }

    public string ID => _teamMember.ID.ToString();
    public string FirstName => _teamMember.FirstName;
    public string LastName => _teamMember.LastName;
    public string Email => _teamMember.Email;
}

public class LoadOfExecutant
{
    private readonly TeamMember _teamMember;
    private readonly int _count;

    internal LoadOfExecutant(TeamMember teamMember, int countTasks)
    {
        _teamMember = teamMember;
        _count = countTasks;

    }

    public string ID => _teamMember.ID.ToString();
    public string FullName => _teamMember.FirstName + _teamMember.LastName;
    public string Count => _count.ToString();
}
