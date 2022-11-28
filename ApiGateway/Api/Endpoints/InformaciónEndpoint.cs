using Api.Configurations.AppSettings;
using Api.CrossCutting.Helpers;
using RabbitMqService.Abstractions;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using System.Linq;
using Api.Models;

namespace Api.Endpoints
{
    [ExcludeFromCodeCoverage]
    public class InformacionEndpoint
    {
        private readonly ILogger<InformacionEndpoint> _logger;
        private readonly FullApisConfig _fullApisConfig;
        public InformacionEndpoint(ILoggerFactory logger, FullApisConfig fullApisConfig)
        {
            _logger = logger.CreateLogger<InformacionEndpoint>();
            _fullApisConfig = fullApisConfig;
        }

        public async Task MapInformacionEndpoint(WebApplication app)
        {
            app.MapGet(
                "/api/informacion/transitoGenerado",
                async () =>
                {
                    try
                    {
                        _logger.LogInformation("Obteniendo tránsito generado de api reconocimiento");
                        string url = _fullApisConfig.PatentesEndpoint + "transitoGenerado";
                        var result = await RequestHelper.GetRequest<int>(url);
                        return Results.Ok(result);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Results.Conflict(new ApiError(ex.Message, (int)ErrorCodeEnum.NoConnection));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error obteniendo tránsito generado de api reconocimiento");
                        return Results.Problem(detail: ex.Message, statusCode: (int)ErrorCodeEnum.BaseError);
                    }
                })
            .WithTags("Informacion")
            .WithMetadata(new SwaggerOperationAttribute("..."))
            .Produces(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);

            app.MapGet(
                "/api/informacion/patentesReconocidas",
                async () =>
                {
                    try
                    {
                        _logger.LogInformation("Obteniendo patentes reconocidas de api reconocimiento");
                        string url = _fullApisConfig.PatentesEndpoint + "transitoReconocido";
                        var result = await RequestHelper.GetRequest<int>(url);
                        return Results.Ok(result);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Results.Conflict(new ApiError(ex.Message, (int)ErrorCodeEnum.NoConnection));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error al obtener patentes reconocidas en api reconocimiento");
                        return Results.Problem(detail: ex.Message, statusCode: (int)ErrorCodeEnum.BaseError);
                    }
                })
            .WithTags("Informacion")
            .WithMetadata(new SwaggerOperationAttribute("..."))
            .Produces(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);

            app.MapGet(
                "/api/informacion/patentesNoReconocidas",
                async () =>
                {
                    try
                    {
                        _logger.LogInformation("Obteniendo patentes no reconocidas de api reconocimiento");
                        string url = _fullApisConfig.PatentesEndpoint + "transitoNoReconocido";
                        var result = await RequestHelper.GetRequest<int>(url);
                        return Results.Ok(result);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Results.Conflict(new ApiError(ex.Message, (int)ErrorCodeEnum.NoConnection));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error al obtener patentes no reconocidas en api reconocimiento");
                        return Results.Problem(detail: ex.Message, statusCode: (int)ErrorCodeEnum.BaseError);
                    }
                })
            .WithTags("Informacion")
            .WithMetadata(new SwaggerOperationAttribute("..."))
            .Produces(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);

            app.MapGet(
                "/api/informacion/multasEmitidas",
                async () =>
                {
                    try
                     {
                        _logger.LogInformation("Obteniendo multas emitidas de api multas");
                        string url = _fullApisConfig.MultasEndpoint;
                        var result = await RequestHelper.GetRequest<int>(url);
                        return Results.Ok(result);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Results.Conflict(new ApiError(ex.Message,(int)ErrorCodeEnum.NoConnection));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error al obtener multas emitidas de api multas");
                        return Results.Problem(detail: ex.Message, statusCode:(int)ErrorCodeEnum.BaseError);
                    }
                })
            .WithTags("Informacion")
            .WithMetadata(new SwaggerOperationAttribute("..."))
            .Produces(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);

            app.MapGet(
                "/api/informacion/pagosEmitidos",
                async () =>
                {
                    try
                    {
                        _logger.LogInformation("Obteniendo pagos emitidos de api pagos");
                        string url = _fullApisConfig.PagosEndpoint;
                        var result = await RequestHelper.GetRequest<int>(url);
                        return Results.Ok(result);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Results.Conflict(new ApiError(ex.Message, (int)ErrorCodeEnum.NoConnection));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error al obtener multas emitidas de api multas");
                        return Results.Problem(detail: ex.Message, statusCode: (int)ErrorCodeEnum.BaseError);
                    }
                })
            .WithTags("Informacion")
            .WithMetadata(new SwaggerOperationAttribute("..."))
            .Produces(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);

            app.MapGet(
                "/api/informacion/totalFacturado",
                async () =>
                {
                    try
                    {
                        _logger.LogInformation("Obteniendo el total facturado de api pagos");
                        string url = _fullApisConfig.PagosEndpoint + "totalFacturado";
                        var result = await RequestHelper.GetRequest<double?>(url);
                        return Results.Ok(result);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Results.Conflict(new ApiError(ex.Message, (int)ErrorCodeEnum.NoConnection));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error al obtener multas emitidas de api multas");
                        return Results.Problem(detail: ex.Message, statusCode: (int)ErrorCodeEnum.BaseError);
                    }
                })
            .WithTags("Informacion")
            .WithMetadata(new SwaggerOperationAttribute("..."))
            .Produces(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);
        }
    }
}
