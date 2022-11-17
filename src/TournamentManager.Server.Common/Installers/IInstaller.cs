using Microsoft.Extensions.DependencyInjection;

namespace TournamentManager.Server.Common.Installers;

public interface IInstaller
{
    void Install(IServiceCollection serviceCollection);
}