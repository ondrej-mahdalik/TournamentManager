using Microsoft.EntityFrameworkCore;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.Data;

public class TournamentManagerDbContext : DbContext
{
    public TournamentManagerDbContext(DbContextOptions<TournamentManagerDbContext> dbContextOptions) : base(dbContextOptions) { }

    public DbSet<MatchModel> Matches => Set<MatchModel>();
    public DbSet<SportModel> Sports => Set<SportModel>();
    public DbSet<TeamIsParticipatingModel> Participatings => Set<TeamIsParticipatingModel>();
    public DbSet<TeamModel> Teams => Set<TeamModel>();
    public DbSet<TournamentModel> Tournaments => Set<TournamentModel>();
    public DbSet<UserModel> Users => Set<UserModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SportModel>()
            .HasMany(i => i.Tournaments)
            .WithOne(i => i.Sport)
            .OnDelete(DeleteBehavior.NoAction); // Cannot delete a sport with assigned tournaments

        modelBuilder.Entity<TeamModel>(entity =>
        {
            entity.HasMany(i => i.Matches)
                .WithOne(i => i.Team1)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasMany(i => i.Members)
                .WithOne(i => i.Team)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(i => i.Participatings)
                .WithOne(i => i.Team)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MatchModel>()
            .HasOne(i => i.Team2)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TournamentModel>(entity =>
        {
            entity.HasMany(i => i.Matches)
                .WithOne(i => i.Tournament)
                .OnDelete(DeleteBehavior.ClientCascade); // TODO

            entity.HasMany(i => i.Participatings)
                .WithOne(i => i.Tournament)
                .OnDelete(DeleteBehavior.ClientCascade); // TODO
        });

        modelBuilder.Entity<UserModel>(entity =>
        {
            entity.HasMany(i => i.Tournaments)
                .WithOne(i => i.Creator)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(i => i.TeamsAsLeader)
                .WithOne(i => i.Leader)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(i => i.TeamsAsMember)
                .WithOne(i => i.User)
                .OnDelete(DeleteBehavior.ClientCascade); // TODO
        });
    }
}