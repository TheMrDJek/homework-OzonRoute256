using Confluent.Kafka;
using NanoPaymentSystem.Application.NotificationHandler;
using Newtonsoft.Json;

namespace NanoPaymentSystem.MessageBroker;

internal sealed class KafkaProducer
{
    private readonly IProducer<Null, string> _producer;

    public KafkaProducer(IProducer<Null, string> producer) => _producer = producer;

    public async Task Publish(IntegrationEvent integrationEvent, CancellationToken cancellationToken)
    {
        var rnd = new Random().Next(0, 99);
        if (rnd < 50)
        {
            throw new Exception();
        }

        await _producer.ProduceAsync("nano_payment_system_callback", new Message<Null, string>
        {
            Value = JsonConvert.SerializeObject(integrationEvent, Formatting.Indented),
        }, cancellationToken);

        _producer.Flush(cancellationToken);
    }
}
