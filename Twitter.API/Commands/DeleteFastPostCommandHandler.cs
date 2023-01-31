using FluentResults;
using MediatR;
using Twitter.Core.Contracts.V1;

namespace Twitter.API.Commands
{
    public class DeleteFastPostCommandHandler : IRequestHandler<DeleteFastPostCommand, Result<bool>>
    {
        private readonly IFastPostService _service;

        public DeleteFastPostCommandHandler(IFastPostService service)
        {
            _service = service;
        }

        public async Task<Result<bool>> Handle(DeleteFastPostCommand request, CancellationToken cancellationToken)
        {
            var result = await _service.DeleteFastPost(request.Id);
            return result;
        }
    }
}
