using Api.Configurations.AppSettings;
using Api.CrossCutting.Helpers;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
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
               "/api/servicesstatus/changeStatusReconocimiento",
               async ([FromBody]bool estado) =>
               {
                   try
                   {
                       _logger.LogInformation("Cambiando el estado de api reconocimiento");
                       //CmdHelper.RunCommand("docker run "+_dockerConfig.ReconocimientoImage);
                       //var a =CmdHelper.RunCommandOutput($"ps --filter status=running");

                       /*var r = await CmdHelper.RunCommandOutput($"status " + _dockerConfig.ReconocimientoImage)*/;

                       if(estado) CmdHelper.RunCommand($"start " + _dockerConfig.ReconocimientoImage);
                       else CmdHelper.RunCommand($"stop " + _dockerConfig.ReconocimientoImage);

                       return Results.Ok(estado);
                   }
                   catch (AlreadyClosedException ex)
                   {
                       _logger.LogError(ex, "Error al cambiar el estado de api reconocimiento");
                       return Results.Conflict(new ApiError(ex.Message, (int)ErrorCodeEnum.NoConnection));
                   }
                   catch (Exception ex)
                   {
                       _logger.LogError(ex, "Error al cambiar el estado de api reconocimiento");
                       return Results.Problem(detail: ex.Message, statusCode: (int)ErrorCodeEnum.BaseError);
                   }
               })
           .WithTags("ServicesStatus")
           .WithMetadata(new SwaggerOperationAttribute("..."))
           .Produces(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);

            _ = app.MapPost(
               "/api/servicesstatus/changeStatusMultas",
               async ([FromBody] bool estado) =>
               {
                   try
                   {
                       _logger.LogInformation("Cambiando el estado de api multas");
                       //CmdHelper.RunCommand("docker run "+_dockerConfig.ReconocimientoImage);
                       //var a =CmdHelper.RunCommandOutput($"ps --filter status=running");

                       /*var r = await CmdHelper.RunCommandOutput($"status " + _dockerConfig.ReconocimientoImage)*/
                       ;

                       if (estado) CmdHelper.RunCommand($"start " + _dockerConfig.MultasImage);
                       else CmdHelper.RunCommand($"stop " + _dockerConfig.MultasImage);

                       return Results.Ok(estado);
                   }
                   catch (AlreadyClosedException ex)
                   {
                       _logger.LogError(ex, "Error al cambiar el estado de api multas");
                       return Results.Conflict(new ApiError(ex.Message, (int)ErrorCodeEnum.NoConnection));
                   }
                   catch (Exception ex)
                   {
                       _logger.LogError(ex, "Error al cambiar el estado de api multas");
                       return Results.Problem(detail: ex.Message, statusCode: (int)ErrorCodeEnum.BaseError);
                   }
               })
           .WithTags("ServicesStatus")
           .WithMetadata(new SwaggerOperationAttribute("..."))
           .Produces(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);

            _ = app.MapPost(
               "/api/servicesstatus/changeStatusPagos",
               async ([FromBody] bool estado) =>
               {
                   try
                   {
                       _logger.LogInformation("Cambiando el estado de api pagos");
                       //CmdHelper.RunCommand("docker run "+_dockerConfig.ReconocimientoImage);
                       //var a =CmdHelper.RunCommandOutput($"ps --filter status=running");

                       /*var r = await CmdHelper.RunCommandOutput($"status " + _dockerConfig.ReconocimientoImage)*/

                       if (estado) CmdHelper.RunCommand($"start " + _dockerConfig.PagosImage);
                       else CmdHelper.RunCommand($"stop " + _dockerConfig.PagosImage);

                       return Results.Ok(estado);
                   }
                   catch (AlreadyClosedException ex)
                   {
                       _logger.LogError(ex, "Error al cambiar el estado de api pagos");
                       return Results.Conflict(new ApiError(ex.Message, (int)ErrorCodeEnum.NoConnection));
                   }
                   catch (Exception ex)
                   {
                       _logger.LogError(ex, "Error al cambiar el estado de api pagos");
                       return Results.Problem(detail: ex.Message, statusCode: (int)ErrorCodeEnum.BaseError);
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
                       _logger.LogError(ex, "Error al obtener el estado de la api gateway");
                       return false;
                   }
                   catch (Exception ex)
                   {
                       _logger.LogError(ex, "Error al obtener el estado de la api gateway");
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
