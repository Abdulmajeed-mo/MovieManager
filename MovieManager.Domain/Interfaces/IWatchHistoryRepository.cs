using MJDVerse.Domain.Entities;

namespace MJDVerse.Domain.Interfaces
{
    public interface IWatchHistoryRepository
    {
        Task<List<WatchHistory>> GetByUserIdAsync(string userId);

        Task<WatchHistory?> GetAsync(string userId,int movieId);

        Task AddAsync(WatchHistory watchHistory);

        Task UpdateAsync(WatchHistory watchHistory);
    }
}