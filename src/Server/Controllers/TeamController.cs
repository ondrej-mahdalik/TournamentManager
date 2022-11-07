using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentManager.Server.Data;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TeamController : AuthorizedControllerBase
{
    public TeamController(TournamentManagerDbContext dbContext) : base(dbContext)
    {
    }
    
    [HttpGet]
    public async Task<ActionResult<List<TeamModel>>> Get()
    {
          return Ok(await DbContext.Teams.ToListAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TeamModel>> GetById(Guid id)
    {
        var team = await DbContext.Teams.FirstOrDefaultAsync(x => x.Id == id);
        if (team is null)
            return NotFound();

        return Ok(team);
    }

    [HttpPut]
    public async Task<ActionResult> InsertOrUpdate([FromBody] TeamModel? team)
    {
        if (team is null || team.LeaderId is null )
            return BadRequest();

        return Ok(await DbContext.Teams.AddAsync(team));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var team = await DbContext.Teams.FirstOrDefaultAsync(x => x.Id == id);
        if (team is null)
            return NoContent();

        if (team.LeaderId != LoggedUser.Id && !LoggedUser.IsAdministrator)
            return Forbid();

        DbContext.Remove(team);
        await DbContext.SaveChangesAsync();

        return Ok();
    }
}