using BLL;
using System.Text.RegularExpressions;

namespace PL;

public static class WindowProject
{
    internal static ProjectController controller;

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
    };

    public static void Start()
    {
        WindowManager.ShowTitle("START WORKING");
        WindowManager.SelectMenu(MenuManageStartWorking);
    }

    internal static void CreateProject()
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

        controller = new(filePath, name, description);

        ManageProject();
    }

    internal static void OpenProject()
    {
        WindowManager.ShowTitle("Open project");

        string filePath = WindowManager.GetData(
            message: "Type file path",
            helperText: "",
            (inputData) => Regex.IsMatch(inputData, "")
        );

        controller = new(filePath);

        ManageProject();
    }

    internal static void ManageProject()
    {
        WindowManager.ShowTitle("Manage project");

        WindowManager.ShowMessage("Project name: " + controller.ProjectName);
        WindowManager.ShowMessage("Project description: " + controller.ProjectDescription);

        WindowManager.SelectMenu(MenuManageProjects);
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
        WindowManager.SelectMenu(WindowSearch.MenuManageSearch);
    }
}