using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TournamentManager.Server.DAL.Factories;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TournamentManagerDbContext>
{
    public TournamentManagerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TournamentManagerDbContext>();
        optionsBuilder.UseSqlite("Data Source=TournamentManager.Main;Cache=Shared");
        
        return new TournamentManagerDbContext(optionsBuilder.Options);
    }
}