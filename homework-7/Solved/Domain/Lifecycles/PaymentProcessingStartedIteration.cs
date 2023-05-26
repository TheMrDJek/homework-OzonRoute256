namespace NanoPaymentSystem.Domain.Lifecycles;

public class PaymentProcessingStartedIteration : PaymentLifecycle
{
    /// <inheritdoc />
    public PaymentProcessingStartedIteration(Guid entityId, int version, PaymentStatus lifecycleType, ILifecycleData data)
        : base(entityId, version, lifecycleType, data)
    {
    }

    /// <inheritdoc />
    public PaymentProcessingStartedIteration(long id, Guid entityId, int version, PaymentStatus lifecycleType, ILifecycleData data)
        : base(id, entityId, version, lifecycleType, data)
    {
    }

    /// <inheritdoc />
    protected override bool CanApplyInternal(PaymentLifecycle? previousIteration)
        => previousIteration?.LifecycleType == PaymentStatus.New;
}
