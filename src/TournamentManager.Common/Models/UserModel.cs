using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Common.Models;

public class UserModel : ModelBase
{
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public bool IsAdministrator { get; set; }
    public DateTime RegisteredAt { get; set; }
    public string? ApplicationUserId { get; set; }
    
    public IList<TournamentModel> Tournaments { get; set; } = new List<TournamentModel>();
    public IList<TeamModel> TeamsAsLeader { get; set; } = new List<TeamModel>();
    public IList<UserIsInTeamModel> TeamsAsMember { get; set; } = new List<UserIsInTeamModel>();

    public UserModel() : this(string.Empty) {}
    public UserModel(string? email = null,
        string? firstName = null,
        string? lastName = null,
        string? phoneNumber = null,
        string? profilePictureUrl = null,
        bool isAdministrator = false,
        DateTime? registeredAt = null)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        ProfilePictureUrl = profilePictureUrl;
        IsAdministrator = isAdministrator;
        RegisteredAt = registeredAt ?? DateTime.Now;
    }
}