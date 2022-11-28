using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TournamentManager.Server.BL.Facades;
namespace TournamentManager.Server.App.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class StatsController : ControllerBase
{
    
    private readonly MatchFacade _matchFacade;
    private readonly TournamentFacade _tournamentFacade;
    private readonly TeamFacade _teamFacade;
    private readonly TeamIsParticipatingFacade _teamIsParticipatingFacade;

    public StatsController(MatchFacade matchFacade, TournamentFacade tournamentFacade, TeamFacade teamFacade, TeamIsParticipatingFacade teamIsParticipatingFacade)
    {
        _matchFacade = matchFacade;
        _tournamentFacade = tournamentFacade;
        _teamFacade = teamFacade;
        _teamIsParticipatingFacade = teamIsParticipatingFacade;
    }
    
    [HttpGet("User/Matches/Played/{userId:guid}")]
    public async Task<ActionResult<int>> GetMatchesPlayedByUser(Guid userId)
    {
        var matches = await _matchFacade.GetByUserIdAsync(userId);
        return Ok(matches.Count);
    }
    
    [HttpGet("User/Matches/Won/{userId:guid}")]
    public async Task<ActionResult<int>> GetMatchesWonByUser(Guid userId)
    {
        var matches = await _matchFacade.GetByUserIdAsync(userId);
        var userTeams = (await _teamFacade.GetByLeaderAsync(userId)).ToList();
        
        int wonMatches = 0;
        foreach (var match in matches)
        {
            if (match.Score1 > match.Score2 && userTeams.Any(x => x.Id == match.Team1Id))
                wonMatches++;
            else if (match.Score2 > match.Score1 && userTeams.Any(x => x.Id == match.Team2Id))
                wonMatches++;
        }

        return Ok(wonMatches);
    }
    
    [HttpGet("User/Tournaments/Played/{userId:guid}")]
    public async Task<ActionResult<int>> GetTournamentsPlayedByUser(Guid userId)
    {
        var participatings = await _teamIsParticipatingFacade.GetByUserIdAsync(userId);
        var tournaments = participatings.Select(x => x.Tournament).Distinct();
        
        return Ok(tournaments.Count());
    }
    
    [HttpGet("User/Tournaments/Won/{userId:guid}")]
    public async Task<ActionResult<int>> GetTournamentsWonByUser(Guid userId)
    {
        var userTeams = (await _teamFacade.GetByUserAsync(userId)).ToList();
        var participatings = await _teamIsParticipatingFacade.GetByUserIdAsync(userId);
        var tournamentIds = participatings.Select(x => x.TournamentId).Distinct();
        
        var tournaments = await _tournamentFacade.GetManyAsync(tournamentIds);
        var wonTournaments = tournaments.Count(tournament => userTeams.Any(x => x.Id == tournament?.WinnerTeamId));

        return Ok(wonTournaments);
    }
}
