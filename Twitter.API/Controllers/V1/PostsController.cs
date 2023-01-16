using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Net;
using System.Security.Claims;
using Twitter.API.ActionFilters;
using Twitter.API.Exceptions;
using Twitter.Core.Contracts.V1;
using Twitter.Core.Contracts.V1.Request;
using Twitter.Core.Entities;

namespace Twitter.API.Controllers.V1
{
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postRepository;
        private readonly IMapper _mapper;

        public PostsController(IPostService postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Post.GetAll)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _postRepository.GetPosts());
        }

        [HttpGet(ApiRoutes.Post.GetPost)]
        public async Task<IActionResult> GetPostById(int id)
        {
            return Ok(await _postRepository.GetPostsById(id));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost(ApiRoutes.Post.Create)]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            Post mappedPost = _mapper.Map<Post>(postRequest);
            var post = await _postRepository.CreatePost(mappedPost);
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut(ApiRoutes.Post.Update)]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdatePostRequest postRequest)
        {
            string claimedId = User.Claims.First(x => x.Type.Equals("id")).Value;
            var userIsOwner = await _postRepository.IsOwner(postRequest.Id, claimedId);

            if (!userIsOwner)
            {
                return BadRequest("You're not the owner of this post!");
            }

            Post mappedPost = _mapper.Map<Post>(postRequest);
            var post = await _postRepository.UpdatePost(mappedPost);
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete(ApiRoutes.Post.Delete)]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            await _postRepository.DeletePost(id);
            return NoContent();
        }
    }
}
