using Api.Configurations.AppSettings;
using Api.CrossCutting.Helpers;
using Api.Models.Response;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RabbitMqService.Abstractions;
using RabbitMqService.Queues;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Api.Endpoints
{
    [ExcludeFromCodeCoverage]
    public class ReconocimientoEndpoint
    {
        private readonly ILogger<ReconocimientoEndpoint> _logger;
        private readonly ApiReconocimientoConfig _apiReconocimientoConfig;
        private readonly string ReconocimientoController = "Reconocimiento";
        private readonly IMessageSender _messageSender;
        public ReconocimientoEndpoint(ILoggerFactory logger, ApiReconocimientoConfig apiReconocimientoConfig, IMessageSender messageSender)
        {
            _logger = logger.CreateLogger<ReconocimientoEndpoint>();
            _apiReconocimientoConfig = apiReconocimientoConfig;
            _messageSender = messageSender;
        }

        public async Task MapReconocimientoEndpoint(WebApplication app)
        {
            app.MapPost(
               "/api/reconocimiento/",
               async (string patente) =>
               {
                   try
                   {
                       _logger.LogInformation("test");
                       await _messageSender.PublishAsync<Reconocimiento, string>(patente);
                       string result = "asdasd";
                       return result;
                   }
                   catch (Exception ex)
                   {
                       _logger.LogError(ex, "Error en endpoint post reconocimiento");
                       throw;
                   }
               })
           .WithTags("ApiReconocimiento")
           .WithMetadata(new SwaggerOperationAttribute("..."))
           .Produces(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);

            _ = app.MapPost(
            "/api/"+ReconocimientoController+"/{patente}",
            async (string patente) =>
            {
                try
                {
                    _logger.LogInformation("Get api Reconocimiento");
                    string url = $"{_apiReconocimientoConfig.BaseUrl}{_apiReconocimientoConfig.PatenteEndpoint}/?patente={patente}";
                    var result = RequestHelper.GetRequest<ReconocimientoResponse>(url);
                    return result;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error en endpoint Multa.");
                    throw;
                }
            })
            .WithTags("ApiReconocimiento")
            .WithMetadata(new SwaggerOperationAttribute("aaa"))
            .Produces<ReconocimientoResponse>(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);
        }
    }
}
