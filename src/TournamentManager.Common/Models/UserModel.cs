using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Common.Models;

public record UserModel(
    string? Email = null,
    string? FirstName = null,
    string? LastName = null,
    string? ProfilePictureUrl = null,
    bool IsAdministrator = false) : ModelBase
{
    public string? Email { get; set; } = Email;
    public string? FirstName { get; set; } = FirstName;
    public string? LastName { get; set; } = LastName;
    public string? ProfilePictureUrl { get; set; } = ProfilePictureUrl;
    public bool IsAdministrator { get; set; } = IsAdministrator;
    
    public string? ApplicationUserId { get; set; }
    
    public IList<TournamentModel> Tournaments { get; set; } = new List<TournamentModel>();
    public IList<TeamModel> TeamsAsLeader { get; set; } = new List<TeamModel>();
    public IList<UserIsInTeamModel> TeamsAsMember { get; set; } = new List<UserIsInTeamModel>();

    public UserModel() : this(string.Empty) {}
}