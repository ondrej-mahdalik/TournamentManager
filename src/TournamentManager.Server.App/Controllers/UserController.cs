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
public class UserController : AuthorizedControllerBase
{
    private readonly UserFacade _userFacade;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(UserFacade userFacade,
        UserManager<ApplicationUser> userManager) : base(userFacade, userManager)
    {
        _userFacade = userFacade;
        _userManager = userManager;
    }

    [HttpGet("current")]
    public async Task<ActionResult<UserModel?>> GetCurrentUser()
    {
        var user = await GetLoggedUser();
        return await GetUserProperties(user);
    }

    [HttpGet]
    public async Task<ActionResult<List<UserModel>>> Get()
    {
        var temp = await _userFacade.GetAsync();
        var users = new List<UserModel>();
        foreach (var user in temp)
            users.Add(await GetUserProperties(user));
        
        return Ok(users);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserModel>> GetById(Guid id)
    {
        var user = await _userFacade.GetAsync(id);
        if (user is null)
            return NotFound();

        return Ok(await GetUserProperties(user));
    }

    [HttpPut]
    public async Task<ActionResult> InsertOrUpdate([FromBody] UserModel? user)
    {
        if (user is null)
            return BadRequest();

        var loggedUser = await GetLoggedUser();
        if (!loggedUser.IsAdministrator && loggedUser.Id != user.Id)
            return Forbid();

        return Ok(await _userFacade.SaveAsync(user));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var user = await _userFacade.GetAsync(id);
        if (user is null)
            return NoContent();

        var loggedUser = await GetLoggedUser();
        if (!loggedUser.IsAdministrator && loggedUser.Id != user.Id)
            return Forbid();

        await _userFacade.DeleteAsync(user);
        return Ok();
    }

    private async Task<UserModel> GetUserProperties(UserModel user)
    {
        var applicationUserId = await _userFacade.GetApplicationUserId(user.Id);
        if (applicationUserId is null)
            return user;
        
        var applicationUser = await _userManager.FindByIdAsync(applicationUserId);
        user.Email = applicationUser?.Email;
        user.FirstName = applicationUser?.FirstName;
        user.LastName = applicationUser?.LastName;

        return user;
    }
}