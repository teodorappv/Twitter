using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Twitter.Core.Interfaces;
using Twitter.Infrastructure.Data;

namespace Twitter.API.ActionFilters
{
    public class ValidateEntityExistsAttribute<T>: IActionFilter where T : class, IEntity
    {
        private readonly TwitterAPIContext _context;
        public ValidateEntityExistsAttribute(TwitterAPIContext context) 
        {
            _context = context;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var Name = string.Empty;
            if (context.ActionArguments.ContainsKey("Name"))
            {
                Name = (string)context.ActionArguments["Name"];
            }

            var NameExists = _context.Set<T>().Any(x => x.Name.Equals(Name));
            if (NameExists)
            {
                context.Result = new BadRequestObjectResult("Category with the same name already exists!");
            }

        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
