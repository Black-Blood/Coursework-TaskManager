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
        new("Show all members", ShowAllMembersInfo),
    };

    internal static void AddMember()
    {
        WindowManager.ShowTitle("Add member");

        string firstName = WindowManager.GetData(
            message: "First name",
            helperText: "",
            (inputData) => Regex.IsMatch(inputData, "")
        );

        string lastName = WindowManager.GetData(
            message: "Last name",
            helperText: "",
            (inputData) => Regex.IsMatch(inputData, "")
        );

        string email = WindowManager.GetData(
            message: "Email",
            helperText: "",
            (inputData) => Regex.IsMatch(inputData, "")
        );


        WindowProject.controller.teamMemberController.CreateTeamMember(firstName, lastName, email);
        WindowProject.ManageProject();
    }

    internal static void EditMember()
    {
        WindowManager.ShowMessage("Edit member");

        uint id = uint.Parse(WindowManager.GetData(
            message: "Team member ID",
            helperText: "",
            (inputData) => Regex.IsMatch(inputData, "")
        ));

        string firstName = WindowManager.GetData(
            message: "First name",
            helperText: "",
            (inputData) => Regex.IsMatch(inputData, "")
        );

        string lastName = WindowManager.GetData(
            message: "Last name",
            helperText: "",
            (inputData) => Regex.IsMatch(inputData, "")
        );

        string email = WindowManager.GetData(
            message: "Email",
            helperText: "",
            (inputData) => Regex.IsMatch(inputData, "")
        );

        WindowProject.controller.teamMemberController.EditTeamMember(id, firstName, lastName, email);

        WindowProject.ManageProject();
    }

    internal static void DeleteMember()
    {
        WindowManager.ShowMessage("Delete member");

        uint id = uint.Parse(WindowManager.GetData(
            message: "Team member ID",
            helperText: "",
            (inputData) => Regex.IsMatch(inputData, "")
        ));

        WindowProject.controller.teamMemberController.DeleteTeamMember(id);

        WindowProject.ManageProject();
    }

    internal static void ShowAllMembersInfo()
    {
        WindowManager.ShowTitle("All members");

        List<TeamMemberView> result = WindowProject.controller.teamMemberController.GetAllTeamMemberInfo();

        result.Sort((x, y) => Convert.ToInt32(x.ID) - Convert.ToInt32(y.ID));

        string tableHeader = $" ID  | ";
        tableHeader += $"{"Full Name".PadRight(20, ' ')} | ";
        tableHeader += $"Email";
        WindowManager.ShowMessage(tableHeader);

        foreach (TeamMemberView view in result)
        {
            string line = PrepareTeamMemberInformation(view);
            WindowManager.ShowMessage(line);
        }
    }

    internal static string PrepareTeamMemberInformation(TeamMemberView teamMember)
    {
        string result = "";

        result += $"{teamMember.ID.ToString().PadLeft(4, '0')} | ";
        result += $"{(teamMember.FirstName + " " + teamMember.LastName).PadRight(20, ' ')} | ";
        result += $"{teamMember.Email}";

        return result;
    }
}
