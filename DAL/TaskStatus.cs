namespace DAL;

public class TaskStatus
{
    public enum StatusType
    {
        Done,
        New,
        InDevelopment
    }

    private StatusType _currentStatus = StatusType.New;
    private List<StatusType> _history = new();

    public StatusType CurrentStatus
    {
        get => _currentStatus;
        set => _history.Add(_currentStatus = value);
    }
    public List<StatusType> History => _history;
}