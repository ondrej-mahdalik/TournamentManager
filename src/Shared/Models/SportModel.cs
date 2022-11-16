using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Shared.Models;

public record SportModel(
    string Name) : ModelBase
{
    public IList<TournamentModel> Tournaments { get; set; } = new List<TournamentModel>();
}