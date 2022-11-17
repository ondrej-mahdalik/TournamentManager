using Microsoft.AspNetCore.Identity;

namespace TournamentManager.Server.App.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Guid? MainUserId { get; set; }
    }
}