using System.Globalization;
using AutoMapper.Extensions.ExpressionMapping;
using TournamentManager.Server.DAL;
using TournamentManager.Server.DAL.Entities;

namespace TournamentManager.Server.Seeds.MainSeeds;

public static class TournamentSeeds
{
    public static readonly TournamentEntity Tournament1 = new(
        "Awesome Tournament",
        DateTime.Now,
        "This is a tournament",
        true,
        true,
        false,
        false,
        20,
        0)
    {
        Id = Guid.Parse("83D0C8E4-E38B-43B2-8E64-3EBA5EC773FD"),
        CreatorId = UserSeeds.JohnDoe.Id,
        SportId = SportSeeds.Football.Id
    };

    public static readonly TournamentEntity NY2023 = new(
        "New Year's tournament",
        DateTime.Parse("1/1/2023 12:00", CultureInfo.InvariantCulture),
        "2023",
        true,
        true,
        false,
        false,
        54,
        0)
    {
        Id = Guid.Parse("C47AA197-EA47-4C5A-8347-5EB4D9CD8CFF"),
        CreatorId = UserSeeds.RexWalsh.Id,
        SportId = SportSeeds.Basketball.Id
    };

    public static readonly TournamentEntity GoldenPuck = new(
        "Golden Puck",
        DateTime.Parse("5/16/2023 16:30", CultureInfo.InvariantCulture),
        "",
        false,
        false,
        false,
        false,
        20,
        0
        )
    {
        Id = Guid.Parse("8A36925B-2431-422D-B6EA-2DE6D2CB1772"),
        CreatorId = UserSeeds.Admin.Id,
        SportId = SportSeeds.IceHockey.Id
    };

    public static readonly TournamentEntity EffortBall = new(
        "Effort ball",
        DateTime.Parse("11/11/2024", CultureInfo.InvariantCulture),
        "Very much effort",
        true,
        true,
        false,
        true,
        20,
        0
        )
    {
        Id = Guid.Parse("F5006C84-4C9F-48F4-B5AC-E2C0730415E1"),
        CreatorId = UserSeeds.Admin.Id,
        SportId = SportSeeds.TableTennis.Id
    };

    public static readonly TournamentEntity NY2021 = new(
        "New years tournament",
        DateTime.Parse("1/1/2021", CultureInfo.InvariantCulture),
        "2021",
        true,
        true,
        false,
        false,
        54,
        0)
    {
        Id = Guid.Parse("11410468-759F-45D2-8DB1-EE03B9A9D8CF"),
        CreatorId = UserSeeds.RexWalsh.Id,
        SportId = SportSeeds.Basketball.Id,
        WinnerTeamId = TeamSeeds.JohnsTeam.Id
    };

    public static readonly TournamentEntity Active = new(
        "Active",
        DateTime.Now - TimeSpan.FromHours(1),
        "",
        true,
        true,
        true,
        false,
        20,
        0)
    {
        Id = Guid.Parse("CA46F6A6-D57C-48F5-9322-CCD8B0B93E20"),
        CreatorId = UserSeeds.Admin.Id,
        SportId = SportSeeds.Football.Id
    };


    public static void Seed(TournamentManagerDbContext dbContext)
    {
        dbContext.Tournaments.Add(Tournament1);
        dbContext.Tournaments.Add(NY2023);
        dbContext.Tournaments.Add(GoldenPuck);
        dbContext.Tournaments.Add(EffortBall);
        dbContext.Tournaments.Add(NY2021);
        dbContext.Tournaments.Add(Active);
    }
}