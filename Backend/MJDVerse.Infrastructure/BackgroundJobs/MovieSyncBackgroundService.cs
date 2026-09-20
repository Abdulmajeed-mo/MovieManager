using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MJDVerse.Application.Interfaces;

namespace MJDVerse.Infrastructure.BackgroundJobs
{
    public class MovieSyncBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public MovieSyncBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();

                var movieService = scope.ServiceProvider
                    .GetRequiredService<IMovieService>();

                await movieService.SyncMoviesAsync();

                await Task.Delay(TimeSpan.FromDays(3), stoppingToken);
            }
        }
    }
}