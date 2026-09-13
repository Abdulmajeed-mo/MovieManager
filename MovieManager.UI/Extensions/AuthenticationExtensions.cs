using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MJDVerse.Application.Options;
using MJDVerse.Domain.Entities;
using MJDVerse.Infrastructure.Context;
using System.Text;

namespace MJDVerse.API.Extensions
{
    public static class AuthenticationExtensions
    {
        // Contains the configuration for Identity and authentication services.
        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services,IConfiguration configuration)
        {



            // Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>{options.User.RequireUniqueEmail = true;}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();







            // JWT Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme =JwtBearerDefaults.AuthenticationScheme;
            })


            .AddJwtBearer(options =>
            {
                var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings!.Issuer,
                    ValidAudience = jwtSettings.Audience,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });

            return services;
        }
    }
}