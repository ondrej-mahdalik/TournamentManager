using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentManager.Server.Data;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserIsInTeamController : AuthorizedControllerBase
{
    public UserIsInTeamController(TournamentManagerDbContext dbContext) : base(dbContext)
    {
    }

    [HttpGet]
    public async Task<ActionResult<List<UserIsInTeamModel>>> GetAll()
    {
        return Ok(await DbContext.UsersIsInTeam.ToListAsync());
    }

    [HttpGet("Team/{id:guid}")]
    public async Task<ActionResult<List<UserIsInTeamModel>>> GetById(Guid teamId)
    {
        return Ok(await DbContext.UsersIsInTeam.Where(
            x => x.TeamId == teamId).ToListAsync());
    }

    [HttpPut]
    public async Task<ActionResult> InsertOrUpdate([FromBody] UserIsInTeamModel? newUserInTeam)
    {
        if (newUserInTeam is null)
            return BadRequest();

        var teamLeaderId = (await DbContext.Teams.FirstOrDefaultAsync(x => x.Id == newUserInTeam.Id))?.LeaderId;
        if (teamLeaderId is null)
            return BadRequest();

        if (!(LoggedUser.IsAdministrator || LoggedUser.Id == teamLeaderId))
            return Forbid();

        return Ok(await DbContext.UsersIsInTeam.AddAsync(newUserInTeam));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var userFromTeamToDelete = await DbContext.UsersIsInTeam.FirstOrDefaultAsync(x => x.Id == id);
        if (userFromTeamToDelete is null)
            return NoContent();

        var teamLeaderId = (await DbContext.Teams.FirstOrDefaultAsync(x => x.Id == userFromTeamToDelete.TeamId))?.LeaderId;
        if (!(LoggedUser.IsAdministrator || LoggedUser.Id == teamLeaderId))
            return Forbid();
        
        DbContext.Remove(userFromTeamToDelete);
        await DbContext.SaveChangesAsync();

        return Ok();
    }
}