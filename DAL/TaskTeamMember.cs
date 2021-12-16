namespace DAL;
public class TaskTeamMember
{
    public uint taskID;
    public uint teamMemberID;

    public TaskTeamMember(uint taskID, uint teamMemberID)
    {
        this.taskID = taskID;
        this.teamMemberID = teamMemberID;
    }
}