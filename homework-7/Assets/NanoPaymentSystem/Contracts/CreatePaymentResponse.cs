namespace NanoPaymentSystem.Contracts;

public sealed class CreatePaymentResponse
{
    public CreatePaymentResponse(string paymentId) => PaymentId = paymentId;

    public string PaymentId { get; }
}
