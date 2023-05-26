using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NanoPaymentSystem.Application.Repository;

namespace NanoPaymentSystem.Application;

public static class Composer
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IPaymentInfrastructure, PaymentInfrastructure>();
        services.AddMediatR(typeof(Composer));
        return services;
    }
}