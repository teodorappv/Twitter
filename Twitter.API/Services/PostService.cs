using Microsoft.EntityFrameworkCore;
using Twitter.API.Exceptions;
using Twitter.Core.Domain.Entities;
using Twitter.Infrastructure.Data;

namespace Twitter.Core.Contracts.V1
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

        public async Task<Post> GetPostsById(int id)
        {
            return await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
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

        public async Task<Post> UpdatePost(Post postRequest)
        {
            Post post = await _context.Posts.FirstOrDefaultAsync(post => post.Id.Equals(postRequest.Id));

            if (post == null)
            {
                throw new ValidationRequestException("Post with Id: '" + postRequest.Id + "' not found");
            }

            if (await _context.Categories.FindAsync(postRequest.CategoryId) == null)
            {
                throw new ValidationRequestException("Category with Id: '" + postRequest.CategoryId + "' not found");
            }

            post.CategoryId = postRequest.CategoryId;
            post.Text = postRequest.Text;
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<bool> DeletePost(int id)
        {
            var existingPost = await GetPostsById(id);
            if (existingPost == null)
            {
                throw new ValidationRequestException("This id doesn't exist.");
            }
            _context.Posts.Remove(existingPost);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
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
    }
}