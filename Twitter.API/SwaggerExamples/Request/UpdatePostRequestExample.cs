using Swashbuckle.AspNetCore.Filters;
using Twitter.Core.Domain.DTOs.Request;

namespace Twitter.API.SwaggerExamples.Request
{
    public class UpdatePostRequestExample : IExamplesProvider<UpdatePostRequest>
    {
        public UpdatePostRequest GetExamples()
        {
            return new UpdatePostRequest
            {
                Id = 1,
                Text = "lorem ipsum",
                CategoryId = 1
            };
        }
    }
}
