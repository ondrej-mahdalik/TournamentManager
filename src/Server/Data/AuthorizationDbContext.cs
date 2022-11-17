using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TournamentManager.Server.App.Models;

namespace TournamentManager.Server.App.Data
{
    public class AuthorizationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public AuthorizationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
    }
}