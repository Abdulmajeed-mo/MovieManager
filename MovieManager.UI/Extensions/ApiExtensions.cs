namespace MJDVerse.API.Extensions
{
    public static class ApiExtensions
    {
        // Contains common API service registrations.
        public static IServiceCollection AddApiServices(
            this IServiceCollection services)
        {
            services.AddControllers();
            services.AddMemoryCache();
            services.AddHttpClient();

            return services;
        }
    }
}