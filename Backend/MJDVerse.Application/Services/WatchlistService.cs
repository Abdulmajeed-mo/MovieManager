using MJDVerse.Application.DTOs.Movies;
using MJDVerse.Application.Interfaces;
using MJDVerse.Domain.Entities;
using MJDVerse.Domain.Interfaces;

namespace MJDVerse.Application.Services
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IWatchlistRepository _watchlistRepository;
        private readonly IMovieRepository _movieRepository;

        public WatchlistService(IWatchlistRepository watchlistRepository,IMovieRepository movieRepository)
        {
            _watchlistRepository = watchlistRepository;
            _movieRepository = movieRepository;
        }






        public async Task<List<MovieDto>> GetMyWatchlistAsync(string userId)
        {
            var items =await _watchlistRepository.GetByUserIdAsync(userId);

            return items.Select(item => new MovieDto
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







        public async Task<bool> AddToWatchlistAsync(string userId,int movieId)
        {
            var movie = await _movieRepository.GetByIdAsync(movieId);

            if (movie == null)
            {
                return false;
            }

            var exists =await _watchlistRepository.ExistsAsync(userId,movieId);

            if (exists)
            {
                return false;
            }

            var item = new WatchlistItem
            {
                UserId = userId,
                MovieId = movieId
            };

            await _watchlistRepository.AddAsync(item);

            return true;
        }







        public async Task<bool> RemoveFromWatchlistAsync(string userId,int movieId)
        {
            var item =await _watchlistRepository.GetAsync(userId,movieId);

            if (item == null)
            {
                return false;
            }

            await _watchlistRepository.RemoveAsync(item);

            return true;
        }
    }
}