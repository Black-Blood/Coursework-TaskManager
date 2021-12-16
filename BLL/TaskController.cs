using DAL;

namespace BLL;

public class TaskController
{
    private Project _project;

    public TaskController(Project project)
    {
        _project = project;
    }

    public void CreateTask(string title, string deadline, string description, string status)
    {
        try
        {
            DAL.Task task = new();

            uint id = Convert.ToUInt32(_project.Tasks.Count);
            for (; true; id++)
                if (!_project.Tasks.Exists(task => task.ID == id))
                    break;

            task.ID = id;
            task.Title = title;
            task.Description = description;
            task.Deadline = deadline;
            task.Status = status;

            _project.AddTask(task);
        }
        catch
        {

        }

    }

    public void EditTask(uint taskID, string? title, string? deadline, string? description, string? status)
    {
        try
        {
            DAL.Task? task = _project.Tasks.Find(task => task.ID == taskID);

            if (task is null) throw new Exception();

            if (title is not null) task.Title = title;
            if (deadline is not null) task.Deadline = deadline;
            if (description is not null) task.Description = description;
            if (status is not null) task.Status = status;

            _project.UpdateTask(task);
        }
        catch
        {

        }

    }

    public void DeleteTask(uint taskID)
    {
        DAL.Task? task = _project.Tasks.Find(task => task.ID == taskID);
        
        if(task is null) throw new Exception();

        _project.RemoveTask(task);
    }

    public string GetTaskInfo(uint taskID)
    {

        DAL.Task? task = _project.Tasks.Find(task => task.ID == taskID);

        if (task is null) throw new Exception();

        return PrepareTaskInformation(task);
    }

    public List<string> GetAllTaskInfo()
    {
        return _project.Tasks.ConvertAll(PrepareTaskInformation);
    }

    private string PrepareTaskInformation(DAL.Task task)
    {
        string result = "";

        result += $"{task.ID.ToString().PadLeft(3, '0')} | ";
        result += $"{task.Title.PadRight(50, ' ')} | ";
        result += $"{task.Deadline.PadRight(10, ' ')} | ";
        result += $"{task.Description}";

        return result;

    }
}
