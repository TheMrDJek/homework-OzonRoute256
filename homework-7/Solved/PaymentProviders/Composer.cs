using Microsoft.Extensions.DependencyInjection;
using NanoPaymentSystem.Application.PaymentProvider;

namespace NanoPaymentSystem.PaymentProviders;

public static class Composer
{
    public static IServiceCollection AddFakePaymentProvider(this IServiceCollection services)
    {
        services.AddSingleton<IPaymentProvider, FakePaymentProvider>();
        return services;
    }
}
