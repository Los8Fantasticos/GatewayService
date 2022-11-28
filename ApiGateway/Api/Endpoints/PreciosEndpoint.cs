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
            _ = app.MapPost(
                "/api/precios/modifyPrice",
                async ([FromBody]double nuevoPrecio) =>
                {
                    try
                    {
                        _logger.LogInformation("Modificando precio de peajes");
                        string url = _fullApisConfig.PagosEndpoint + $"modificarPrecioPeaje";
                        var result = await RequestHelper.PostRequest<double?,double?>(url, nuevoPrecio);
                        return result;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error al modificar precio de peajes");
                        throw ex;
                    }
                })
            .WithTags("Precios")
            .WithMetadata(new SwaggerOperationAttribute("..."))
            .Produces(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);

            _ = app.MapGet(
                "/api/precios/getPrice",
                async () =>
                {
                    try
                    {
                        _logger.LogInformation("Obteniendo precio de api pagos");
                        string url = _fullApisConfig.PagosEndpoint +"getPrice";
                        var result = await RequestHelper.GetRequest<double?>(url);
                        return result;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error al modificar precio de peajes");
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
