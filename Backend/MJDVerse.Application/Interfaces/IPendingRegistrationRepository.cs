using MJDVerse.Domain.Entities;

namespace MJDVerse.Application.Interfaces
{
    public interface IPendingRegistrationRepository
    {
        Task<PendingRegistration?> GetByEmailAsync(string email);

        Task AddAsync(PendingRegistration pendingRegistration);

        Task DeleteAsync(PendingRegistration pendingRegistration);

        Task SaveChangesAsync();
    }
}