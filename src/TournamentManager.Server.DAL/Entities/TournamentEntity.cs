namespace TournamentManager.Server.DAL.Entities;

public record TournamentEntity(string Name,
    DateTime Date,
    string? Description,
    bool IsPublic,
    bool IsApproved,
    bool InProgress,
    int MaxAttendees) : EntityBase
{
    public Guid CreatorId { get; init; }
    public UserEntity? Creator { get; init; }
    
    public Guid? WinnerTeamOverrideId { get; init; }
    
    public TeamEntity? WinnerTeamOverride { get; init; }
    
    public Guid? SportId { get; init; }
    public SportEntity? Sport { get; init; }
    
    public IList<TeamIsParticipatingEntity> Participatings { get; init; } = new List<TeamIsParticipatingEntity>();
    public IList<MatchEntity> Matches { get; init; } = new List<MatchEntity>();
}