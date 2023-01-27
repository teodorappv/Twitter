using Swashbuckle.AspNetCore.Filters;
using Twitter.Core.Domain.DTOs.Request;

namespace Twitter.API.SwaggerExamples.Request
{
    public class CreatePostRequestExample : IExamplesProvider<CreatePostRequest>
    {
        public CreatePostRequest GetExamples()
        {
            return new CreatePostRequest
            {
                Text = "lorem ipsum",
                CategoryId = 1
            };
        }
    }
}
