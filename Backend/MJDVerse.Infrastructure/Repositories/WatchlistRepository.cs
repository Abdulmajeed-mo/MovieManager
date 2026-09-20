using Microsoft.EntityFrameworkCore;
using MJDVerse.Domain.Entities;
using MJDVerse.Domain.Interfaces;
using MJDVerse.Infrastructure.Context;

namespace MJDVerse.Infrastructure.Repositories
{
    public class WatchlistRepository : IWatchlistRepository
    {
        private readonly AppDbContext _context;

        public WatchlistRepository(AppDbContext context)
        {
            _context = context;
        }






        public async Task<List<WatchlistItem>> GetByUserIdAsync(string userId)
        {
            return await _context.WatchlistItems.Include(x => x.Movie).Where(x => x.UserId == userId).OrderByDescending(x => x.AddedAt).ToListAsync();
        }






        public async Task<WatchlistItem?> GetAsync(string userId,int movieId)
        {
            return await _context.WatchlistItems.FirstOrDefaultAsync(x => x.UserId == userId &&x.MovieId == movieId);
        }






        public async Task<bool> ExistsAsync(string userId,int movieId)
        {
            return await _context.WatchlistItems.AnyAsync(x => x.UserId == userId &&x.MovieId == movieId);
        }

        public async Task AddAsync(WatchlistItem item)
        {
            await _context.WatchlistItems.AddAsync(item);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(WatchlistItem item)
        {
            _context.WatchlistItems.Remove(item);

            await _context.SaveChangesAsync();
        }
    }
}