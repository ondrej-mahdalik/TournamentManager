using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Shared.Models;

public record TeamIsParticipatingModel(Guid Id,
    bool Approved)
{
    [Key]
    public Guid Id { get; set; } = Id;
    
    public Guid TeamId { get; set; }
    public TeamModel? Team { get; set; }
    
    public Guid TournamentId { get; set; }
    public TournamentModel? Tournament { get; set; }
}