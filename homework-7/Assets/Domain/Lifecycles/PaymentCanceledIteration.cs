namespace NanoPaymentSystem.Domain.Lifecycles;

public sealed class PaymentCanceledIteration : PaymentLifecycle
{
    /// <inheritdoc />
    public PaymentCanceledIteration(Guid entityId, int version, PaymentStatus lifecycleType, ILifecycleData data)
        : base(entityId, version, lifecycleType, data)
    {
    }

    /// <inheritdoc />
    public PaymentCanceledIteration(long id, Guid entityId, int version, PaymentStatus lifecycleType, ILifecycleData data)
        : base(id, entityId, version, lifecycleType, data)
    {
    }

    /// <inheritdoc />
    protected override bool CanApplyInternal(PaymentLifecycle? previousIteration)
        => previousIteration?.LifecycleType == PaymentStatus.CancelRequested;
}
