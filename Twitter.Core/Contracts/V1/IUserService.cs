using Twitter.Core.Contracts.V1.Request;
using Twitter.Core.Contracts.V1.Responses;
using Twitter.Core.Entities;

namespace Twitter.Core.Contracts.V1
{
    public interface IUserService
    {
        Task<AppUser> Register(RegisterUserRequest userRequest);
        Task<AuthResponse> Login(LoginUserRequest userRequest);
    }
}
