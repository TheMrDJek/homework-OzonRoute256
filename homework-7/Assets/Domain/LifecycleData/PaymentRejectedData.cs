using NanoPaymentSystem.Domain.Lifecycles;

namespace NanoPaymentSystem.Domain.LifecycleData;

public sealed class PaymentRejectedData : ILifecycleData
{
    public string Message { get; }

    public PaymentRejectedData(string message)
        => Message = message;
}
