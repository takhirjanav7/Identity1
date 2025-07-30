using FluentValidation;
using Identity.Bll.Services;
using Identity.Bll.Services.Helper;
using Identity.Bll.Validator;
using Identity.Repository.Repositories;

namespace Identity.Api.Configurations
{
    public static class DependancyInjectionConfiguration
    {
        public static void ConfigureDI(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            builder.Services.AddValidatorsFromAssemblyContaining<UserCreateDtoValidator>();

            var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
            builder.Services.AddSingleton<JwtSettings>(jwtSettings);
        }
    }
}
