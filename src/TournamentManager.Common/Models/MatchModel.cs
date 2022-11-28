using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace TournamentManager.Common.Models;

public class MatchModel : ModelBase, IValidatableObject
{
    public MatchModel(Guid tournamentId,
        int score1,
        int score2,
        int round,
        int order,
        bool isLocked)
    {
        Score1 = score1;
        Score2 = score2;
        Round = round;
        Order = order;
        IsLocked = isLocked;
        TournamentId = tournamentId;
    }
    public int Score1 { get; set; }
    public int Score2 { get; set; }
    public int Round { get; set; }
    public int Order { get; set; }
    public bool IsLocked { get; set; }
    
    public Guid TournamentId { get; set; }
    
    [ValidateNever]
    public TournamentModel? Tournament { get; set; }
    
    public Guid? Team1Id { get; set; }
    
    [ValidateNever]
    public TeamModel? Team1 { get; set; }
    
    public Guid? Team2Id { get; set; }
    
    [ValidateNever]
    public TeamModel? Team2 { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Team1Id == Team2Id)
        {
            yield return new ValidationResult("Teams cannot be the same", new[] { nameof(Team1Id), nameof(Team2Id) });
        }
    }
}