using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentManager.Server.App.Data;
using TournamentManager.Server.App.Models;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.App.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TournamentController : AuthorizedControllerBase
{
    public TournamentController(TournamentManagerDbContext dbContext,
        UserManager<ApplicationUser> userManager) : base(dbContext, userManager)
    {
    }
    
    [HttpGet]
    public async Task<ActionResult<List<TournamentModel>>> Get()
    {
        var user = await GetLoggedUser();
        if (user.IsAdministrator)
            return Ok(await DbContext.Tournaments.ToListAsync());

        return Ok(await DbContext.Tournaments.Where(x => x.IsPublic || x.CreatorId == user.Id).ToListAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TournamentModel>> GetById(Guid id)
    {
        var tournament = await DbContext.Tournaments.FirstOrDefaultAsync(x => x.Id == id);
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

        return Ok(await DbContext.Tournaments.AddAsync(tournament));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var tournament = await DbContext.Tournaments.FirstOrDefaultAsync(x => x.Id == id);
        if (tournament is null)
            return NoContent();

        var user = await GetLoggedUser();
        if (tournament.CreatorId != user.Id && !user.IsAdministrator)
            return Forbid();

        DbContext.Remove(tournament);
        await DbContext.SaveChangesAsync();

        return Ok();
    }
}