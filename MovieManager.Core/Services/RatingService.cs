using MJDVerse.Application.DTOs.Movies;
using MJDVerse.Application.Interfaces;
using MJDVerse.Domain.Entities;
using MJDVerse.Domain.Interfaces;

namespace MJDVerse.Application.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMovieRepository _movieRepository;

        public RatingService(
            IRatingRepository ratingRepository,
            IMovieRepository movieRepository)
        {
            _ratingRepository = ratingRepository;
            _movieRepository = movieRepository;
        }
        
       




public async Task<bool> RateMovieAsync(string userId,int movieId,decimal value)
        {
            if (value < 1 || value > 10)
            {
                return false;
            }

            var movie =await _movieRepository.GetByIdAsync(movieId);

            if (movie == null)
            {
                return false;
            }

            var existingRating =await _ratingRepository.GetAsync(
                    userId,
                    movieId);

            if (existingRating != null)
            {
                return false;
            }

            var rating = new Rating
            {
                UserId = userId,
                MovieId = movieId,
                Value = value
            };

            await _ratingRepository.AddAsync(rating);

            return true;
        }





        public async Task<List<MovieDto>> GetMyRatedMoviesAsync(string userId)
        {
            var ratings =await _ratingRepository.GetByUserIdAsync(userId);

            return ratings.Select(rating => new MovieDto
            {
                Id = rating.Movie.Id,
                Title = rating.Movie.Title,
                Description = rating.Movie.Description,
                ReleaseDate = rating.Movie.ReleaseDate,
                RuntimeMinutes = rating.Movie.RuntimeMinutes,
                AverageRating = rating.Movie.AverageRating,
                PosterUrl = rating.Movie.PosterUrl
            }).ToList();
        }




        public async Task<bool> UpdateRatingAsync(string userId,int movieId,decimal value)
        {
            if (value < 1 || value > 10)
            {
                return false;
            }

            var rating =await _ratingRepository.GetAsync(userId,movieId);

            if (rating == null)
            {
                return false;
            }

            rating.Value = value;
            rating.RatedAt = DateTime.UtcNow;

            await _ratingRepository.UpdateAsync(rating);

            return true;
        }
    }
}