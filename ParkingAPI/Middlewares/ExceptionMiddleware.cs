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
                _logger.LogError(ex, "Something went wrong.");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ApplicationError applicationError = DefineApplicationError(exception);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)applicationError.StatusCode;
            await context.Response.WriteAsync(
                new ErrorResponse(applicationError.Message, (int)applicationError.StatusCode, applicationError.Code).ToString()
            );
        }

        private static ApplicationError DefineApplicationError(Exception exception)
        {
            var error = ((ServiceException)exception)?.ApplicationError;
            if(error != null)
                return error;
            return ApplicationError.INTERNAL_SERVER_ERROR;
        }
    }
}
