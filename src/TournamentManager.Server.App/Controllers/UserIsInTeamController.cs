﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TournamentManager.Server.BL.Facades;
using TournamentManager.Common.Models;
using TournamentManager.Server.Auth.Models;

namespace TournamentManager.Server.App.Controllers;

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

    [HttpGet("UserIsInTeam/{id:guid}")]
    public async Task<ActionResult<List<UserIsInTeamModel>>> GetById(Guid id)
    {
        return Ok(await _userIsInTeamFacade.GetAsync(id));
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult> InsertOrUpdate([FromBody] UserIsInTeamModel? newUserInTeam)
    {
        ModelState.Remove(nameof(newUserInTeam.Team));
        ModelState.Remove(nameof(newUserInTeam.User));
        
        if (newUserInTeam is null)
            return BadRequest();

        var teamLeaderId = (await _teamFacade.GetAsync(newUserInTeam.TeamId))?.LeaderId;
        if (teamLeaderId is null)
            return BadRequest();

        var user = await GetLoggedUser();
        if (user is null || newUserInTeam.IsApproved && !(user.IsAdministrator || user.Id == teamLeaderId))
            return Forbid();

        return Ok(await _userIsInTeamFacade.SaveAsync(newUserInTeam));
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var userFromTeamToDelete = await _userIsInTeamFacade.GetAsync(id);
        if (userFromTeamToDelete is null)
            return NoContent();

        var user = await GetLoggedUser();
        var teamLeaderId = (await _teamFacade.GetAsync(userFromTeamToDelete.TeamId))?.LeaderId;
        if (user is null || !(user.IsAdministrator || user.Id == teamLeaderId || user.Id == userFromTeamToDelete.UserId))
            return Forbid();
        
        await _userIsInTeamFacade.DeleteAsync(id);
        return Ok();
    }
}