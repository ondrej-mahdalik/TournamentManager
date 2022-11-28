using TournamentManager.Server.DAL;
using TournamentManager.Server.DAL.Entities;
namespace TournamentManager.Server.Seeds.MainSeeds;

public static class TeamIsParticipatingSeeds
{
    public static readonly TeamIsParticipatingEntity Team1InAwesomeTournament = new(false,
        DateTime.Now - TimeSpan.FromDays(2))
    {
        TournamentId = TournamentSeeds.Tournament1.Id,
        TeamId = TeamSeeds.JohnsTeam.Id
    };

    public static readonly TeamIsParticipatingEntity Team2InAwesomeTournament = new(false,
        DateTime.Now - TimeSpan.FromDays(1))
    {
        TournamentId = TournamentSeeds.Tournament1.Id,
        TeamId = TeamSeeds.Team1.Id
    };
    public static readonly TeamIsParticipatingEntity Team3InAwesomeTournament = new(false,
        DateTime.Now - TimeSpan.FromHours(12))
    {
        TournamentId = TournamentSeeds.Tournament1.Id,
        TeamId = TeamSeeds.Team2.Id
    };
    public static readonly TeamIsParticipatingEntity Team4InAwesomeTournament = new(false,
        DateTime.Now - TimeSpan.FromHours(8))
    {
        TournamentId = TournamentSeeds.Tournament1.Id,
        TeamId = TeamSeeds.Team3.Id
    };
    public static readonly TeamIsParticipatingEntity Team5InAwesomeTournament = new(false,
        DateTime.Now - TimeSpan.FromHours(3))
    {
        TournamentId = TournamentSeeds.Tournament1.Id,
        TeamId = TeamSeeds.Team4.Id
    };
    public static readonly TeamIsParticipatingEntity Team6InAwesomeTournament = new(false,
        DateTime.Now - TimeSpan.FromMinutes(30))
    {
        TournamentId = TournamentSeeds.Tournament1.Id,
        TeamId = TeamSeeds.Team5.Id
    };
    public static readonly TeamIsParticipatingEntity JohsInNYTournament = new(true,
        DateTime.Parse("25 October 2022 15:40"))
    {
        TournamentId = TournamentSeeds.NY2024.Id,
        TeamId = TeamSeeds.JohnsTeam.Id
    };
    
    
    public static readonly TeamIsParticipatingEntity Team7InAwesomeTournament = new(false,
        DateTime.Now - TimeSpan.FromDays(2))
    {
        TournamentId = TournamentSeeds.Tournament1.Id,
        TeamId = TeamSeeds.JohnsTeam2.Id
    };

    public static readonly TeamIsParticipatingEntity Team8InAwesomeTournament = new(false,
        DateTime.Now - TimeSpan.FromDays(1))
    {
        TournamentId = TournamentSeeds.Tournament1.Id,
        TeamId = TeamSeeds.Team6.Id
    };
    public static readonly TeamIsParticipatingEntity Team9InAwesomeTournament = new(false,
        DateTime.Now - TimeSpan.FromHours(12))
    {
        TournamentId = TournamentSeeds.Tournament1.Id,
        TeamId = TeamSeeds.Team7.Id
    };
    public static readonly TeamIsParticipatingEntity Team10InAwesomeTournament = new(false,
        DateTime.Now - TimeSpan.FromHours(8))
    {
        TournamentId = TournamentSeeds.Tournament1.Id,
        TeamId = TeamSeeds.Team8.Id
    };
    public static readonly TeamIsParticipatingEntity Team11InAwesomeTournament = new(false,
        DateTime.Now - TimeSpan.FromHours(3))
    {
        TournamentId = TournamentSeeds.Tournament1.Id,
        TeamId = TeamSeeds.Team9.Id
    };
    public static readonly TeamIsParticipatingEntity Team12InAwesomeTournament = new(false,
        DateTime.Now - TimeSpan.FromMinutes(30))
    {
        TournamentId = TournamentSeeds.Tournament1.Id,
        TeamId = TeamSeeds.Team10.Id
    };


    public static void Seed(TournamentManagerDbContext dbContext)
    {
        dbContext.Participatings.Add(Team1InAwesomeTournament);
        dbContext.Participatings.Add(Team2InAwesomeTournament);
        dbContext.Participatings.Add(Team3InAwesomeTournament);
        dbContext.Participatings.Add(Team4InAwesomeTournament);
        dbContext.Participatings.Add(Team5InAwesomeTournament);
        dbContext.Participatings.Add(Team6InAwesomeTournament);
        dbContext.Participatings.Add(Team7InAwesomeTournament);
        dbContext.Participatings.Add(Team8InAwesomeTournament);
        dbContext.Participatings.Add(Team9InAwesomeTournament);
        dbContext.Participatings.Add(Team10InAwesomeTournament);
        dbContext.Participatings.Add(Team11InAwesomeTournament);
        dbContext.Participatings.Add(Team12InAwesomeTournament);
    }

}