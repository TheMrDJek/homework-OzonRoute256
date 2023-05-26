using NanoPaymentSystem.Domain.Lifecycles;

namespace NanoPaymentSystem.Domain.LifecycleData;

public sealed class PaymentRequestCancelData : ILifecycleData
{
    public PaymentRequestCancelData(string message)
        => Message = message;

    public string Message { get; }
}
