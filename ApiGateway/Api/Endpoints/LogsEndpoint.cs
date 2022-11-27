using Api.Configurations.AppSettings;
using Api.CrossCutting.Helpers;
using Api.Models.Response;
using Api.Services;
using Microsoft.AspNetCore.Cors;
using RabbitMqService.RabbitMq;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Api.Endpoints
{
    [EnableCors("CorsApi")]
    public class LogsEndpoint
    {
        private readonly ILogger<LogsEndpoint> _logger;
        private readonly MessageManager _messageManager;
        private readonly DockerConfig _dockerConfig;
        private readonly ILogServices _logServices;
        public LogsEndpoint(ILoggerFactory logger, MessageManager messageManager, DockerConfig dockerConfig, ILogServices logServices)
        {
            _logger = logger.CreateLogger<LogsEndpoint>();
            _messageManager = messageManager;
            _dockerConfig = dockerConfig;
            _logServices = logServices;
        }

        public async Task MapLogEndpoint(WebApplication app)
        {
            _ = app.MapGet(
               "/api/logservices/getlogs",
               async () =>
               {
                   try
                   {
                       _logger.LogInformation("Obteniendo logs...");
                       var listaLogs = _logServices.ObtenerLogs();
                       //Cast logs to logResponse
                       var listaLogsResponse = listaLogs.Select(x => new LogResponse
                       {
                           idCol = x.Id.ToString(),
                           msgCol = x.Message,
                           levelCol = x.Level,
                           tmpCol = x.Timestamp.ToString(),
                           excepCol = x.Exception
                       }).ToList();
                       
                       return listaLogsResponse;
                   }
                   catch (Exception ex)
                   {
                       _logger.LogError(ex, "Error el obtener logs");
                       throw;
                   }
               })
           .WithTags("LogService")
           .WithMetadata(new SwaggerOperationAttribute("..."))
           .Produces(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);
        }
    }
}
