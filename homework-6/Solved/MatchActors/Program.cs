using MatchActors.Infrastructure.DataBase;
using MatchActors.Infrastructure.OpenExchangeActorsClient;
using MediatR;

namespace MatchActors
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddMediatR(typeof(Program));
            builder.Services.AddControllers();
            
            AddOptions(builder.Services,builder.Configuration);
            
            builder.Services.AddSingleton<ICommandBuilder, CommandBuilder>();
            builder.Services.AddSingleton<IExchangeActorsRepository, ExchangeActorsRepository>();
            builder.Services.AddHttpClient<IOpenExchangeActorClient, OpenExchangeActorClient>();

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void AddOptions(IServiceCollection services, IConfiguration configuration)
        {
            var openExchangeRatesSettings = configuration.GetSection(nameof(OpenExchangeActorsOptions)).Get<OpenExchangeActorsOptions>();
            var dbRepositorySettings = configuration.GetSection(nameof(ExchangeActorConnectionString)).Get<ExchangeActorConnectionString>();

            services.AddSingleton(openExchangeRatesSettings);
            services.AddSingleton(dbRepositorySettings);
        }
    }
}