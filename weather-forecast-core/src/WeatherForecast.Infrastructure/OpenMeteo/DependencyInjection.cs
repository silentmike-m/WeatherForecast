namespace WeatherForecast.Infrastructure.OpenMeteo;

using System.Net;
using global::WeatherForecast.Infrastructure.OpenMeteo.Interfaces;
using global::WeatherForecast.Infrastructure.OpenMeteo.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

internal static class DependencyInjection
{
    public static IServiceCollection AddOpenMeteo(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(nameof(OpenMeteoOptions)).Get<OpenMeteoOptions>();
        options ??= new OpenMeteoOptions();

        services.AddSingleton(options);

        services.AddHttpClient(OpenMeteoOptions.HTTP_CLIENT_NAME,
                client =>
                {
                    client.BaseAddress = options.BaseAddress;
                    client.Timeout = TimeSpan.FromSeconds(options.TimeoutInSeconds);
                })
            .AddPolicyHandler(_ =>
            {
                return HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .OrResult(result => result.StatusCode != HttpStatusCode.OK)
                    .WaitAndRetryAsync(
                        options.RetryCount,
                        _ => TimeSpan.FromSeconds(options.RetrySleepDurationInSeconds)
                    );
            });

        services.AddScoped<IOpenMeteoClient, OpenMeteoClient>();

        return services;
    }
}
