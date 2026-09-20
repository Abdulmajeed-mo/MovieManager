using MJDVerse.Application.DTOs.Users;
using MJDVerse.Application.Interfaces;

namespace MJDVerse.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IIdentityService _identityService;

        public UserService(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<UserProfileDto?> GetProfileAsync(string userId)
        {
            return await _identityService.GetUserProfileAsync(userId);
        }
    }
}