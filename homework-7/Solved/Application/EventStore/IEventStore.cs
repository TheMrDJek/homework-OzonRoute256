using NanoPaymentSystem.Domain.Lifecycles;

namespace NanoPaymentSystem.Application.EventStore;

public interface IEventStore
{
    Task Save(IEnumerable<PaymentLifecycle> paymentLifecycle, CancellationToken cancellationToken);

    Task<IEnumerable<PaymentLifecycle>> LoadEvents(Guid paymentId, CancellationToken cancellationToken);
}
