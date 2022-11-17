using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentManager.Server.App.Data;
using TournamentManager.Server.App.Models;
using TournamentManager.Server.BL.Facades;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.App.Controllers;

[Authorize]
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

    [HttpPut]
    public async Task<ActionResult> InsertOrUpdate([FromBody] TeamModel? team)
    {
        if (team is null)
            return BadRequest();
        
        var user = await GetLoggedUser();
        if (team.LeaderId != user.Id && !user.IsAdministrator)
            return Forbid();

        return Ok(await _teamFacade.SaveAsync(team));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var team = await _teamFacade.GetAsync(id);
        if (team is null)
            return NoContent();

        var user = await GetLoggedUser();
        if (team.LeaderId != user.Id && !user.IsAdministrator)
            return Forbid();

        await _teamFacade.DeleteAsync(id);
        return Ok();
    }
}