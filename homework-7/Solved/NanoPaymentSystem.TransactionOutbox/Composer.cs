using Microsoft.Extensions.DependencyInjection;
using NanoPaymentSystem.Application.EventStore;
using NanoPaymentSystem.Application.NotificationHandler;
using NanoPaymentSystem.Database;

namespace NanoPaymentSystem.TransactionOutbox;

public static class Composer
{
    public static IServiceCollection AddTransactionOutbox(this IServiceCollection services)
    {
        services.AddSingleton<ITransactionOutbox, TransactionOutbox>();
        services.AddSingleton<IEventStoreOutbox, EventStoreOutbox>();

        return services;
    }
}