using Confluent.Kafka;
using NanoPaymentSystem.Application.NotificationHandler;

namespace NanoPaymentSystem.HostedService;

public class BackgroundWorker : BackgroundService
{
    private readonly ITransactionOutbox _transactionOutbox;
    private readonly IMessageBroker _messageBroker;
    private readonly int _maxRetryCount;

    public BackgroundWorker(ITransactionOutbox transactionOutbox, IMessageBroker messageBroker)
    {
        _transactionOutbox = transactionOutbox;
        _messageBroker = messageBroker;
        _maxRetryCount = 5;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var payments = _transactionOutbox.FindByPayments(stoppingToken);
            var retryTransaction = 0;
            foreach (var payment in payments.Result)
            {
                if (retryTransaction > _maxRetryCount)
                {
                    try
                    {
                        await _messageBroker.Publish(payment, stoppingToken);
                        await _transactionOutbox.Delete(payment.PaymentId, stoppingToken);
                    }
                    catch (Exception e)
                    {
                        retryTransaction++;
                    }
                }
                else
                {
                    throw new Exception("Привышен лимит");
                }
            }
            await Task.Delay(5000, stoppingToken);
        }
    }
}