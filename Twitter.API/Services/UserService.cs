using Microsoft.AspNetCore.Identity;
using Twitter.API.Exceptions;
using Twitter.Core.Contracts.V1;
using Twitter.Core.Contracts.V1.Request;
using Twitter.Core.Contracts.V1.Responses;
using Twitter.Core.Entities;

namespace Twitter.API.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<AppUser> Register(RegisterUserRequest userRequest)
        {
            var userExists = await _userManager.FindByNameAsync(userRequest.Username);

            if (userExists != null)
            {
                throw new ValidationRequestException("User with Username: '" + userRequest.Username + "' already exists.");
            }

            AppUser user = new()
            {
                Id = Guid.NewGuid().ToString(),
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                Email = userRequest.Email,
                City = userRequest.City,
                UserName= userRequest.Username
            };

            await _userManager.CreateAsync(user, userRequest.Password);
            
            await _userManager.AddToRoleAsync(user, UserRoles.User);

            return user;
        }

        public Task<AuthResponse> Login(LoginUserRequest userRequest)
        {
            throw new NotImplementedException();
        }
    }
}
