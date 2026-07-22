using Microsoft.EntityFrameworkCore;
using MovieManager.Models;

namespace MovieManager.Repositories
{
    public interface IMovieRepository
    {
        //لأننا نخبر أي كلاس ينفذ هذا الـ الانترفيس بأنه يجب أن يوفر دالة اسمها قت الل موفي 
        Task<List<Movie>> GetAllMovies();
        Task AddAsync(Movie movie);
        Task<Movie?> GetByIdAsync(int id);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(int id);

    }
}