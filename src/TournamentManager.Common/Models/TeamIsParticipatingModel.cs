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
    
    public TeamModel? Team { get; set; }
    public Guid TournamentId { get; set; }
    
    public TournamentModel? Tournament { get; set; }
    public DateTime CreatedOn { get; init; }
}