using MediatR;
using NanoPaymentSystem.Domain.LifecycleData;

namespace NanoPaymentSystem.Domain.DomainEvents;

public sealed class PaymentCreatedEvent : BasePaymentDomainEvent
{
    public PaymentCreatedEvent(PaymentStatus status, Guid paymentId, PaymentCreatedData @event)
        : base(status, paymentId) => Event = @event;

    public PaymentCreatedData Event { get; }
}
