using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Common.Models;

public record TeamIsParticipatingModel(bool Approved,
    DateTime CreatedOn,
    Guid TeamId,
    Guid TournamentId): ModelBase
{
    public bool Approved { get; set; } = Approved;
    
    public Guid TeamId { get; set; } = TeamId;
    public TeamModel? Team { get; set; }
    
    public Guid TournamentId { get; set; } = TournamentId;
    public TournamentModel? Tournament { get; set; }
}