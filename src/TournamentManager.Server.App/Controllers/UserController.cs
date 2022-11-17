using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TournamentManager.Server.App.Models;
using TournamentManager.Server.BL.Facades;
using TournamentManager.Common.Models;

namespace TournamentManager.Server.App.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : AuthorizedControllerBase
{
    private  readonly UserFacade _userFacade;
    
    public UserController(UserFacade userFacade,
        UserManager<ApplicationUser> userManager) : base(userFacade, userManager)
    {
        _userFacade = userFacade;
    }
    
    [HttpGet("current")]
    public async Task<ActionResult<UserModel?>> GetCurrentUser()
    {
        return Ok(await GetLoggedUser());
    }
    
    [HttpGet]
    public async Task<ActionResult<List<UserModel>>> Get()
    {
        return Ok(await _userFacade.GetAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserModel>> GetById(Guid id)
    {
        var user = await _userFacade.GetAsync(id);
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
}