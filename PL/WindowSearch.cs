using BLL;

namespace PL;

internal static class WindowSearchAndFiltering
{
    internal static KeyValuePair<string, WindowManager.MenuMethod>[] MenuManageSearchAndFiltering =
    {
        new("Search", Search),
        new("Filtering", Filtering),
    };

    public static KeyValuePair<string, WindowManager.MenuMethod>[] MenuManageSearchTask =
    {
        new("Task by team member id", SearchTask),
        new("Team member by task id", SearchTeamMember)
    };

    public static KeyValuePair<string, SearchController.FilterTaskType>[] MenuManageSearchTeamMember =
    {
        new("Filter by status 'done'", SearchController.FilterTaskType.Done),
        new("Filter by status 'not done'", SearchController.FilterTaskType.NotDone),
        new("Filter by deadline 'active'", SearchController.FilterTaskType.DeadlineLess),
        new("Filter by deadline 'pass'", SearchController.FilterTaskType.DeadlineMore),
    };

    internal static void SearchTask()
    {
        WindowManager.ShowTitle("Task by team member id");
        
        uint searchText = uint.Parse(WindowManager.GetData(
            message: "Type team member id",
            helperText: ""));

        List<uint> result =  WindowProject.searchController.SearchTaskByTeamMember(searchText);

        List<TaskView> taskViews = result.ConvertAll((id) => WindowProject.taskController.GetTaskInfo(id));

        WindowTasks.ShowTaskInfo(taskViews);
    }

    internal static void SearchTeamMember()
    {
        WindowManager.ShowTitle("Team member by task id");

        uint searchText = uint.Parse(WindowManager.GetData(
                  message: "Type task id",
                  helperText: ""));

        List<uint> result = WindowProject.searchController.SearchTeamMemberByTask(searchText);

        List<TeamMemberView> teamMemberViews = result.ConvertAll((id) => WindowProject.teamMemberController.GetTeamMemberInfo(id));
    
        WindowTeamMembers.ShowTeamMemberInfo(teamMemberViews);
    }

    internal static void Search()
    {
        WindowManager.ShowTitle("Search");
        WindowManager.SelectMenu(MenuManageSearchTask);
    }    
    
    internal static void Filtering()
    {
        WindowManager.ShowTitle("Filtering");
        WindowManager.SelectMenu(MenuManageSearchTask);

        WindowManager.ShowMenu("choose", Array.ConvertAll(MenuManageSearchTeamMember, element => element.Key));

        string input = WindowManager.GetData(
            message: "Type number",
            helperText: "",
            validator: (inputData) => Int32.TryParse(inputData, out int result) && result < MenuManageSearchTeamMember.Length);

        SearchController.FilterTaskType searchType = MenuManageSearchTeamMember[Int32.Parse(input)].Value;

        WindowManager.ShowTitle(MenuManageSearchTask[Int32.Parse(input)].Key);

        List<uint> tasksId = WindowProject.searchController.FilterTaskBy(searchType);
        List<TaskView> result = tasksId.ConvertAll((id) => WindowProject.taskController.GetTaskInfo(id));

        WindowTasks.ShowTaskInfo(result);
    }
}