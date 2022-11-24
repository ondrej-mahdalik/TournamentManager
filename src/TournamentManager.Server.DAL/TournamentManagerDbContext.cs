using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TournamentManager.Server.DAL.Entities;

namespace TournamentManager.Server.DAL;

public class TournamentManagerDbContext : DbContext
{
    public TournamentManagerDbContext(DbContextOptions<TournamentManagerDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }

    public DbSet<MatchEntity> Matches => Set<MatchEntity>();
    public DbSet<SportEntity> Sports => Set<SportEntity>();
    public DbSet<TeamIsParticipatingEntity> Participatings => Set<TeamIsParticipatingEntity>();
    public DbSet<TeamEntity> Teams => Set<TeamEntity>();
    public DbSet<TournamentEntity> Tournaments => Set<TournamentEntity>();
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<UserIsInTeamEntity> UsersIsInTeam => Set<UserIsInTeamEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SportEntity>()
            .HasMany(i => i.Tournaments)
            .WithOne(i => i.Sport)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<TeamEntity>(entity =>
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

        modelBuilder.Entity<MatchEntity>()
            .HasOne(i => i.Team2)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TournamentEntity>(entity =>
        {
            entity.HasMany(i => i.Matches)
                .WithOne(i => i.Tournament)
                .OnDelete(DeleteBehavior.ClientCascade);

            entity.HasMany(i => i.Participatings)
                .WithOne(i => i.Tournament)
                .OnDelete(DeleteBehavior.ClientCascade);
        });

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasMany(i => i.Tournaments)
                .WithOne(i => i.Creator)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasMany(i => i.TeamsAsLeader)
                .WithOne(i => i.Leader)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasMany(i => i.TeamsAsMember)
                .WithOne(i => i.User)
                .OnDelete(DeleteBehavior.ClientCascade);
        });
    }
}