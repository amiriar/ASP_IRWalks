using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP_CORE_API.CustomActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    // Check if the ModelState is invalid
        //    if (!context.ModelState.IsValid)
        //    {
        //        context.Result = new BadRequestObjectResult(context.ModelState);
        //    }
        //}
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = string.Join("; ", context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                Console.WriteLine($"Validation Errors: {errors}"); // Replace with proper logging
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

    }
}
