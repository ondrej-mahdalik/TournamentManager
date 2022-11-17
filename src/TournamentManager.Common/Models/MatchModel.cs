using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TournamentManager.Common.Models;

public record MatchModel(
    Guid TournamentId,
    int Score1,
    int Score2,
    int Round,
    int Order) : ModelBase
{
    public int Score1 { get; set; } = Score1;
    public int Score2 { get; set; } = Score2;
    public int Round { get; set; } = Round;
    public int Order { get; set; } = Order;
    
    public Guid TournamentId { get; set; } = TournamentId;
    public TournamentModel? Tournament { get; set; }
    
    public Guid? Team1Id { get; set; }
    public TeamModel? Team1 { get; set; }
    
    public Guid? Team2Id { get; set; }
    public TeamModel? Team2 { get; set; }
}