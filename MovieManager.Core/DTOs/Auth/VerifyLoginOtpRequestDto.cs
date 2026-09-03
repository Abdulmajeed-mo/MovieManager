namespace MJDVerse.Application.DTOs.Auth
{
    public class VerifyLoginOtpRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string Otp { get; set; } = string.Empty;
    }
}