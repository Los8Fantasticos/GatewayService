using Api.Configurations;
using Api.Configurations.AppSettings;
using Api.CrossCutting.Extensions;
using Api.Endpoints;
using RabbitMqService.Queues;
using RabbitMqService.RabbitMq;

var builder = WebApplication
                .CreateBuilder(args)
                .ConfigureBuilder();

ConfigureServices(builder.Services, builder.Configuration);
var app = builder.Build();
using (var scope = app.Services.CreateScope())
    scope.ServiceProvider.GetService<ReconocimientoEndpoint>()?.MapReconocimientoEndpoint(app);

Configure(app, app.Environment);
app.Run();


void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddScoped<ReconocimientoEndpoint>();
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

    builder.Services.AddConfig<ApiReconocimientoConfig>(builder.Configuration, nameof(ApiReconocimientoConfig));

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

    app.UseHttpsRedirection();

    app.UseRouting();
}