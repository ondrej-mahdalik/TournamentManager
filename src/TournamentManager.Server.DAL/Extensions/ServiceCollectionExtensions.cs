using Microsoft.Extensions.DependencyInjection;
namespace TournamentManager.Server.DAL.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInstaller<TInstaller>(this IServiceCollection serviceCollection, string connectionString,
        bool skipMigrationAndSeedDemoData)
        where TInstaller : IInstallerWithConnectionString, new()
    {
        var installer = new TInstaller();
        installer.Install(serviceCollection, connectionString, skipMigrationAndSeedDemoData);
    }
}
