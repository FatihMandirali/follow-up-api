using Follow_Up.Application.Enums;
using Follow_Up.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow_Up.Application.Middleware
{
    public class ExceptionCatcherMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionCatcherMiddleware> _logger;

        public ExceptionCatcherMiddleware(ILogger<ExceptionCatcherMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Catch Middleware");
                await context.Response.WriteAsJsonAsync(new BaseResponse<object>(ProcessStatusEnum.InternalServerError,
                    new FriendlyMessage("Sunucu Kaynaklı Hata Meydana Geldi")));
            }
        }
    }
}
