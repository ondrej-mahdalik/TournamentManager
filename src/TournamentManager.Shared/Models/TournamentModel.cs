using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Shared.Models;

public record TournamentModel(string Name,
    DateTime Date,
    string? Description,
    bool IsPublic,
    bool IsApproved,
    int MaxPlayers) : ModelBase
{
    public Guid CreatorId { get; set; }
    public UserModel? Creator { get; set; }
    
    public Guid? WinnerTeamOverrideId { get; set; }
    
    public TeamModel? WinnerTeamOverride { get; set; }
    
    public Guid SportId { get; set; }
    public SportModel? Sport { get; set; }
    
    public IList<TeamIsParticipatingModel> Participatings { get; set; } = new List<TeamIsParticipatingModel>();
    public IList<MatchModel> Matches { get; set; } = new List<MatchModel>();
}