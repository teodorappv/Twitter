using AutoMapper;
using MediatR;
using Twitter.Core.Contracts.V1;
using Twitter.Core.Domain.Entities;


namespace Twitter.API.Commands
{
    public class CreateFastPostCommandHandler : IRequestHandler<CreateFastPostCommand, FastPost>
    {
        private readonly IMapper _mapper;
        private readonly IFastPostService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateFastPostCommandHandler(IMapper mapper, IFastPostService service, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<FastPost> Handle(CreateFastPostCommand request, CancellationToken cancellationToken)
        {
            var mappedFastPost = _mapper.Map<FastPost>(request);

            mappedFastPost.CreatedById = _httpContextAccessor.HttpContext.User.Claims.First(x => x.Type.Equals("id")).Value;

            var fastPost = await _service.CreateFastPost(mappedFastPost);
            
            return fastPost;

        }
    }
}
