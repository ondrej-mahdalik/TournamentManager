using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Common.Models;

public class TeamModel : ModelBase
{
    public TeamModel(string name,
        string? logoUrl, bool isPersonal)
    {
        Name = name;
        LogoUrl = logoUrl;
        IsPersonal = isPersonal;
    }
    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string Name { get; set; }
    public string? LogoUrl { get; set; }
    public bool IsPersonal { get; set; }
    
    public Guid? LeaderId { get; set; }
    
    public UserModel? Leader { get; set; }

    public IList<UserIsInTeamModel> Members { get; set; } = new List<UserIsInTeamModel>();

    public IList<MatchModel> Matches { get; set; } = new List<MatchModel>();

    public IList<TeamIsParticipatingModel> Participatings { get; set; } = new List<TeamIsParticipatingModel>();
    
    public IList<TournamentModel> WinnedTournaments { get; set; } = new List<TournamentModel>();
    public static TeamModel Empty => new(string.Empty, null, false);
}