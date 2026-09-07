using Microsoft.EntityFrameworkCore;
using MJDVerse.Infrastructure.Context;

namespace MJDVerse.API.Extensions
{
    public static class DatabaseExtensions
    {
        // Contains the registration of Entity Framework Core and the application database context.
        public static IServiceCollection AddDatabase(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}