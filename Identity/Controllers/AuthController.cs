using Identity.Bll.DTOs;
using Identity.Bll.DTOs.UserDTOs;
using Identity.Bll.Services;
using Identity.Dal.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService AuthService;

        public AuthController(IAuthService authService)
        {
            AuthService = authService;
        }

        [HttpPost("sign-up")]
         public async Task<long> SignUp(UserCreateDTO userCreateDto)
        {
            return await AuthService.SignUpAsync(userCreateDto, UserRole.User);
        }

        [HttpPost("login")]
        public async Task<LoginResponseDto> Login(LoginDto loginDto)
        {
            return await AuthService.LoginAsync(loginDto);
        }

        [HttpPost("refresh-token")]
        public async Task<LoginResponseDto> RefreshToken(RefreshTokenDto refreshTokenDto)
        {
            return await AuthService.RefreshTokenAsync(refreshTokenDto);
        }
    }
}
