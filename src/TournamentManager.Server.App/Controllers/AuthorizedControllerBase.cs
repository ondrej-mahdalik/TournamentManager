﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TournamentManager.Server.BL.Facades;
using TournamentManager.Common.Models;
using TournamentManager.Server.Auth.Models;

namespace TournamentManager.Server.App.Controllers;

public class AuthorizedControllerBase : ControllerBase
{
    private readonly UserFacade _userFacade;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public AuthorizedControllerBase(UserFacade userFacade, UserManager<ApplicationUser> userManager)
    {
        _userFacade = userFacade;
        _userManager = userManager;
    }

    public async Task<UserModel> GetLoggedUser()
    {
        var applicationUser = await _userManager.GetUserAsync(User) ?? throw new("Failed to obtain logged user");
        return await _userFacade.GetByApplicationUserIdAsync(applicationUser.Id) ?? throw new("User not found");
    }
}