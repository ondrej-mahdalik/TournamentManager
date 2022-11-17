using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TournamentManager.Server.BL.Facades;
using TournamentManager.Server.Common.Installers;
using TournamentManager.Server.DAL;
using TournamentManager.Server.DAL.UnitOfWork;

namespace TournamentManager.Server.BL.Installers;

public class BLInstaller : IInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        // Unit of work factory
        serviceCollection.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();
        
        // Facades
        serviceCollection.AddSingleton<MatchFacade>();
        serviceCollection.AddSingleton<SportFacade>();
        serviceCollection.AddSingleton<TeamFacade>();
        serviceCollection.AddSingleton<TeamIsParticipatingFacade>();
        serviceCollection.AddSingleton<TournamentFacade>();
        serviceCollection.AddSingleton<UserFacade>();
        serviceCollection.AddSingleton<UserIsInTeamFacade>();
        
        // AutoMapper
        serviceCollection.AddAutoMapper((serviceProvider, cfg) =>
        {
            cfg.AddCollectionMappers();

            var dbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<TournamentManagerDbContext>>();
            using var dbContext = dbContextFactory.CreateDbContext();
            cfg.UseEntityFrameworkCoreModel(dbContext.Model);
        }, typeof(BusinessLogic).Assembly);
    }
}