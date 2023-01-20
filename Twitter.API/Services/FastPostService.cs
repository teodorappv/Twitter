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
    }
}
