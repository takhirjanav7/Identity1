﻿using Serilog;

namespace Identity.Api.Configurations
{
    public static class SerilogConfiguration
    {
        public static void ConfigureSerilog(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(dispose: true);
        }
    }
}
