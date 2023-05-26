using NanoPaymentSystem.Domain.Lifecycles;

namespace NanoPaymentSystem.Domain.LifecycleData;

public sealed class PaymentAuthorizedData : ILifecycleData
{
    public PaymentAuthorizedData(string providerPaymentId)
        => ProviderPaymentId = providerPaymentId;

    public string ProviderPaymentId { get; }
}
