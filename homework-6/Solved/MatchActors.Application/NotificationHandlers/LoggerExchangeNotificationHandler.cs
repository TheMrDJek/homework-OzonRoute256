using MatchActors.Application.ExchangeActorsMatches;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MatchActors.Application.NotificationHandlers;

internal sealed class LoggerExchangeNotificationHandler : INotificationHandler<ExchangeNotification>
{
    private readonly ILogger<LoggerExchangeNotificationHandler> _logger;

    public LoggerExchangeNotificationHandler(ILogger<LoggerExchangeNotificationHandler> logger)
        => _logger = logger;

    /// <inheritdoc />
    public Task Handle(ExchangeNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{Notification}", notification);
        return Task.CompletedTask;
    }
}