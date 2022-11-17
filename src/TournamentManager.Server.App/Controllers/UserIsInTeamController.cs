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
public class UserIsInTeamController : AuthorizedControllerBase
{
    private readonly UserIsInTeamFacade _userIsInTeamFacade;
    private readonly TeamFacade _teamFacade;
    
    public UserIsInTeamController(UserIsInTeamFacade userIsInTeamFacade,
        TeamFacade teamFacade,
        UserFacade userFacade,
        UserManager<ApplicationUser> userManager) : base(userFacade, userManager)
    {
        _userIsInTeamFacade = userIsInTeamFacade;
        _teamFacade = teamFacade;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserIsInTeamModel>>> GetAll()
    {
        return Ok(await _userIsInTeamFacade.GetAsync());
    }

    [HttpGet("Team/{id:guid}")]
    public async Task<ActionResult<List<UserIsInTeamModel>>> GetById(Guid id)
    {
        return Ok(await _userIsInTeamFacade.GetAsync(id));
    }

    [HttpPut]
    public async Task<ActionResult> InsertOrUpdate([FromBody] UserIsInTeamModel? newUserInTeam)
    {
        if (newUserInTeam is null)
            return BadRequest();

        var teamLeaderId = (await _teamFacade.GetAsync(newUserInTeam.TeamId))?.LeaderId;
        if (teamLeaderId is null)
            return BadRequest();

        var user = await GetLoggedUser();
        if (!(user.IsAdministrator || user.Id == teamLeaderId))
            return Forbid();

        return Ok(await _userIsInTeamFacade.SaveAsync(newUserInTeam));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var userFromTeamToDelete = await _userIsInTeamFacade.GetAsync(id);
        if (userFromTeamToDelete is null)
            return NoContent();

        var user = await GetLoggedUser();
        var teamLeaderId = (await _teamFacade.GetAsync(userFromTeamToDelete.TeamId))?.LeaderId;
        if (!(user.IsAdministrator || user.Id == teamLeaderId))
            return Forbid();
        
        await _userIsInTeamFacade.DeleteAsync(id);
        return Ok();
    }
}