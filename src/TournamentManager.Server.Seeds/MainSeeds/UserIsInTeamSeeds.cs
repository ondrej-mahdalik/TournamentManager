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

    public static readonly UserIsInTeamEntity AlenaInJohns = new()
    {
        Id = Guid.Parse("02275EE5-DFE1-4737-AEB4-EDCC5344071D"),
        IsApproved = false,
        TeamId = TeamSeeds.JohnsTeam.Id,
        UserId = UserSeeds.AlenaVaf.Id,
    };

}