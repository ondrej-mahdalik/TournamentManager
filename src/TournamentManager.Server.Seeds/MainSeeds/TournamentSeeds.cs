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
        20)
    {
        Id = Guid.Parse("83D0C8E4-E38B-43B2-8E64-3EBA5EC773FD"),
        CreatorId = UserSeeds.JohnDoe.Id,
        SportId = SportSeeds.Football.Id
    };

    public static readonly TournamentEntity NY2024 = new(
        "New Year's tournament",
        DateTime.Parse("1.1.2023 19:00"),
        "2023",
        true,
        true,
        false,
        54)
    {
        Id = Guid.Parse("C47AA197-EA47-4C5A-8347-5EB4D9CD8CFF"),
        CreatorId = UserSeeds.RexWalsh.Id,
        SportId = SportSeeds.Basketball.Id
    };


    public static void Seed(TournamentManagerDbContext dbContext)
    {
        dbContext.Tournaments.Add(Tournament1);
        dbContext.Tournaments.Add(NY2024);
    }
}