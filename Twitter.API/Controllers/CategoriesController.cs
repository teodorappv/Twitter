using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("Categories/GetCategoryById/{id}")]
        public async Task<IActionResult> GetCategoryById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoriesService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost("Categories/Create")]
        public async Task<IActionResult> Create(string Name, [Bind("Id,Name")] Category category)
        {
            if (_categoriesService.NameExists(Name))
            {
                return BadRequest("Category with the same name already exists!");
            }
            await _categoriesService.Create(category);
            return Ok(category);
        }
       
        [HttpPost("Categories/DeleteById/{id}"), ActionName("Delete")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var category = await _categoriesService.DeleteById(id);
            return Ok(category);
        }

        [HttpPost("Categories/DeleteByName/{Name}")]
        public async Task<IActionResult> DeleteByName(string Name)
        {
            var category = await _categoriesService.DeleteByName(Name);
            return Ok(category);
        }
    }
}
