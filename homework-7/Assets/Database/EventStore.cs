using Dapper;
using NanoPaymentSystem.Application.EventStore;
using NanoPaymentSystem.Domain;
using NanoPaymentSystem.Domain.LifecycleData;
using NanoPaymentSystem.Domain.Lifecycles;
using Newtonsoft.Json;
using Npgsql;

namespace NanoPaymentSystem.Database;

internal sealed class EventStore : IEventStore
{
    private readonly string _connectionString;

    private const string InsertCommand =
        @"insert into payment_lifecycle(id, entity_id, version, type, data) VALUES(DEFAULT, @paymentId, @version, @iterationType, @data)";

    private const string SelectCommand =
        @"select id, entity_id as paymentId, version, type as iterationType, data from payment_lifecycle where entity_id = @paymentId";

    public EventStore(string connectionString)
        => _connectionString = connectionString;

    /// <inheritdoc />
    public async Task Save(IEnumerable<PaymentLifecycle> paymentLifecycle, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        foreach (var lifecycle in paymentLifecycle)
        {
            await connection.ExecuteAsync(InsertCommand, new
            {
                paymentId = lifecycle.EntityId,
                version = lifecycle.Version,
                iterationType = lifecycle.LifecycleType,
                data = JsonConvert.SerializeObject(lifecycle.Data),
            });
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<PaymentLifecycle>> LoadEvents(Guid paymentId, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        var iterations = await connection.QueryAsync<LifecycleDto>(SelectCommand, new { paymentId, });

        var result = new List<PaymentLifecycle>();

        foreach (var dto in iterations)
        {
            switch ((PaymentStatus)dto.IterationType)
            {
                case PaymentStatus.New:
                    result.Add(new PaymentCreatedIteration(dto.Id, paymentId, dto.Version, (PaymentStatus)dto.IterationType,
                        JsonConvert.DeserializeObject<PaymentCreatedData>(dto.Data)!));
                    break;
                case PaymentStatus.Processing:
                    result.Add(new PaymentProcessingStartedIteration(dto.Id, paymentId, dto.Version, (PaymentStatus)dto.IterationType,
                        JsonConvert.DeserializeObject<PaymentProcessingStartedData>(dto.Data)!));
                    break;
                case PaymentStatus.Authorized:
                    result.Add(new PaymentAuthorizedIteration(dto.Id, paymentId, dto.Version, (PaymentStatus)dto.IterationType,
                        JsonConvert.DeserializeObject<PaymentAuthorizedData>(dto.Data)!));
                    break;
                case PaymentStatus.Rejected:
                    result.Add(new PaymentRejectedIteration(dto.Id, paymentId, dto.Version, (PaymentStatus)dto.IterationType,
                        JsonConvert.DeserializeObject<PaymentRejectedData>(dto.Data)!));
                    break;
                case PaymentStatus.Cancelled:
                    result.Add(new PaymentCanceledIteration(dto.Id, paymentId, dto.Version, (PaymentStatus)dto.IterationType,
                        JsonConvert.DeserializeObject<PaymentCanceledData>(dto.Data)!));
                    break;
                case PaymentStatus.CancelRequested:
                    result.Add(new PaymentRequestCancelIteration(dto.Id, paymentId, dto.Version, (PaymentStatus)dto.IterationType,
                        JsonConvert.DeserializeObject<PaymentRequestCancelData>(dto.Data)!));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return result;
    }
}
