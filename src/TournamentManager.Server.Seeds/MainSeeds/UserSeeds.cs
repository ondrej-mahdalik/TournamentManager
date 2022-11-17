using TournamentManager.Server.DAL;
using TournamentManager.Server.DAL.Entities;

namespace TournamentManager.Server.Seeds.MainSeeds;

public static class UserSeeds
{
    public static readonly UserEntity JohnDoe = new(
        true)
    {
        Id = Guid.Parse("817A00B4-6A3A-427E-B569-1C72342483A3")
    };
    
    // TODO User seeds

    public static void Seed(TournamentManagerDbContext dbContext)
    {
        dbContext.Users.Add(JohnDoe);
    }
}