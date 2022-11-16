using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentManager.Server.Data;
using TournamentManager.Server.Models;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TeamController : AuthorizedControllerBase
{
    public TeamController(TournamentManagerDbContext dbContext,
        UserManager<ApplicationUser> userManager) : base(dbContext, userManager)
    {
    }
    
    [HttpGet]
    public async Task<ActionResult<List<TeamModel>>> GetAll()
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
        if (team is null)
            return BadRequest();
        
        var user = await GetLoggedUser();
        if (team.LeaderId != user.Id && !user.IsAdministrator)
            return Forbid();

        return Ok(await DbContext.Teams.AddAsync(team));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var team = await DbContext.Teams.FirstOrDefaultAsync(x => x.Id == id);
        if (team is null)
            return NoContent();

        var user = await GetLoggedUser();
        if (team.LeaderId != user.Id && !user.IsAdministrator)
            return Forbid();

        DbContext.Remove(team);
        await DbContext.SaveChangesAsync();

        return Ok();
    }
}