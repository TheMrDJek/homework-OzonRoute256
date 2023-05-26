namespace NanoPaymentSystem.Application.PaymentProvider;

public sealed class CancelPaymentResponse
{
    public CancelPaymentResponse(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; }

    public string Message { get; }
}
