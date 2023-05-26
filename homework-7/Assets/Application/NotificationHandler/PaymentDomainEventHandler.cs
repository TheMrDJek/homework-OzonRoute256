using MediatR;
using NanoPaymentSystem.Domain.DomainEvents;

namespace NanoPaymentSystem.Application.NotificationHandler;

internal class PaymentDomainEventHandler :
    INotificationHandler<PaymentCreatedEvent>,
    INotificationHandler<PaymentProcessingStartedEvent>,
    INotificationHandler<PaymentAuthorizedEvent>,
    INotificationHandler<PaymentRejectedEvent>,
    INotificationHandler<PaymentCancelledEvent>
{
    private readonly IMessageBroker _messageBroker;

    public PaymentDomainEventHandler(IMessageBroker messageBroker)
        => _messageBroker = messageBroker;

    /// <inheritdoc />
    public Task Handle(PaymentCreatedEvent notification, CancellationToken cancellationToken)
        => _messageBroker.Publish(
            new IntegrationEvent(notification.PaymentId, notification.PaymentStatus, notification.Event),
            cancellationToken);
    

    /// <inheritdoc />
    public Task Handle(PaymentProcessingStartedEvent notification, CancellationToken cancellationToken)
        => _messageBroker.Publish(
            new IntegrationEvent(notification.PaymentId, notification.PaymentStatus, notification.Event),
            cancellationToken);

    /// <inheritdoc />
    public Task Handle(PaymentAuthorizedEvent notification, CancellationToken cancellationToken)
        => _messageBroker.Publish(
            new IntegrationEvent(notification.PaymentId, notification.PaymentStatus, notification.Event),
            cancellationToken);

    /// <inheritdoc />
    public Task Handle(PaymentRejectedEvent notification, CancellationToken cancellationToken)
        => _messageBroker.Publish(
            new IntegrationEvent(notification.PaymentId, notification.PaymentStatus, notification.Event),
            cancellationToken);

    /// <inheritdoc />
    public Task Handle(PaymentCancelledEvent notification, CancellationToken cancellationToken)
        => _messageBroker.Publish(
            new IntegrationEvent(notification.PaymentId, notification.PaymentStatus, notification.Event),
            cancellationToken);
}
