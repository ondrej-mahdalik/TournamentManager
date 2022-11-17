﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentManager.Server.App.Data;
using TournamentManager.Server.App.Models;
using TournamentManager.Shared.Models;
using TournamentManager.Shared.Structs;

namespace TournamentManager.Server.App.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : AuthorizedControllerBase
{
    public UserController(TournamentManagerDbContext dbContext,
        UserManager<ApplicationUser> userManager) : base(dbContext, userManager)
    {
    }
    
    [HttpGet("current")]
    public async Task<ActionResult<UserModel?>> GetCurrentUser()
    {
        return Ok(await GetLoggedUser());
    }
    
    [HttpGet]
    public async Task<ActionResult<List<UserModel>>> Get()
    {
        return Ok(await DbContext.Users.ToListAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserModel>> GetById(Guid id)
    {
        var user = await DbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user is null)
            return NotFound();

        return Ok(user);
    }
    
    [HttpGet("{id:guid}/userinfo")]
    public async Task<ActionResult<UserInfo>> GetUserInfoById(Guid id)
    {
        var userInfo = new UserInfo(String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
        return Ok(userInfo);
    }

    [HttpPut]
    public async Task<ActionResult> InsertOrUpdate([FromBody] UserModel? user)
    {
        if (user is null)
            return BadRequest();
        
        var loggedUser = await GetLoggedUser();
        if (!loggedUser.IsAdministrator && loggedUser.Id != user.Id)
            return Forbid();

        return Ok(await DbContext.Users.AddAsync(user));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var user = await DbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user is null)
            return NoContent();

        var loggedUser = await GetLoggedUser();
        if (!loggedUser.IsAdministrator && loggedUser.Id != user.Id)
            return Forbid();

        DbContext.Remove(user);
        await DbContext.SaveChangesAsync();

        return Ok();
    }
}