using Swashbuckle.AspNetCore.Filters;
using Twitter.Core.Domain.Entities;

namespace Twitter.API.SwaggerExamples.Responses
{
    public class FastPostResponseExample : IExamplesProvider<FastPost>
    {
        public FastPost GetExamples()
        {
            return new FastPost
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