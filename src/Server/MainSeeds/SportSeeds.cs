using TournamentManager.Server.Data;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.MainSeeds;

public static class SportSeeds
{
    public static readonly SportModel Football = new(Guid.Parse("3F20807D-34FC-49E5-BAE0-AF64D984D7CF"),
        "Football");

    public static void Seed(TournamentManagerDbContext dbContext)
    {
        dbContext.Sports.Add(Football);
    }
}