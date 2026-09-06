using MJDVerse.Domain.Entities;

namespace MJDVerse.Domain.Interfaces
{
    public interface IWatchlistRepository
    {
        Task<List<WatchlistItem>> GetByUserIdAsync(string userId);

        Task<WatchlistItem?> GetAsync(string userId,int movieId);

        Task<bool> ExistsAsync(string userId,int movieId);

        Task AddAsync(WatchlistItem item);

        Task RemoveAsync(WatchlistItem item);
    }
}