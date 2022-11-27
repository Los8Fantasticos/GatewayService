using Api.Configurations.AppSettings;
using Api.CrossCutting.Helpers;
using RabbitMQ.Client.Exceptions;
using RabbitMqService.RabbitMq;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Api.Endpoints
{
    public class RabbitServiceEndpoint
    {
        private readonly ILogger<RabbitServiceEndpoint> _logger;
        private readonly ApisConfig _apiReconocimientoConfig;
        private readonly MessageManager _messageManager;
        private readonly DockerConfig _dockerConfig;
        private readonly string ReconocimientoController = "Reconocimiento";
        public RabbitServiceEndpoint(ILoggerFactory logger, ApisConfig apiReconocimientoConfig, MessageManager messageManager, DockerConfig dockerConfig)
        {
            _logger = logger.CreateLogger<RabbitServiceEndpoint>();
            _apiReconocimientoConfig = apiReconocimientoConfig;
            _messageManager = messageManager;
            _dockerConfig = dockerConfig;
        }
        
        public async Task MapRabbitServiceEndpoint(WebApplication app)
        {
            _ = app.MapPost(
               "/api/rabbitservice/stopAllServices",
               async () =>
               {
                   try
                   {
                       _logger.LogInformation("Cerrando el canal de RabbitMq");
                       _messageManager.Channel.Close();
                       return "Ok";
                   }
                   catch(AlreadyClosedException ex)
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
           .WithTags("ApiRabbitService")
           .WithMetadata(new SwaggerOperationAttribute("..."))
           .Produces(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);

            app.MapPost(
               "/api/rabbitservice/stopService",
               async (string queue) =>
               {
                   try
                   {
                       _logger.LogInformation("Cerrando el canal de RabbitMq");
                       _messageManager.Channel.QueueDelete(queue, false,false);
                       return "123";
                   }
                   catch (Exception ex)
                   {
                       _logger.LogError(ex, "Error en endpoint post reconocimiento");
                       throw;
                   }
               })
            .WithTags("ApiRabbitService")
            .WithMetadata(new SwaggerOperationAttribute("..."))
            .Produces(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);
        }
    }
}
