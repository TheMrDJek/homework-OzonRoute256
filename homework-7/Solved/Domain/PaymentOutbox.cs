namespace NanoPaymentSystem.Domain;

public class PaymentOutbox
{
    public Guid Id { get; set; }
    
    public int RetryCount { get; set; }
}