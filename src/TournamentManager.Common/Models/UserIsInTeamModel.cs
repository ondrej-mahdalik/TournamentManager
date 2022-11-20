using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Common.Models;

public record UserIsInTeamModel(Guid UserId,
    Guid TeamId,
    bool IsApproved = false) : ModelBase
{
    public bool IsApproved { get; set; } = IsApproved;
    
    public Guid UserId { get; set; } = UserId;
    public UserModel? User { get; set; }

    public Guid TeamId { get; set; } = TeamId;
    public TeamModel? Team { get; set; }
}