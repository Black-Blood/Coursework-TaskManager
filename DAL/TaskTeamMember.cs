using System.Runtime.Serialization;

namespace DAL;

[DataContract]
public class TaskTeamMember
{
    private uint _taskID;
    private uint _teamMemberID;

    [DataMember]
    public uint TaskID
    {
        get => _taskID;
        protected set => _taskID = value;
    }

    [DataMember]
    public uint TeamMemberID
    {
        get => _teamMemberID;
        protected set => _teamMemberID = value;
    }

    public TaskTeamMember(uint taskID, uint teamMemberID)
    {
        _taskID = taskID;
        _taskID = teamMemberID;
    }
}