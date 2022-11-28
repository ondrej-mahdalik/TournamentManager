using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TournamentManager.Common.Models;

public class TeamIsParticipatingModel : ModelBase
{
    public TeamIsParticipatingModel(bool approved,
        DateTime createdOn,
        Guid teamId,
        Guid tournamentId)
    {
        CreatedOn = createdOn;
        Approved = approved;
        TeamId = teamId;
        TournamentId = tournamentId;
    }
    public bool Approved { get; set; }
    
    public Guid TeamId { get; set; }
    
    [ValidateNever]
    public TeamModel? Team { get; set; }
    public Guid TournamentId { get; set; }
    
    [ValidateNever]    
    public TournamentModel? Tournament { get; set; }
    public DateTime CreatedOn { get; init; }
}