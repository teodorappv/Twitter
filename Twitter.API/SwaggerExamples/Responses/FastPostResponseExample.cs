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
                CreatedById = "cd4ce98c - ce4e - 415d - a046 - 0fcce677b16c"
            };
        }
    }
}