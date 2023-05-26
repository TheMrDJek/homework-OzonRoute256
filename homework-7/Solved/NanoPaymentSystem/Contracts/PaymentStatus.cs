namespace NanoPaymentSystem.Contracts;

public enum PaymentStatus
{
    New = 1,
    Processing = 2,
    Authorized = 3,
    Rejected = 4,
    Cancelled = 5,
}