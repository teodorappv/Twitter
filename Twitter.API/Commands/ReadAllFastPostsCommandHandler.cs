using FluentResults;
using MediatR;
using Twitter.Core.Contracts.V1;
using Twitter.Core.Domain.Entities;

namespace Twitter.API.Commands
{
    public class ReadAllFastPostsCommandHandler : IRequestHandler<ReadAllFastPostsCommand, Result<List<FastPost>>>
    {
        private readonly IFastPostService _service;

        public ReadAllFastPostsCommandHandler(IFastPostService service)
        {
            _service = service;
        }

        public async Task<Result<List<FastPost>>> Handle(ReadAllFastPostsCommand request, CancellationToken cancellationToken)
        {
            var result = await _service.ReadAllFastPosts();
            return result;
        }
    }
}
