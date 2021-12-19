using System.Text.RegularExpressions;
using BLL;

namespace PL;

internal class WindowTeamMembers
{
    internal static KeyValuePair<string, WindowManager.MenuMethod>[] MenuManageTeamMembers =
    {
        new("Add member", AddMember),
        new("Edit member", EditMember),
        new("Delete member", DeleteMember),
        new("Get all members", GetAllMembersInfo),
    };

    internal static void AddMember()
    {
        WindowManager.ShowTitle("Add member");

        string firstName = WindowManager.GetData(
            message: "First name",
            helperText: "Only letters (from 3 to 10 symbols)",
            (inputData) => Regex.IsMatch(inputData, @"^[a-zA-Z]{3,10}$")
        );

        string lastName = WindowManager.GetData(
            message: "Last name",
            helperText: "Only letters (from 3 to 10 symbols)",
            (inputData) => Regex.IsMatch(inputData, @"^[a-zA-Z]{3,10}$")
        );

        string email = WindowManager.GetData(
            message: "Email",
            helperText: "Enter valid email",
            (inputData) => Regex.IsMatch(inputData, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")
        );


        WindowProject.teamMemberController.CreateTeamMember(firstName, lastName, email);
        WindowProject.ManageProject();
    }

    internal static void EditMember()
    {
        WindowManager.ShowMessage("Edit member");

        uint id = uint.Parse(WindowManager.GetData(
            message: "Team member ID",
            helperText: "Only numbers",
            (inputData) => Regex.IsMatch(inputData, @"^\d{1,}$")
        ));

        string firstName = WindowManager.GetData(
            message: "First name",
            helperText: "Only letters (from 3 to 10 symbols)",
            (inputData) => Regex.IsMatch(inputData, @"^[a-zA-Z]{3,10}$")
        );

        string lastName = WindowManager.GetData(
            message: "Last name",
            helperText: "Only letters (from 3 to 10 symbols)",
            (inputData) => Regex.IsMatch(inputData, @"^[a-zA-Z]{3,10}$")
        );

        string email = WindowManager.GetData(
            message: "Email",
            helperText: "Enter valid email",
            (inputData) => Regex.IsMatch(inputData, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")
        );

        WindowProject.teamMemberController.EditTeamMember(id, firstName, lastName, email);

        WindowProject.ManageProject();
    }

    internal static void DeleteMember()
    {
        WindowManager.ShowMessage("Delete member");

        uint id = uint.Parse(WindowManager.GetData(
            message: "Team member ID",
            helperText: "Only numbers",
            (inputData) => Regex.IsMatch(inputData, @"^\d{1,}$")
        ));

        WindowProject.teamMemberController.DeleteTeamMember(id);

        WindowProject.ManageProject();
    }

    internal static void GetAllMembersInfo()
    {
        WindowManager.ShowTitle("All members");

        List<TeamMemberView> result = WindowProject.teamMemberController.GetAllTeamMemberInfo();

        ShowTeamMemberInfo(result);
    }

    internal static void ShowTeamMemberInfo(List<TeamMemberView> views)
    {
        views.Sort((x, y) => Convert.ToInt32(x.ID) - Convert.ToInt32(y.ID));

        string tableHeader = $" ID  | ";
        tableHeader += $"{"Full Name",-20} | ";
        tableHeader += $"Email";
        WindowManager.ShowMessage(tableHeader);

        foreach (TeamMemberView view in views)
        {
            string line = "";
            line += $"{view.ID.ToString().PadLeft(4, '0')} | ";
            line += $"{view.FirstName + " " + view.LastName,-20} | ";
            line += $"{view.Email}";
            WindowManager.ShowMessage(line);
        }

        Console.ReadKey();

        WindowProject.ManageProject();
    }
}
