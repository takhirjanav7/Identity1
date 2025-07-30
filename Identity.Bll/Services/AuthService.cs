using FluentValidation;
using Identity.Bll.Converter;
using Identity.Bll.DTOs;
using Identity.Bll.DTOs.UserDTOs;
using Identity.Bll.Services.Helper;
using Identity.Dal.Entities;
using Identity.Repository.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Bll.Services
{
    public  class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> Logger;
        private readonly IUserRepository UserRepository;
        private readonly IValidator<UserCreateDTO> Validator;
        private readonly ITokenService TokenService;
        private readonly IRefreshTokenRepository RefreshTokenRepository;

        public AuthService(IUserRepository userRepository, IValidator<UserCreateDTO> validator, ITokenService tokenService, IRefreshTokenRepository refreshTokenRepository)
        {
            UserRepository = userRepository;
            Validator = validator;
            TokenService = tokenService;
            RefreshTokenRepository = refreshTokenRepository;
        }
        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            if (loginDto.UserName == null && loginDto.Email == null)
            {
                throw new ArgumentException("UserName or Email must be provided");
            }

            var user = loginDto.UserName != null ? await UserRepository.SelectUserByUserNameAsync(loginDto.UserName) :
                await UserRepository.SelectUserByUserEmailAsync(loginDto.Email);

            if (user == null)
            {
                throw new Exception("UserName or password incorrect");
            }

            var checkUserPassword = PasswordHasher.Verify(loginDto.Password, user.Password, user.Salt);

            if (checkUserPassword == false)
            {
                throw new Exception("UserName or password incorrect");
            }

            var userGetDto = new UserTokenDto()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserRole = (UserRoleDto)user.Role,
            };

            var token = TokenService.GenerateToken(userGetDto);
            var refreshToken = TokenService.GenerateRefreshToken();

            var refreshTokenToDB = new RefreshToken()
            {
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(21),
                IsRevoked = false,
                UserId = user.UserId
            };

            await RefreshTokenRepository.AddRefreshTokenAsync(refreshTokenToDB);

            var loginResponseDto = new LoginResponseDto()
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                TokenType = "Bearer",
                Expires = 24
            };


            return loginResponseDto;
        }

        public Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
        {
            throw new NotImplementedException();
        }

        public async Task<long> SignUpAsync(UserCreateDTO userCreateDto, UserRole userRole)
        {
            if (userRole >= UserRole.Admin)
            {
                Logger.LogInformation("User sign-up attempt by role {Role}", userRole);
            }

            var result = Validator.Validate(userCreateDto);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var hashedPassword = PasswordHasher.HashPassword(userCreateDto.Password);

            var user = Mappings.ConvertToUser(userCreateDto);
            user.Password = hashedPassword.Hash;
            user.Salt = hashedPassword.Salt;
            user.Role = UserRole.User;

            var userId = await UserRepository.InsertAsync(user);

            return userId;
        }
    }
}
