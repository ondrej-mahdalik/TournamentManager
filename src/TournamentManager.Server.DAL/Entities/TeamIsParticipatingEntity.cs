namespace TournamentManager.Server.DAL.Entities;

public record TeamIsParticipatingEntity(bool Approved,
    DateTime CreatedOn) : EntityBase
{
    public Guid TeamId { get; init; }
    public TeamEntity? Team { get; init; }

    public Guid TournamentId { get; init; }
    public TournamentEntity? Tournament { get; init; }
}