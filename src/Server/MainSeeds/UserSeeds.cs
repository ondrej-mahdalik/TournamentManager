using TournamentManager.Server.App.Data;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.App.MainSeeds;

public static class UserSeeds
{
    public static readonly UserModel JohnDoe = new(
        "john.doe",
        "John",
        "Doe",
        "john.doe@gmail.com")
    {
        Id = Guid.Parse("817A00B4-6A3A-427E-B569-1C72342483A3")
    };
    
    // TODO User seeds

    public static void Seed(TournamentManagerDbContext dbContext)
    {
        dbContext.Users.Add(JohnDoe);
    }
}