using Microsoft.EntityFrameworkCore;
using MJDVerse.Application.Interfaces;
using MJDVerse.Domain.Entities;
using MJDVerse.Infrastructure.Context;

namespace MJDVerse.Infrastructure.Repositories
{
    public class PendingRegistrationRepository : IPendingRegistrationRepository
    {
        private readonly AppDbContext _context;

        public PendingRegistrationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PendingRegistration?> GetByEmailAsync(string email)
        {
            return await _context.PendingRegistrations
                .FirstOrDefaultAsync(x => x.Email == email && !x.IsUsed);
        }

        public async Task AddAsync(PendingRegistration pendingRegistration)
        {
            await _context.PendingRegistrations.AddAsync(pendingRegistration);
        }

        public async Task DeleteAsync(PendingRegistration pendingRegistration)
        {
            _context.PendingRegistrations.Remove(pendingRegistration);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}