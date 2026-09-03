using Microsoft.EntityFrameworkCore;
using MJDVerse.Application.Interfaces;
using MJDVerse.Domain.Entities;
using MJDVerse.Infrastructure.Context;

namespace MJDVerse.Infrastructure.Repositories
{
    public class OtpRepository : IOtpRepository
    {
        private readonly AppDbContext _context;

        public OtpRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OtpVerification otp)
        {
            await _context.OtpVerifications.AddAsync(otp);
        }

        public async Task<OtpVerification?> GetLatestOtpAsync(
            string email)
        {
            return await _context.OtpVerifications
                .Where(x => x.Email == email && !x.IsUsed)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}