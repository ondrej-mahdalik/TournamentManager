using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TournamentManager.Common.Models;

public class MatchModel : ModelBase, IValidatableObject
{
    public MatchModel(Guid tournamentId,
        int score1,
        int score2,
        int round,
        int order)
    {
        Score1 = score1;
        Score2 = score2;
        Round = round;
        Order = order;
        TournamentId = tournamentId;
    }
    public int Score1 { get; set; }
    public int Score2 { get; set; }
    public int Round { get; set; }
    public int Order { get; set; }
    
    public Guid TournamentId { get; set; }
    public TournamentModel? Tournament { get; set; }
    
    public Guid? Team1Id { get; set; }
    public TeamModel? Team1 { get; set; }
    
    public Guid? Team2Id { get; set; }
    public TeamModel? Team2 { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Team1Id == Team2Id)
        {
            yield return new ValidationResult("Teams cannot be the same", new[] { nameof(Team1Id), nameof(Team2Id) });
        }
    }
}