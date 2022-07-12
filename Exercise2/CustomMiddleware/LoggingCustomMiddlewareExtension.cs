using Microsoft.AspNetCore.Builder;

namespace EmployeeManagerment.CustomMiddleware
{
    public static class LoggingCustomMiddlewareExtension
    {
        public static IApplicationBuilder UseLoggingCustomMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingCustomMiddleware>();
        }
    }
}
