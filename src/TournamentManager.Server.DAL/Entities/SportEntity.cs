namespace TournamentManager.Server.DAL.Entities;

public record SportEntity(
    string Name) : EntityBase
{
    public IList<TournamentEntity> Tournaments { get; set; } = new List<TournamentEntity>();
}