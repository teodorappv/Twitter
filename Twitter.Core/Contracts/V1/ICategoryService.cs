using Twitter.Core.Domain.Entities;

namespace Twitter.Core.Contracts.V1
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
        Task<Category> GetCategoryByName(string name);
        Task<Category> Create(string name);
        Task<bool> DeleteById(int id);
        Task<bool> DeleteByName(string Name);

    }
}
