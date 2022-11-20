using System.Security.Claims;
using Duende.IdentityServer.AspNetIdentity;
using Duende.IdentityServer.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using TournamentManager.Common.Enums;
using TournamentManager.Server.Auth.Models;

namespace TournamentManager.Server.App.Services;

public class ApplicationProfileService : ProfileService<ApplicationUser>
{
    public ApplicationProfileService(UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        : base(userManager, claimsFactory)
    {
        
    }

#pragma warning disable CS1998
    protected override async Task GetProfileDataAsync(ProfileDataRequestContext context, ApplicationUser user)
#pragma warning restore CS1998
    {
        // Name
        var nameClaim = context.Subject.FindAll(JwtClaimTypes.Name);
        context.IssuedClaims.AddRange(nameClaim);

        // Email
        var roleClaims = context.Subject.FindAll(JwtClaimTypes.Role);
        context.IssuedClaims.AddRange(roleClaims);
    }
}