using AutoMapper;
using Twitter.API.Commands;
using Twitter.Core.Domain.Entities;

namespace Twitter.API.Profiles
{
    public class FastPostProfile : Profile
    {
        public FastPostProfile()
        {
            CreateMap<CreateFastPostCommand, FastPost>();
        }
    }
}
