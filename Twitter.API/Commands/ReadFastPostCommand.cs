using FluentResults;
using MediatR;
using Twitter.Core.Domain.Entities;

namespace Twitter.API.Commands
{
    public class ReadFastPostCommand : IRequest<Result<FastPost>>
    {
        public int Id { get; set; }
        
        public ReadFastPostCommand(int id)
        {
            Id = id;
        }
    }
}
