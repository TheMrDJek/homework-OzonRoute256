namespace NanoPaymentSystem.Domain.Lifecycles;

public sealed class PaymentRejectedIteration : PaymentLifecycle
{
    /// <inheritdoc />
    protected override bool CanApplyInternal(PaymentLifecycle? previousIteration)
        => previousIteration?.LifecycleType == PaymentStatus.Processing;

    /// <inheritdoc />
    public PaymentRejectedIteration(Guid entityId, int version, PaymentStatus lifecycleType, ILifecycleData data)
        : base(entityId, version, lifecycleType, data)
    {
    }

    /// <inheritdoc />
    public PaymentRejectedIteration(long id, Guid entityId, int version, PaymentStatus lifecycleType, ILifecycleData data)
        : base(id, entityId, version, lifecycleType, data)
    {
    }
}
