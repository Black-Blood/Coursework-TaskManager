using DAL;
using System.Globalization;

namespace BLL;

public class TaskController
{
    private readonly Project _project;

    internal TaskController(Project project)
    {
        _project = project;
    }

    public void CreateTask(string title, string deadline, string description)
    {
        DAL.Task task = new();

        uint id = Convert.ToUInt32(_project.Tasks.Count);
        for (; true; id++)
            if (!_project.Tasks.Exists(t => t.ID == id))
                break;

        task.ID = id;
        task.Title = title;
        task.Description = description;
        task.Deadline = DateTime.ParseExact(deadline, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
        task.Status = new();

        _project.AddTask(task);


    }

    public void EditTask(uint taskID, string title = "", string deadline = "", string status = "", string description = "")
    {
        DAL.Task? task = _project.Tasks.Find(t => t.ID == taskID);

        if (task is null) throw new Exception("No such task");

        if (title is not "") task.Title = title;
        if (deadline is not "") task.Deadline = DateTime.Parse(deadline);
        if (description is not "") task.Description = description;
        if (status is not "") task.Status.CurrentStatus = Enum.Parse<DAL.TaskStatus.StatusType>(status);

        _project.UpdateTask(task);

    }

    public void DeleteTask(uint taskID)
    {
        DAL.Task? task = _project.Tasks.Find(t => t.ID == taskID);

        if (task is null) throw new Exception("No such task");

        _project.RemoveTask(task);
    }

    public TaskView GetTaskInfo(uint taskID)
    {

        DAL.Task? task = _project.Tasks.Find(t => t.ID == taskID);

        if (task is null) throw new Exception("No such task");

        return new TaskView(task);
    }

    public List<TaskView> GetAllTaskInfo()
    {
        return _project.Tasks.ConvertAll(task => new TaskView(task));
    }
}
