using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TournamentManager.Server.Models;

namespace TournamentManager.Server.Data
{
    public class AuthorizationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public override DbSet<ApplicationUser> Users => Set<ApplicationUser>();

        public AuthorizationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
    }
}