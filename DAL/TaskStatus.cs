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

    public StatusType CurrentStatus => _currentStatus;
    public List<StatusType> History => _history;
}