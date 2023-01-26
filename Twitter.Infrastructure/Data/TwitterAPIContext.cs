using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Twitter.Core.Domain.Entities;

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
        public DbSet<FastPost> FastPosts { get; set; }
    }
}
