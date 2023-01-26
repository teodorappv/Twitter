using Microsoft.AspNetCore.Mvc;
using System.Net;
using Twitter.API.ActionFilters;
using Twitter.API.Exceptions;
using Twitter.Core.Contracts;
using Twitter.Core.Contracts.V1;

namespace Twitter.API.Services
{

    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        private ILoggerManager _logger;

        public CategoryController(ICategoryService service, ILoggerManager logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet(ApiRoutes.Categories.GetAll)]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _service.GetCategories());
        }

        [HttpGet(ApiRoutes.Categories.GetCategory)]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            return Ok(await _service.GetCategoryById(id));
        }

        [HttpPost(ApiRoutes.Categories.Create)]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(string name)
        {
            return Ok(await _service.Create(name));
        }

        [HttpDelete(ApiRoutes.Categories.Delete)]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _service.DeleteById(id);
            return NoContent();
        }

        [HttpDelete(ApiRoutes.Categories.DeleteByName)]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteByName(string Name)
        {
            await _service.DeleteByName(Name);
            return NoContent();
        }
    }
}
