using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Shared.Models;

public record UserModel(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    bool IsAdministrator = false)
{
    [Key]
    public Guid Id { get; set; } = Id;
    
    public IList<TournamentModel> Tournaments { get; set; } = new List<TournamentModel>();
    public IList<TeamModel> TeamsAsLeader { get; set; } = new List<TeamModel>();
    public IList<UserIsInTeam> TeamsAsMember { get; set; } = new List<UserIsInTeam>();
}