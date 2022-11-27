using Api.Configurations.AppSettings;
using Api.CrossCutting.Helpers;
using RabbitMQ.Client.Exceptions;
using RabbitMqService.RabbitMq;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Api.Endpoints
{
    public class ServicesStatusEndpoint
    {
        private readonly ILogger<ServicesStatusEndpoint> _logger;
        private readonly ApisConfig _apiReconocimientoConfig;
        private readonly MessageManager _messageManager;
        private readonly DockerConfig _dockerConfig;
        public ServicesStatusEndpoint(ILoggerFactory logger, ApisConfig apiReconocimientoConfig, MessageManager messageManager, DockerConfig dockerConfig)
        {
            _logger = logger.CreateLogger<ServicesStatusEndpoint>();
            _apiReconocimientoConfig = apiReconocimientoConfig;
            _messageManager = messageManager;
            _dockerConfig = dockerConfig;
        }

        public async Task MapServicesStatusEndpoint(WebApplication app)
        {
            _ = app.MapPost(
               "/api/servicesstatus/stopsadasd",
               async () =>
               {
                   try
                   {
                       _logger.LogInformation("Cerrando el canal de RabbitMq");
                       //CmdHelper.RunCommand("docker run "+_dockerConfig.ReconocimientoImage);
                       var a =CmdHelper.RunCommandOutput($"ps --filter status=running");

                       var r = CmdHelper.RunCommandOutput($"status " + _dockerConfig.ReconocimientoImage);

                       CmdHelper.RunCommand($"stop " + _dockerConfig.ReconocimientoImage);

                       CmdHelper.RunCommand($"start " + _dockerConfig.ReconocimientoImage);
                       return "Ok";
                   }
                   catch (AlreadyClosedException ex)
                   {
                       _logger.LogError(ex, "Error al cerrar el canal de RabbitMq");
                       return "Error al cerrar el canal de RabbitMq";
                   }
                   catch (Exception ex)
                   {
                       _logger.LogError(ex, "Error en endpoint post reconocimiento");
                       throw;
                   }
               })
           .WithTags("ServicesStatus")
           .WithMetadata(new SwaggerOperationAttribute("..."))
           .Produces(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);

            _ = app.MapGet(
               "/api/servicesstatus/getStatus",
               async () =>
               {
                   try
                   {
                       return true;
                   }
                   catch (AlreadyClosedException ex)
                   {
                       _logger.LogError(ex, "Error al cerrar el canal de RabbitMq");
                       return false;
                   }
                   catch (Exception ex)
                   {
                       _logger.LogError(ex, "Error en endpoint post reconocimiento");
                       return false;
                   }
               })
           .WithTags("ServicesStatus")
           .WithMetadata(new SwaggerOperationAttribute("..."))
           .Produces(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);

        }
    }
}
