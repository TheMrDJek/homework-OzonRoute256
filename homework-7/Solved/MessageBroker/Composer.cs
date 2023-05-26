using Confluent.Kafka;
using Confluent.Kafka.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using NanoPaymentSystem.Application.NotificationHandler;

namespace NanoPaymentSystem.MessageBroker;

public static class Composer
{
    public static IServiceCollection AddMessageBroker(this IServiceCollection services)
    {
        services.AddKafkaClient<KafkaProducer>(new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
        });

        services.AddSingleton<IMessageBroker, MessageBroker>();

        return services;
    }
}
