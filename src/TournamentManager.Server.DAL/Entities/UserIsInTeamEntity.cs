namespace TournamentManager.Server.DAL.Entities;

public record UserIsInTeamEntity : EntityBase
{
    public Guid UserId { get; set; }
    public UserEntity? User { get; set; }
    
    public Guid TeamId { get; set; }
    public TeamEntity? Team { get; set; }
}