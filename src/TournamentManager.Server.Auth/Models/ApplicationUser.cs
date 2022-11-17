using Microsoft.AspNetCore.Identity;

namespace TournamentManager.Server.Auth.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Guid MainUserId { get; set; }
    }
}