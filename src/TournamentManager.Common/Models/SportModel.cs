using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Common.Models;

public record SportModel(
    string Name) : ModelBase
{
    public string Name { get; set; } = Name;
    
    public IList<TournamentModel> Tournaments { get; set; } = new List<TournamentModel>();
}