using Microsoft.AspNetCore.Mvc;
using TournamentManager.Server.Data;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.Controllers;

public class AuthorizedControllerBase : ControllerBase
{
    public readonly UserModel LoggedUser;
    public readonly TournamentManagerDbContext DbContext;

    public AuthorizedControllerBase(TournamentManagerDbContext dbContext)
    {
        DbContext = dbContext;
        
        LoggedUser = new UserModel(Guid.NewGuid(), String.Empty, String.Empty, String.Empty);
    }
}