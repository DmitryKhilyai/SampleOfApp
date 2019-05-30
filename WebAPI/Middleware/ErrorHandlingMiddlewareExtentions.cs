using Microsoft.AspNetCore.Builder;

namespace WebAPI.Middleware
{
    public static class ErrorHandlingMiddlewareExtentions
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
