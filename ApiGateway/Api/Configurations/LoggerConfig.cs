namespace Api.Configurations
{
    public static class LoggerConfig
    {
        [System.Obsolete]
        public static WebApplicationBuilder ConfigureLogger(this IServiceCollection services, WebApplicationBuilder builder)
        {
            #region Logging

            //_ = builder.Host.UseSerilog((hostContext, loggerConfiguration) =>
            //{
            //    var assembly = Assembly.GetEntryAssembly();
            //    _ = loggerConfiguration.WriteTo.MSSqlServer(connectionString: builder.Configuration.GetConnectionString("SqlConnection"),
            //        tableName: builder.Configuration.GetSection("Serilog").GetSection("TableName").Value,
            //        appConfiguration: builder.Configuration,
            //        autoCreateSqlTable: true,
            //        columnOptionsSection: builder.Configuration.GetSection("Serilog").GetSection("ColumnOptions"),
            //        schemaName: builder.Configuration.GetSection("Serilog").GetSection("SchemaName").Value);
            //    _ = loggerConfiguration.WriteTo.MSSqlServer(connectionString: builder.Configuration.GetConnectionString("SqlConnection2"),
            //        tableName: builder.Configuration.GetSection("Serilog").GetSection("TableName").Value,
            //        appConfiguration: builder.Configuration,
            //        autoCreateSqlTable: true,
            //        columnOptionsSection: builder.Configuration.GetSection("Serilog").GetSection("ColumnOptions"),
            //        schemaName: builder.Configuration.GetSection("Serilog").GetSection("SchemaName").Value);
            //});

            #endregion Logging

            return builder;
        }


    }
}
