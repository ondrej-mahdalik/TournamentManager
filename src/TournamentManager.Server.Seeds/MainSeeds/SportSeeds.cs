using TournamentManager.Server.DAL;
using TournamentManager.Server.DAL.Entities;

namespace TournamentManager.Server.Seeds.MainSeeds;

public static class SportSeeds
{
    public static readonly SportEntity Football = new("Football")
    {
        Id = Guid.Parse("3F20807D-34FC-49E5-BAE0-AF64D984D7CF")
    };

    public static void Seed(TournamentManagerDbContext dbContext)
    {
        dbContext.Sports.Add(Football);
    }
}