using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using CreditCard.Application.Validations;
using CreditCard.Application.Messaging.CustomerMainAddressRegistered;

namespace CreditCard.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services
            .AddValidators()
            .AddConsumers()
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<AddCreditCardToCustumerCommandValidation>(ServiceLifetime.Scoped);

        return services;
    }

    private static IServiceCollection AddConsumers(this IServiceCollection services) 
    {
        // Registra o consumidor RabbitMQ
        services.AddHostedService<CreditProposalHasCreditReleasedConsumer>();

        return services;
    }
}
