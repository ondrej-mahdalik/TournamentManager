using Microsoft.Extensions.DependencyInjection;
using TournamentManager.Server.Common.Installers;

namespace TournamentManager.Server.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInstaller<TInstaller>(this IServiceCollection serviceCollection)
        where TInstaller : IInstaller, new()
    {
        var installer = new TInstaller();
        installer.Install(serviceCollection);
    }
}
