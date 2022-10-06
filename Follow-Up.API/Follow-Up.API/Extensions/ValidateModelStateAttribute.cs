using Follow_Up.Application.Enums;
using Follow_Up.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Follow_Up.API.Extensions
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Any())
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage)
                        .ToList();

                var responseObj = new BaseResponse<object>(ProcessStatusEnum.BadRequest, new FriendlyMessage { Message = errors.FirstOrDefault() ?? "Lütfen İsteğinizi Kontrol Edin." }, null);


                context.Result = new JsonResult(responseObj)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }
    }
}
