using Serilog;
using Serilog.Filters;
using System.Reflection;

namespace Api.Configurations
{
    public static class LoggerConfig
    {
        [System.Obsolete]
        public static WebApplicationBuilder ConfigureLogger(this IServiceCollection services, WebApplicationBuilder builder)
        {
            #region Logging

            bool EnableMicrosoftLogs = builder.Configuration.GetValue<bool>("Serilog:EnableMicrosoftLogs");
            bool EnableMicrosoftAspNetCoreLogs = builder.Configuration.GetValue<bool>("Serilog:EnableMicrosoftAspNetCoreLogs");
            bool EnableSystemLogs = builder.Configuration.GetValue<bool>("Serilog:EnableSystemLogs");

            _ = builder.Host.UseSerilog((hostContext, loggerConfiguration) =>
            {
                if (!EnableMicrosoftLogs) loggerConfiguration.Filter.ByExcluding(Matching.FromSource("Microsoft"));
                if (!EnableSystemLogs) loggerConfiguration.Filter.ByExcluding(Matching.FromSource("System"));
                if (!EnableMicrosoftAspNetCoreLogs) loggerConfiguration.Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore"));
                var assembly = Assembly.GetEntryAssembly();
                _ = loggerConfiguration.WriteTo.MSSqlServer(connectionString: builder.Configuration.GetConnectionString("SqlConnection"),
                    tableName: builder.Configuration.GetSection("Serilog").GetSection("TableName").Value,
                    appConfiguration: builder.Configuration,
                    autoCreateSqlTable: true,
                    columnOptionsSection: builder.Configuration.GetSection("Serilog").GetSection("ColumnOptions"),
                    schemaName: builder.Configuration.GetSection("Serilog").GetSection("SchemaName").Value);
            });

            #endregion Logging

            return builder;
        }


    }
}
