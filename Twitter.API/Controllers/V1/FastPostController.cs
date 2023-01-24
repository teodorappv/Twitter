using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Twitter.API.ActionFilters;
using Twitter.API.Commands;
using Twitter.API.Exceptions;
using Twitter.Core.Contracts;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Twitter.API.Controllers.V1
{
    [ApiController]
    public class FastPostController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FastPostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(ApiRoutes.FastPost.Create)]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateFastPostCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.FastPost.GetFastPost)]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ReadFastPost(int id)
        {
            var result = await _mediator.Send(new ReadFastPostCommand(id));
            return Ok(result);
        }
    }
}
