using Microsoft.EntityFrameworkCore;
using Twitter.Core.Entities;

namespace Twitter.API.Data
{
    public class TwitterAPIContext : DbContext
    {
        public TwitterAPIContext (DbContextOptions<TwitterAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; } = default!;
    }
}
