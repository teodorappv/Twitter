using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Twitter.Core.Domain.Entities;

namespace Twitter.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TwitterAPIContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<TwitterAPIContext>>()))
            {
                if (context.Categories.Any())
                {
                    return;
                }
                context.Categories.AddRange(
                    new Category
                    {
                        Name = "Life"
                    },
                    new Category
                    {
                        Name = "People"
                    },
                    new Category
                    {
                        Name = "Everyday"
                    },
                    new Category
                    {
                        Name = "News"
                    },
                    new Category
                    {
                        Name = "Travel"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
