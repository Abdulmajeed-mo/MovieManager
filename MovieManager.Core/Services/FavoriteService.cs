using MJDVerse.Application.DTOs.Movies;
using MJDVerse.Application.Interfaces;
using MJDVerse.Domain.Entities;
using MJDVerse.Domain.Interfaces;

namespace MJDVerse.Application.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IMovieRepository _movieRepository;

        public FavoriteService(
            IFavoriteRepository favoriteRepository,
            IMovieRepository movieRepository)
        {
            _favoriteRepository = favoriteRepository;
            _movieRepository = movieRepository;
        }






        public async Task<List<MovieDto>> GetMyFavoritesAsync(string userId)
        {
            var favorites =await _favoriteRepository.GetByUserIdAsync(userId);

            return favorites.Select(favorite => new MovieDto
            {
                Id = favorite.Movie.Id,
                Title = favorite.Movie.Title,
                Description = favorite.Movie.Description,
                ReleaseDate = favorite.Movie.ReleaseDate,
                RuntimeMinutes = favorite.Movie.RuntimeMinutes,
                AverageRating = favorite.Movie.AverageRating,
                PosterUrl = favorite.Movie.PosterUrl
            }).ToList();
        }






        public async Task<bool> AddToFavoritesAsync(string userId,int movieId)
        {
            var movie =
                await _movieRepository.GetByIdAsync(movieId);

            if (movie == null)
            {
                return false;
            }

            var exists =await _favoriteRepository.ExistsAsync(userId,movieId);

            if (exists)
            {
                return false;
            }

            var favorite = new Favorite
            {
                UserId = userId,
                MovieId = movieId
            };

            await _favoriteRepository.AddAsync(favorite);

            return true;
        }






        public async Task<bool> RemoveFromFavoritesAsync(string userId,int movieId)
        {
            var favorite =await _favoriteRepository.GetAsync(userId,movieId);

            if (favorite == null)
            {
                return false;
            }

            await _favoriteRepository.RemoveAsync(favorite);

            return true;
        }
    }
}