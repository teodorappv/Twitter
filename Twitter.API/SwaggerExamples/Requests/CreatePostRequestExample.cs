using Swashbuckle.AspNetCore.Filters;
using Twitter.Core.Domain.DTOs.Requests;

namespace Twitter.API.SwaggerExamples.Requests
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
