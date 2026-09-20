using MJDVerse.Domain.Entities;

namespace MJDVerse.Domain.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<List<Favorite>> GetByUserIdAsync(string userId);

        Task<Favorite?> GetAsync(string userId,int movieId);

        Task<bool> ExistsAsync(string userId,int movieId);

        Task AddAsync(Favorite favorite);

        Task RemoveAsync(Favorite favorite);
    }
}