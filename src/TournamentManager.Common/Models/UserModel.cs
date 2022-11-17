using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Common.Models;

public record UserModel(
    string Email,
    string? FirstName = null,
    string? LastName = null,
    string? ProfilePictureUrl = null,
    bool IsAdministrator = false) : ModelBase
{
    public IList<TournamentModel> Tournaments { get; set; } = new List<TournamentModel>();
    public IList<TeamModel> TeamsAsLeader { get; set; } = new List<TeamModel>();
    public IList<UserIsInTeamModel> TeamsAsMember { get; set; } = new List<UserIsInTeamModel>();

    public UserModel() : this(string.Empty) {}
}