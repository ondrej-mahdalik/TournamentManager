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

    public static readonly ApplicationUser AlenaVaf = new()
    {
        Id = Guid.Parse("D9152C90-73E8-40B4-845B-E03CF1CAD133").ToString(),
        UserName = "vaf@gmail.com",
        FirstName = "Alena",
        LastName = "Vaf",
        Email = "vaf@gmail.com",
        EmailConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString("D")
    };

    public static readonly ApplicationUser PavelWolf = new()
    {
        Id = Guid.Parse("4A805AFF-EFBF-4429-9D8D-7C6EF7CD5F40").ToString(),
        UserName = "vlkzlesa@seznam.cz",
        FirstName = "Pavel",
        LastName = "Wolf",
        Email = "vlkzlesa@seznam.cz",
        EmailConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString("D")
    };

    public static readonly ApplicationUser DarthVader = new()
    {
        Id = Guid.Parse("7BBE3851-A3DD-4C27-9999-EC14E20829FB"),
        UserName = "darth@vader.com",
        FirstName = "Darth",
        LastName = "Vader",
        Email = "darth@vader.com",
        EmailConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString("D")
    };

    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        
        await userManager.CreateAsync(Admin, "Pass123$");
        await userManager.CreateAsync(User1, "Pass123$");
        await userManager.CreateAsync(RexWalsh, "Pass123$");
        await userManager.CreateAsync(AlenaVaf, "Pass123$");
        await userManager.CreateAsync(PavelWolf, "Pass123$");
        await userManager.CreateAsync(DarthVader, "Pass123$");
    }
}