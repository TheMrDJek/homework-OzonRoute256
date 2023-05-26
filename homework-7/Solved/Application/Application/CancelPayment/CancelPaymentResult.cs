namespace NanoPaymentSystem.Application.Application.CancelPayment;

public sealed class CancelPaymentResult
{
    public CancelPaymentResult(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; }

    public string Message { get; }
}
