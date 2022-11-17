using Microsoft.Extensions.DependencyInjection;

namespace TournamentManager.Server.DAL.Extensions;

public interface IInstallerWithConnectionString
{
    public void Install(IServiceCollection serviceCollection, string connectionString);
}