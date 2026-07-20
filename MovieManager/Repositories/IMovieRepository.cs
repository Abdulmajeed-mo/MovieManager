using Microsoft.EntityFrameworkCore;
using MovieManager.Models;

namespace MovieManager.Repositories
{
    public interface IMovieRepository
    {
        //لأننا نخبر أي كلاس ينفذ هذا الـ الانترفيس بأنه يجب أن يوفر دالة اسمها قت الل موفي 
        List<Movie> GetAllMovies();
        Task AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        async Task DeleteAsync(int id)
        {var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }

            Task<Movie?> GetByIdAsync(int id);
        }
    }
}