using Common;
using Confluent.Kafka;

var consumerConfig = new ConsumerConfig
{
    BootstrapServers = "***",
    GroupId = "test_group_1",
    AutoOffsetReset = AutoOffsetReset.Earliest,
    EnableAutoCommit = false,
    HeartbeatIntervalMs = 5000
};

const int batchSize = 15;
Del del = new(DelProcessMessages);
var consumer1 = Task.Run(async () => await ConsumeMessages(batchSize, consumerConfig, del, CancellationToken.None));

await consumer1;

static async Task ConsumeMessages(int batchSize, ConsumerConfig consumerConfig,Del del ,CancellationToken ct)
{
    IConsumer<string, Person> consumer = new ConsumerBuilder<string, Person>(consumerConfig)
        .SetValueDeserializer(new CustomKafkaSerializer<Person>())
        .Build();

    consumer.Subscribe(new[] { "person_event" });

    var lastProcessedIsSuccessfully = true;
    
    while (!ct.IsCancellationRequested)
    {
        var consumerRecords = new List<ConsumeResult<string, Person>>();

        if (lastProcessedIsSuccessfully)
        { 
            consumerRecords =  GetConsumeResults(consumer, batchSize);
        }

        lastProcessedIsSuccessfully = TryReadConsumerResults(consumer, consumerRecords, del);
    }
}

static List<ConsumeResult<string, Person>> GetConsumeResults(IConsumer<string, Person> consumer, int batchSize)
{
    var consumerRecords = new List<ConsumeResult<string, Person>>();
    
    try
    {
        while (consumerRecords.Count < batchSize)
        {
            consumerRecords.Add(consumer.Consume());
                    
        }
    }
    catch (ConsumeException consumeException)
    {
        if (consumeException.Error.Code != ErrorCode.RequestTimedOut)
        {
            Console.WriteLine("Timeout");
            throw;
        }
    }

    return consumerRecords;
}

static bool TryReadConsumerResults(IConsumer<string, Person> consumer, List<ConsumeResult<string, Person>> consumeResults, Del del)
{
    try
    {
        if (consumeResults.Count > 0)
        {
            del(consumeResults);
            foreach (var msg in consumeResults)
            {
                consumer.Commit(msg);
            }
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"processing error");

        return false;
    }

    return true;
}

void DelProcessMessages(List<ConsumeResult<string, Person>> consumeResults)
{
    
}
internal delegate void Del(List<ConsumeResult<string, Person>> consumeResults);

