using Serilog;

namespace MJDVerse.API.Extensions
{
    public static class LoggingExtensions
    {
        // Contains the configuration of Serilog logging.
        public static IHostBuilder AddLoggingServices(this IHostBuilder host,IConfiguration configuration)
     
        {

            host.UseSerilog((hostingContext, loggerConfiguration) =>loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

            return host;
        }
    }
}