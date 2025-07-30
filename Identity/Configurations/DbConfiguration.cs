using Identity.Dal;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Configurations
{
    public static class DbConfiguration
    {
            public static WebApplicationBuilder ConfigureDatabase(this WebApplicationBuilder builder)
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

                builder.Services.AddDbContext<MainContext>(options =>
                    options.UseNpgsql(connectionString));

                return builder;
            }
    }
}
