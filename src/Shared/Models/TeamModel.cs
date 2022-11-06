using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Shared.Models;

public record TeamModel(Guid Id,
    string Name,
    string? LogoUrl)
{
    [Key]
    public Guid Id { get; set; } = Id;
    
    public Guid LeaderId { get; set; }
    public UserModel? Leader { get; set; }
    
    public IList<UserIsInTeam> Members { get; set; } = new List<UserIsInTeam>();
    public IList<MatchModel> Matches { get; set; } = new List<MatchModel>();
    public IList<TeamIsParticipatingModel> Participatings { get; set; } = new List<TeamIsParticipatingModel>();
}