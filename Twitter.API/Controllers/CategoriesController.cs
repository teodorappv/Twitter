using Microsoft.AspNetCore.Mvc;
using Twitter.API.ActionFilters;
using Twitter.Core.Entities;
using Twitter.Core.Interfaces;

namespace Twitter.API.Controllers
{

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private ILoggerManager _logger;

        public CategoriesController(ICategoriesService categoriesService, ILoggerManager logger)
        {
            _categoriesService = categoriesService;
            _logger = logger;
        }

        [HttpGet("Categories")]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _categoriesService.GetCategories());
        }

        [HttpGet("Categories/{id}")]
        public async Task<IActionResult> GetCategoryById(int? id)
        {
            var category = await _categoriesService.GetCategoryById(id);
            return Ok(category);
        }

        [HttpPost("Categories")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Category>))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Create(string Name, [Bind("Id,Name")] Category category)
        {
            await _categoriesService.Create(category);
            return Ok(category);
        }
       
        [HttpDelete("Categories/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var category = await _categoriesService.DeleteById(id);
            return Ok(category);
        }

        [HttpDelete("Categories/DeleteByName/{Name}")]
        public async Task<IActionResult> DeleteByName(string Name)
        {
            var category = await _categoriesService.DeleteByName(Name);
            return Ok(category);
        }
    }
}
