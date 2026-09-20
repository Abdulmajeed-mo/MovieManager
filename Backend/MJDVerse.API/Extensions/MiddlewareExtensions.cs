using MJDVerse.API.Middlewere;
using Serilog;

namespace MJDVerse.API.Extensions
{
    public static class MiddlewareExtensions
    {
        // Contains the registration and ordering of application middleware.
        public static WebApplication UseApplicationMiddleware(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            app.UseMiddleware<CorrelationIdMiddleware>();

            app.UseSerilogRequestLogging();

            return app;
        }
    }
}