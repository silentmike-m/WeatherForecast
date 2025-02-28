namespace WeatherForecast.Application.UnitTests;

using System.Reflection;
using FluentAssertions;
using MediatR;

[TestClass]
public sealed class MediatRUnitTests
{
    private static List<AssemblyName> ASSEMBLIES =
    [
        new("WeatherForecast.Application"),
        new("WeatherForecast.Infrastructure"),
    ];

    private static readonly Type NOTIFICATION_HANDLER_TYPE = typeof(INotificationHandler<>);
    private static readonly Type NOTIFICATION_TYPE = typeof(INotification);

    private static readonly List<Type> REQUEST_HANDLER_TYPES =
    [
        typeof(IRequestHandler<,>),
        typeof(IRequestHandler<>),
    ];

    [TestMethod]
    public void Should_ContainHandler_For_AllNotifications()
    {
        //ARRANGE
        var types = ASSEMBLIES
            .SelectMany(name => Assembly.Load(name).GetTypes())
            .ToList();

        var notificationTypes = types
            .Where(type => NOTIFICATION_TYPE.IsAssignableFrom(type))
            .ToList();

        //ACT
        var handlerTypes = types
            .Where(type => type.GetInterfaces().Any(interfaceType => interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == NOTIFICATION_HANDLER_TYPE))
            .ToList();

        //ASSERT
        var errors = new List<string>();

        foreach (var notificationType in notificationTypes)
        {
            var existsHandler = handlerTypes
                .Any(handler => handler.GetInterfaces()
                    .Any(type => type.GenericTypeArguments
                        .Any(argument => argument == notificationType)));

            if (!existsHandler)
            {
                errors.Add($"Missing handler for notification {notificationType}");
            }
        }

        errors.Should()
            .BeEmpty()
            ;
    }

    [TestMethod]
    public void Should_ContainSingleHandler_For_AlRequestsAndQueries()
    {
        //ARRANGE
        var types = ASSEMBLIES
            .SelectMany(name => Assembly.Load(name).GetTypes())
            .ToList();

        var requestTypes = new List<Type>();

        foreach (var type in types)
        {
            var isRequestType = typeof(IBaseRequest).IsAssignableFrom(type);

            if (isRequestType)
            {
                requestTypes.Add(type);
            }
        }

        //ACT
        var handlerTypes = new List<Type>();

        foreach (var type in types)
        {
            var handlerInterface = type.GetInterfaces()
                .Where(interfaceType => interfaceType.IsGenericType)
                .SingleOrDefault(interfaceType => REQUEST_HANDLER_TYPES.Contains(interfaceType.GetGenericTypeDefinition()));

            if (handlerInterface is not null)
            {
                handlerTypes.Add(type);
            }
        }

        //ASSERT
        foreach (var requestType in requestTypes)
        {
            handlerTypes.Should().ContainSingle(type =>
                    type.GetInterfaces().Any(interfaceType => interfaceType.GenericTypeArguments.Any(genericType => genericType == requestType)),
                $"Missing handler for request {requestType}");
        }
    }
}
