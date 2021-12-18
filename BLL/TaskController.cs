using DAL;
using System.Globalization;

namespace BLL;

public class TaskController
{
    private Project _project;

    internal TaskController(Project project)
    {
        _project = project;
    }

    public void CreateTask(string title, string deadline, string description)
    {
        try
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
        catch
        {

        }

    }

    public void EditTask(uint taskID, string? title, string? deadline, string? status, string? description)
    {
        try
        {
            DAL.Task? task = _project.Tasks.Find(t => t.ID == taskID);

            if (task is null) throw new Exception();

            if (title is not null) task.Title = title;
            if (deadline is not null) task.Deadline = DateTime.Parse(deadline);
            if (description is not null) task.Description = description;
            if (status is not null) task.Status.CurrentStatus = Enum.Parse<DAL.TaskStatus.StatusType>(status);

            _project.UpdateTask(task);
        }
        catch
        {

        }

    }

    public void DeleteTask(uint taskID)
    {
        DAL.Task? task = _project.Tasks.Find(t => t.ID == taskID);
        
        if(task is null) throw new Exception();

        _project.RemoveTask(task);
    }

    public TaskView GetTaskInfo(uint taskID)
    {

        DAL.Task? task = _project.Tasks.Find(t => t.ID == taskID);

        if (task is null) throw new Exception();

        return new TaskView(task);
    }

    public List<TaskView> GetAllTaskInfo()
    {
        return _project.Tasks.ConvertAll(task => new TaskView(task));
    }
}
