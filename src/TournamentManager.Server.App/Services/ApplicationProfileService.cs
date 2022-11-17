using System.Security.Claims;
using Duende.IdentityServer.AspNetIdentity;
using Duende.IdentityServer.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using TournamentManager.Server.App.Models;
using TournamentManager.Common.Enums;

namespace TournamentManager.Server.App.Services;

public class ApplicationProfileService : ProfileService<ApplicationUser>
{
    public ApplicationProfileService(UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        : base(userManager, claimsFactory)
    {
        
    }

    protected override async Task GetProfileDataAsync(ProfileDataRequestContext context, ApplicationUser user)
    {
        // Name
        var nameClaim = context.Subject.FindAll(JwtClaimTypes.Name);
        context.IssuedClaims.AddRange(nameClaim);

        // Email
        var roleClaims = context.Subject.FindAll(JwtClaimTypes.Role);
        context.IssuedClaims.AddRange(roleClaims);
        
        // Main User Id
        var principal = await GetUserClaimsAsync(user);
        var id = (ClaimsIdentity?)principal.Identity;
        if (id is null)
            return;
        
        id.AddClaim(new Claim(CustomClaimTypes.MainUserId, user.MainUserId.ToString()));
        
        context.AddRequestedClaims(principal.Claims);
    }
}