using NanoPaymentSystem.Domain.LifecycleData;

namespace NanoPaymentSystem.Domain.DomainEvents;

public sealed class PaymentRejectedEvent : BasePaymentDomainEvent
{
    public PaymentRejectedEvent(PaymentStatus status, Guid paymentId, PaymentRejectedData @event)
        : base(status, paymentId) => Event = @event;

    public PaymentRejectedData Event { get; }
}