using Microsoft.EntityFrameworkCore;
using Twitter.Core.Entities;

namespace Twitter.Infrastructure.Data
{
    public class TwitterAPIContext : DbContext
    {
        public TwitterAPIContext(DbContextOptions<TwitterAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = default!;
    }
}
