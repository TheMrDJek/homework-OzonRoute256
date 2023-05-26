namespace NanoPaymentSystem.Application.Application.AuthorizePayment;

public sealed class AuthorizePaymentResult
{
    public AuthorizePaymentResult(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; }

    public string Message { get; }
}
