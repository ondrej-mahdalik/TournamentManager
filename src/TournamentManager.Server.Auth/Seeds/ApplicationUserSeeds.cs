using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TournamentManager.Server.Auth.Models;

namespace TournamentManager.Server.Auth.Seeds;

public static class ApplicationUserSeeds
{
    public static readonly ApplicationUser Admin = new()
    {
        Id = Guid.Parse("3C1D10EE-22D9-4BC9-BAA0-A2B13887206C").ToString(),
        UserName = "admin@localhost.com",
        Email = "admin@localhost.com",
        EmailConfirmed = true,
        FirstName = "Administrator",
        PhoneNumber = "1234567890",
        PhoneNumberConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString("D")
    };

    public static readonly ApplicationUser User1 = new()
    {
        Id = Guid.Parse("A0941FA5-DEFF-45C3-ADE7-96475B77FC47").ToString(),
        UserName = "john.doe@gmail.com",
        FirstName = "John",
        LastName = "Doe",
        Email = "john.doe@gmail.com",
        EmailConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString("D")
    };

    public static readonly ApplicationUser RexWalsh = new()
    {
        Id = Guid.Parse("F808427F-92DC-4FEE-B76D-1B1F3F7FD88D").ToString(),
        UserName = "rex@walsh.com",
        FirstName = "Rex",
        LastName = "Walsh",
        Email = "rex@walsh.com",
        EmailConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString("D")
    };
    
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        
        await userManager.CreateAsync(Admin, "Pass123$");
        await userManager.CreateAsync(User1, "Pass123$");
        await userManager.CreateAsync(RexWalsh, "Pass123$");
    }
}