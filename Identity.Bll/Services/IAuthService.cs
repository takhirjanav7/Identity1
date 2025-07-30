using Identity.Bll.DTOs;
using Identity.Bll.DTOs.UserDTOs;
using Identity.Dal.Entities;

namespace Identity.Bll.Services
{
    public interface IAuthService
    {
        public Task<long> SignUpAsync(UserCreateDTO userCreateDto, UserRole userRole);
        public Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
        public Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
    }
}