using Microsoft.Extensions.DependencyInjection;
using NanoPaymentSystem.Application.NotificationHandler;

namespace NanoPaymentSystem.MessageBroker;

internal class MessageBroker : IMessageBroker
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public MessageBroker(IServiceScopeFactory serviceScopeFactory)
        => _serviceScopeFactory = serviceScopeFactory;

    /// <inheritdoc />
    public async Task Publish(IntegrationEvent integrationEvent, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();

        var scopedServices = scope.ServiceProvider;
        var producer = scopedServices.GetRequiredService<KafkaProducer>();

        await producer.Publish(integrationEvent, cancellationToken);
    }
}
