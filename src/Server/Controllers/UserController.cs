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
public class UserController : AuthorizedControllerBase
{
    public UserController(TournamentManagerDbContext dbContext,
        UserManager<ApplicationUser> userManager) : base(dbContext, userManager)
    {
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