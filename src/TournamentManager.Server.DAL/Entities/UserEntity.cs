namespace TournamentManager.Server.DAL.Entities;

public record UserEntity(
    string? ApplicationUserId = null,
    bool IsAdministrator = false) : EntityBase
{
    public IList<TournamentEntity> Tournaments { get; set; } = new List<TournamentEntity>();
    public IList<TeamEntity> TeamsAsLeader { get; set; } = new List<TeamEntity>();
    public IList<UserIsInTeamEntity> TeamsAsMember { get; set; } = new List<UserIsInTeamEntity>();
}