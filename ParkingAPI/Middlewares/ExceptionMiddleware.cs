using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using ParkingAPI.Exceptions;
using Microsoft.Extensions.Logging;

namespace ParkingAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if(exception is ServiceException)
            {
                ApplicationError applicationError = ((ServiceException)exception).ApplicationError;
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)applicationError.StatusCode;
                await context.Response.WriteAsync(
                    new ErrorResponse(applicationError.Message, (int)applicationError.StatusCode, applicationError.Code).ToString()
                );
            } 
            else
            {
                ApplicationError applicationError = ApplicationError.INTERNAL_SERVER_ERROR;
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)applicationError.StatusCode;
                await context.Response.WriteAsync(
                    new ErrorResponse(applicationError.Message, (int)applicationError.StatusCode, applicationError.Code).ToString()
                );
            }
        }
    }
}
