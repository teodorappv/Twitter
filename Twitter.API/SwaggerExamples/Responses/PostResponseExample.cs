using Swashbuckle.AspNetCore.Filters;
using Twitter.Core.Domain.Entities;

namespace Twitter.API.SwaggerExamples.Responses
{
    public class PostResponseExample : IExamplesProvider<Post>
    {
        public Post GetExamples()
        {
            return new Post
            {
                Id = 1,
                Text = "lorem ipsum",
                Created = DateTime.Now,
                CategoryId = 1,
                CreatedById = Guid.NewGuid().ToString()
            };
        }
    }
}

