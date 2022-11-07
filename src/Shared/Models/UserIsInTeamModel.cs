using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Shared.Models;

public record UserIsInTeamModel(Guid Id)
{
    [Key]
    public Guid Id { get; set; } = Id;
    
    public Guid UserId { get; set; }
    public UserModel? User { get; set; }
    
    public Guid TeamId { get; set; }
    public TeamModel? Team { get; set; }
}