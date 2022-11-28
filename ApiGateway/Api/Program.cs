using Api.Configurations;
using Api.Configurations.AppSettings;
using Api.CrossCutting.Extensions;
using Api.Endpoints;
using Api.Endpoints.Middleware;
using Api.Repositories;
using Api.Services;
using RabbitMqService.Queues;
using RabbitMqService.RabbitMq;

var builder = WebApplication
                .CreateBuilder(args)
                .ConfigureBuilder();

ConfigureServices(builder.Services, builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsApi",
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000")
                                 .AllowAnyMethod()
                                 .AllowAnyHeader()
                                 .AllowCredentials();
                      });

});
var app = builder.Build();

app.UseCors("CorsApi");
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetService<RabbitServiceEndpoint>()?.MapRabbitServiceEndpoint(app);
    scope.ServiceProvider.GetService<ServicesStatusEndpoint>()?.MapServicesStatusEndpoint(app);
    scope.ServiceProvider.GetService<LogsEndpoint>()?.MapLogEndpoint(app);
    scope.ServiceProvider.GetService<InformacionEndpoint>()?.MapInformacionEndpoint(app);
    scope.ServiceProvider.GetService<PreciosEndpoint>()?.MapPrecioEndpoint(app);
}


Configure(app, app.Environment);
app.Run();


void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddScoped<RabbitServiceEndpoint>();
    services.AddScoped<ServicesStatusEndpoint>();
    services.AddScoped<LogsEndpoint>();
    services.AddScoped<PreciosEndpoint>();
    services.AddScoped<InformacionEndpoint>();
    services.AddScoped<FullApisConfig>();

    services.AddScoped<ILogServices,LogServices>();
    services.AddScoped<ILogRepository, LogRepository>();

    services.ConfigureLogger(builder);

    services.AddRabbitMq(settings =>
    {
        settings.ConnectionString = configuration.GetValue<string>("RabbitMq:ConnectionString");
        settings.ExchangeName = configuration.GetValue<string>("AppSettings:ApplicationName");
        settings.QueuePrefetchCount = configuration.GetValue<ushort>("AppSettings:QueuePrefetchCount");
    }, queues =>
    {
        //Agregamos colas para las apis...
        queues.Add<Multas>();
        queues.Add<Reconocimiento>();
    });

    builder.Services.AddConfig<ApisConfig>(builder.Configuration, nameof(ApisConfig));
    builder.Services.AddConfig<DockerConfig>(builder.Configuration, nameof(DockerConfig));
    builder.Services.AddConfig<ConnectionStrings>(builder.Configuration, nameof(ConnectionStrings));

    //services.AddHttpClient<ReconocimientoEndpoint>(client =>
    //{
    //    client.BaseAddress = new Uri(builder.Configuration.GetSection(nameof(ApiReconocimientoConfig)).Get<ApiReconocimientoConfig>().BaseUrl);
    //    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", builder.Configuration.GetSection(nameof(ApiReconocimientoConfig)).Get<ApiReconocimientoConfig>().ApiKey);
    //});
}


void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "WhiteRabbit v1"));
    }
    app.UseMiddleware<ActionMiddleware>();
    app.UseHttpsRedirection();

    app.UseRouting();
}