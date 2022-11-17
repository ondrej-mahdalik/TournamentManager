using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TournamentManager.Server.DAL.Extensions;
using TournamentManager.Server.DAL.Factories;

namespace TournamentManager.Server.DAL.Installers
{
    public class DALSQLServerInstaller : IInstallerWithConnectionString
    {
        public void Install(IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddSingleton<IDbContextFactory<TournamentManagerDbContext>>(_ =>
                new SqlServerDbContextFactory(connectionString));
        }
    }
}
