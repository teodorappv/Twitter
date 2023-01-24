using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Twitter.API.Exceptions;
using Twitter.Core.Contracts.V1;
using Twitter.Core.Domain.Entities;
using Twitter.Infrastructure.Data;

namespace Twitter.API.Services
{
    public class FastPostService : IFastPostService
    {
        private readonly TwitterAPIContext _context;

        public FastPostService(TwitterAPIContext context)
        {
            _context = context;
        }

        public async Task<FastPost> CreateFastPost(FastPost fastPostRequest)
        {
            if (await _context.Categories.FindAsync(fastPostRequest.CategoryId) == null)
            {
                throw new ValidationRequestException("Category with Id: '" + fastPostRequest.CategoryId + "' not found");
            }

            fastPostRequest.Created = DateTime.Now;
            
            await _context.FastPosts.AddAsync(fastPostRequest);
            await _context.SaveChangesAsync();
            return fastPostRequest;
        }

        public async Task<FastPost> ReadFastPost(int id)
        {
            var fastPost = await _context.FastPosts.FirstOrDefaultAsync(p => p.Id == id);
            if (fastPost == null)
            {
                throw new ValidationRequestException("FastPost with Id: '" + id + "' doesn't exist!");
            }

            var time = DateTime.Now.Subtract(fastPost.Created).TotalHours;
            if (time > 24)
            {
                throw new ValidationRequestException("24 hours passed since creation! Tweet with Id: '" + id + "' not found!");
            }
           
            return fastPost;
        }
    }
}
