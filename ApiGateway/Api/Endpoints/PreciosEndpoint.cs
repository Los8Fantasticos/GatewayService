using Api.Configurations.AppSettings;
using Api.CrossCutting.Helpers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Api.Endpoints
{
    public class PreciosEndpoint
    {
        private readonly ILogger<PreciosEndpoint> _logger;
        private readonly FullApisConfig _fullApisConfig;
        public PreciosEndpoint(ILoggerFactory logger, FullApisConfig fullApisConfig)
        {
            _logger = logger.CreateLogger<PreciosEndpoint>();
            _fullApisConfig = fullApisConfig;
        }

        public async Task MapPrecioEndpoint(WebApplication app)
        {
            app.MapPost(
                "/api/precios/modifyPrice",
                async ([FromBody]int nuevoPrecio) =>
                {
                    try
                    {
                        _logger.LogInformation("test");
                        string url = _fullApisConfig.MultasEndpoint + $"modificarPrecio";
                        var result = await RequestHelper.PostRequest<int,int>(url, nuevoPrecio);
                        return result;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error en endpoint post reconocimiento");
                        throw ex;
                    }
                })
            .WithTags("Precios")
            .WithMetadata(new SwaggerOperationAttribute("..."))
            .Produces(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);

        }
    }
}
