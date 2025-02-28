namespace WeatherForecast.Application;

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Application.Common;
using WeatherForecast.Application.Common.Interfaces;
using WeatherForecast.Application.Common.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddSingleton<IGuidService, GuidService>();

        return services;
    }
}
