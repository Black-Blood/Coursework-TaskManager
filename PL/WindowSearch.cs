using BLL;

namespace PL;

internal static class WindowSearch
{
    internal static KeyValuePair<string, WindowManager.MenuMethod>[] MenuManageSearch =
    {
        new("Search task", SearchTask),
        new("Search team member", SearchTeamMember),
        //new("Search team members by task id",),
        //new("Search tasks by team member id",),
    };

    public static KeyValuePair<string, SearchController.SearchTaskBy>[] MenuManageSearchTask =
    {
        new("Search task by all", SearchController.SearchTaskBy.All),
        new("Search task by ID", SearchController.SearchTaskBy.ID),
        new("Search task by title", SearchController.SearchTaskBy.Title),
        new("Search task by deadline", SearchController.SearchTaskBy.Deadline),
        new("Search task by description", SearchController.SearchTaskBy.Description),
    };

    public static KeyValuePair<string, SearchController.SearchTeamMemberBy>[] MenuManageSearchTeamMember =
    {
        new("Search team member by all", SearchController.SearchTeamMemberBy.All),
        new("Search team member by ID", SearchController.SearchTeamMemberBy.ID),
        new("Search team member by name", SearchController.SearchTeamMemberBy.Name),
        new("Search team member by email", SearchController.SearchTeamMemberBy.Email),
    };

    internal static void SearchTask()
    {
        WindowManager.ShowTitle("Search Task");
        WindowManager.ShowMenu("choose", Array.ConvertAll(MenuManageSearchTask, element => element.Key));

        string input = WindowManager.GetData(
            message: "Type number",
            helperText: "",
            validator: (inputData) => Int32.TryParse(inputData, out int result) && result < MenuManageSearchTask.Length);

        SearchController.SearchTaskBy searchType = MenuManageSearchTask[Int32.Parse(input)].Value;

        WindowManager.ShowTitle(MenuManageSearchTask[Int32.Parse(input)].Key);

        string searchText = WindowManager.GetData(
            message: "Type search te",
            helperText: "");

        List<TaskView> result =  WindowProject.controller.searchController.SearchTask(searchText, searchType);

        result.Sort((x, y) => Convert.ToInt32(x.ID) - Convert.ToInt32(y.ID));

        string tableHeader = $" ID  | ";
        tableHeader += $"{"Deadline".PadRight(10, ' ')} | ";
        tableHeader += $"{"Title".PadRight(20, ' ')} | ";
        tableHeader += $"Description";
        WindowManager.ShowMessage(tableHeader);

        foreach (TaskView view in result)
        {
            string line = WindowTasks.PrepareTaskInformation(view);
            WindowManager.ShowMessage(line);
        }
    }

    internal static void SearchTeamMember()
    {
        WindowManager.ShowTitle("Search Team Member");
        WindowManager.ShowMenu("choose", Array.ConvertAll(MenuManageSearchTeamMember, element => element.Key));

        string input = WindowManager.GetData(
            message: "Type number",
            helperText: "",
            validator: (inputData) => Int32.TryParse(inputData, out int result) && result < MenuManageSearchTeamMember.Length);

        SearchController.SearchTeamMemberBy searchType = MenuManageSearchTeamMember[Int32.Parse(input)].Value;

        WindowManager.ShowTitle(MenuManageSearchTask[Int32.Parse(input)].Key);

        string searchText = WindowManager.GetData(
            message: "Type search te",
            helperText: "");

        List<TeamMemberView> result = WindowProject.controller.searchController.SearchTeamMember(searchText, searchType);

        result.Sort((x, y) => Convert.ToInt32(x.ID) - Convert.ToInt32(y.ID));

        string tableHeader = $" ID  | ";
        tableHeader += $"{"Deadline".PadRight(10, ' ')} | ";
        tableHeader += $"{"Title".PadRight(20, ' ')} | ";
        tableHeader += $"Description";
        WindowManager.ShowMessage(tableHeader);

        foreach (TeamMemberView view in result)
        {
            string line = WindowTeamMembers.PrepareTeamMemberInformation(view);
            WindowManager.ShowMessage(line);
        }
    }
}