using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Shared.Models;

public record SportModel(Guid Id,
    string Name)
{
    [Key]
    public Guid Id { get; set; } = Id;
    public IList<TournamentModel> Tournaments { get; set; } = new List<TournamentModel>();
}