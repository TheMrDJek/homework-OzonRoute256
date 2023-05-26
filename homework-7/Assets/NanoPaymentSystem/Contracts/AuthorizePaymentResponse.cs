namespace NanoPaymentSystem.Contracts;

public sealed class AuthorizePaymentResponse
{
    public AuthorizePaymentResponse(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; }

    public string Message { get; }
}
