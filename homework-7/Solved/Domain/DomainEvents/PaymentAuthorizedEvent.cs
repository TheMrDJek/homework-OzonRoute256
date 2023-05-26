using MediatR;
using NanoPaymentSystem.Domain.LifecycleData;

namespace NanoPaymentSystem.Domain.DomainEvents;

public sealed class PaymentAuthorizedEvent : BasePaymentDomainEvent
{
    public PaymentAuthorizedEvent(PaymentStatus status, Guid paymentId, PaymentAuthorizedData @event)
        : base(status, paymentId) => Event = @event;

    public PaymentAuthorizedData Event { get; }
}
