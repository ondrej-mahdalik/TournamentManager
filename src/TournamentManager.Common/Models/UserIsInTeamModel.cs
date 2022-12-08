namespace TournamentManager.Common.Models;

public class UserIsInTeamModel : ModelBase
{
    public UserIsInTeamModel(Guid UserId,
        Guid TeamId,
        bool IsApproved = false)
    {
        this.IsApproved = IsApproved;
        this.UserId = UserId;
        this.TeamId = TeamId;
    }
    public bool IsApproved { get; set; }
    
    public Guid UserId { get; set; }
    public UserModel? User { get; set; }

    public Guid TeamId { get; set; }
    public TeamModel? Team { get; set; }
    public void Deconstruct(out Guid UserId, out Guid TeamId, out bool IsApproved)
    {
        UserId = this.UserId;
        TeamId = this.TeamId;
        IsApproved = this.IsApproved;
    }
}