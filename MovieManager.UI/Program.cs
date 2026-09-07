using MJDVerse.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Serilog
builder.Host.AddLoggingServices(builder.Configuration);
// Services
builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddAuthenticationServices();

builder.Services.AddApplicationServices(builder.Configuration);


builder.Services.AddValidationServices();

builder.Services.AddApiServices();

var app = builder.Build();

// Middleware
app.UseApplicationMiddleware();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();