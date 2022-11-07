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
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<List<MatchModel>>> Get(Guid tournamentId)
    {
        // TODO Determine who can see the matches (admin, participants)?
        return Ok(await DbContext.Matches.Where(x => x.TournamentId == tournamentId).ToListAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MatchModel>> GetById(Guid id)
    {
        var match = await DbContext.Matches.FirstOrDefaultAsync(x => x.Id == id);
        if (match is null)
            return NotFound();
        
        var tournament = await DbContext.Tournaments.FirstOrDefaultAsync(x => x.Id == match.TournamentId);
        if (!LoggedUser.IsAdministrator && tournament?.CreatorId != LoggedUser.Id)
            return Forbid();

        return Ok(match);
    }

    [HttpPut]
    public async Task<ActionResult> InsertOrUpdate([FromBody] MatchModel? match)
    {
        if (match is null || match.Score1 < 0 || match.Score2 < 0)
            return BadRequest();
        var tournament = await DbContext.Tournaments.FirstOrDefaultAsync(
            x => x.Id == match.TournamentId);

        if (!(LoggedUser.IsAdministrator || LoggedUser.Id == tournament?.CreatorId))
        {
            return Forbid();
        }
        return Ok(await DbContext.Matches.AddAsync(match));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var match = await DbContext.Matches.FirstOrDefaultAsync(x => x.Id == id);
        if (match is null)
            return NoContent();
        
        var tournament = await DbContext.Tournaments.FirstOrDefaultAsync(x => x.Id == match.TournamentId);
        if (!(LoggedUser.IsAdministrator || LoggedUser.Id == tournament?.CreatorId))
            return Forbid();

        DbContext.Remove(match);
        await DbContext.SaveChangesAsync();

        return Ok();
    }
}