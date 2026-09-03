using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MJDVerse.Application.Interfaces;
using MJDVerse.Application.Services;
using MJDVerse.Domain.Entities;
using MJDVerse.Infrastructure.Context;
using MJDVerse.Infrastructure.Repositories;
using MJDVerse.Infrastructure.Services;
using MovieManager.UI.Middlewere;
using Serilog;
using MJDVerse.Application.Options;

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

// Controllers

builder.Services.AddControllers();

//Add Memory Cache
builder.Services.AddMemoryCache();
// HTTP Client
builder.Services.AddHttpClient();

var app = builder.Build();

// Serilog Request Logging
app.UseSerilogRequestLogging();

// Middleware
app.UseMiddleware<LogMiddleware>();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();