namespace NanoPaymentSystem.Domain.Lifecycles;

public class PaymentAuthorizedIteration : PaymentLifecycle
{
    /// <inheritdoc />
    public PaymentAuthorizedIteration(Guid entityId, int version, PaymentStatus lifecycleType, ILifecycleData data)
        : base(entityId, version, lifecycleType, data)
    {
    }

    /// <inheritdoc />
    public PaymentAuthorizedIteration(long id, Guid entityId, int version, PaymentStatus lifecycleType, ILifecycleData data)
        : base(id, entityId, version, lifecycleType, data)
    {
    }

    /// <inheritdoc />
    protected override bool CanApplyInternal(PaymentLifecycle? previousIteration)
        => previousIteration?.LifecycleType is PaymentStatus.Processing or PaymentStatus.CancelRequested;
}
