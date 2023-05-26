using NanoPaymentSystem.Domain;
using NanoPaymentSystem.Domain.Lifecycles;

namespace NanoPaymentSystem.Application.NotificationHandler;

public sealed class IntegrationEvent
{
    public IntegrationEvent(Guid paymentId, PaymentStatus status, ILifecycleData data)
    {
        PaymentId = paymentId;
        Status = status;
        Data = data;
    }

    public Guid PaymentId { get; }

    public PaymentStatus Status { get; }

    public ILifecycleData Data { get;}
}
