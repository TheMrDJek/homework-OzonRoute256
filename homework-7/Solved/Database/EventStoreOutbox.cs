using System.Collections.Concurrent;
using NanoPaymentSystem.Application.EventStore;
using NanoPaymentSystem.Application.NotificationHandler;
using NanoPaymentSystem.Domain;

namespace NanoPaymentSystem.Database;

public class EventStoreOutbox : IEventStoreOutbox
{
    private readonly ConcurrentDictionary<Guid, IntegrationEvent> _eventSensors = new();

    public void InsertEvent(Guid paymentId, IntegrationEvent outboxEvent)
    {
        _eventSensors.AddOrUpdate(paymentId, _ => outboxEvent, (_, _) => outboxEvent);
    }

    public List<IntegrationEvent> GetEvents(List<PaymentOutbox> paymentOutboxes)
    {
        return (from item in _eventSensors 
            from paymentOutbox in paymentOutboxes 
            where item.Key == paymentOutbox.Id 
            select item.Value).ToList();
    }

    public void DeleteEvent(Guid paymentId)
    {
        if (_eventSensors.ContainsKey(paymentId))
        {
            _eventSensors.Remove(paymentId, out _);
        }
    }
}