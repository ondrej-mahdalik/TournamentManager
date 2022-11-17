namespace TournamentManager.Server.DAL.Entities;

public record TeamIsParticipatingEntity(bool Approved) : EntityBase
{
    public Guid TeamId { get; set; }
    public TeamEntity? Team { get; set; }

    public Guid TournamentId { get; set; }
    public TournamentEntity? Tournament { get; set; }
}