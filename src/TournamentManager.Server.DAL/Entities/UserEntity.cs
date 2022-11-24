namespace TournamentManager.Server.DAL.Entities;

public record UserEntity(
    DateTime RegisteredAt,
    string? ApplicationUserId = null,
    bool IsAdministrator = false) : EntityBase
{
    public IList<TournamentEntity> Tournaments { get; init; } = new List<TournamentEntity>();
    public IList<TeamEntity> TeamsAsLeader { get; init; } = new List<TeamEntity>();
    public IList<UserIsInTeamEntity> TeamsAsMember { get; init; } = new List<UserIsInTeamEntity>();
}