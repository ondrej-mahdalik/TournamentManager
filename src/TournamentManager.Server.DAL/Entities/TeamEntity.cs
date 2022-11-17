namespace TournamentManager.Server.DAL.Entities;

public record TeamEntity(string Name,
    string? LogoUrl) : EntityBase
{
    public Guid LeaderId { get; set; }
    public UserEntity? Leader { get; set; }
    
    public IList<UserIsInTeamEntity> Members { get; set; } = new List<UserIsInTeamEntity>();
    public IList<MatchEntity> Matches { get; set; } = new List<MatchEntity>();
    public IList<TeamIsParticipatingEntity> Participatings { get; set; } = new List<TeamIsParticipatingEntity>();
}