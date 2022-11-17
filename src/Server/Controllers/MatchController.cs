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
public class MatchController : AuthorizedControllerBase
{
    public MatchController(TournamentManagerDbContext dbContext,
        UserManager<ApplicationUser> userManager) : base(dbContext, userManager)
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

        var user = await GetLoggedUser();

        if (!(user.IsAdministrator || user.Id == tournament?.CreatorId))
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
        
        var user = await GetLoggedUser();
        
        var tournament = await DbContext.Tournaments.FirstOrDefaultAsync(x => x.Id == match.TournamentId);
        if (!(user.IsAdministrator || user.Id == tournament?.CreatorId))
            return Forbid();

        DbContext.Remove(match);
        await DbContext.SaveChangesAsync();

        return Ok();
    }

    private async Task<bool> CanViewTournament(TournamentModel tournament)
    {
        var user = await GetLoggedUser();
        return tournament.IsPublic || user.IsAdministrator || tournament.CreatorId == user.Id ||
               await DbContext.Participatings.AnyAsync(x =>
                   x.TournamentId == tournament.Id && x.Team != null &&
                   x.Team.Members.Any(y => y.UserId == user.Id));
    }
}