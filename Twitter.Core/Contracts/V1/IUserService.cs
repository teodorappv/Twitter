using Microsoft.AspNetCore.Identity;
using Twitter.Core.Domain.DTOs.Requests;
using Twitter.Core.Domain.DTOs.Responses;
using Twitter.Core.Domain.Entities;

namespace Twitter.Core.Contracts.V1
{
    public interface IUserService
    {
        Task<IdentityRole> CreateRole(string Name);
        Task<AppUser> Register(RegisterUserRequest userRequest);
        Task<AuthResponse> Login(LoginUserRequest userRequest);
    }
}
