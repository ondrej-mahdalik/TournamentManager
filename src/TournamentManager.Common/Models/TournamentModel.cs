using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Common.Models;

public record TournamentModel(
    [Required(ErrorMessage = "Tournament name is required")]
    [MinLength(5)]
    [MaxLength(50, ErrorMessage = "The maximum length is 50 characters")]
    [Display(Name = "Tournament Name")]
    string Name,
    
    [Required]
    [Display(Name = "Tournament Date")]
    DateTime Date,
    
    [Required]
    [Display(Name = "Display tournament publicly")]
    bool IsPublic,
    
    bool IsApproved,
    
    [Required]
    [Range(1, 1000, ErrorMessage = "The number of attendees has to be between 1 and 1000")]
    [Display(Name = "Maximum number of attendees")]
    int MaxAttendees,

    [Display(Name = "Description")]
    string? Description = null) : ModelBase
{
    public string Name { get; set; } = Name;
    public DateTime Date { get; set; } = Date;
    public bool IsPublic { get; set; } = IsPublic;
    public bool IsApproved { get; set; } = IsApproved;
    public int MaxAttendees { get; set; } = MaxAttendees;
    public string? Description { get; set; } = Description;

    public Guid? CreatorId { get; set; }
    public UserModel? Creator { get; set; }
    
    public Guid? WinnerTeamOverrideId { get; set; }
    public TeamModel? WinnerTeamOverride { get; set; }
    
    public Guid? SportId { get; set; }
    public SportModel? Sport { get; set; }
    
    public IList<TeamIsParticipatingModel> Participatings { get; set; } = new List<TeamIsParticipatingModel>();
    public IList<MatchModel> Matches { get; set; } = new List<MatchModel>();
    
    public static TournamentModel Empty => new(string.Empty, DateTime.Now, false, false, 1);
}