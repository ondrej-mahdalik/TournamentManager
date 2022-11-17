using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Common.Models;

public record UserIsInTeamModel(Guid UserId,
    Guid TeamId) : ModelBase
{
    public Guid UserId { get; set; } = UserId;
    public UserModel? User { get; set; }

    public Guid TeamId { get; set; } = TeamId;
    public TeamModel? Team { get; set; }
}