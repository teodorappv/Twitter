using Swashbuckle.AspNetCore.Filters;
using Twitter.API.Commands;

namespace Twitter.API.SwaggerExamples.Requests
{
    public class CreateFastPostCommandExample : IExamplesProvider<CreateFastPostCommand>
    {
        public CreateFastPostCommand GetExamples()
        {
            return new CreateFastPostCommand
            {
                Text = "lorem ipsum",
                CategoryId = 1
            };
        }
    }
}