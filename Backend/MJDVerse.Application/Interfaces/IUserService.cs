using MJDVerse.Application.DTOs.Users;

namespace MJDVerse.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileDto?> GetProfileAsync(string userId);
    }
}