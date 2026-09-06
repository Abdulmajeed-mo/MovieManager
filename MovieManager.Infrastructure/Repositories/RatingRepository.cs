using Microsoft.EntityFrameworkCore;
using MJDVerse.Domain.Entities;
using MJDVerse.Domain.Interfaces;
using MJDVerse.Infrastructure.Context;

namespace MJDVerse.Infrastructure.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly AppDbContext _context;

        public RatingRepository(AppDbContext context)
        {
            _context = context;
        }




        public async Task<Rating?> GetAsync(string userId,int movieId)
        {
            return await _context.Ratings.FirstOrDefaultAsync(x => x.UserId == userId &&x.MovieId == movieId);
        }




        public async Task<List<Rating>> GetByUserIdAsync(string userId)
        {
            return await _context.Ratings.Include(x => x.Movie).Where(x => x.UserId == userId).OrderByDescending(x => x.RatedAt).ToListAsync();
        }




        public async Task AddAsync(Rating rating)
        {
            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();
        }




        public async Task UpdateAsync(Rating rating)
        {
            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();
        }
    }
}