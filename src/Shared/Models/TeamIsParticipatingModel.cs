using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Shared.Models;

public record TeamIsParticipatingModel(bool Approved) : ModelBase
{
    public Guid TeamId { get; set; }
    public TeamModel? Team { get; set; }

    public Guid TournamentId { get; set; }
    public TournamentModel? Tournament { get; set; }
}