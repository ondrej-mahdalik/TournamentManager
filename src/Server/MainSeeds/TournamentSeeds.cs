using TournamentManager.Server.Data;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.MainSeeds;

public static class TournamentSeeds
{
    public static readonly TournamentModel Tournament1 = new(
        "Awesome Tournament",
        DateTime.Now,
        "This is a tournament",
        true,
        true,
        20)
    {
        Id = Guid.Parse("83D0C8E4-E38B-43B2-8E64-3EBA5EC773FD"),
        CreatorId = UserSeeds.JohnDoe.Id,
        SportId = SportSeeds.Football.Id
    };

    public static void Seed(TournamentManagerDbContext dbContext)
    {
        dbContext.Tournaments.Add(Tournament1);
    }
}