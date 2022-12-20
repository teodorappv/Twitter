using Microsoft.EntityFrameworkCore;
using Twitter.Core.Entities;
using Twitter.Core.Interfaces;
using Twitter.Infrastructure.Data;

namespace Twitter.API.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly TwitterAPIContext _context;
        private ILoggerManager _logger;

        public CategoriesService(TwitterAPIContext context, ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int? id)
        {
            return await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> Create(Category category)
        {
            await _context.Categories.AddAsync(category);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }
        
        public async Task<bool> DeleteById(int id)
        {
            var category = await GetCategoryById(id);
            _context.Categories.Remove(category);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> DeleteByName(string Name)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Name == Name);
            _context.Categories.Remove(category);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }

    }
}