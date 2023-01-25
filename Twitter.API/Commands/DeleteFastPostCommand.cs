using MediatR;

namespace Twitter.API.Commands
{
    public class DeleteFastPostCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteFastPostCommand(int id)
        {
            Id = id;
        }
    }

}
