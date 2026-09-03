using MJDVerse.Domain.Entities;

namespace MJDVerse.Application.Interfaces
{
    public interface IOtpRepository
    {
        Task AddAsync(OtpVerification otp);

        Task<OtpVerification?> GetLatestOtpAsync(string email);

        Task SaveChangesAsync();
    }
}