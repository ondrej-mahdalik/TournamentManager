using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentManager.Server.Data;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TournamentController : AuthorizedControllerBase
{
    public TournamentController(TournamentManagerDbContext dbContext) : base(dbContext)
    {
    }
    
    [HttpGet]
    public async Task<ActionResult<List<TournamentModel>>> Get()
    {
        if (LoggedUser.IsAdministrator)
            return Ok(await DbContext.Tournaments.ToListAsync());
        
        return Ok(await DbContext.Tournaments.Where(x => x.IsPublic || x.CreatorId == LoggedUser.Id).ToListAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TournamentModel>> GetById(Guid id)
    {
        var tournament = await DbContext.Tournaments.FirstOrDefaultAsync(x => x.Id == id);
        if (tournament is null)
            return NotFound();
        if (!LoggedUser.IsAdministrator && !tournament.IsPublic && tournament.CreatorId != LoggedUser.Id)
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

        if (tournament.CreatorId != LoggedUser.Id && !LoggedUser.IsAdministrator)
            return Forbid();

        DbContext.Remove(tournament);
        await DbContext.SaveChangesAsync();

        return Ok();
    }
}