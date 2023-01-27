using Swashbuckle.AspNetCore.Filters;
using Twitter.Core.Domain.Entities;

namespace Twitter.API.SwaggerExamples.Responses
{
    public class ListPostResponseExample : IExamplesProvider<List<Post>>
    {
        public List<Post> GetExamples()
        {
            var posts = new List<Post>
            {
                new Post
                {
                    Id = 1,
                    Text = "lorem ipsum",
                    Created = DateTime.Now,
                    CategoryId = 1,
                    CreatedById = "cd4ce98c - ce4e - 415d - a046 - 0fcce677b16c"
                },

                new Post
                {
                    Id = 2,
                    Text = "lorem ipsum",
                    Created = DateTime.Now,
                    CategoryId = 2,
                    CreatedById = "cd4ce98c - ce4e - 415d - a046 - 0fcce677b16c"
                }
            };

            return posts;
        }
    }
}
