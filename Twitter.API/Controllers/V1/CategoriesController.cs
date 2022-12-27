using Microsoft.AspNetCore.Mvc;
using System.Net;
using Twitter.API.ActionFilters;
using Twitter.API.Exceptions;
using Twitter.Core.Contracts;
using Twitter.Core.Contracts.V1;

namespace Twitter.API.Services
{

    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;
        private ILoggerManager _logger;

        public CategoriesController(ICategoriesService categoriesService, ILoggerManager logger)
        {
            _categoriesService = categoriesService;
            _logger = logger;
        }

        [HttpGet(ApiRoutes.Categories.GetAll)]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _categoriesService.GetCategories());
        }

        [HttpGet(ApiRoutes.Categories.GetCategory)]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            return Ok(await _categoriesService.GetCategoryById(id));
        }

        [HttpPost(ApiRoutes.Categories.Create)]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(string name)
        {
            return Ok(await _categoriesService.Create(name));
        }

        [HttpDelete(ApiRoutes.Categories.Delete)]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _categoriesService.DeleteById(id);
            return NoContent();
        }

        [HttpDelete(ApiRoutes.Categories.DeleteByName)]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteByName(string Name)
        {
            await _categoriesService.DeleteByName(Name);
            return NoContent();
        }
    }
}
