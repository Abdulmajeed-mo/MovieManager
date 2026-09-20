using MJDVerse.Application.Interfaces;
using MJDVerse.Application.Options;
using MJDVerse.Application.Services;
using MJDVerse.Domain.Interfaces;
using MJDVerse.Infrastructure.BackgroundJobs;
using MJDVerse.Infrastructure.Providers;
using MJDVerse.Infrastructure.Repositories;
using MJDVerse.Infrastructure.Services;
using Resend;

namespace MJDVerse.API.Extensions
{
    public static class ServiceExtensions
    {
        // Contains registrations for all Application Services and Repositories
        // responsible for application logic and data access.
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
        {
            // Email & Authentication
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddHttpClient<ResendClient>();

            services.Configure<ResendClientOptions>(options =>
            {
                options.ApiToken = configuration["Resend:ApiKey"] ?? string.Empty;
            });

            services.AddTransient<IResend, ResendClient>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IAuthService, AuthService>();

            // User Service
            services.AddScoped<IUserService, UserService>();


          

            services.AddHttpClient<IMovieMetadataProvider, TmdbMovieMetadataProvider>();
            // OTP
            services.AddScoped<IOtpRepository, OtpRepository>();
            services.AddSingleton<IOtpRateLimiter, OtpRateLimiter>();

            // SMTP Settings
            services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));


            // JWT Setting
            services.Configure<JwtSettings>( configuration.GetSection("JwtSettings"));
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            // Movies
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IMovieRepository, MovieRepository>();

            // Background Service for Movie Sync
            services.AddHostedService<MovieSyncBackgroundService>();

            // Genres
            services.AddScoped<IGenreRepository, GenreRepository>();


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


            // CORS Policy for Angular Frontend
            services.AddCors(options =>{options.AddPolicy("AngularPolicy", policy =>
                {
                    policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                });
            });


            return services;
        }
    }
}