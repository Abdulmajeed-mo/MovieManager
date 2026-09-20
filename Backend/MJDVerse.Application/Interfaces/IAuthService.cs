using MJDVerse.Application.DTOs.Auth;

namespace MJDVerse.Application.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, string[] Errors)> RegisterAsync(RegisterRequestDto request);
        Task<bool> VerifyOtpAsync(VerifyOtpRequestDto request);

        Task<bool> LoginAsync(LoginRequestDto request);

        Task<AuthResponseDto?> VerifyLoginOtpAsync(VerifyLoginOtpRequestDto request);
    }
}