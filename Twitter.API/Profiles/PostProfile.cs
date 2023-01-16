using AutoMapper;
using Twitter.Core.Contracts.V1.Request;
using Twitter.Core.Entities;

namespace Twitter.API.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<CreatePostRequest, Post>();
            CreateMap<UpdatePostRequest, Post>();
        }
    }
}
