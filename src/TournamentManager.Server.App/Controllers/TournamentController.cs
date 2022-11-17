using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentManager.Server.App.Data;
using TournamentManager.Server.App.Models;
using TournamentManager.Server.BL.Facades;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.App.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TournamentController : AuthorizedControllerBase
{
    private readonly TournamentFacade _tournamentFacade;
    
    public TournamentController(TournamentFacade tournamentFacade,
        UserFacade userFacade,
        UserManager<ApplicationUser> userManager) : base(userFacade, userManager)
    {
        _tournamentFacade = tournamentFacade;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<TournamentModel>>> Get()
    {
        var tournaments = await _tournamentFacade.GetAsync();
        
        var user = await GetLoggedUser();
        return Ok(user.IsAdministrator ? tournaments : tournaments.Where(x => x.IsPublic || x.CreatorId == user.Id));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TournamentModel>> GetById(Guid id)
    {
        var tournament = await _tournamentFacade.GetAsync(id);
        if (tournament is null)
            return NotFound();
        
        var user = await GetLoggedUser();
        if (!user.IsAdministrator && !tournament.IsPublic && tournament.CreatorId != user.Id)
            return Forbid();

        return Ok(tournament);
    }

    [HttpPut]
    public async Task<ActionResult> InsertOrUpdate([FromBody] TournamentModel? tournament)
    {
        if (tournament is null || tournament.MaxPlayers < 1)
            return BadRequest();

        return Ok(await _tournamentFacade.SaveAsync(tournament));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var tournament = await _tournamentFacade.GetAsync(id);
        if (tournament is null)
            return NoContent();

        var user = await GetLoggedUser();
        if (tournament.CreatorId != user.Id && !user.IsAdministrator)
            return Forbid();

        await _tournamentFacade.DeleteAsync(id);
        return Ok();
    }
}