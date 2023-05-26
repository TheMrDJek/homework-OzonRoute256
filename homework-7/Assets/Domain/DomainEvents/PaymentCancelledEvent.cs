using NanoPaymentSystem.Domain.LifecycleData;

namespace NanoPaymentSystem.Domain.DomainEvents;

public sealed class PaymentCancelledEvent : BasePaymentDomainEvent
{
    public PaymentCancelledEvent(PaymentStatus status, Guid paymentId, PaymentCanceledData @event)
        : base(status, paymentId) => Event = @event;

    public PaymentCanceledData Event { get; }
}
