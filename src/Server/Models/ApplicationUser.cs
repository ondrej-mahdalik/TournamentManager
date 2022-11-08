using Microsoft.AspNetCore.Identity;

namespace TournamentManager.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Guid? MainUserId { get; set; }
    }
}