using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MJDVerse.API.Middlewere;
using MJDVerse.Application.Interfaces;
using MJDVerse.Application.Options;
using MJDVerse.Application.Services;
using MJDVerse.Application.Validators.Movies;
using MJDVerse.Domain.Entities;
using MJDVerse.Domain.Interfaces;
using MJDVerse.Infrastructure.Context;
using MJDVerse.Infrastructure.Repositories;
using MJDVerse.Infrastructure.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

// EF Core
builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddScoped<IIdentityService, IdentityService>();

builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddScoped<IOtpRepository, OtpRepository>();

builder.Services.AddScoped<IOtpRateLimiter, OtpRateLimiter>();

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));



builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

builder.Services.AddScoped<IWatchlistService, WatchlistService>();

builder.Services.AddScoped<IWatchlistRepository, WatchlistRepository>();

builder.Services.AddScoped<IFavoriteService, FavoriteService>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();


builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();

builder.Services.AddScoped<IWatchHistoryService,WatchHistoryService>();
builder.Services.AddScoped<IWatchHistoryRepository, WatchHistoryRepository>();
// Controllers

builder.Services.AddControllers();

//Add Memory Cache
builder.Services.AddMemoryCache();
// HTTP Client
builder.Services.AddHttpClient();


builder.Services.AddValidatorsFromAssemblyContaining<CreateMovieValidator>();










var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseMiddleware<CorrelationIdMiddleware>();

app.UseSerilogRequestLogging();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();