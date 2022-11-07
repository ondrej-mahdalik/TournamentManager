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
    public async Task<ActionResult<List<TeamIsParticipatingModel>>> Get()
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
    public async Task<ActionResult> InsertOrUpdate([FromBody] TeamModel? participation)
    {
        if (participation is null || participation.TeamId is null || participation.TournamentId is null )
            return BadRequest();

        return Ok(await DbContext.Participatings.AddAsync(participation));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var participation = await DbContext.Participatings.FirstOrDefaultAsync(x => x.Id == id);
        if (participation is null)
            return NoContent();

        if (participation.Team.LeaderId != LoggedUser.Id && participation.Tournament.CreatorId != LoggedUser.Id && !LoggedUser.IsAdministrator)
            return Forbid();

        DbContext.Remove(participation);
        await DbContext.SaveChangesAsync();

        return Ok();
    }
}