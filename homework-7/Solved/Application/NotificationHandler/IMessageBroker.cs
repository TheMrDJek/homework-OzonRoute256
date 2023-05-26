namespace NanoPaymentSystem.Application.NotificationHandler;

public interface IMessageBroker
{
    Task Publish(IntegrationEvent integrationEvent, CancellationToken cancellationToken);
}