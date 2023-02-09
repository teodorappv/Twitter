using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Twitter.API.Exceptions;
using Twitter.Core.Contracts.V1;
using Twitter.Core.Domain.DTOs.Requests;
using Twitter.Core.Domain.DTOs.Responses;
using Twitter.Core.Domain.Entities;

namespace Twitter.API.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly IPostService _postService;

        public UserService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config, IPostService postService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
            _postService = postService;
        }

        public async Task<IdentityRole> CreateRole(string Name)
        {
            var newRole = new IdentityRole() { Name = Name };
            await _roleManager.CreateAsync(newRole);
            return newRole;
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

        public async Task<AuthResponse> Login(LoginUserRequest userRequest)
        {
            var user = await _userManager.FindByNameAsync(userRequest.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, userRequest.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("id", user.Id)
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GenerateToken(authClaims);
           
                return new AuthResponse
                {
                    Success = true,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Posts = await _postService.UserNonArchivedPosts(user.Id)
                };
            }
            else
            {
                throw new ValidationRequestException("Username or password is not correct!");
            }
        }

        private JwtSecurityToken GenerateToken(List<Claim> authClaims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(30),
                claims: authClaims,
                signingCredentials: credentials
                );

            return token;
        }
    }
}
