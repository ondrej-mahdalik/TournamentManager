using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentManager.Server.App.Data;
using TournamentManager.Server.App.Models;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.App.Controllers;

public class AuthorizedControllerBase : ControllerBase
{
    protected readonly TournamentManagerDbContext DbContext;
    protected readonly UserManager<ApplicationUser> UserManager;
    
    public AuthorizedControllerBase(TournamentManagerDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        DbContext = dbContext;
        UserManager = userManager;
    }

    public async Task<UserModel> GetLoggedUser()
    {
        var applicationUser = await UserManager.GetUserAsync(User) ?? throw new("Failed to obtain logged user");
        return await DbContext.Users.FirstOrDefaultAsync(x => x.Id == applicationUser.MainUserId) ?? throw new("User not found");
    }
}