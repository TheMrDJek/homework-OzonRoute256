using NanoPaymentSystem.Application;
using NanoPaymentSystem.Database;
using NanoPaymentSystem.HostedService;
using NanoPaymentSystem.MessageBroker;
using NanoPaymentSystem.PaymentProviders;
using NanoPaymentSystem.TransactionOutbox;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFakePaymentProvider();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddMessageBroker();
builder.Services.AddTransactionOutbox();
builder.Services.AddHostedService<BackgroundWorker>();
builder.Services.AddControllers();
builder.Services.AddApplication();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
