using MJDVerse.Domain.Entities;

namespace MJDVerse.Domain.Interfaces
{
    public interface IRatingRepository
    {
        Task<Rating?> GetAsync(string userId,int movieId);

        Task<List<Rating>> GetByUserIdAsync(string userId);

        Task AddAsync(Rating rating);

        Task UpdateAsync(Rating rating);
    }
}