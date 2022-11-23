using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TournamentManager.Server.BL.Facades;
using TournamentManager.Common.Models;
using TournamentManager.Server.Auth.Models;

namespace TournamentManager.Server.App.Controllers;

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

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<MatchModel>>> GetAll()
    {
        var user = await GetLoggedUser();
        if (user is null || !user.IsAdministrator)
            return Forbid();

        return Ok(await _matchFacade.GetAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MatchModel>> GetById(Guid id)
    {
        var match = await _matchFacade.GetAsync(id);
        if (match is null)
            return NotFound();

        if (match.Tournament is { IsPublic: true })
            return Ok(match);

        var user = await GetLoggedUser();
        if (user is null)
            return Forbid();

        if (await _tournamentFacade.CanUserViewTournament(user.Id, match.TournamentId))
            return Ok(match);

        return Forbid();
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult> InsertOrUpdate([FromBody] MatchModel? match)
    {
        if (match is null)
            return BadRequest();
        var tournament = await _tournamentFacade.GetAsync(match.TournamentId);

        var user = await GetLoggedUser();
        if (user == null || !(user.IsAdministrator || user.Id == tournament?.CreatorId))
            return Forbid();

        await _matchFacade.SaveAsync(match);
        return Ok();
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var match = await _matchFacade.GetAsync(id);
        if (match is null)
            return NoContent();
        
        var user = await GetLoggedUser();

        var tournament = await _tournamentFacade.GetAsync(match.Id);
        if (user is null || !(user.IsAdministrator || user.Id == tournament?.CreatorId))
            return Forbid();

        await _matchFacade.DeleteAsync(id);
        return Ok();
    }
}