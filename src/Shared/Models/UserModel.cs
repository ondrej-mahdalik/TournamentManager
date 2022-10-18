using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Shared.Models;

public record UserModel(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email)
{
    [Key]
    public Guid UserId { get; set; } = UserId;
    
    public IList<TournamentModel> Tournaments { get; set; } = new List<TournamentModel>();
    public IList<TeamModel> TeamsAsLeader { get; set; } = new List<TeamModel>();
    public IList<UserIsInTeam> TeamsAsMember { get; set; } = new List<UserIsInTeam>();
}