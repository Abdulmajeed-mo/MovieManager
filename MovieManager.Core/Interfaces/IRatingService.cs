using MJDVerse.Application.DTOs.Movies;

namespace MJDVerse.Application.Interfaces
{
    public interface IRatingService
    {
        Task<bool> RateMovieAsync(string userId,int movieId,decimal value);

        Task<List<MovieDto>> GetMyRatedMoviesAsync(string userId);

        Task<bool> UpdateRatingAsync(string userId,int movieId,decimal value);
    }
}