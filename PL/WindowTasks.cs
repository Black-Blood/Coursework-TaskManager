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
        new("Show all tasks", ShowAllTasksInfo),
    };


    internal static void AddTask()
    {
        WindowManager.ShowTitle("Add Task");

        string title = WindowManager.GetData(
            message: "Type task title",
            helperText: "",
            (inputData) => Regex.IsMatch(inputData, "")
        );

        string deadline = WindowManager.GetData(
            message: "Type task deadline",
            helperText: "",
            (inputData) => Regex.IsMatch(inputData, "")
        );

        string description = WindowManager.GetData(
            message: "Type task description",
            helperText: "",
            (inputData) => Regex.IsMatch(inputData, "")
        );


        WindowProject.controller.taskController.CreateTask(title, deadline, description);
        WindowProject.ManageProject();
    }

    internal static void EditTask()
    {
        WindowManager.ShowMessage("Edit Task");

        uint id = uint.Parse(WindowManager.GetData(
            message: "Task ID",
            helperText: "",
            (inputData) => Regex.IsMatch(inputData, "")
        ));

        string title = WindowManager.GetData(
            message: "Type task title",
            helperText: "",
            (inputData) => Regex.IsMatch(inputData, "")
        );

        string deadline = WindowManager.GetData(
            message: "Type task deadline",
            helperText: "",
            (inputData) => Regex.IsMatch(inputData, "")
        );

        string status = WindowManager.GetData(
            message: "Type task status",
            helperText: "",
            (inputData) => Regex.IsMatch(inputData, "")
        );

        string description = WindowManager.GetData(
            message: "Type task description",
            helperText: "",
            (inputData) => Regex.IsMatch(inputData, "")
        );

        WindowProject.controller.taskController.EditTask(
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
            helperText: "",
            (inputData) => Regex.IsMatch(inputData, "")
        ));

        WindowProject.controller.taskController.DeleteTask(id);

        WindowProject.ManageProject();
    }

    internal static void ShowAllTasksInfo()
    {
        WindowManager.ShowTitle("All members");

        List<TaskView> result = WindowProject.controller.taskController.GetAllTaskInfo();

        result.Sort((x, y) => Convert.ToInt32(x.ID) - Convert.ToInt32(y.ID));

        string tableHeader = $" ID  | ";
        tableHeader += $"{"Deadline".PadRight(10, ' ')} | ";
        tableHeader += $"{"Title".PadRight(20, ' ')} | ";
        tableHeader += $"Description";
        WindowManager.ShowMessage(tableHeader);

        foreach (TaskView view in result)
        {
            string line = PrepareTaskInformation(view);
            WindowManager.ShowMessage(line);
        }
    }

    internal static string PrepareTaskInformation(TaskView task)
    {
        string result = "";

        result += $"{task.ID.ToString().PadLeft(3, '0')} | ";
        result += $"{task.Deadline.ToString().PadRight(10, ' ')} | ";
        result += $"{task.Title.PadRight(20, ' ')} | ";
        result += $"{task.Description}";

        return result;
    }
}
