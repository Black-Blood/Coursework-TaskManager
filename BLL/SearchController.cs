using DAL;

namespace BLL;

public class SearchController
{
    private readonly Project _project;

    internal SearchController(Project project)
    {
        _project = project;
    }

    public enum FilterTaskType
    {
        Done = 2,
        NotDone = 3,
        DeadlineLess = 4,
        DeadlineMore = 5,
    }

    public List<uint> SearchTaskByTeamMember(uint teamMemberID)
    {
        List<TaskTeamMember> result = _project.TaskTeamMembers.FindAll(
            (taskTeamMember) => taskTeamMember.TeamMemberID == teamMemberID
        );
        
        return result.ConvertAll((taskTeamMember) => taskTeamMember.TaskID);
    }

    public List<uint> SearchTeamMemberByTask(uint taskID)
    {
        List<TaskTeamMember> result = _project.TaskTeamMembers.FindAll(
            (taskTeamMember) => taskTeamMember.TaskID == taskID
        );

        return result.ConvertAll((taskTeamMember) => taskTeamMember.TeamMemberID);
    }

    public List<uint> FilterTaskBy(FilterTaskType type)
    {
        List<DAL.Task> res;
        switch (type)
        {
            case FilterTaskType.Done:
                res = _project.Tasks.FindAll(
                    (task) => task.Status.CurrentStatus == DAL.TaskStatus.StatusType.Done
                 );
                break;

            case FilterTaskType.NotDone:
                res = _project.Tasks.FindAll(
                    (task) => task.Status.CurrentStatus != DAL.TaskStatus.StatusType.Done
                 );
                break;

            case FilterTaskType.DeadlineLess:
                res = _project.Tasks.FindAll(
                    task => DateTime.Compare(task.Deadline, DateTime.Now) > 0
                );
                break;

            case FilterTaskType.DeadlineMore:
                res = _project.Tasks.FindAll(
                    task => DateTime.Compare(task.Deadline, DateTime.Now) <= 0
                );
                break;
            default:
                throw new Exception("something went wrong");
        }

        return res.ConvertAll(task => task.ID);
    }
}