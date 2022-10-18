using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Shared.Models;

public record SportModel(Guid SportId,
    string Name)
{
    [Key]
    public Guid SportId { get; set; } = SportId;
    public IList<TournamentModel> Tournaments { get; set; } = new List<TournamentModel>();
}