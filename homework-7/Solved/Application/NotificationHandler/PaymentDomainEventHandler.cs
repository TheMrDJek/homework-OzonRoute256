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
    private readonly ITransactionOutbox _transactionOutbox;

    public PaymentDomainEventHandler(ITransactionOutbox transactionOutbox)
        => _transactionOutbox = transactionOutbox;

    /// <inheritdoc />
    public Task Handle(PaymentCreatedEvent notification, CancellationToken cancellationToken)
        => _transactionOutbox.Insert(notification.PaymentId,
            new IntegrationEvent(notification.PaymentId, notification.PaymentStatus, notification.Event),
            cancellationToken);
    

    /// <inheritdoc />
    public Task Handle(PaymentProcessingStartedEvent notification, CancellationToken cancellationToken)
        => _transactionOutbox.Insert(notification.PaymentId,
            new IntegrationEvent(notification.PaymentId, notification.PaymentStatus, notification.Event),
            cancellationToken);

    /// <inheritdoc />
    public Task Handle(PaymentAuthorizedEvent notification, CancellationToken cancellationToken)
        => _transactionOutbox.Insert(notification.PaymentId,
            new IntegrationEvent(notification.PaymentId, notification.PaymentStatus, notification.Event),
            cancellationToken);

    /// <inheritdoc />
    public Task Handle(PaymentRejectedEvent notification, CancellationToken cancellationToken)
        => _transactionOutbox.Insert(notification.PaymentId,
            new IntegrationEvent(notification.PaymentId, notification.PaymentStatus, notification.Event),
            cancellationToken);

    /// <inheritdoc />
    public Task Handle(PaymentCancelledEvent notification, CancellationToken cancellationToken)
        => _transactionOutbox.Insert(notification.PaymentId,
            new IntegrationEvent(notification.PaymentId, notification.PaymentStatus, notification.Event),
            cancellationToken);
}
