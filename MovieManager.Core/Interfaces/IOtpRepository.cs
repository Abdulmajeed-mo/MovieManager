using MJDVerse.Domain.Entities;
using MJDVerse.Domain.Enums;

namespace MJDVerse.Application.Interfaces
{
    public interface IOtpRepository
    {
        Task AddAsync(OtpVerification otp);

        Task<OtpVerification?> GetLatestOtpAsync(string email,OtpPurpose purpose);

        Task SaveChangesAsync();
    }
}