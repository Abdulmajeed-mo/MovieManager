using MJDVerse.Application.DTOs.Auth;
using MJDVerse.Application.Interfaces;
using MJDVerse.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace MJDVerse.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IIdentityService _identityService;
        private readonly IEmailSender _emailSender;
        private readonly IOtpRepository _otpRepository;

        public AuthService(
            IIdentityService identityService,
            IEmailSender emailSender,
            IOtpRepository otpRepository)
        {
            _identityService = identityService;
            _emailSender = emailSender;
            _otpRepository = otpRepository;
        }






        public async Task<bool> RegisterAsync(RegisterRequestDto request)
        {
            var user = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            var result = await _identityService.CreateUserAsync(
                user,
                request.Password);

            if (!result.Success)
            {
                return false;
            }

            var otp = RandomNumberGenerator.GetInt32(1000, 10000).ToString();

            var otpHash = HashOtp(otp);

            var otpVerification = new OtpVerification
            {
                UserId = user.Id,
                Email = user.Email!,
                CodeHash = otpHash,
                ExpiresAt = DateTime.UtcNow.AddSeconds(60)
            };

            await _otpRepository.AddAsync(otpVerification);
            await _otpRepository.SaveChangesAsync();

            await _emailSender.SendEmailAsync(
                request.Email,
                "MJDVerse Email Verification",
                $"Your verification code is: {otp}");

            return true;
        }






        public async Task<bool> VerifyOtpAsync(VerifyOtpRequestDto request)
        {
            var otpVerification =
                await _otpRepository.GetLatestOtpAsync(request.Email);

            if (otpVerification == null)
            {
                return false;
            }

            if (otpVerification.ExpiresAt <= DateTime.UtcNow)
            {
                return false;
            }

            var otpHash = HashOtp(request.Otp);

            if (otpHash != otpVerification.CodeHash)
            {
                return false;
            }

            var user =
                await _identityService.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return false;
            }

            var confirmed =
                await _identityService.ConfirmEmailAsync(user);

            if (!confirmed)
            {
                return false;
            }

            otpVerification.IsUsed = true;

            await _otpRepository.SaveChangesAsync();

            return true;
        }



        //Verify Login OTP
        public async Task<bool> VerifyLoginOtpAsync(VerifyLoginOtpRequestDto request)
        {
            var otpVerification =
                await _otpRepository.GetLatestOtpAsync(request.Email);

            if (otpVerification == null)
            {
                return false;
            }

            if (otpVerification.ExpiresAt <= DateTime.UtcNow)
            {
                return false;
            }

            var otpHash = HashOtp(request.Otp);

            if (otpHash != otpVerification.CodeHash)
            {
                return false;
            }

            otpVerification.IsUsed = true;

            await _otpRepository.SaveChangesAsync();

            return true;
        }




        //Login 
        public async Task<bool> LoginAsync(
            LoginRequestDto request)
        {
            ApplicationUser? user;

            if (request.Identifier.Contains("@"))
            {
                user = await _identityService.FindByEmailAsync(
                    request.Identifier);
            }
            else
            {
                user = await _identityService.FindByUsernameAsync(request.Identifier);
            }

            if (user == null)
            {
                return false;
            }

            if (!user.EmailConfirmed)
            {
                return false;
            }

            var passwordValid =
                await _identityService.CheckPasswordAsync(user,request.Password);

            if (!passwordValid)
            {
                return false;
            }

            await SendOtpAsync(user);

            return true;
        }







        private async Task SendOtpAsync(ApplicationUser user)
        {
            var otp = RandomNumberGenerator.GetInt32(1000, 10000).ToString();

            var otpHash = HashOtp(otp);

            var otpVerification = new OtpVerification
            {
                UserId = user.Id,
                Email = user.Email!,
                CodeHash = otpHash,
                ExpiresAt = DateTime.UtcNow.AddSeconds(60)
            };

            await _otpRepository.AddAsync(otpVerification);
            await _otpRepository.SaveChangesAsync();

            await _emailSender.SendEmailAsync(user.Email!,"MJDVerse Login Verification",$"Your verification code is: {otp}");
        }




        private static string HashOtp(string otp)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(otp));

            return Convert.ToHexString(bytes);
        }
    }
}

