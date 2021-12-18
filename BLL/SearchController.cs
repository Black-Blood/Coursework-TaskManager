using DAL;

namespace BLL;

public class SearchController
{
    private Project _project;

    internal SearchController(Project project)
    {
        _project = project;
    }

    public enum SearchTeamMemberBy
    {
        All = 0,
        ID = 1,
        Email = 2,
        Name = 3
    }

    public enum SearchTaskBy
    {
        Title,
        Description,
        ID,
        Status,
        Deadline,
        All
    }

    public List<TeamMemberView> SearchTeamMember(string searchString, SearchTeamMemberBy searchType)
    {
        List<TeamMember> teamMembers = new();

        switch (searchType)
        {
            case SearchTeamMemberBy.Name:
                teamMembers = _project.TeamMembers.FindAll(
                    teamMember =>
                        (teamMember.FirstName + " " + teamMember.LastName).Contains(searchString)
                );
                break;


            case SearchTeamMemberBy.Email:
                teamMembers = _project.TeamMembers.FindAll(
                    teamMember =>
                        teamMember.Email.Contains(searchString) ||
                        searchString.Contains(teamMember.Email)
                );
                break;

            case SearchTeamMemberBy.ID:
                teamMembers = _project.TeamMembers.FindAll(
                    teamMember =>
                       teamMember.ID.ToString().Contains(searchString) ||
                       searchString.Contains(teamMember.ID.ToString())
                );
                break;

            case SearchTeamMemberBy.All:
                List<TeamMemberView> result = new();

                result.AddRange(SearchTeamMember(searchString, SearchTeamMemberBy.Name));
                result.AddRange(SearchTeamMember(searchString, SearchTeamMemberBy.Email));
                result.AddRange(SearchTeamMember(searchString, SearchTeamMemberBy.ID));

                return result.Distinct().ToList();
        }

        return teamMembers.ConvertAll(teamMember => new TeamMemberView(teamMember));
    }

    public List<TaskView> SearchTask(string searchString, SearchTaskBy searchType)
    {
        List<DAL.Task> tasks = new();

        switch (searchType)
        {
            case SearchTaskBy.Title:
                tasks = _project.Tasks.FindAll(
                    task =>
                        task.Title.Contains(searchString) ||
                        searchString.Contains(task.Title)
                );
                break;

            case SearchTaskBy.Description:
                tasks = _project.Tasks.FindAll(
                    task =>
                        task.Description.Contains(searchString) ||
                        searchString.Contains(task.Description)
                );
                break;

            case SearchTaskBy.ID:
                tasks = _project.Tasks.FindAll(
                    task =>
                       task.ID.ToString().Contains(searchString) ||
                       searchString.Contains(task.ID.ToString())
                );
                break;

            case SearchTaskBy.Deadline:
                tasks = _project.Tasks.FindAll(
                    task =>
                       task.Deadline.ToString().Contains(searchString) ||
                       searchString.Contains(task.Deadline.ToString())
                );
                break;

            case SearchTaskBy.Status:
                tasks = _project.Tasks.FindAll(
                    task =>
                        task.Status.ToString().Contains(searchString)
                );
                break;

            case SearchTaskBy.All:
                List<TaskView> result = new();

                result.AddRange(SearchTask(searchString, SearchTaskBy.Title));
                result.AddRange(SearchTask(searchString, SearchTaskBy.Deadline));
                result.AddRange(SearchTask(searchString, SearchTaskBy.Description));
                result.AddRange(SearchTask(searchString, SearchTaskBy.ID));

                return result.Distinct().ToList();
        }

        return tasks.ConvertAll(task => new TaskView(task));
    }
}