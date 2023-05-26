namespace NanoPaymentSystem.Application.Application.CreatePayment;

public sealed class CreatePaymentResult
{
    public CreatePaymentResult(string paymentId)
        => PaymentId = paymentId;

    public string PaymentId { get; }
}
