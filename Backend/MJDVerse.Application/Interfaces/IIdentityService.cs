using MJDVerse.Application.DTOs.Users;
using MJDVerse.Domain.Entities;

namespace MJDVerse.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<(bool Success, string[] Errors)> CreateUserAsync(
            ApplicationUser user,
            string password);

        Task<(bool Success, string[] Errors)> CreateUserWithHashAsync(
            ApplicationUser user);

        Task<ApplicationUser?> FindByEmailAsync(string email);

        Task<ApplicationUser?> FindByUsernameAsync(string username);

        Task<bool> ConfirmEmailAsync(ApplicationUser user);

        Task<bool> CheckPasswordAsync(
            ApplicationUser user,
            string password);

        Task<UserProfileDto?> GetUserProfileAsync(string userId);
    }
}