using Microsoft.EntityFrameworkCore;
using Twitter.API.Exceptions;
using Twitter.Core.Domain.Entities;
using Twitter.Infrastructure.Data;
using Twitter.Core.Contracts.V1;
using Twitter.Core.Contracts;
using FluentResults;

namespace Twitter.API.Services
{
    public class PostService : IPostService
    {
        private readonly TwitterAPIContext _context;
        private ILoggerManager _logger;

        public PostService(TwitterAPIContext context, ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Post>> GetPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<List<Post>> ReadAllPosts(PostParameters postParameters)
        {
            if (postParameters.CategoryId.Equals(null))
            {
                return await _context.Posts.Where(p => p.IsArchived == false).OrderBy(p => p.Created).ToListAsync();
            }
            return await _context.Posts.Where(p => p.CategoryId == postParameters.CategoryId && p.IsArchived == false).OrderBy(p => p.Created).
                Skip((postParameters.PageNumber - 1) * postParameters.PageSize).Take(postParameters.PageSize).ToListAsync();
        }

        public async Task<int> NumberOfAvailablePosts (int? categoryId)
        {
            if(categoryId == null)
            {
                return await _context.Posts.Where(p => p.IsArchived == false).CountAsync();
            }
            return await _context.Posts.Where(p => p.IsArchived == false && p.CategoryId == categoryId).CountAsync();
        }

        public async Task<Post> GetPostById(int id)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(p => p.Id == id && p.IsArchived == false);
            if (post == null)
            {
                throw new ValidationRequestException("Post with Id: '" + id + "' doesn't exist.");
            }
            return post;
        }

        public async Task<Post> CreatePost(Post postRequest)
        {
            if (await _context.Categories.FindAsync(postRequest.CategoryId) == null)
            {
                throw new ValidationRequestException("Category with Id: '" + postRequest.CategoryId + "' not found");
            }

            postRequest.Created = DateTime.Now;

            await _context.Posts.AddAsync(postRequest);
            await _context.SaveChangesAsync();

            return postRequest;
        }

        public async Task<Result<Post>> UpdatePost(Post postRequest)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(post => post.Id.Equals(postRequest.Id) && post.IsArchived == false);

            if (post == null)
            {
                return Result.Fail(new Error("Post with Id: '" + postRequest.Id + "' not found"));
            }

            if (await _context.Categories.FindAsync(postRequest.CategoryId) == null)
            {
                return Result.Fail(new Error("Category with Id: '" + postRequest.CategoryId + "' not found"));
            }
            
            post.CategoryId = postRequest.CategoryId;
            post.Text = postRequest.Text;
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return Result.Ok(post);
        }

        public async Task<bool> DeletePost(int id)
        {
            var existingPost = await GetPostById(id);
            existingPost.IsArchived = true;
            _context.Posts.Update(existingPost);
            var updatedPost = await _context.SaveChangesAsync();
            return updatedPost > 0;
        }

        public async Task<bool> IsOwner(int postId, string userId)
        {
            var post = await _context.Posts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == postId);
            if (post == null || post.CreatedById != userId)
            {
                return false;
            }
            return true;
        }

        public async Task<List<Post>> UserNonArchivedPosts (string userId)
        {
            var firstPost = await _context.Posts.Where(post => post.CreatedById.Equals(userId) && post.IsArchived == false).
                OrderByDescending(post => post.Created).FirstOrDefaultAsync();

            var thirdPost = await _context.Posts.Where(post => post.CreatedById.Equals(userId) && post.IsArchived == false).
                OrderBy(post => post.Created).Skip(2).Take(1).FirstOrDefaultAsync();

            var lastPost = await _context.Posts.Where(post => post.CreatedById.Equals(userId) && post.IsArchived == false).
                OrderByDescending(post => post.Created).LastOrDefaultAsync();

            List<Post> listOfPosts = new List<Post>
            {
                firstPost,
                thirdPost,
                lastPost
            };

            return listOfPosts;
        }
    }
}