namespace NanoPaymentSystem.Domain.Lifecycles;

public class PaymentRequestCancelIteration : PaymentLifecycle
{
    /// <inheritdoc />
    public PaymentRequestCancelIteration(Guid entityId, int version, PaymentStatus lifecycleType, ILifecycleData data)
        : base(entityId, version, lifecycleType, data)
    {
    }

    /// <inheritdoc />
    public PaymentRequestCancelIteration(long id, Guid entityId, int version, PaymentStatus lifecycleType, ILifecycleData data)
        : base(id, entityId, version, lifecycleType, data)
    {
    }

    /// <inheritdoc />
    protected override bool CanApplyInternal(PaymentLifecycle? previousIteration)
        => previousIteration?.LifecycleType == PaymentStatus.Authorized;
}
