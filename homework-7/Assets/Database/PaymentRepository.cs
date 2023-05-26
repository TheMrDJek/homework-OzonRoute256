using Dapper;
using NanoPaymentSystem.Application.Repository;
using NanoPaymentSystem.Domain;
using NanoPaymentSystem.Domain.Exceptions;
using NanoPaymentSystem.Domain.LifecycleData;
using Npgsql;

namespace NanoPaymentSystem.Database;

internal sealed class PaymentRepository : IPaymentRepository
{
    private readonly string _connectionString;

    private const string InsertCommand =
        @"insert into payment (id, order_id, client_id, amount, currency_code, status, message, provider_payment_id, card_first_6, card_last_4, card_expiration_year, card_expiration_month)
          values (@id, @order_id, @client_id, @amount, @currency_code, @status, @message, @provider_payment_id, @card_first_6, @card_last_4, @card_expiration_year, @card_expiration_month);";

    private const string GetPaymentQuery = @"
select 
    Id, Amount, currency_code as CurrencyCode, order_id as OrderId, client_id as ClientId, Message, provider_payment_id as ProviderPaymentId, Status, card_first_6 as BankCardFirst6, card_last_4 as BankCardLast4, card_expiration_month as BankCardExpirationMonth, card_expiration_year as BankCardExpirationYear
from payment
where id = @id;";

    private const string UpdatePaymentCommand = @"
update payment set 
    status = @status, message = @message, provider_payment_id = @provider_payment_id,
    card_first_6 = @card_first_6, card_last_4 = @card_last_4,
    card_expiration_year = @card_expiration_year, card_expiration_month = @card_expiration_month
where id = @id;
";

    private const string FindByClientIdQuery = @"
select 
    Id, Amount, currency_code as CurrencyCode, order_id as OrderId, client_id as ClientId, Message, provider_payment_id as ProviderPaymentId, Status, card_first_6 as BankCardFirst6, card_last_4 as BankCardLast4, card_expiration_month as BankCardExpirationMonth, card_expiration_year as BankCardExpirationYear
from payment
where client_id = @clientId;
";


    private const string FindByOrderIdQuery = @"
select 
    Id, Amount, currency_code as CurrencyCode, order_id as OrderId, client_id as ClientId, Message, provider_payment_id as ProviderPaymentId, Status, card_first_6 as BankCardFirst6, card_last_4 as BankCardLast4, card_expiration_month as BankCardExpirationMonth, card_expiration_year as BankCardExpirationYear
from payment
where order_id = @orderId;
";

    public PaymentRepository(string connectionString)
        => _connectionString = connectionString;

    /// <inheritdoc />
    public async Task SavePayment(Payment payment, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        await connection.ExecuteAsync(InsertCommand, new
        {
            id = payment.Id,
            order_id = payment.OrderId,
            client_id = payment.ClientId,
            amount = payment.Price.Amount,
            currency_code = payment.Price.CurrencyCode,
            status = payment.Status,
            message = payment.Message,
            provider_payment_id = payment.ProviderPaymentId,
            card_first_6 = payment.BankCard?.First6,
            card_last_4 = payment.BankCard?.Last4,
            card_expiration_year = payment.BankCard?.ExpirationYear,
            card_expiration_month = payment.BankCard?.ExpirationMonth,
        });
    }

    /// <inheritdoc />
    public async Task<Payment> GetPaymentById(Guid id, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        var dao = await connection.QueryFirstOrDefaultAsync<PaymentDao>(GetPaymentQuery, new { id, });

        if (dao == null)
        {
            throw new PaymentNotFoundException();
        }

        return ToDomainObject(dao);
    }

    /// <inheritdoc />
    public async Task<List<Payment>> FindPaymentsByClientId(string clientId, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        var result = await connection.QueryAsync<PaymentDao>(FindByClientIdQuery, new { clientId });

        return result.Select(ToDomainObject).ToList();
    }

    /// <inheritdoc />
    public async Task<List<Payment>> FindPaymentsByOrderId(string orderId, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        var result = await connection.QueryAsync<PaymentDao>(FindByOrderIdQuery, new { orderId });

        return result.Select(ToDomainObject).ToList();
    }

    /// <inheritdoc />
    public async Task UpdatePayment(Payment payment, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        await connection.ExecuteAsync(UpdatePaymentCommand, new
        {
            id = payment.Id,
            status = payment.Status,
            message = payment.Message,
            provider_payment_id = payment.ProviderPaymentId,
            card_first_6 = payment.BankCard?.First6,
            card_last_4 = payment.BankCard?.Last4,
            card_expiration_year = payment.BankCard?.ExpirationYear,
            card_expiration_month = payment.BankCard?.ExpirationMonth,
        });
    }

    private static Payment ToDomainObject(PaymentDao dao)
        => new (new PaymentCreatedData(dao.ClientId, dao.OrderId, new Price(dao.Amount, dao.CurrencyCode)));
}
