using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Twitter.Core.Entities;

namespace Twitter.Core.Interfaces
{
    public interface ICategoriesService
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategoryById(int? id);
        Task<bool> Create(Category category);
        bool NameExists(string Name);
        Task<bool> DeleteById(int id);
        Task<bool> DeleteByName(string Name);

    }
}
