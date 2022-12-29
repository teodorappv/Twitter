﻿using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Net;
using Twitter.API.ActionFilters;
using Twitter.API.Exceptions;
using Twitter.Core.Contracts.V1;
using Twitter.Core.Contracts.V1.Request;

namespace Twitter.API.Controllers.V1
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IUserService _userService;

        public IdentityController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost(ApiRoutes.User.Register)]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest userRequest)
        {
            var user = await _userService.Register(userRequest);
            return Ok(user);
        }

        
    }
}