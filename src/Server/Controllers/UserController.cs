using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentManager.Server.Data;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    private readonly TournamentManagerDbContext _dbContext;

    public UserController(ILogger<UserController> logger, TournamentManagerDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<List<UserModel>> Get()
    {
        return await _dbContext.Users.ToListAsync();
    }
}