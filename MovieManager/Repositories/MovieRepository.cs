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

        public async Task AddAsync(Movie movie)
        {

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public List<Movie> GetAllMovies()
        {
           return _context.Movies.ToList();
        }
    }
}
