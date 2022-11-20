namespace TournamentManager.Server.DAL.Entities;

public record UserIsInTeamEntity(bool IsApproved = false) : EntityBase
{
    public Guid UserId { get; init; }
    public UserEntity? User { get; init; }
    
    public Guid TeamId { get; init; }
    public TeamEntity? Team { get; init; }
}