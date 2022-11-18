namespace TournamentManager.Server.DAL.Entities;

public record TournamentEntity(string Name,
    DateTime Date,
    string? Description,
    bool IsPublic,
    bool IsApproved,
    int MaxAttendees) : EntityBase
{
    public Guid CreatorId { get; set; }
    public UserEntity? Creator { get; set; }
    
    public Guid? WinnerTeamOverrideId { get; set; }
    
    public TeamEntity? WinnerTeamOverride { get; set; }
    
    public Guid SportId { get; set; }
    public SportEntity? Sport { get; set; }
    
    public IList<TeamIsParticipatingEntity> Participatings { get; set; } = new List<TeamIsParticipatingEntity>();
    public IList<MatchEntity> Matches { get; set; } = new List<MatchEntity>();
}