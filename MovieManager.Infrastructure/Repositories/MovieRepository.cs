using Microsoft.EntityFrameworkCore;
using MJDVerse.Domain.Entities;
using MJDVerse.Domain.Interfaces;
using MJDVerse.Infrastructure.Context;

namespace MJDVerse.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            return await _context.Movies.ToListAsync();
        }





        public async Task<(List<Movie> Movies, int TotalCount)> GetMoviesAsync(string? query,string? genre,string? sortBy,bool descending,int page,int pageSize)
        {
            var moviesQuery = _context.Movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                moviesQuery = moviesQuery.Where(movie =>movie.Title.Contains(query));
            }

            if (!string.IsNullOrWhiteSpace(genre))
            {
                moviesQuery = moviesQuery.Where(movie =>movie.MovieGenres.Any(movieGenre =>movieGenre.Genre.Name == genre));
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                moviesQuery = sortBy.ToLower() switch
                {
                    "title" => descending? moviesQuery.OrderByDescending(movie => movie.Title): moviesQuery.OrderBy(movie => movie.Title),

                    "rating" => descending? moviesQuery.OrderByDescending(movie => movie.AverageRating): moviesQuery.OrderBy(movie => movie.AverageRating),

                    "releasedate" => descending? moviesQuery.OrderByDescending(movie => movie.ReleaseDate): moviesQuery.OrderBy(movie => movie.ReleaseDate),_ => moviesQuery
                };
            }
            var totalCount = await moviesQuery.CountAsync();


            moviesQuery = moviesQuery.Skip((page - 1) * pageSize).Take(pageSize);

            var movies = await moviesQuery.ToListAsync();

            return (movies, totalCount);
        }





        public async Task AddAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _context.Movies.FirstOrDefaultAsync(movie => movie.Id == id);
        }

        public async Task UpdateAsync(Movie movie)
        {
            _context.Movies.Update(movie);

            await _context.SaveChangesAsync();
        }







        public async Task DeleteAsync(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(movie => movie.Id == id);

            if (movie == null)
            {
                return;
            }

            _context.Movies.Remove(movie);

            await _context.SaveChangesAsync();
        }





        public async Task<List<Movie>> SearchAsync(string query)
        {
            return await _context.Movies.Where(movie => movie.Title.Contains(query)).ToListAsync();
        }
    }
}