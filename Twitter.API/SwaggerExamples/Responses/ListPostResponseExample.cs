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
                    CreatedById = Guid.NewGuid().ToString()
                },

                new Post
                {
                    Id = 2,
                    Text = "lorem ipsum",
                    Created = DateTime.Now,
                    CategoryId = 2,
                    CreatedById = Guid.NewGuid().ToString()
                }
            };

            return posts;
        }
    }
}
