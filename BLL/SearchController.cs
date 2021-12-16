using DAL;

public class SearchController
{
    private Project _project;

    public SearchController(Project project)
    {
        _project = project;
    }

    public enum SearchTeamMemberBy
    {
        FirstName,
        LastName,
        Email,
        ID,
        All
    }

    public enum SearchTaskBy
    {
        Name,
        Deadline,
        Description,
        ID,
        All
    }

    public List<TeamMember> SearchTeamMember(string searchString, SearchTeamMemberBy searchType)
    {
        switch (searchType)
        {
            case SearchTeamMemberBy.FirstName:
                return _project.TeamMembers.FindAll(
                    teamMember =>
                        teamMember.FirstName.Contains(searchString) ||
                        searchString.Contains(teamMember.FirstName)
                );

            case SearchTeamMemberBy.LastName:
                return _project.TeamMembers.FindAll(
                    teamMember =>
                        teamMember.LastName.Contains(searchString) ||
                        searchString.Contains(teamMember.LastName)
                );

            case SearchTeamMemberBy.Email:
                return _project.TeamMembers.FindAll(
                    teamMember =>
                        teamMember.Email.Contains(searchString) ||
                        searchString.Contains(teamMember.Email)
                );

            case SearchTeamMemberBy.ID:
                return _project.TeamMembers.FindAll(
                    teamMember =>
                       teamMember.ID.ToString().Contains(searchString) ||
                       searchString.Contains(teamMember.ID.ToString())
                );

            case SearchTeamMemberBy.All:
                List<TeamMember> result = new();

                result.AddRange(SearchTeamMember(searchString, SearchTeamMemberBy.FirstName));
                result.AddRange(SearchTeamMember(searchString, SearchTeamMemberBy.LastName));
                result.AddRange(SearchTeamMember(searchString, SearchTeamMemberBy.Email));
                result.AddRange(SearchTeamMember(searchString, SearchTeamMemberBy.ID));

                return result;

        }

        throw new Exception();
    }

    public List<DAL.Task> SearchTask(string searchString, SearchTaskBy searchType)
    {
        switch (searchType)
        {
            case SearchTaskBy.Name:
                return _project.Tasks.FindAll(
                    task =>
                        task.Title.Contains(searchString) ||
                        searchString.Contains(task.Title)
                );

            case SearchTaskBy.Deadline:
                return _project.Tasks.FindAll(
                    task =>
                        task.Deadline.Contains(searchString) ||
                        searchString.Contains(task.Deadline)
                );

            case SearchTaskBy.Description:
                return _project.Tasks.FindAll(
                    task =>
                        task.Description.Contains(searchString) ||
                        searchString.Contains(task.Description)
                );

            case SearchTaskBy.ID:
                return _project.Tasks.FindAll(
                    task =>
                       task.ID.ToString().Contains(searchString) ||
                       searchString.Contains(task.ID.ToString())
                );

            case SearchTaskBy.All:
                List<DAL.Task> result = new();

                result.AddRange(SearchTask(searchString, SearchTaskBy.Name));
                result.AddRange(SearchTask(searchString, SearchTaskBy.Deadline));
                result.AddRange(SearchTask(searchString, SearchTaskBy.Description));
                result.AddRange(SearchTask(searchString, SearchTaskBy.ID));

                return result;
        }

        throw new Exception();
    }

    public List<TaskTeamMember> SearchAssignedTasks(uint teamMemberID)
    {
        return _project.TaskTeamMembers.FindAll(
            assigned => assigned.teamMemberID == teamMemberID
        );
    }

    public List<TaskTeamMember> SearchAssignedTeamMembers(uint taskID)
    {
        return _project.TaskTeamMembers.FindAll(
            assigned => assigned.taskID == taskID
        );
    }
}