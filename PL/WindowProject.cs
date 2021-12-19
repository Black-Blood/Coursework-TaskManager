using BLL;
using System.Text.RegularExpressions;

namespace PL;

public static class WindowProject
{
    internal static ProjectController projectController;
    internal static TaskController taskController;
    internal static TeamMemberController teamMemberController;
    internal static SearchController searchController;

    internal static KeyValuePair<string, WindowManager.MenuMethod>[] MenuManageStartWorking =
    {
            new("Create project", CreateProject),
            new("Open project", OpenProject),
        };
    internal static KeyValuePair<string, WindowManager.MenuMethod>[] MenuManageProjects =
    {
        new("Manage team members", ManageTeamMembers),
        new("Manage tasks", ManageTasks),
        new("Manage search", ManageSearch),
        new("Set Executant", SetExecutant),
        new("Change Task Status", ChangeTaskStatus),
        new("Check Load Of Executants", CheckLoadOfExecutants),
    };

    public static void Start()
    {
        WindowManager.ShowTitle("START WORKING");
        WindowManager.SelectMenu(MenuManageStartWorking);
    }

    internal static void CreateProject()
    {
        try
        {

            WindowManager.ShowTitle("Create project");

            string filePath = WindowManager.GetData(
                message: "Type file path",
                helperText: "",
                (inputData) => Regex.IsMatch(inputData, "")
            );

            string name = WindowManager.GetData(
                message: "Type project name",
                helperText: "",
                (inputData) => Regex.IsMatch(inputData, "")
            );

            string description = WindowManager.GetData(
                message: "Type project description",
                helperText: "",
                (inputData) => Regex.IsMatch(inputData, "")
            );

            projectController = new(filePath, name, description);
            taskController = projectController.taskController;
            teamMemberController = projectController.teamMemberController;
            searchController = projectController.searchController;

            ManageProject();
        }
        catch (Exception ex)
        {
            WindowManager.Clear();
            WindowManager.ShowMessage(ex.Message, ConsoleColor.Red);
            Thread.Sleep(10000);
            CreateProject();
        }
    }

    internal static void OpenProject()
    {
        try
        {
            WindowManager.ShowTitle("Open project");

            string filePath = WindowManager.GetData(
                message: "Type file path",
                helperText: "",
                (inputData) => Regex.IsMatch(inputData, "")
            );

            projectController = new(filePath);
            taskController = projectController.taskController;
            teamMemberController = projectController.teamMemberController;
            searchController = projectController.searchController;
            ManageProject();

        }
        catch (Exception ex)
        {
            WindowManager.Clear();
            WindowManager.ShowMessage(ex.Message, ConsoleColor.Red);
            Thread.Sleep(10000);
            OpenProject();
        }
    }

    internal static void ManageProject()
    {
        try
        {
            WindowManager.ShowTitle("Manage project");

            ProjectView view = projectController.GetProjectInfo();

            WindowManager.ShowMessage("Project name: " + view.Name);
            WindowManager.ShowMessage("Project progress: " + view.Progress);
            WindowManager.ShowMessage("Project description: " + view.Description);

            WindowManager.SelectMenu(MenuManageProjects);
        }
        catch (Exception ex)
        {
            WindowManager.Clear();
            WindowManager.ShowMessage(ex.Message, ConsoleColor.Red);

            Thread.Sleep(10000);
            ManageProject();
        }
    }

    internal static void ManageTeamMembers()
    {
        WindowManager.ShowTitle("Manage team members");
        WindowManager.SelectMenu(WindowTeamMembers.MenuManageTeamMembers);
    }

    internal static void ManageTasks()
    {
        WindowManager.ShowTitle("Manage task");
        WindowManager.SelectMenu(WindowTasks.MenuManageTasks);
    }

    internal static void ManageSearch()
    {
        WindowManager.ShowTitle("Manage search");
        WindowManager.SelectMenu(WindowSearchAndFiltering.MenuManageSearchAndFiltering);
    }

    internal static void SetExecutant()
    {
        WindowManager.ShowTitle("Set Executant");

        uint taskID = uint.Parse(WindowManager.GetData(
            message: "Task ID",
            helperText: "",
            (inputData) => Regex.IsMatch(inputData, "")
        ));

        uint teamMemberID = uint.Parse(WindowManager.GetData(
            message: "Team Member ID",
            helperText: "",
            (inputData) => Regex.IsMatch(inputData, "")
        ));

        projectController.SetExecutant(taskID, teamMemberID);
    }

    internal static void ChangeTaskStatus()
    {
        WindowManager.ShowTitle("Change Task Status");

        uint taskID = uint.Parse(WindowManager.GetData(
            message: "Task ID",
            helperText: "",
            (inputData) => Regex.IsMatch(inputData, "")
        ));

        string taskStatus = WindowManager.GetData(
            message: "Task ID",
            helperText: "",
            (inputData) => Regex.IsMatch(inputData, "")
        );

        projectController.ChangeTaskStatus(taskID, taskStatus);
    }

    internal static void CheckLoadOfExecutants()
    {
        WindowManager.ShowTitle("Check Load Of Executants");

        string tableHeader = $" ID  | ";
        tableHeader += $"{"Full name",-20} | ";
        tableHeader += $"Count of assigned tasks";

        WindowManager.ShowMessage(tableHeader);

        foreach (LoadOfExecutant view in projectController.CheckLoadOfExecutants())
        {
            string line = "";

            line += $"{view.ID.ToString().PadLeft(4, '0')} | ";
            line += $"{view.FullName,-20} | ";
            line += $"{view.Count}";

            WindowManager.ShowMessage(line);
        }
    }

}