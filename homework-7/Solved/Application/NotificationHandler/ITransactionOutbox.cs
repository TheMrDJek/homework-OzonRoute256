using NanoPaymentSystem.Domain;

namespace NanoPaymentSystem.Application.NotificationHandler;

public interface ITransactionOutbox
{
    Task Insert(Guid paymentId, IntegrationEvent integrationEvent , CancellationToken cancellationToken);
    
    Task Delete(Guid paymentId,  CancellationToken cancellationToken);

    Task<List<IntegrationEvent>> FindByPayments(CancellationToken cancellationToken);
}