using TournamentManager.Server.Auth.Seeds;
using TournamentManager.Server.DAL;
using TournamentManager.Server.DAL.Entities;

namespace TournamentManager.Server.Seeds.MainSeeds;

public static class UserSeeds
{
    public static readonly UserEntity Admin = new(
        DateTime.Now,
        ApplicationUserSeeds.Admin.Id,
        true)
    {
        Id = Guid.Parse("13F7D8CC-EB4D-4F32-A990-4444BC677A80")
    };
    
    public static readonly UserEntity JohnDoe = new(
        DateTime.Now - TimeSpan.FromDays(1),
        ApplicationUserSeeds.User1.Id)
    {
        Id = Guid.Parse("817A00B4-6A3A-427E-B569-1C72342483A3")
    };
    
    // TODO User seeds

    public static void Seed(TournamentManagerDbContext dbContext)
    {
        dbContext.Users.Add(JohnDoe);
        dbContext.Users.Add(Admin);
    }
}