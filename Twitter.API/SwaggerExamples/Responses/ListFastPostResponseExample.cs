using Swashbuckle.AspNetCore.Filters;
using Twitter.Core.Domain.Entities;

namespace Twitter.API.SwaggerExamples.Responses
{
    public class ListFastPostResponseExample : IExamplesProvider<List<FastPost>>
    {
        public List<FastPost> GetExamples()
        {
            var fastPosts = new List<FastPost>
            {
                new FastPost
                {
                    Id = 1,
                    Text = "lorem ipsum",
                    Created = DateTime.Now,
                    CategoryId = 1,
                    CreatedById = Guid.NewGuid().ToString()
                },

                new FastPost
                {
                    Id = 2,
                    Text = "lorem ipsum",
                    Created = DateTime.Now,
                    CategoryId = 2,
                    CreatedById = Guid.NewGuid().ToString()
                }
            };

            return fastPosts;
        }
    }
}