namespace TournamentManager.Server.DAL.Entities;

public record MatchEntity(
    int Score1,
    int Score2,
    int Round,
    int Order) : EntityBase
{
    public Guid TournamentId { get; init; }
    public TournamentEntity? Tournament { get; init; }
    
    public Guid? Team1Id { get; init; }
    public TeamEntity? Team1 { get; init; }
    
    public Guid? Team2Id { get; init; }
    public TeamEntity? Team2 { get; init; }
}