using Microsoft.AspNetCore.Mvc;
using System.Net;
using Twitter.API.ActionFilters;
using Twitter.API.Exceptions;
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
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            return Ok(await _categoriesService.GetCategoryById(id));
        }

        [HttpPost("Categories")]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(string name)
        {
            return Ok(await _categoriesService.Create(name));
        }
       
        [HttpDelete("Categories/{id}")]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteById(int id)
        {
            return Ok(await _categoriesService.DeleteById(id));
        }

        [HttpDelete("Categories/DeleteByName/{Name}")]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteByName(string Name)
        {
            return Ok(await _categoriesService.DeleteByName(Name));
        }
    }
}
