namespace TournamentManager.Server.DAL.Entities;

public record MatchEntity(
    int Score1,
    int Score2,
    int Round,
    int Order) : EntityBase
{
    public Guid TournamentId { get; set; }
    public TournamentEntity? Tournament { get; set; }
    
    public Guid? Team1Id { get; set; }
    public TeamEntity? Team1 { get; set; }
    
    public Guid? Team2Id { get; set; }
    public TeamEntity? Team2 { get; set; }
}