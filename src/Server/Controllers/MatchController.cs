using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentManager.Server.Data;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class MatchController : AuthorizedControllerBase
{
    public MatchController(TournamentManagerDbContext dbContext) : base(dbContext)
    {
    }
    
    [HttpGet]
    public async Task<ActionResult<List<MatchModel>>> Get()
    {
          return Ok(await DbContext.Matches.ToListAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MatchModel>> GetById(Guid id)
    {
        var match = await DbContext.Matches.FirstOrDefaultAsync(x => x.Id == id);
        if (match is null)
            return NotFound();

        return Ok(match);
    }

    [HttpGet("{tournamentId:guid}")]
    public async Task<ActionResult<MatchModel>> GetByTournamentId(Guid id)
    {
        var match = await DbContext.Matches.Where(x => x.TournamentId == id);
        if (match is null)
            return NotFound();

        return Ok(match);
    }

    [HttpPut]
    public async Task<ActionResult> InsertOrUpdate([FromBody] MatchModel? match)
    {
        if (match is null || match.TournamentId is null )
            return BadRequest();

        return Ok(await DbContext.Matches.AddAsync(match));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var match = await DbContext.Matches.FirstOrDefaultAsync(x => x.Id == id);
        if (match is null)
            return NoContent();

        if (match.Tournament.CreatorId != LoggedUser.Id && !LoggedUser.IsAdministrator)
            return Forbid();

        DbContext.Remove(match);
        await DbContext.SaveChangesAsync();

        return Ok();
    }
}