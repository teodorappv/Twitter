using Microsoft.AspNetCore.Identity;

namespace Twitter.Core.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }

    }
}
