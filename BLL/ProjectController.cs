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

    public string ProjectName => _project.Name;

    public string ProjectDescription => _project.Description;

    public ProjectController(string projectFilePath)
    {
        _provider = SelectProvider(projectFilePath);

        _projectFilePath = projectFilePath;

        _project = _provider.Read(projectFilePath) as Project ?? throw new Exception();

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
