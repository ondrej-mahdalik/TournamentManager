using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Shared.Models;

public record UserModel(
    bool IsAdministrator = false) : ModelBase
{
    public IList<TournamentModel> Tournaments { get; set; } = new List<TournamentModel>();
    public IList<TeamModel> TeamsAsLeader { get; set; } = new List<TeamModel>();
    public IList<UserIsInTeamModel> TeamsAsMember { get; set; } = new List<UserIsInTeamModel>();
}