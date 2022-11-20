namespace TournamentManager.Server.DAL.Entities;

public record SportEntity(
    string Name) : EntityBase
{
    public IList<TournamentEntity> Tournaments { get; init; } = new List<TournamentEntity>();
}