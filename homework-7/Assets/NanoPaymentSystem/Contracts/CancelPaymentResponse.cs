namespace NanoPaymentSystem.Contracts;

public class CancelPaymentResponse
{
    public CancelPaymentResponse(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; }

    public string Message { get; }
}
