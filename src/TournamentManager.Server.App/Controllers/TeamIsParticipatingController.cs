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
public class TeamIsParticipatingController : AuthorizedControllerBase
{
    private readonly TeamIsParticipatingFacade _teamIsParticipatingFacade;
    private readonly TournamentFacade _tournamentFacade;
    private readonly TeamFacade _teamFacade;
    
    public TeamIsParticipatingController(TeamIsParticipatingFacade teamIsParticipatingFacade,
        TeamFacade teamFacade,
        TournamentFacade tournamentFacade,
        UserFacade userFacade,
        UserManager<ApplicationUser> userManager) : base(userFacade, userManager)
    {
        _teamIsParticipatingFacade = teamIsParticipatingFacade;
        _teamFacade = teamFacade;
        _tournamentFacade = tournamentFacade;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<TeamIsParticipatingModel>>> GetAll()
    {
          return Ok(await _teamIsParticipatingFacade.GetAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TeamIsParticipatingModel>> GetById(Guid id)
    {
        var participation = await _teamIsParticipatingFacade.GetAsync(id);
        if (participation is null)
            return NotFound();

        return Ok(participation);
    }

    [HttpPut]
    public async Task<ActionResult> InsertOrUpdate([FromBody] TeamIsParticipatingModel? participation)
    {
        if (participation is null)
            return BadRequest();

        var tournament = await _tournamentFacade.GetAsync(participation.TournamentId);
        var team = await _teamFacade.GetAsync(participation.TeamId);
        if (tournament is null || team is null)
            return BadRequest();
        
        var user = await GetLoggedUser();
        if (user.IsAdministrator || tournament.CreatorId == user.Id ||
            (tournament.IsPublic && team.LeaderId == user.Id))
            return Ok(await _teamIsParticipatingFacade.SaveAsync(participation));

        return Forbid();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var participation = await _teamIsParticipatingFacade.GetAsync(id);
        
        if (participation is null)
            return NoContent();

        var user = await GetLoggedUser();
        if (!user.IsAdministrator && participation.Team?.LeaderId != user.Id &&
            participation.Tournament?.CreatorId != user.Id)
            return Forbid();

        await _teamIsParticipatingFacade.DeleteAsync(id);
        return Ok();
    }
}