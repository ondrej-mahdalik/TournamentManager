using TournamentManager.Server.DAL.Entities;

namespace TournamentManager.Server.Seeds.MainSeeds;

public static class UserIsInTeamSeeds
{
    public static readonly UserIsInTeamEntity RexInJohns = new()
    {
        Id = Guid.Parse("33650692-F179-4085-B991-52228D37A394"),
        IsApproved = true,
        TeamId = TeamSeeds.JohnsTeam.Id,
        UserId = UserSeeds.RexWalsh.Id,
    };

}