using MovieManager.Models;
using MovieManager.Data;
using Microsoft.EntityFrameworkCore;

namespace MovieManager.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        //privet read
        private readonly AppDbContext _context;
        //constructer
        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        //For Repository 
        // AddAsync()
        // GetByIdAsync()
        // UpdateAsync()
        // DeleteAsync()
        public async Task AddAsync(Movie movie)
        {

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }
        public async Task<Movie?> GetByIdAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            return movie;
        }

        public async Task UpdateAsync(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }

        }

       




    }
}
