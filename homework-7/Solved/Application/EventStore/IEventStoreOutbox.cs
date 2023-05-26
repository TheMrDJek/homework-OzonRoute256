using NanoPaymentSystem.Application.NotificationHandler;
using NanoPaymentSystem.Domain;

namespace NanoPaymentSystem.Application.EventStore;

public interface IEventStoreOutbox
{
    void InsertEvent(Guid paymentId, IntegrationEvent outboxEvent);

    List<IntegrationEvent> GetEvents(List<PaymentOutbox> paymentOutboxes);

    void DeleteEvent(Guid paymentId);
}