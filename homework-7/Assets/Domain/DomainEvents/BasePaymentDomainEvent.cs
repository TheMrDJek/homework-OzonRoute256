using MediatR;

namespace NanoPaymentSystem.Domain.DomainEvents;

public class BasePaymentDomainEvent : INotification
{
    protected BasePaymentDomainEvent(PaymentStatus paymentStatus, Guid paymentId)
    {
        PaymentStatus = paymentStatus;
        PaymentId = paymentId;
    }

    public PaymentStatus PaymentStatus { get; }

    public Guid PaymentId { get; }
}
