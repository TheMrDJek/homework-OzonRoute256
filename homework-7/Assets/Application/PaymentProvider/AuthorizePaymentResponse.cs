namespace NanoPaymentSystem.Application.PaymentProvider;

public sealed class AuthorizePaymentResponse
{
    public AuthorizePaymentResponse(bool isSuccess, string message, string? providerPaymentId)
    {
        IsSuccess = isSuccess;
        Message = message;
        ProviderPaymentId = providerPaymentId;
    }

    public bool IsSuccess { get; }

    public string Message { get; }

    public string? ProviderPaymentId { get; }
}
