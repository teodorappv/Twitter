using AutoMapper;
using Twitter.Core.Domain.DTOs.Requests;
using Twitter.Core.Domain.Entities;

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
