using MediatR;
using Twitter.Core.Contracts.V1;
using Twitter.Core.Domain.Entities;

namespace Twitter.API.Commands
{
    public class ReadAllFastPostsCommandHandler : IRequestHandler<ReadAllFastPostsCommand, List<FastPost>>
    {
        private readonly IFastPostService _service;

        public ReadAllFastPostsCommandHandler(IFastPostService service)
        {
            _service = service;
        }

        public async Task<List<FastPost>> Handle(ReadAllFastPostsCommand request, CancellationToken cancellationToken)
        {
            var fastPost = await _service.ReadAllFastPosts();
            return fastPost;
        }
    }
}
