using MJDVerse.Application.Interfaces;
using MJDVerse.Application.Options;
using MJDVerse.Application.Services;
using MJDVerse.Domain.Interfaces;
using MJDVerse.Infrastructure.Repositories;
using MJDVerse.Infrastructure.Services;

namespace MJDVerse.API.Extensions
{
    public static class ServiceExtensions
    {
        // Contains registrations for all Application Services and Repositories
        // responsible for application logic and data access.
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Email & Authentication
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IAuthService, AuthService>();





            // OTP
            services.AddScoped<IOtpRepository, OtpRepository>();
            services.AddSingleton<IOtpRateLimiter, OtpRateLimiter>();

            // SMTP Settings
            services.Configure<SmtpSettings>(
                configuration.GetSection("SmtpSettings"));

            // Movies
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IMovieRepository, MovieRepository>();



            // Watchlist
            services.AddScoped<IWatchlistService, WatchlistService>();
            services.AddScoped<IWatchlistRepository, WatchlistRepository>();

            // Favorites
            services.AddScoped<IFavoriteService, FavoriteService>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();

            // Ratings
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IRatingRepository, RatingRepository>();

            // Watch History
            services.AddScoped<IWatchHistoryService, WatchHistoryService>();
            services.AddScoped<IWatchHistoryRepository, WatchHistoryRepository>();

            return services;
        }
    }
}