using TournamentManager.Server.DAL;
using TournamentManager.Server.DAL.Entities;
namespace TournamentManager.Server.Seeds.MainSeeds;

public static class TeamIsParticipatingSeeds
{
    public static readonly TeamIsParticipatingEntity Team1InAwesomeTournament = new(false)
    {
        TournamentId = TournamentSeeds.Tournament1.Id,
        TeamId = TeamSeeds.JohnsTeam.Id
    };

    public static readonly TeamIsParticipatingEntity Team2InAwesomeTournament = new(false)
    {
        TournamentId = TournamentSeeds.Tournament1.Id,
        TeamId = TeamSeeds.Team1.Id
    };
    
    public static void Seed(TournamentManagerDbContext dbContext)
    {
        dbContext.Participatings.Add(Team1InAwesomeTournament);
        dbContext.Participatings.Add(Team2InAwesomeTournament);
    }

}