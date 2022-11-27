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

    public static readonly TeamIsParticipatingEntity JohnsInGoldenPuck = new(true,
        DateTime.Now - TimeSpan.FromDays(69))
    {
        TournamentId = TournamentSeeds.GoldenPuck.Id,
        TeamId = TeamSeeds.JohnsTeam.Id
    };

    public static readonly TeamIsParticipatingEntity JohnsInNY2021 = new(true,
        DateTime.Parse("12/21/2020 12:21"))
    {
        TournamentId = TournamentSeeds.NY2021.Id,
        TeamId = TeamSeeds.JohnsTeam.Id
    };

    public static readonly TeamIsParticipatingEntity CardWarriorsInNY2021 = new(true,
        DateTime.Parse("12/6/2020 4:21"))
    {
        TournamentId = TournamentSeeds.NY2021.Id,
        TeamId = TeamSeeds.CardWarriors.Id
    };


    public static void Seed(TournamentManagerDbContext dbContext)
    {
        dbContext.Participatings.Add(Team1InAwesomeTournament);
        dbContext.Participatings.Add(Team2InAwesomeTournament);
        dbContext.Participatings.Add(Team3InAwesomeTournament);
        dbContext.Participatings.Add(Team4InAwesomeTournament);
        dbContext.Participatings.Add(Team5InAwesomeTournament);
        dbContext.Participatings.Add(Team6InAwesomeTournament);
        dbContext.Participatings.Add(JohsInNYTournament);
        dbContext.Participatings.Add(JohnsInGoldenPuck);
        dbContext.Participatings.Add(JohnsInNY2021);
        dbContext.Participatings.Add(CardWarriorsInNY2021);
    }

}