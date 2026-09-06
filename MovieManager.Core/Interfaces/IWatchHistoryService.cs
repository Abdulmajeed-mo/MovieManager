using MJDVerse.Application.DTOs.Movies;

namespace MJDVerse.Application.Interfaces
{
    public interface IWatchHistoryService
    {
        Task<List<MovieDto>> GetMyWatchHistoryAsync(string userId);

        Task<bool> AddOrUpdateWatchHistoryAsync(string userId,int movieId,int progressInSeconds);
        Task<List<ContinueWatchingDto>> GetContinueWatchingAsync(string userId);
    }
}