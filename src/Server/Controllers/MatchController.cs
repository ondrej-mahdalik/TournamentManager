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
    public async Task<ActionResult<List<MatchModel>>> GetAll()
    {
        return Ok(await DbContext.Matches.ToListAsync());
    }

    [HttpGet("Tournament/{id:guid}")]
    public async Task<ActionResult<List<MatchModel>>> Get(Guid tournamentId)
    {
        var tournament = await DbContext.Tournaments.Include(x => x.Participatings)
            .FirstOrDefaultAsync(x => x.Id == tournamentId);
        
        if (tournament is null)
            return BadRequest();
        
        if (await CanViewTournament(tournament))
            return Ok(await DbContext.Matches.Where(x => x.TournamentId == tournamentId).ToListAsync());

        return Forbid();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MatchModel>> GetById(Guid id)
    {
        var match = await DbContext.Matches.FirstOrDefaultAsync(x => x.Id == id);
        if (match is null)
            return NotFound();

        var tournament = await DbContext.Tournaments.FirstOrDefaultAsync(x => x.Id == match.TournamentId);
        if (tournament is null)
            return BadRequest();
        
        if (await CanViewTournament(tournament))
            return Ok(match);

        return Forbid();
    }

    [HttpPut]
    public async Task<ActionResult> InsertOrUpdate([FromBody] MatchModel? match)
    {
        if (match is null)
            return BadRequest();
        var tournament = await DbContext.Tournaments.FirstOrDefaultAsync(
            x => x.Id == match.TournamentId);

        if (!(LoggedUser.IsAdministrator || LoggedUser.Id == tournament?.CreatorId))
            return Forbid();

        await DbContext.Matches.AddAsync(match);
        return Ok();
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

    private async Task<bool> CanViewTournament(TournamentModel tournament)
    {
        return tournament.IsPublic || LoggedUser.IsAdministrator || tournament.CreatorId == LoggedUser.Id ||
               await DbContext.Participatings.AnyAsync(x =>
                   x.TournamentId == tournament.Id && x.Team != null &&
                   x.Team.Members.Any(y => y.UserId == LoggedUser.Id));
    }
}