using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
    
    [ValidateNever]
    public UserModel? Leader { get; set; }
    
    [ValidateNever]
    public IList<UserIsInTeamModel> Members { get; set; } = new List<UserIsInTeamModel>();
    [ValidateNever]
    public IList<MatchModel> Matches { get; set; } = new List<MatchModel>();
    [ValidateNever]
    public IList<TeamIsParticipatingModel> Participatings { get; set; } = new List<TeamIsParticipatingModel>();
    public static TeamModel Empty => new(string.Empty, null, false);
}