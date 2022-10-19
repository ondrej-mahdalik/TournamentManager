﻿using TournamentManager.Server.Data;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.Seeds;

public static class UserSeeds
{
    public static readonly UserModel JohnDoe = new UserModel(
        Guid.Parse("817A00B4-6A3A-427E-B569-1C72342483A3"),
        "John",
        "Doe",
        "john.doe@gmail.com");
    
    // TODO User seeds

    public static void Seed(TournamentManagerDbContext dbContext)
    {
        dbContext.Users.Add(JohnDoe);
        dbContext.SaveChanges();
    }
}