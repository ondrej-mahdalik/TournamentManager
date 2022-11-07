using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentManager.Server.Data;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : AuthorizedControllerBase
{
    public UserController(TournamentManagerDbContext dbContext) : base(dbContext)
    {
    }
    
    [HttpGet]
    public async Task<ActionResult<List<UserModel>>> Get()
    {
        if (!LoggedUser.IsAdministrator)
            return Forbid();
        
        return Ok(await DbContext.Users.ToListAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TournamentModel>> GetById(Guid id)
    {
        var user = await DbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user is null)
            return NotFound();
        
        if (!(LoggedUser.IsAdministrator || LoggedUser.Id == id))
            return Forbid();

        return Ok(user);
    }

    [HttpPut]
    public async Task<ActionResult> InsertOrUpdate([FromBody] UserModel? user)
    {
        if (!LoggedUser.IsAdministrator)
            return Forbid();

        if (user is null)
            return BadRequest();

        return Ok(await DbContext.Users.AddAsync(user));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var user = await DbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user is null)
            return NoContent();

        if (!LoggedUser.IsAdministrator)
            return Forbid();

        DbContext.Remove(user);
        await DbContext.SaveChangesAsync();

        return Ok();
    }
}