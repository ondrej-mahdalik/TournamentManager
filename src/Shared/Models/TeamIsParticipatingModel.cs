using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Shared.Models;

public record TeamIsParticipatingModel(Guid TeamIsParticipatingId,
    bool Approved)
{
    [Key]
    public Guid TeamIsParticipatingId { get; set; } = TeamIsParticipatingId;
    
    public Guid TeamId { get; set; }
    public TeamModel? Team { get; set; }
    
    public Guid TournamentId { get; set; }
    public TournamentModel? Tournament { get; set; }
}