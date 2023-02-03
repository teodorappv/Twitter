using FluentResults;
using MediatR;
using Twitter.Core.Domain.Entities;

namespace Twitter.API.Commands
{
    public class CreateFastPostCommand : IRequest<Result<FastPost>>
    {
        public string? Text { get; set; }
        public int CategoryId { get; set; }

    }
}
