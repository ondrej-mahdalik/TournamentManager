using Microsoft.EntityFrameworkCore;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.Data;

public class MainDbInitializer
{
    public static void Initialize(TournamentManagerDbContext dbContext, bool seedDemoData = false)
    {
        if (seedDemoData)
        {
            dbContext.Database.EnsureCreated();

            if (dbContext.Users.Any())
                return;

            var users = new UserModel[]
            {
                new UserModel(Guid.Parse("0DA8E423-8EF4-4382-B1CE-0E36B9C9AEBE"),
                    "John",
                    "Doe",
                    "john.doe@gmail.com")
            };

            // TODO Seed more models

            foreach (var user in users)
            {
                dbContext.Users.Add(user);
            }

            dbContext.SaveChanges();
        }
        else
        {
            // dbContext.Database.Migrate();
        }
    }
}