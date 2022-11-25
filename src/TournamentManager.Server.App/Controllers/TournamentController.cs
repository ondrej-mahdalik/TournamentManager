using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TournamentManager.Server.BL.Facades;
using TournamentManager.Common.Models;
using TournamentManager.Server.Auth.Models;

namespace TournamentManager.Server.App.Controllers;

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
        return Ok(user?.IsAdministrator ?? false ? tournaments : tournaments.Where(x => x.IsPublic || x.CreatorId == user?.Id));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TournamentModel>> GetById(Guid id)
    {
        var tournament = await _tournamentFacade.GetAsync(id);
        if (tournament is null)
            return NotFound();

        var user = await GetLoggedUser();
        if (tournament.IsPublic || user is not null && (user.IsAdministrator || tournament.CreatorId == user.Id))
            return Ok(tournament);

        return Forbid();
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult> InsertOrUpdate([FromBody] TournamentModel? tournament)
    {
        Console.WriteLine(JsonConvert.SerializeObject(tournament));
        
        if (tournament is null || tournament.MaxAttendees < 1)
            return BadRequest();

        return Ok(await _tournamentFacade.SaveAsync(tournament));
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var tournament = await _tournamentFacade.GetAsync(id);
        if (tournament is null)
            return NoContent();

        var user = await GetLoggedUser();
        if (user is null || tournament.CreatorId != user.Id && !user.IsAdministrator)
            return Forbid();

        await _tournamentFacade.DeleteAsync(id);
        return Ok();
    }
}