using Microsoft.AspNetCore.Mvc;
using MJDVerse.Application.DTOs.Auth;
using MJDVerse.Application.Interfaces;

namespace MJDVerse.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }





        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            var result = await _authService.RegisterAsync(request);

            if (!result.Success)
            {
                return BadRequest(result.Errors);
            }

            return Ok("OTP sent to your email.");
        }




        [HttpPost("login")]
        public async Task<IActionResult> Login(
    LoginRequestDto request)
        {
            var result = await _authService.LoginAsync(request);

            if (!result)
            {
                return Unauthorized("Invalid credentials or email is not verified.");
            }

            return Ok("OTP sent to your email.");
        }






        [HttpPost("verify-login-otp")]
        public async Task<IActionResult> VerifyLoginOtp(
      VerifyLoginOtpRequestDto request)
        {
            var result = await _authService.VerifyLoginOtpAsync(request);

            if (result is null)
            {
                return BadRequest("Invalid or expired OTP.");
            }

            return Ok(result);
        }




        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(VerifyOtpRequestDto request)
        {
            var result = await _authService.VerifyOtpAsync(request);

            if (!result)
            {
                return BadRequest("Invalid or expired OTP.");
            }

            return Ok("Email verified successfully.");
        }
    }
}