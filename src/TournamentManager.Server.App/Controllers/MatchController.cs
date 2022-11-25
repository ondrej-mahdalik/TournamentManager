using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TournamentManager.Server.BL.Facades;
using TournamentManager.Common.Models;
using TournamentManager.Server.Auth.Models;

namespace TournamentManager.Server.App.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MatchController : AuthorizedControllerBase
{
    private readonly MatchFacade _matchFacade;
    private readonly TournamentFacade _tournamentFacade;

    public MatchController(MatchFacade matchFacade,
        TournamentFacade tournamentFacade,
        UserFacade userFacade,
        UserManager<ApplicationUser> userManager) : base(userFacade, userManager)
    {
        _matchFacade = matchFacade;
        _tournamentFacade = tournamentFacade;
    }

    [HttpGet]
    public async Task<ActionResult<List<MatchModel>>> GetAll()
    {
        return Ok(await _matchFacade.GetAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MatchModel>> GetById(Guid id)
    {
        var match = await _matchFacade.GetAsync(id);
        if (match is null)
            return NotFound();

        var tournament = await _tournamentFacade.GetAsync(match.Id);
        if (tournament is null)
            return BadRequest();

        var user = await GetLoggedUser();
        if (await _tournamentFacade.CanUserViewTournament(user?.Id, tournament.Id))
            return Ok(match);

        return Forbid();
    }

    [HttpPut("multiple")]
    public async Task<ActionResult> InsertMultiple([FromBody] List<MatchModel>? matches)
    {
        if (matches is null || matches.Count == 0)
            return BadRequest();

        var user = await GetLoggedUser();
        if (user is null || (!user.IsAdministrator && matches.Any(x => user.Tournaments.All(y => y.Id != x.TournamentId))))
            return Forbid();

        if (!await _matchFacade.InsertManyAsync(matches))
            return UnprocessableEntity();

        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> InsertOrUpdate([FromBody] MatchModel? match)
    {
        if (match is null)
            return BadRequest();
        var tournament = await _tournamentFacade.GetAsync(match.TournamentId);

        var user = await GetLoggedUser();
        if (user is null || !(user.IsAdministrator || user.Id == tournament?.CreatorId))
            return Forbid();

        await _matchFacade.SaveAsync(match);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var match = await _matchFacade.GetAsync(id);
        if (match is null)
            return NoContent();
        
        var user = await GetLoggedUser();

        var tournament = await _tournamentFacade.GetAsync(match.Id);
        if (!((user?.IsAdministrator ?? false) || user?.Id == tournament?.CreatorId))
            return Forbid();

        await _matchFacade.DeleteAsync(id);
        return Ok();
    }
}