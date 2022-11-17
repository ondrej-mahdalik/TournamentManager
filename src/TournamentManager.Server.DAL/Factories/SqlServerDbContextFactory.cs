using Microsoft.EntityFrameworkCore;

namespace TournamentManager.Server.DAL.Factories;

public class SqlServerDbContextFactory : IDbContextFactory<TournamentManagerDbContext>
{
    private readonly string _connectionString;

    public SqlServerDbContextFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public TournamentManagerDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<TournamentManagerDbContext>();
        optionsBuilder.UseSqlServer(_connectionString);

        //optionsBuilder.LogTo(System.Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
#if DEBUG
        optionsBuilder.EnableSensitiveDataLogging();
#endif

        return new TournamentManagerDbContext(optionsBuilder.Options);
    }
}
