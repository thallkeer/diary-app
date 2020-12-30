using DiaryApp.API.Middleware;
using Microsoft.AspNetCore.Builder;

namespace DiaryApp.API.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}