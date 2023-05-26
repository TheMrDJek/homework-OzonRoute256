using NanoPaymentSystem.Domain.Lifecycles;

namespace NanoPaymentSystem.Domain.LifecycleData;

public sealed class PaymentProcessingStartedData : ILifecycleData
{
    public PaymentProcessingStartedData(CardInfo bankCard)
        => BankCard = bankCard;

    public CardInfo BankCard { get; }
}
