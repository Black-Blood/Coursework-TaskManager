namespace BLL;

using DAL;

public class TeamMemberController
{
    private readonly Project _project;

    internal TeamMemberController(Project project)
    {
        _project = project;
    }

    public void CreateTeamMember(string firstName, string lastName, string email)
    {
        try
        {
            TeamMember teamMember = new();

            uint id = Convert.ToUInt32(_project.TeamMembers.Count);
            for (; true; id++)
                if (!_project.TeamMembers.Exists(teamMember => teamMember.ID == id))
                    break;

            teamMember.ID = id;
            teamMember.FirstName = firstName;
            teamMember.LastName = lastName;
            teamMember.Email = email;

            _project.AddTeamMember(teamMember);
        }
        catch
        {

        }

    }

    public void EditTeamMember(uint teamMemberID, string? firstName, string? lastName, string? email)
    {
        try
        {
            TeamMember? teamMember = _project.TeamMembers.Find(teamMember => teamMember.ID == teamMemberID);

            if (teamMember is null) throw new Exception("No such team member");

            if (firstName is not null) teamMember.FirstName = firstName;
            if (lastName is not null) teamMember.LastName = lastName;
            if (email is not null) teamMember.Email = email;

            _project.UpdateTeamMember(teamMember);
        }
        catch
        {

        }

    }

    public void DeleteTeamMember(uint teamMemberID)
    {
        TeamMember? teamMember = _project.TeamMembers.Find(teamMember => teamMember.ID == teamMemberID);

        if (teamMember is null) throw new Exception("No such team member");

        _project.RemoveMember(teamMember);
    }

    public TeamMemberView GetTeamMemberInfo(uint teamMemberID)
    {
        TeamMember? teamMember = _project.TeamMembers.Find(teamMember => teamMember.ID == teamMemberID);

        if (teamMember is null) throw new Exception("No such team member");

        return new TeamMemberView(teamMember);
    }

    public List<TeamMemberView> GetAllTeamMemberInfo()
    {
        return _project.TeamMembers.ConvertAll(teamMember => new TeamMemberView(teamMember));
    }
}
