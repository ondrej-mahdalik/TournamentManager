using Microsoft.EntityFrameworkCore;

namespace TournamentManager.Server.DAL.Factories;

public class SQLiteDbContextFactory : IDbContextFactory<TournamentManagerDbContext>
{
    private readonly string _connectionString;

    public SQLiteDbContextFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public TournamentManagerDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<TournamentManagerDbContext>();
        optionsBuilder.UseSqlite(_connectionString);

        //optionsBuilder.LogTo(System.Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
#if DEBUG
        optionsBuilder.EnableSensitiveDataLogging();
#endif

        return new TournamentManagerDbContext(optionsBuilder.Options);
    }
}
