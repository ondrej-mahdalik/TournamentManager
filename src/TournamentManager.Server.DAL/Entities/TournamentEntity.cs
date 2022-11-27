namespace TournamentManager.Server.DAL.Entities;

public record TournamentEntity(string Name,
    DateTime Date,
    string? Description,
    bool IsPublic,
    bool IsApproved,
    bool InProgress,
    bool IsIndividual,
    int MaxAttendees,
    int NumOfRounds) : EntityBase
{
    public Guid? CreatorId { get; init; }
    public UserEntity? Creator { get; init; }
    
    public Guid? WinnerTeamId { get; init; }
    
    public TeamEntity? WinnerTeam { get; init; }
    
    public Guid? SportId { get; init; }
    public SportEntity? Sport { get; init; }
    
    public IList<TeamIsParticipatingEntity> Participatings { get; init; } = new List<TeamIsParticipatingEntity>();
    public IList<MatchEntity> Matches { get; init; } = new List<MatchEntity>();
}