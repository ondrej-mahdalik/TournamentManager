using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Common.Models;

public class TournamentModel : ModelBase, IValidatableObject
{
    public TournamentModel(string name,
        DateTime date,
        bool isPublic,
        bool isApproved,
        bool inProgress,
        int maxAttendees,
        string? description = null)
    {
        Name = name;
        Date = date;
        IsPublic = isPublic;
        IsApproved = isApproved;
        InProgress = inProgress;
        MaxAttendees = maxAttendees;
        Description = description;
    }
    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public bool IsPublic { get; set; }
    public bool IsApproved { get; set; }
    public bool InProgress { get; set; }
    
    [Range(1, 1000, ErrorMessage = "The number of attendees has to be between 1 and 1000")]
    public int MaxAttendees { get; set; }
    
    [MaxLength(500)]
    public string? Description { get; set; }

    public Guid? CreatorId { get; set; }
    public UserModel? Creator { get; set; }
    
    public Guid? WinnerTeamId { get; set; }
    public TeamModel? WinnerTeam { get; set; }
    
    public Guid? SportId { get; set; }
    public SportModel? Sport { get; set; }
    
    public IList<TeamIsParticipatingModel> Participatings { get; set; } = new List<TeamIsParticipatingModel>();
    public IList<MatchModel> Matches { get; set; } = new List<MatchModel>();
    
    public static TournamentModel Empty => new(string.Empty, DateTime.Now, false, false, false, 1);
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Date < DateTime.Today)
        {
            yield return new ValidationResult("The date of the tournament has to be in the future", new[] { nameof(Date) });
        }
    }
}