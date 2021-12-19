using System.Text.RegularExpressions;
using BLL;

namespace PL;

public class WindowTasks
{
    internal static KeyValuePair<string, WindowManager.MenuMethod>[] MenuManageTasks =
        {
        new("Add task", AddTask),
        new("Edit task", EditTask),
        new("Delete task", DeleteTask),
        new("Get all tasks", GetAllTasksInfo),
    };


    internal static void AddTask()
    {
        WindowManager.ShowTitle("Add Task");

        string title = WindowManager.GetData(
            message: "Type task title",
            helperText: "Avalaible only 'space', numbers and letters (from 3 to 20 symbols)",
            (inputData) => Regex.IsMatch(inputData, @"^[a-zA-Z0-9 ]{3,20}$")
        );

        string deadline = WindowManager.GetData(
            message: "Type task deadline",
            helperText: "Avalaible only this structure 'yyyy-mm-dd hh:mm'",
            (inputData) => Regex.IsMatch(inputData, @"^\d\d\d\d-\d\d-\d\d \d\d:\d\d$")
        );

        string description = WindowManager.GetData(
            message: "Type task description",
            helperText: "Avalaible all symbols (from 3 to 50 symbols)",
            (inputData) => Regex.IsMatch(inputData, "^.{3,50}$")
        );


        WindowProject.taskController.CreateTask(title, deadline, description);
        WindowProject.ManageProject();
    }

    internal static void EditTask()
    {
        WindowManager.ShowMessage("Edit Task");

        uint id = uint.Parse(WindowManager.GetData(
            message: "Task ID",
            helperText: "Only numbers",
            (inputData) => Regex.IsMatch(inputData, @"\d{1,}")
        ));

        string title = WindowManager.GetData(
            message: "Type task title",
            helperText: "Avalaible only 'space', letters and numbers (from 3 to 20 symbols)",
        (inputData) => Regex.IsMatch(inputData, @"^[a-zA-Z0-9 ]{3,20}$|^$")
        );

        string deadline = WindowManager.GetData(
            message: "Type task deadline",
            helperText: "Avalaible only this structure 'yyyy-mm-dd hh:mm'",
            (inputData) => Regex.IsMatch(inputData, @"^\d\d\d\d-\d\d-\d\d \d\d:\d\d$|^$")
        );

        string status = WindowManager.GetData(
            message: "Type task status",
            helperText: "Avalaible only letters",
            (inputData) => Regex.IsMatch(inputData, "^[a-zA-Z]*$|^$")
        );

        string description = WindowManager.GetData(
            message: "Type task description",
            helperText: "Avalaible all symbols (from 3 to 50 symbols)",
            (inputData) => Regex.IsMatch(inputData, "^.{3,50}$|^$")
        );

        WindowProject.taskController.EditTask(
            taskID: id,
            title: title,
            description: description,
            deadline: deadline,
            status: status
        );

        WindowProject.ManageProject();
    }

    internal static void DeleteTask()
    {
        WindowManager.ShowMessage("Delete Task");

        uint id = uint.Parse(WindowManager.GetData(
            message: "Task ID",
            helperText: "Only numbers",
            (inputData) => Regex.IsMatch(inputData, @"^\d{1,}$")
        ));

        WindowProject.taskController.DeleteTask(id);

        WindowProject.ManageProject();
    }

    internal static void GetAllTasksInfo()
    {
        WindowManager.ShowTitle("All members");

        List<TaskView> result = WindowProject.taskController.GetAllTaskInfo();

        ShowTaskInfo(result);
    }

    internal static void ShowTaskInfo(List<TaskView> views)
    {
        views.Sort((x, y) => Convert.ToInt32(x.ID) - Convert.ToInt32(y.ID));

        string tableHeader = $" ID  | ";
        tableHeader += $"{"Deadline",-30} | ";
        tableHeader += $"{"Status",-10} | ";
        tableHeader += $"{"Title",-20} | ";
        tableHeader += $"Description";

        WindowManager.ShowMessage(tableHeader);

        foreach (TaskView view in views)
        {
            string line = "";

            line += $"{view.ID.ToString().PadLeft(4, '0')} | ";
            line += $"{view.Deadline,-30} | ";
            line += $"{view.Status,-10} | ";
            line += $"{view.Title,-20} | ";
            line += $"{view.Description}";

            WindowManager.ShowMessage(line);
        }

        Console.ReadKey();

        WindowProject.ManageProject();
    }
}
