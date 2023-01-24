using MediatR;
using Twitter.Core.Contracts.V1;
using Twitter.Core.Domain.Entities;

namespace Twitter.API.Commands
{
    public class ReadFastPostCommandHandler : IRequestHandler<ReadFastPostCommand, FastPost>
    {
        private readonly IFastPostService _service;

        public ReadFastPostCommandHandler(IFastPostService service)
        {
            _service = service;
        }

        public async Task<FastPost> Handle(ReadFastPostCommand request, CancellationToken cancellationToken)
        {
            var fastPost = await _service.ReadFastPost(request.Id);
            return fastPost;
        }
    }
}
