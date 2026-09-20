using Microsoft.EntityFrameworkCore;
using MJDVerse.Domain.Entities;
using MJDVerse.Domain.Interfaces;
using MJDVerse.Infrastructure.Context;

namespace MJDVerse.Infrastructure.Repositories
{
    public class WatchHistoryRepository : IWatchHistoryRepository
    {
        private readonly AppDbContext _context;

        public WatchHistoryRepository(AppDbContext context)
        {
            _context = context;
        }




        public async Task<List<WatchHistory>> GetByUserIdAsync(string userId)
        {
            return await _context.WatchHistories.Include(x => x.Movie).Where(x => x.UserId == userId).OrderByDescending(x => x.WatchedAt).ToListAsync();
        }




        public async Task<WatchHistory?> GetAsync(string userId,int movieId)
        {
            return await _context.WatchHistories.FirstOrDefaultAsync(x => x.UserId == userId &&x.MovieId == movieId);
        }





        public async Task AddAsync(WatchHistory watchHistory)
        {
            await _context.WatchHistories.AddAsync(watchHistory);

            await _context.SaveChangesAsync();
        }





        public async Task UpdateAsync(WatchHistory watchHistory)
        {
            _context.WatchHistories.Update(watchHistory);

            await _context.SaveChangesAsync();
        }
    }
}