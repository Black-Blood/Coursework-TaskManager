using DAL;

namespace BLL;


public class TaskController
{
    public DAL.Task CreateTask(string title, string deadline, string description)
    {
        DAL.Task task = new();

        task.Title = title;
        task.Description = description;
        task.Deadline = deadline;

        return task;
    }

    public DAL.Task EditTask(DAL.Task task, string? title, string? deadline, string? description)
    {
        if(title is not null) task.Title = title;
        if (deadline is not null) task.Title = deadline;
        if (description is not null) task.Description = description;

        return task;
    }
}
