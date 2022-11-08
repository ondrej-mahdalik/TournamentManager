using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentManager.Server.Data;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TeamIsParticipatingController : AuthorizedControllerBase
{
    public TeamIsParticipatingController(TournamentManagerDbContext dbContext) : base(dbContext)
    {
    }
    
    [HttpGet]
    public async Task<ActionResult<List<TeamIsParticipatingModel>>> GetAll()
    {
          return Ok(await DbContext.Participatings.ToListAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TeamIsParticipatingModel>> GetById(Guid id)
    {
        var participation = await DbContext.Participatings.FirstOrDefaultAsync(x => x.Id == id);
        if (participation is null)
            return NotFound();

        return Ok(participation);
    }

    [HttpPut]
    public async Task<ActionResult> InsertOrUpdate([FromBody] TeamIsParticipatingModel? participation)
    {
        if (participation is null)
            return BadRequest();

        var tournament = await DbContext.Tournaments.FirstOrDefaultAsync(x => x.Id == participation.TournamentId);
        var team = await DbContext.Teams.FirstOrDefaultAsync(x => x.Id == participation.TeamId);
        if (tournament is null || team is null)
            return BadRequest();
        
        if (LoggedUser.IsAdministrator || tournament.CreatorId == LoggedUser.Id || (tournament.IsPublic && team.LeaderId == LoggedUser.Id))
            return Ok(await DbContext.Participatings.AddAsync(participation));

        return Forbid();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var participation = await DbContext.Participatings
            .Include(x => x.Team)
            .Include(x => x.Tournament)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (participation is null)
            return NoContent();

        if (!LoggedUser.IsAdministrator && participation.Team?.LeaderId != LoggedUser.Id &&
            participation.Tournament?.CreatorId != LoggedUser.Id)
            return Forbid();

        DbContext.Remove(participation);
        await DbContext.SaveChangesAsync();

        return Ok();
    }
}