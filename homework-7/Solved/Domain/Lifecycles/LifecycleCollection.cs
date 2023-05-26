using NanoPaymentSystem.Domain.Exceptions;

namespace NanoPaymentSystem.Domain.Lifecycles;

public sealed class LifecycleCollection
{
    private readonly List<PaymentLifecycle> _lifecycle = new();

    public void Add(PaymentLifecycle lifecycle)
    {
        var previousIteration = _lifecycle.LastOrDefault();

        if (lifecycle.CanApply(previousIteration))
        {
            _lifecycle.Add(lifecycle);
        }
        else
        {
            throw new InvalidIterationOrderException();
        }
    }

    public int GetNextVersion()
        => _lifecycle.Any() ? _lifecycle.Max(i => i.Version) + 1 : 1;

    public IEnumerable<PaymentLifecycle> GetTransient()
        => _lifecycle.Where(i => i.IsTransient);

    public void Load(List<PaymentLifecycle> lifecycle)
    {
        var orderedList = lifecycle.OrderBy(i => i.Version);

        PaymentLifecycle? previous = null;

        foreach (var paymentLifecycle in orderedList)
        {
            if (paymentLifecycle.CanApply(previous))
            {
                _lifecycle.Add(paymentLifecycle);
                previous = paymentLifecycle;
            }
            else
            {
                throw new InvalidIterationOrderException();
            }
        }
        
    }
}
