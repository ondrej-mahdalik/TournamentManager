using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Common.Models;

public class TeamModel : ModelBase
{
    public TeamModel(string name,
        string? logoUrl, bool isPersonal)
    {
        this.Name = name;
        this.LogoUrl = logoUrl;
        this.IsPersonal = isPersonal;
    }
    public string Name { get; set; }
    public string? LogoUrl { get; set; }
    public bool IsPersonal { get; set; }
    
    public Guid? LeaderId { get; set; }
    public UserModel? Leader { get; set; }
    
    public IList<UserIsInTeamModel> Members { get; set; } = new List<UserIsInTeamModel>();
    public IList<MatchModel> Matches { get; set; } = new List<MatchModel>();
    public IList<TeamIsParticipatingModel> Participatings { get; set; } = new List<TeamIsParticipatingModel>();
    public static TeamModel Empty => new(string.Empty, null, false);
}