using DAL;
using System.Text.RegularExpressions;

namespace BLL;

public class ProjectController
{
    private readonly string _projectFilePath;
    private readonly Project _project;
    private readonly IDataProvider _provider;

    public readonly TaskController taskController;
    public readonly TeamMemberController teamMemberController;
    public readonly SearchController searchController;

    public ProjectController(string projectFilePath)
    {
        _provider = SelectProvider(projectFilePath);

        _projectFilePath = projectFilePath;

        _project = _provider.Read(projectFilePath) as Project ?? throw new Exception("Invalid document structure");

        _project.Notify += UpdateProjectFile;

        taskController = new(_project);
        searchController = new(_project);
        teamMemberController = new(_project);
    }

    public ProjectController(string projectFilePath, string projectName, string projectDescription)
    {
        _provider = SelectProvider(projectFilePath);

        _projectFilePath = projectFilePath;

        _project = new()
        {
            Name = projectName,
            Description = projectDescription
        };

        _provider.Write(_projectFilePath, _project);

        _project.Notify += UpdateProjectFile;

        taskController = new(_project);
        searchController = new(_project);
        teamMemberController = new(_project);
    }

    public ProjectView GetProjectInfo()
    {
        return new(_project);
    }

    public void SetExecutant(uint teamMemberID, uint taskID)
    {
        _project.AddTaskTeamMember(teamMemberID, taskID);
        UpdateProjectFile();
    }

    public void ChangeTaskStatus(uint taskID, string taskStatus)
    {
        DAL.Task? task = _project.Tasks.Find(x => x.ID == taskID);

        if (task is null) throw new Exception("No such task");

        task.Status.CurrentStatus = Enum.Parse<DAL.TaskStatus.StatusType>(taskStatus);

        _project.UpdateTask(task);
    }

    public List<LoadOfExecutant> CheckLoadOfExecutants()
    {
        List<LoadOfExecutant> result = new();

        foreach (TeamMember teamMember in _project.TeamMembers)
        {
            int count = 0;

            foreach (TaskTeamMember taskTeamMember in _project.TaskTeamMembers)
                if (taskTeamMember.TeamMemberID == teamMember.ID)
                    count++;

            result.Add(new(teamMember, count));
        }

        return result;
    }


    private void UpdateProjectFile()
    {
        _provider.Write(_projectFilePath, _project);
    }

    private static IDataProvider SelectProvider(string fileName)
    {
        if (Regex.IsMatch(fileName, "^.*.json$"))
            return new JSONDataProvider();

        if (Regex.IsMatch(fileName, "^.*.xml$"))
            return new XMLDataProvider();

        if (Regex.IsMatch(fileName, "^.*.dat$"))
            return new BinaryDataProvider();

        throw new Exception("The file type is not supported!");
    }
}
