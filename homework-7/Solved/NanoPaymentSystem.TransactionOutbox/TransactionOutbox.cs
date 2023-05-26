using NanoPaymentSystem.Application.EventStore;
using NanoPaymentSystem.Application.NotificationHandler;
using NanoPaymentSystem.Application.Repository;
using NanoPaymentSystem.Domain;

namespace NanoPaymentSystem.TransactionOutbox;

public class TransactionOutbox : ITransactionOutbox
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IEventStoreOutbox _eventStore;

    public TransactionOutbox(IPaymentRepository paymentRepository, IEventStoreOutbox eventStore)
    {
        _paymentRepository = paymentRepository;
        _eventStore = eventStore;
    }

    public async Task Insert(Guid paymentId,IntegrationEvent integrationEvent, CancellationToken cancellationToken)
    {
        await _paymentRepository.InsertTransactionOutbox(paymentId, cancellationToken);
        _eventStore.InsertEvent(paymentId, integrationEvent);
    }

    public async Task Delete(Guid paymentId, CancellationToken cancellationToken)
    {
        await _paymentRepository.DeleteTransactionOutbox(paymentId, cancellationToken);
        _eventStore.DeleteEvent(paymentId);
    }

    public async Task<List<IntegrationEvent>> FindByPayments(CancellationToken cancellationToken)
    {
        var result = await _paymentRepository.FindByPayments(cancellationToken);

        return _eventStore.GetEvents(result);
    }
}