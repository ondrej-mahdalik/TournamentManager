using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentManager.Server.Data;
using TournamentManager.Server.Models;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.Controllers;

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
        var applicationUser = await UserManager.GetUserAsync(User) ?? throw new Exception("Failed to obtain logged user");
        return await DbContext.Users.FirstOrDefaultAsync(x => x.Id == applicationUser.MainUserId) ?? throw new Exception("User not found");
    }
}