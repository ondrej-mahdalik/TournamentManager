using Microsoft.EntityFrameworkCore;

namespace TournamentManager.Server.DAL.UnitOfWork;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<TournamentManagerDbContext> _dbContextFactory;

    public UnitOfWorkFactory(IDbContextFactory<TournamentManagerDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public IUnitOfWork Create()
    {
        return new UnitOfWork(_dbContextFactory.CreateDbContext());
    }
}
