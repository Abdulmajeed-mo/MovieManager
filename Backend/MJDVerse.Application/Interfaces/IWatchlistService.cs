using MJDVerse.Application.DTOs.Movies;

namespace MJDVerse.Application.Interfaces
{
    public interface IWatchlistService
    {
        Task<List<MovieDto>> GetMyWatchlistAsync(string userId);

        Task<bool> AddToWatchlistAsync(string userId,int movieId);

        Task<bool> RemoveFromWatchlistAsync(string userId,int movieId);
    }
}