using MJDVerse.Application.DTOs.Movies;
using MJDVerse.Application.Interfaces;
using MJDVerse.Domain.Entities;
using MJDVerse.Domain.Interfaces;

namespace MJDVerse.Application.Services
{
    public class WatchHistoryService : IWatchHistoryService
    {
        private readonly IWatchHistoryRepository _watchHistoryRepository;
        private readonly IMovieRepository _movieRepository;

        public WatchHistoryService(
            IWatchHistoryRepository watchHistoryRepository,
            IMovieRepository movieRepository)
        {
            _watchHistoryRepository = watchHistoryRepository;
            _movieRepository = movieRepository;
        }
        





        public async Task<List<MovieDto>> GetMyWatchHistoryAsync(string userId)
        {
            var history =await _watchHistoryRepository.GetByUserIdAsync(userId);

            return history.Select(item => new MovieDto
            {
                Id = item.Movie.Id,
                Title = item.Movie.Title,
                Description = item.Movie.Description,
                ReleaseDate = item.Movie.ReleaseDate,
                RuntimeMinutes = item.Movie.RuntimeMinutes,
                AverageRating = item.Movie.AverageRating,
                PosterUrl = item.Movie.PosterUrl
            }).ToList();
        }
        








        public async Task<bool> AddOrUpdateWatchHistoryAsync(string userId,int movieId,int progressInSeconds)
        {
            if (progressInSeconds < 0)
            {
                return false;
            }

            var movie =await _movieRepository.GetByIdAsync(movieId);

            if (movie == null)
            {
                return false;
            }

            var history =
                await _watchHistoryRepository.GetAsync(userId,movieId);

            if (history == null)
            {
                history = new WatchHistory
                {
                    UserId = userId,
                    MovieId = movieId,
                    ProgressInSeconds = progressInSeconds,
                    WatchedAt = DateTime.UtcNow
                };

                await _watchHistoryRepository.AddAsync(history);
            }
            else
            {
                history.ProgressInSeconds = progressInSeconds;
                history.WatchedAt = DateTime.UtcNow;

                await _watchHistoryRepository.UpdateAsync(history);
            }

            return true;
        }







        public async Task<List<ContinueWatchingDto>> GetContinueWatchingAsync(string userId)
        {
            var history =await _watchHistoryRepository.GetByUserIdAsync(userId);

            return history.Where(x =>x.ProgressInSeconds > 0 &&x.ProgressInSeconds < x.Movie.RuntimeMinutes * 60) .Select(x => new ContinueWatchingDto
                {
                    MovieId = x.Movie.Id,
                    Title = x.Movie.Title,
                    Description = x.Movie.Description,
                    ReleaseDate = x.Movie.ReleaseDate,
                    RuntimeMinutes = x.Movie.RuntimeMinutes,
                    AverageRating = x.Movie.AverageRating,
                    PosterUrl = x.Movie.PosterUrl,
                    ProgressInSeconds = x.ProgressInSeconds,
                    ProgressPercentage =(double)x.ProgressInSeconds /(x.Movie.RuntimeMinutes * 60) * 100
                })
                .OrderByDescending(x => x.ProgressInSeconds).ToList();
        }
    }
}