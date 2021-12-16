using DAL;
using System.Text.RegularExpressions;

namespace BLL;

public class ProjectController
{
    public enum Regime
    {
        Open,
        Create
    }

    private Project _project;
    private string _projectFilePath;
    private IDataProvider _provider;

    private Type[] _serializationTypes =
    {
        typeof(Project),
        typeof(DAL.Task),
        typeof(TeamMember),
    };

    public TaskController taskController;
    public TeamMemberController teamMemberController;
    public SearchController searchController;

    public ProjectController(string projectFilePath, Regime regime)
    {
        _provider = SelectProvider(projectFilePath);

        switch (regime)
        {
            case Regime.Open:
                OpenProject(projectFilePath);
                break;


            case Regime.Create:
                CreateProject(projectFilePath);
                break;

            default:
                throw new Exception();
        }

        taskController = new(_project);
        searchController = new(_project);
        teamMemberController = new(_project);

        _project.Notify += UpdateProjectFile;
    }

    private void OpenProject(string projectFilePath)
    {
        _projectFilePath = projectFilePath;
        _project = _provider.Read(projectFilePath, _serializationTypes) as Project ?? throw new Exception();
    }

    private void CreateProject(string projectFilePath)
    {
        _projectFilePath = projectFilePath;
        _project = new Project();
    }

    private void UpdateProjectFile()
    {
        _provider.Write(_projectFilePath, _project, _serializationTypes);
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
