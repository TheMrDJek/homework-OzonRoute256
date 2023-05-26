using Common;
using Confluent.Kafka;

var producerConfig = new ProducerConfig
{
    BootstrapServers = "***"
};

using IProducer<string, Person> producer = new ProducerBuilder<string, Person>(producerConfig)
    .SetValueSerializer(new CustomKafkaSerializer<Person>())
    .Build();

var rand = new Random();

for (var i = 0; i < 100; i++)
{
    var msg = new Message<string, Person>
    {
        Key = i.ToString(),
        Value = new Person
        {
            Name = Guid.NewGuid().ToString(),
            Age = rand.Next(20, 50)
        }
    };

    producer.Produce("person_event", msg);
}

producer.Flush();