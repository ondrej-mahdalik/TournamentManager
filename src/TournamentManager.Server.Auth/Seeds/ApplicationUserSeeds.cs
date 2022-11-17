using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TournamentManager.Server.Auth.Models;
using TournamentManager.Server.Seeds.MainSeeds;

namespace TournamentManager.Server.Auth.Seeds;

public static class ApplicationUserSeeds
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        
        var admin = new ApplicationUser
        {
            UserName = "admin@localhost.com",
            Email = "admin@localhost.com",
            EmailConfirmed = true,
            PhoneNumber = "1234567890",
            PhoneNumberConfirmed = true,
            MainUserId = UserSeeds.Admin.Id,
            SecurityStamp = Guid.NewGuid().ToString("D")
        };
        var user1 = new ApplicationUser
        {
            UserName = "john.doe@gmail.com",
            Email = "john.doe@gmail.com",
            EmailConfirmed = true,
            MainUserId = UserSeeds.JohnDoe.Id,
            SecurityStamp = Guid.NewGuid().ToString("D")
        };

        await userManager.CreateAsync(admin, "Pass123$");
        await userManager.CreateAsync(user1, "Pass123$");
        
        await userManager.AddClaimsAsync(admin, new List<Claim>
        {
            new Claim(JwtClaimTypes.Name, "Admin"),
            new Claim(JwtClaimTypes.GivenName, "Admin"),
            new Claim(JwtClaimTypes.FamilyName, "Admin")
        });

        await userManager.AddClaimsAsync(user1, new List<Claim>
        {
            new Claim(JwtClaimTypes.Name, "John Doe"),
            new Claim(JwtClaimTypes.GivenName, "John"),
            new Claim(JwtClaimTypes.FamilyName, "Doe")
        });
    }
}