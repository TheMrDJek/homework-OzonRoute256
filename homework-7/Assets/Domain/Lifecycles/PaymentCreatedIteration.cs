namespace NanoPaymentSystem.Domain.Lifecycles;

public sealed class PaymentCreatedIteration : PaymentLifecycle
{
    /// <inheritdoc />
    public PaymentCreatedIteration(Guid entityId, int version, PaymentStatus lifecycleType, ILifecycleData data)
        : base(entityId, version, lifecycleType, data)
    {
    }

    /// <inheritdoc />
    public PaymentCreatedIteration(long id, Guid entityId, int version, PaymentStatus lifecycleType, ILifecycleData data)
        : base(id, entityId, version, lifecycleType, data)
    {
    }

    /// <inheritdoc />
    protected override bool CanApplyInternal(PaymentLifecycle? previousIteration)
        => previousIteration == null;
}
