using FluentResults;
using MediatR;
using Twitter.Core.Contracts.V1;
using Twitter.Core.Domain.Entities;

namespace Twitter.API.Commands
{
    public class ReadFastPostCommandHandler : IRequestHandler<ReadFastPostCommand, Result<FastPost>>
    {
        private readonly IFastPostService _service;

        public ReadFastPostCommandHandler(IFastPostService service)
        {
            _service = service;
        }

        public async Task<Result<FastPost>> Handle(ReadFastPostCommand request, CancellationToken cancellationToken)
        {
            var result = await _service.ReadFastPost(request.Id);
            return result;
        }
    }
}
