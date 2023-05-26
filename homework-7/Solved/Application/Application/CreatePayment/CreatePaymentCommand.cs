using MediatR;

namespace NanoPaymentSystem.Application.Application.CreatePayment;

public sealed class CreatePaymentCommand : IRequest<CreatePaymentResult>
{
    public CreatePaymentCommand(string clientId, string orderId, decimal amount, string currencyCode)
    {
        ClientId = clientId;
        OrderId = orderId;
        Amount = amount;
        CurrencyCode = currencyCode;
    }

    public string ClientId { get; }

    public string OrderId { get; }

    public decimal Amount { get; }

    public string CurrencyCode { get; }
}
