using Microsoft.EntityFrameworkCore;
using Twitter.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Twitter.Infrastructure.Data
{
    public class TwitterAPIContext : IdentityDbContext<AppUser>
    {
        public TwitterAPIContext(DbContextOptions<TwitterAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
