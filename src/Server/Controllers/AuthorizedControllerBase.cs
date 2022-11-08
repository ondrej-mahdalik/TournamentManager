using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentManager.Server.Data;
using TournamentManager.Server.MainSeeds;
using TournamentManager.Shared.Enums;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.Controllers;

public class AuthorizedControllerBase : ControllerBase
{
    protected readonly UserModel LoggedUser;
    protected readonly TournamentManagerDbContext DbContext;

    public AuthorizedControllerBase(TournamentManagerDbContext dbContext)
    {
        DbContext = dbContext;
        // if (!Guid.TryParse(HttpContext.User.FindFirstValue(CustomClaimTypes.MainUserId), out var userId))
        //     throw new NotImplementedException(); // TODO properly handle missing Id
        //
        // LoggedUser = DbContext.Users.FirstOrDefault(x => x.Id == userId) ?? throw new NotImplementedException(); // TODO properly handle non-existing user

        LoggedUser = DbContext.Users.First(x => x.Id == UserSeeds.JohnDoe.Id);
    }
}