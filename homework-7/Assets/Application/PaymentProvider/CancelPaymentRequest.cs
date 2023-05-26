namespace NanoPaymentSystem.Application.PaymentProvider;

public sealed class CancelPaymentRequest
{
    public CancelPaymentRequest(string providerPaymentId)
        => ProviderPaymentId = providerPaymentId;

    public string ProviderPaymentId { get; }
}
