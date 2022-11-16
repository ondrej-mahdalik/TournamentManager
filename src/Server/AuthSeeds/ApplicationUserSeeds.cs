using System.Security.Claims;
using Duende.IdentityServer.Test;
using IdentityModel;
using TournamentManager.Server.MainSeeds;
using TournamentManager.Shared.Enums;

namespace TournamentManager.Server.AuthSeeds;

public class ApplicationUserSeeds
{
    public static List<TestUser> Users = new()
    {
        new TestUser
        {
            SubjectId = "1",
            Username = "john.doe@gmail.com",
            Password = "john",
            Claims =
            {
                new Claim(JwtClaimTypes.Name, "John Doe"),
                new Claim(JwtClaimTypes.GivenName, "John"),
                new Claim(JwtClaimTypes.FamilyName, "Doe"),
                new Claim(JwtClaimTypes.Email, "john.doe@gmail.com"),
                new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean)
            }
        }
    };
}