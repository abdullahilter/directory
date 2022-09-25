using common.Options;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace common.RabbitMq;

public static class RabbitMqExtensions
{
    public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services, RabbitMqOptions rabbitMqOptions)
    {
        services.AddMassTransit(configure =>
        {
            configure.AddConsumers(Assembly.GetEntryAssembly());

            configure.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(rabbitMqOptions.Host);
                configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(rabbitMqOptions.Prefix, false));
            });
        });

        return services;
    }
}