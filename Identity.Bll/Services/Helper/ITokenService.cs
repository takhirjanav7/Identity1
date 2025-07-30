using Identity.Bll.DTOs;
using System.Security.Claims;

namespace Identity.Bll.Services.Helper
{
    public interface ITokenService
    {
        public string GenerateToken(UserTokenDto user);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}