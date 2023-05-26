using NanoPaymentSystem.Domain.Lifecycles;

namespace NanoPaymentSystem.Domain.LifecycleData;

public sealed class PaymentCreatedData : ILifecycleData
{
    public PaymentCreatedData(string clientId, string orderId, Price price)
    {
        ClientId = clientId;
        OrderId = orderId;
        Price = price;
    }

    public string ClientId { get; }

    public string OrderId { get; }

    public Price Price { get; }
}
