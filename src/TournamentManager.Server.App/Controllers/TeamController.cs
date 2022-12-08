using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TournamentManager.Server.BL.Facades;
using TournamentManager.Common.Models;
using TournamentManager.Server.Auth.Models;

namespace TournamentManager.Server.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController : AuthorizedControllerBase
{
    private readonly TeamFacade _teamFacade;
    
    public TeamController(TeamFacade teamFacade,
        UserFacade userFacade,
        UserManager<ApplicationUser> userManager) : base(userFacade, userManager)
    {
        _teamFacade = teamFacade;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<TeamModel>>> GetAll()
    {
          return Ok(await _teamFacade.GetAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TeamModel>> GetById(Guid id)
    {
        var team = await _teamFacade.GetAsync(id);
        if (team is null)
            return NotFound();

        return Ok(team);
    }
    
    [HttpGet("ByUser/{id:guid}")]
    public async Task<ActionResult<List<TeamModel>>> GetByUser(Guid id)
    {
        var teams = await _teamFacade.GetByLeaderAsync(id);
        return Ok(teams);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult> InsertOrUpdate([FromBody] TeamModel? team)
    {
        ModelState.Remove(nameof(team.Leader));
        ModelState.Remove(nameof(team.Matches));
        ModelState.Remove(nameof(team.Members));
        ModelState.Remove(nameof(team.Participatings));
        
        
        if (team is null)
            return BadRequest();
        
        var user = await GetLoggedUser();
        if (user is null || team.LeaderId != user.Id && !user.IsAdministrator)
            return Forbid();

        return Ok(await _teamFacade.SaveAsync(team));
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var team = await _teamFacade.GetAsync(id);
        if (team is null)
            return NoContent();

        var user = await GetLoggedUser();
        if (user is null || team.LeaderId != user.Id && !user.IsAdministrator)
            return Forbid();

        await _teamFacade.DeleteAsync(id);
        return Ok();
    }
}