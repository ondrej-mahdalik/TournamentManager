using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Shared.Models;

public record UserIsInTeamModel : ModelBase
{
    public Guid UserId { get; set; }
    public UserModel? User { get; set; }
    
    public Guid TeamId { get; set; }
    public TeamModel? Team { get; set; }
}