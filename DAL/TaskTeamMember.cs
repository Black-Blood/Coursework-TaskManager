namespace DAL;

public class TaskTeamMember
{
    private uint _taskID;
    private uint _teamMemberID;

    public uint TaskID => _taskID;
    public uint TeamMemberID => _teamMemberID;

    public TaskTeamMember(uint taskID, uint teamMemberID)
    {
        _taskID = taskID;
        _taskID = teamMemberID;
    }
}