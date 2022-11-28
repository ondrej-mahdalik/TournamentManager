using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using TournamentManager.Server.DAL;
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

    public static readonly UserIsInTeamEntity StormTrooper1InGalacticTroopers = new()
    {
        Id = Guid.Parse("335F339F-AC2B-4B73-8F6A-5A5E4CC414A6"),
        IsApproved = true,
        TeamId = TeamSeeds.GalacticTroopers.Id,
        UserId = UserSeeds.StormTrooper1.Id
    };

    public static readonly UserIsInTeamEntity StormTrooper2InGalacticTroopers = new()
    {
        Id = Guid.Parse("679622AB-F6DD-4907-9FF8-9F9CB29B2C70"),
        IsApproved = true,
        TeamId = TeamSeeds.GalacticTroopers.Id,
        UserId = UserSeeds.StormTrooper2.Id
    };

    public static readonly UserIsInTeamEntity StormTrooper3InGalacticTroopers = new()
    {
        Id = Guid.Parse("166DF1B0-4A5C-4464-A686-CDBB5AEE9155"),
        IsApproved = true,
        TeamId = TeamSeeds.GalacticTroopers.Id,
        UserId = UserSeeds.StormTrooper3.Id
    };

    public static void Seed(TournamentManagerDbContext dbContext)
    {
        dbContext.UsersIsInTeam.Add(RexInJohns);
        dbContext.UsersIsInTeam.Add(AlenaInJohns);
        dbContext.UsersIsInTeam.Add(StormTrooper1InGalacticTroopers);
        dbContext.UsersIsInTeam.Add(StormTrooper2InGalacticTroopers);
        dbContext.UsersIsInTeam.Add(StormTrooper3InGalacticTroopers);
    }

}