namespace NanoPaymentSystem.Contracts;

public class PaymentResponse
{
    public PaymentResponse(
        Guid id,
        string clientId,
        string orderId,
        decimal amount,
        string currencyCode,
        PaymentStatus status,
        string? message,
        CardInfoResponse? bankCard,
        string? providerPaymentId)
    {
        Id = id;
        ClientId = clientId;
        OrderId = orderId;
        Amount = amount;
        CurrencyCode = currencyCode;
        Status = status;
        Message = message;
        BankCard = bankCard;
        ProviderPaymentId = providerPaymentId;
    }

    public Guid Id { get; }

    public string ClientId { get; }

    public string OrderId { get; }

    public decimal Amount { get; }

    public string CurrencyCode { get; }

    public PaymentStatus Status { get; }

    public string? Message { get; }

    public CardInfoResponse? BankCard { get; }

    public string? ProviderPaymentId { get; }
}
