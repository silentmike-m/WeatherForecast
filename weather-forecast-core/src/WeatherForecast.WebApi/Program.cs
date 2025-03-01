using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using WeatherForecast.Application;
using WeatherForecast.Application.Common.Shared;
using WeatherForecast.Infrastructure;
using WeatherForecast.WebApi.Extensions;
using WeatherForecast.WebApi.Filters;

const int EXIT_FAILURE = 1;
const int EXIT_SUCCESS = 0;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.WithProperty(nameof(ServiceConstants.ServiceName), ServiceConstants.ServiceName)
    .Enrich.WithProperty(nameof(ServiceConstants.ServiceVersion), ServiceConstants.ServiceVersion));

builder.Services.AddHealthChecks();

builder.Services
    .AddProblemDetails(options =>
        options.CustomizeProblemDetails = ctx =>
        {
            ctx.ProblemDetails.Extensions.Add("trace_id", ctx.HttpContext.TraceIdentifier);
            ctx.ProblemDetails.Extensions.Add("request", $"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}");
            ctx.ProblemDetails.Extensions.Add("service_name", ServiceConstants.ServiceName);
            ctx.ProblemDetails.Extensions.Add("service_version", ServiceConstants.ServiceVersion);
        });

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName);
});

builder.Services.AddSwaggerGen();

builder.Services.AddControllers(options =>
    {
        options.Filters.Add<ApiExceptionFilterAttribute>();
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

try
{
    Log.Information("Starting host...");

    var app = builder.Build();

    app.MapHealthChecks("/hc",
        new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = HealthCheckExtensions.WriteResponse,
        });

    app.UseExceptionHandler();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(swaggerUiOptions => swaggerUiOptions.DocExpansion(DocExpansion.None));
    }

    app.MapControllers();

    await app.RunAsync();

    return EXIT_SUCCESS;
}
catch (Exception exception)
{
    Log.Fatal(exception, "Host terminated unexpectedly");

    return EXIT_FAILURE;
}
finally
{
    await Log.CloseAndFlushAsync();
}
