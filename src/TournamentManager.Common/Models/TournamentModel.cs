using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Common.Models;

public record TournamentModel(string Name,
    DateTime Date,
    bool IsPublic,
    bool IsApproved,
    int MaxPlayers,
    Guid CreatorId,
    Guid SportId,
    string? Description = null) : ModelBase
{
    public string Name { get; set; } = Name;
    public DateTime Date { get; set; } = Date;
    public bool IsPublic { get; set; } = IsPublic;
    public bool IsApproved { get; set; } = IsApproved;
    public int MaxPlayers { get; set; } = MaxPlayers;
    public string? Description { get; set; } = Description;

    public Guid CreatorId { get; set; } = CreatorId;
    public UserModel? Creator { get; set; }
    
    public Guid? WinnerTeamOverrideId { get; set; }
    
    public TeamModel? WinnerTeamOverride { get; set; }

    public Guid SportId { get; set; } = SportId;
    public SportModel? Sport { get; set; }
    
    public IList<TeamIsParticipatingModel> Participatings { get; set; } = new List<TeamIsParticipatingModel>();
    public IList<MatchModel> Matches { get; set; } = new List<MatchModel>();
}