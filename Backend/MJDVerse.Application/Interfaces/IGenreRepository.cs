using MJDVerse.Domain.Entities;

namespace MJDVerse.Application.Interfaces
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetAllAsync();
        Task<Genre?> GetByIdAsync(int id);
        Task AddAsync(Genre genre);
        Task SaveChangesAsync();
    }
}