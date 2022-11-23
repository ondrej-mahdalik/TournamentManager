using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Common.Models;

public class UserModel : ModelBase
{
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public bool IsAdministrator { get; set; }
    
    public string? ApplicationUserId { get; set; }
    
    public IList<TournamentModel> Tournaments { get; set; } = new List<TournamentModel>();
    public IList<TeamModel> TeamsAsLeader { get; set; } = new List<TeamModel>();
    public IList<UserIsInTeamModel> TeamsAsMember { get; set; } = new List<UserIsInTeamModel>();

    public UserModel() : this(string.Empty) {}
    public UserModel(string? Email = null,
        string? FirstName = null,
        string? LastName = null,
        string? ProfilePictureUrl = null,
        bool IsAdministrator = false)
    {
        this.Email = Email;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.ProfilePictureUrl = ProfilePictureUrl;
        this.IsAdministrator = IsAdministrator;
    }
    public void Deconstruct(out string? Email, out string? FirstName, out string? LastName, out string? ProfilePictureUrl, out bool IsAdministrator)
    {
        Email = this.Email;
        FirstName = this.FirstName;
        LastName = this.LastName;
        ProfilePictureUrl = this.ProfilePictureUrl;
        IsAdministrator = this.IsAdministrator;
    }
}