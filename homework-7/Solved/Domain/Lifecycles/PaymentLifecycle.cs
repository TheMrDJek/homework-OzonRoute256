namespace NanoPaymentSystem.Domain.Lifecycles;

public abstract class PaymentLifecycle
{
    protected PaymentLifecycle(Guid entityId, int version, PaymentStatus lifecycleType, ILifecycleData data)
        : this (0, entityId, version, lifecycleType, data)
    {
    }

    protected PaymentLifecycle(long id, Guid entityId, int version, PaymentStatus lifecycleType, ILifecycleData data)
    {
        Id = id;
        EntityId = entityId;
        Version = version;
        LifecycleType = lifecycleType;
        Data = data;
    }

    public long Id { get; }

    public Guid EntityId { get; }

    public int Version { get; }

    public PaymentStatus LifecycleType { get; }

    public ILifecycleData Data { get; }

    public bool IsTransient => Id == 0;

    public bool CanApply(PaymentLifecycle? previousIteration)
    {
        var isCorrectOrder = Version - (previousIteration?.Version ?? 0) == 1;

        return isCorrectOrder && CanApplyInternal(previousIteration);
    }

    protected abstract bool CanApplyInternal(PaymentLifecycle? previousIteration);
}
