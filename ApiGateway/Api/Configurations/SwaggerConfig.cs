using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;

namespace Api.Configurations
{
    public static class SwaggerConfig
    {
        public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
        {
            //#region Serialisation

            //_ = builder.Services.Configure<JsonOptions>(opt =>
            //{
            //    opt.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            //    opt.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            //    opt.SerializerOptions.PropertyNameCaseInsensitive = true;
            //    opt.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            //    opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            //});

            //#endregion Serialisation

            #region Swagger

            var ti = CultureInfo.CurrentCulture.TextInfo;

            _ = builder.Services.AddEndpointsApiExplorer();
            _ = builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Version = "v1",
                        Title = $"ApiGateway - {ti.ToTitleCase(builder.Environment.EnvironmentName)} ",
                        Description = "API intermediaria para conexión a las apis",
                        Contact = new OpenApiContact
                        {
                            Name = "ApiGateway",
                            Email = "asd@asd.com",
                            Url = new Uri("https://github.com/Los8Fantasticos/Sistema-Telepeaje") //cambiar
                        },
                        License = new OpenApiLicense()
                        {
                            Name = "ApiGateway - License - MIT",
                            Url = new Uri("https://opensource.org/licenses/MIT")
                        }
                    });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                options.DocInclusionPredicate((name, api) => true);
            });

            #endregion Swagger

            return builder;
        }
    }
}
