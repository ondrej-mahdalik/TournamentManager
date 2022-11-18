﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TournamentManager.Server.BL.Facades;
using TournamentManager.Common.Models;
using TournamentManager.Server.Auth.Models;

namespace TournamentManager.Server.App.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SportController : AuthorizedControllerBase
{
    private readonly SportFacade _sportFacade;

    public SportController(SportFacade sportFacade,
        UserFacade userFacade,
        UserManager<ApplicationUser> userManager) : base(userFacade, userManager)
    {
        _sportFacade = sportFacade;
    }

    [HttpGet]
    public async Task<ActionResult<List<SportModel>>> GetAll()
    {
        return Ok(await _sportFacade.GetAsync());
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SportModel>> GetById(Guid id)
    {
        var sport = await _sportFacade.GetAsync(id);
        if (sport is null)
            return NotFound();

        return Ok(sport);
    }

    [HttpPut]
    public async Task<ActionResult> InsertOrUpdate([FromBody] SportModel? sport)
    {
        if (sport is null)
            return BadRequest();

        var user = await GetLoggedUser();
        if (!user.IsAdministrator)
            return Forbid();

        await _sportFacade.SaveAsync(sport);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var sport = await _sportFacade.GetAsync(id);
        if (sport is null)
            return NoContent();
        
        var user = await GetLoggedUser();
        
        if (!user.IsAdministrator )
            return Forbid();

        await _sportFacade.DeleteAsync(id);
        return Ok();
    }
}