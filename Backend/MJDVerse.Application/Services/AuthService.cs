using Microsoft.AspNetCore.Identity;
using MJDVerse.Application.DTOs.Auth;
using MJDVerse.Application.Interfaces;
using MJDVerse.Domain.Entities;
using MJDVerse.Domain.Enums;
using System.Security.Cryptography;
using System.Text;

namespace MJDVerse.Application.Services
{
    public class AuthService : IAuthService
    {
        // Dependencies
        private readonly IIdentityService _identityService;
        private readonly IEmailSender _emailSender;
        private readonly IOtpRepository _otpRepository;
        private readonly IPendingRegistrationRepository _pendingRegistrationRepository;
        private readonly IOtpRateLimiter _otpRateLimiter;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        // Constructor
        public AuthService(
            IIdentityService identityService,
            IEmailSender emailSender,
            IOtpRepository otpRepository,
            IPendingRegistrationRepository pendingRegistrationRepository,
            IOtpRateLimiter otpRateLimiter,
            IJwtTokenGenerator jwtTokenGenerator,
            IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _identityService = identityService;
            _emailSender = emailSender;
            _otpRepository = otpRepository;
            _pendingRegistrationRepository = pendingRegistrationRepository;
            _otpRateLimiter = otpRateLimiter;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
        }

        // Register user
        public async Task<(bool Success, string[] Errors)> RegisterAsync(
            RegisterRequestDto request)
        {
            // Check if email is already registered
            var existingUserByEmail =
                await _identityService.FindByEmailAsync(request.Email);

            if (existingUserByEmail != null)
            {
                return (
                    false,
                    new[] { "Email is already taken." }
                );
            }

            // Check if username is already registered
            var existingUserByUsername =
                await _identityService.FindByUsernameAsync(request.Username);

            if (existingUserByUsername != null)
            {
                return (
                    false,
                    new[] { "Username is already taken." }
                );
            }

            // Check OTP rate limit
            if (!_otpRateLimiter.IsAllowed(request.Email))
            {
                return (
                    false,
                    new[] { "Too many OTP requests. Please try again later." }
                );
            }

            // Remove previous pending registration for this email
            var existingPendingRegistration =
                await _pendingRegistrationRepository.GetByEmailAsync(
                    request.Email);

            if (existingPendingRegistration != null)
            {
                await _pendingRegistrationRepository.DeleteAsync(
                    existingPendingRegistration);

                await _pendingRegistrationRepository.SaveChangesAsync();
            }

            // Generate OTP
            var otp =
                RandomNumberGenerator.GetInt32(1000, 10000).ToString();

            var otpHash = HashOtp(otp);

            // Create a temporary ApplicationUser object only for hashing
            var temporaryUser = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email
            };

            var passwordHash =
                _passwordHasher.HashPassword(
                    temporaryUser,
                    request.Password);

            // Create pending registration
            var pendingRegistration = new PendingRegistration
            {
                Email = request.Email,
                Username = request.Username,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = passwordHash,
                OtpHash = otpHash,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(3),
                IsUsed = false
            };

            await _pendingRegistrationRepository.AddAsync(
                pendingRegistration);

            await _pendingRegistrationRepository.SaveChangesAsync();

            // Send OTP
            await _emailSender.SendEmailAsync(
                request.Email,
                "MJDVerse Email Verification",
                $"Your verification code is: {otp}");

            return (true, Array.Empty<string>());
        }

        // Verify Registration OTP
        public async Task<bool> VerifyOtpAsync(
            VerifyOtpRequestDto request)
        {
            var pendingRegistration =
                await _pendingRegistrationRepository.GetByEmailAsync(
                    request.Email);

            if (pendingRegistration == null)
            {
                return false;
            }

            if (pendingRegistration.ExpiresAt <= DateTime.UtcNow)
            {
                await _pendingRegistrationRepository.DeleteAsync(
                    pendingRegistration);

                await _pendingRegistrationRepository.SaveChangesAsync();

                return false;
            }

            var otpHash = HashOtp(request.Otp);

            if (otpHash != pendingRegistration.OtpHash)
            {
                return false;
            }

            // Double-check email and username before creating the user
            var existingUserByEmail =
                await _identityService.FindByEmailAsync(
                    pendingRegistration.Email);

            if (existingUserByEmail != null)
            {
                return false;
            }

            var existingUserByUsername =
                await _identityService.FindByUsernameAsync(
                    pendingRegistration.Username);

            if (existingUserByUsername != null)
            {
                return false;
            }

            // Create the real user only after successful OTP verification
            var user = new ApplicationUser
            {
                UserName = pendingRegistration.Username,
                Email = pendingRegistration.Email,
                PhoneNumber = pendingRegistration.PhoneNumber,
                PasswordHash = pendingRegistration.PasswordHash,
                EmailConfirmed = true
            };

            var result =
                await _identityService.CreateUserWithHashAsync(user);

            if (!result.Success)
            {
                return false;
            }

            // Mark pending registration as used
            pendingRegistration.IsUsed = true;

            await _pendingRegistrationRepository.SaveChangesAsync();

            return true;
        }

        // Verify Login OTP
        public async Task<AuthResponseDto?> VerifyLoginOtpAsync(
            VerifyLoginOtpRequestDto request)
        {
            var otpVerification =
                await _otpRepository.GetLatestOtpAsync(
                    request.Email,
                    OtpPurpose.Login);

            if (otpVerification == null)
            {
                return null;
            }

            if (otpVerification.ExpiresAt <= DateTime.UtcNow)
            {
                return null;
            }

            var otpHash = HashOtp(request.Otp);

            if (otpHash != otpVerification.CodeHash)
            {
                return null;
            }

            var user =
                await _identityService.FindByEmailAsync(
                    request.Email);

            if (user == null)
            {
                return null;
            }

            otpVerification.IsUsed = true;

            await _otpRepository.SaveChangesAsync();

            var token =
                _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token
            };
        }

        // Login
        public async Task<bool> LoginAsync(
            LoginRequestDto request)
        {
            ApplicationUser? user;

            if (request.Identifier.Contains("@"))
            {
                user =
                    await _identityService.FindByEmailAsync(
                        request.Identifier);
            }
            else
            {
                user =
                    await _identityService.FindByUsernameAsync(
                        request.Identifier);
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
                await _identityService.CheckPasswordAsync(
                    user,
                    request.Password);

            if (!passwordValid)
            {
                return false;
            }

            var otpSent = await SendOtpAsync(user);

            return otpSent;
        }

        // Send OTP to user email
        private async Task<bool> SendOtpAsync(
            ApplicationUser user)
        {
            if (!_otpRateLimiter.IsAllowed(user.Email!))
            {
                return false;
            }

            var otp =
                RandomNumberGenerator.GetInt32(1000, 10000).ToString();

            var otpHash = HashOtp(otp);

            var otpVerification = new OtpVerification
            {
                UserId = user.Id,
                Email = user.Email!,
                CodeHash = otpHash,
                Purpose = OtpPurpose.Login,
                ExpiresAt = DateTime.UtcNow.AddMinutes(3)
            };

            await _otpRepository.AddAsync(otpVerification);
            await _otpRepository.SaveChangesAsync();

            await _emailSender.SendEmailAsync(
                user.Email!,
                "MJDVerse Login Verification",
                $"Your verification code is: {otp}");

            return true;
        }

        private static string HashOtp(string otp)
        {
            var bytes =
                SHA256.HashData(
                    Encoding.UTF8.GetBytes(otp));

            return Convert.ToHexString(bytes);
        }
    }
}