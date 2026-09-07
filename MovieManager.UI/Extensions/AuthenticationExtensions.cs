using Microsoft.AspNetCore.Identity;
using MJDVerse.Domain.Entities;
using MJDVerse.Infrastructure.Context;

namespace MJDVerse.API.Extensions
{
    public static class AuthenticationExtensions
    {
        // Contains the configuration for Identity and authentication services.
        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            return services;
        }
    }
}