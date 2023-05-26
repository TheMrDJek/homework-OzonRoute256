using NanoPaymentSystem.Domain.LifecycleData;

namespace NanoPaymentSystem.Domain.DomainEvents;

public sealed class PaymentProcessingStartedEvent : BasePaymentDomainEvent
{
    public PaymentProcessingStartedEvent(PaymentStatus status, Guid paymentId, PaymentProcessingStartedData @event)
        : base(status, paymentId) => Event = @event;

    public PaymentProcessingStartedData Event { get; }
}