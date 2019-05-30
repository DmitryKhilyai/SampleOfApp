using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using WebAPI.Models;

namespace WebAPI.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this._next = next;
            this._logger = loggerFactory.CreateLogger<ErrorHandlingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                await HandleExceptionAsync(context, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (ex is ArgumentNullException) code = HttpStatusCode.NotFound;
            else if (ex is ArgumentException) code = HttpStatusCode.BadRequest;
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(
                new ErrorDetails {
                    Message = ex.Message,
                    StatusCode = (int)code
                }.ToString());
        }
    }
}
