using Microsoft.AspNetCore.Mvc;
using System.Net;
using Twitter.API.ActionFilters;
using Twitter.API.Exceptions;
using Twitter.Core.Contracts;
using Twitter.Core.Contracts.V1;
using Twitter.Core.Domain.DTOs.Requests;

namespace Twitter.API.Controllers.V1
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IUserService _service;

        public IdentityController(IUserService service)
        {
            _service = service;
        }

        [HttpPost(ApiRoutes.User.CreateRole)]
        public async Task<IActionResult> CreateRole([FromBody] string Name)
        {
            var role = await _service.CreateRole(Name);
            return Ok(role);
        }

        [HttpPost(ApiRoutes.User.Register)]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest userRequest)
        {
            var user = await _service.Register(userRequest);
            return Ok(user);
        }

        [HttpPost(ApiRoutes.User.Login)]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest userRequest)
        {
            return Ok(await _service.Login(userRequest));
        }
    }
}