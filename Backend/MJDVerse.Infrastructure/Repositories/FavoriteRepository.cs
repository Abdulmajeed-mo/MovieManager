using Microsoft.EntityFrameworkCore;
using MJDVerse.Domain.Entities;
using MJDVerse.Domain.Interfaces;
using MJDVerse.Infrastructure.Context;

namespace MJDVerse.Infrastructure.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly AppDbContext _context;

        public FavoriteRepository(AppDbContext context)
        {
            _context = context;
        }





        public async Task<List<Favorite>> GetByUserIdAsync(string userId)
        {
            return await _context.Favorites.Include(x => x.Movie).Where(x => x.UserId == userId).OrderByDescending(x => x.AddedAt).ToListAsync();
        }






        public async Task<Favorite?> GetAsync(string userId,int movieId)
        {
            return await _context.Favorites.FirstOrDefaultAsync(x => x.UserId == userId &&x.MovieId == movieId);
        }






        public async Task<bool> ExistsAsync(string userId,int movieId)
        {
            return await _context.Favorites.AnyAsync(x => x.UserId == userId &&x.MovieId == movieId);
        }







        public async Task AddAsync(Favorite favorite)
        {
            await _context.Favorites.AddAsync(favorite);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Favorite favorite)
        {
            _context.Favorites.Remove(favorite);

            await _context.SaveChangesAsync();
        }
    }
}