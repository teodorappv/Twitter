using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Twitter.API.ActionFilters;
using Twitter.API.Exceptions;
using Twitter.Core.Contracts;
using Twitter.Core.Contracts.V1;
using Twitter.Core.Domain.DTOs.Requests;
using Twitter.Core.Domain.DTOs.Responses;
using Twitter.Core.Domain.Entities;

namespace Twitter.API.Controllers.V1
{
    [ApiController]
    [Produces("application/json")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _service;
        private readonly IMapper _mapper;

        public PostController(IPostService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all posts.
        /// </summary>
        /// <returns>Returns all posts.</returns>
        /// <response code = "200">Successfully returned all posts.</response>
        [HttpGet(ApiRoutes.Post.GetAll)]
        [ProducesResponseType(typeof(List<Post>), 200)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetPosts());
        }

        /// <summary>
        /// Returns all posts with pagination.
        /// </summary>
        /// <returns>Returns all posts with pagination.</returns>
        /// <response code = "200">Successfully returned all posts with pagination.</response>
        [HttpGet(ApiRoutes.Post.ReadAll)]
        [ProducesResponseType(typeof(PostResponse<Post>), 200)]
        public async Task<IActionResult> ReadAll([FromQuery] PostParameters postParameters)
        {
            var posts = await _service.ReadAllPosts(postParameters);
            return Ok(new PostResponse<Post>(posts));
        }

        /// <summary>
        /// Returns a number of available posts.
        /// </summary>
        /// <returns>Returns a number of available posts.</returns>
        /// <response code = "200">Successfully returned number of available posts.</response>
        [HttpGet(ApiRoutes.Post.NumberOfAvailablePosts)]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<ActionResult<int>> NumberOfAvailablePosts([FromQuery] int? categoryId)
        {
            return Ok(await _service.NumberOfAvailablePosts(categoryId));
        }

        /// <summary>
        /// Returns a post.
        /// </summary>
        /// <param name="id">if post doesn't exists, an exception will be thrown</param>
        /// <returns>Returns a post.</returns>
        /// <response code = "200">Post successfully returned.</response>
		/// <response code = "400">Post doesn't exists.</response>
        [HttpGet(ApiRoutes.Post.GetPost)]
        [ProducesResponseType(typeof(Post), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPostById(int id)
        {
            return Ok(await _service.GetPostById(id));
        }

        /// <summary>
        /// Only admin users can create new post if text field is less then 400 characters.
        /// </summary>
        /// <param name="postRequest">Text field must be less then 400 characters and category id must be not null.</param>
        /// <returns>Returns created post.</returns>
        /// <response code = "200">Post successfully created.</response>
		/// <response code = "400">Category id doesn't exists.</response>
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost(ApiRoutes.Post.Create)]
        [ProducesResponseType(typeof(Post), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            Post mappedPost = _mapper.Map<Post>(postRequest);
            mappedPost.CreatedById = User.Claims.First(x => x.Type.Equals("id")).Value;
            var post = await _service.CreatePost(mappedPost);
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
        }

        /// <summary>
        /// Only admin users can update only their own posts if text field is less then 400 characters.
        /// </summary>
        /// <param name="postRequest">Text field must be less then 400 characters and category id must be not null.</param>
        /// <returns>Returns updated post.</returns>
        /// <response code = "200">Post updated successfully.</response>
		/// <response code = "400">User doesn't own the post or category id doesn't exists.</response>
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut(ApiRoutes.Post.Update)]
        [ProducesResponseType(typeof(Post), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        public async Task<IActionResult> Update([FromBody] UpdatePostRequest postRequest)
        {
            string claimedId = User.Claims.First(x => x.Type.Equals("id")).Value;
            var userIsOwner = await _service.IsOwner(postRequest.Id, claimedId);
            if (!userIsOwner)
            {
                return BadRequest("You're not the owner of this post!");
            }
            Post mappedPost = _mapper.Map<Post>(postRequest);
            var post = await _service.UpdatePost(mappedPost);
            int counter = 1;
            if (post.IsFailed)
            {
                return BadRequest("Errors: " + post.Errors.Select(e => "Error " + counter++ + ": " + e.Message).Aggregate((i, j) => i + "; " + j));
            }
            return CreatedAtAction(nameof(Get), new { id = post.Value.Id }, post.Value);
        }

        /// <summary>
        /// Only admin users can delete a post.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		/// <response code = "400">Post doesn't exists.</response>
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete(ApiRoutes.Post.Delete)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeletePost(id);
            return NoContent();
        }
    }
}
