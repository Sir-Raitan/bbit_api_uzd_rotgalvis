using Microsoft.AspNetCore.Identity;

namespace IdServer.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ResidentId { get; set; }
    }
}