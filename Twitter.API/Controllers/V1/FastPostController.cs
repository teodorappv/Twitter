using MediatR;
using Microsoft.AspNetCore.Mvc;
using Twitter.API.Commands;
using Twitter.Core.Contracts;
using Twitter.Core.Domain.Entities;

namespace Twitter.API.Controllers.V1
{
    [ApiController]
    [Produces("application/json")]
    public class FastPostController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FastPostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a fast post if text field is less then 400 characters.
        /// </summary>
        /// <param name="command">Text field must be less then 400 characters.</param>
        /// <returns>Returns created fast post.</returns>
        /// <response code = "200">Fast post successfully created.</response>
		/// <response code = "400">Category id doesn't exists.</response>
        [HttpPost(ApiRoutes.FastPost.Create)]
        [ProducesResponseType(typeof(FastPost), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        public async Task<IActionResult> Create([FromBody] CreateFastPostCommand command)
        {
            var result = await _mediator.Send(command);
            int counter = 1;
            if (result.IsFailed)
            {
                return BadRequest("Errors: " + result.Errors.Select(e => "Error " + counter++ + ": " + e.Message).Aggregate((i, j) => i + "; " + j));
            }
            return Ok(result.Value);
        }

        /// <summary>
        /// Returns a fast post if it was created in the last 24 hours.
        /// </summary>
        /// <param name="id">If 24 hours passed since creation, fast post will not be returned.</param>
        /// <returns>Returns a fast post.</returns>
        /// <response code = "200">Successfully returned all fast posts.</response>
		/// <response code = "400">Post doesn't exists.</response>
        [HttpGet(ApiRoutes.FastPost.GetFastPost)]
        [ProducesResponseType(typeof(FastPost), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        public async Task<IActionResult> ReadFastPost(int id)
        {
            var result = await _mediator.Send(new ReadFastPostCommand(id));
            int counter = 1;
            if (result.IsFailed)
            {
                return BadRequest("Errors: " + result.Errors.Select(e => "Error " + counter++ + ": " + e.Message).Aggregate((i, j) => i + "; " + j));
            }
            return Ok(result.Value);
        }

        /// <summary>
        /// Returns the list of posts if the post was created in the last 24 hours.
        /// </summary>
        /// <returns>Returns a list of fast posts.</returns>
        /// <response code = "200">Returns a list of fast posts.</response>
        [HttpGet(ApiRoutes.FastPost.GetAll)]
        [ProducesResponseType(typeof(List<FastPost>), 200)]
        public async Task<IActionResult> ReadAllFastPost()
        {
            var result = await _mediator.Send(new ReadAllFastPostsCommand());
            return Ok(result.Value);
        }

        /// <summary>
        /// Deletes a post if it was created in the last 24 hours.
        /// </summary>
        /// <param name="id">If 24 hours passed since creation, fast post will not be deleted.</param>
        /// <returns></returns>
		/// <response code = "400">Fast post doesn't exists.</response>
        [HttpDelete(ApiRoutes.FastPost.Delete)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        public async Task<ActionResult> DeleteFastPost(int id)
        {
            var result = await _mediator.Send(new DeleteFastPostCommand(id));
            int counter = 1;
            if (result.IsFailed)
            {
                return BadRequest("Errors: " + result.Errors.Select(e => "Error " + counter++ + ": " + e.Message).Aggregate((i, j) => i + "; " + j));
            }
            return NoContent();
        }
    }
}
