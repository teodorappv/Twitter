using Microsoft.EntityFrameworkCore;
using Twitter.API.Exceptions;
using Twitter.Core.Contracts;
using Twitter.Infrastructure.Data;
using Twitter.Core.Contracts.V1;
using Twitter.Core.Domain.Entities;

namespace Twitter.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly TwitterAPIContext _context;
        private ILoggerManager _logger;

        public CategoryService(TwitterAPIContext context, ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(m => m.Name == name);
        }

        public async Task<Category> Create(string name)
        {
            var existingCategory = await GetCategoryByName(name);
            if(existingCategory != null)
            {
                throw new ValidationRequestException("This name already exist.");
            }
            var newCategory = new Category() { Name = name };
            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            return newCategory;
        }
        
        public async Task<bool> DeleteById(int id)
        {
            var existingCategory = await GetCategoryById(id);
            if(existingCategory == null) 
            {
                throw new ValidationRequestException("This id doesn't exist.");
            }
            _context.Categories.Remove(existingCategory);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> DeleteByName(string Name)
        {
            var existingCategory = await GetCategoryByName(Name);
            if (existingCategory == null)
            {
                throw new ValidationRequestException("This name doesn't exist.");
            }
            _context.Categories.Remove(existingCategory);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }
    }
}