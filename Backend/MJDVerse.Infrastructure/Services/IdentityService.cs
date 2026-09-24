using Microsoft.AspNetCore.Identity;
using MJDVerse.Application.DTOs.Users;
using MJDVerse.Application.Interfaces;
using MJDVerse.Domain.Entities;

namespace MJDVerse.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(bool Success, string[] Errors)> CreateUserAsync(
            ApplicationUser user,
            string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return (true, Array.Empty<string>());
            }

            return (
                false,
                result.Errors.Select(e => e.Description).ToArray()
            );
        }

        public async Task<(bool Success, string[] Errors)> CreateUserWithHashAsync(
            ApplicationUser user)
        {
            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                return (true, Array.Empty<string>());
            }

            return (
                false,
                result.Errors.Select(e => e.Description).ToArray()
            );
        }

        public async Task<ApplicationUser?> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser?> FindByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<bool> ConfirmEmailAsync(ApplicationUser user)
        {
            user.EmailConfirmed = true;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<bool> CheckPasswordAsync(
            ApplicationUser user,
            string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<UserProfileDto?> GetUserProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            return new UserProfileDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                Username = user.UserName ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty
            };
        }
    }
}