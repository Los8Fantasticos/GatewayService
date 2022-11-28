using Api.Configurations;

namespace Api.Endpoints.Middleware
{
    public class ActionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        private readonly bool _readRequestBody;
        private readonly int _maximumLength;
        private bool isActive;

        public ActionMiddleware(
            RequestDelegate next,
            ILoggerFactory loggerFactory,
            IConfiguration configuration)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ActionMiddleware>();
            _configuration = configuration;
            isActive = true;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(context.Request.Path == "/api/servicesstatus/changeStatusGateway")
            {
                await _next(context);
            }
            if (StatusApi.Current.IsActive)
            {
                await _next(context);
            }
            else
            {
                if (context.Request.Path != "/api/servicesstatus/getStatus")
                {
                    context.Response.StatusCode = 503;
                    await context.Response.WriteAsync("Service Unavailable");
                }
                else
                {
                    await _next(context);
                }
            }
        }
    }
}
