using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using EventWeather.BL;
using EventWeatherGenerator;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventWeatherClient.HostedService;

public class ClientHostedService : BackgroundService
{
    private readonly ILogger<ClientHostedService> _logger;
    private readonly IEventSetting _setting;
    private readonly IEventSensorStorage _storage;
    private readonly IServiceProvider _provider;

    public ClientHostedService(ILogger<ClientHostedService> logger, IServiceProvider provider, IEventSetting setting, IEventSensorStorage storage)
    {
        _logger = logger;
        _setting = setting;
        _storage = storage;
        _provider = provider;

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var scope = _provider.CreateAsyncScope();
        var client = scope.ServiceProvider.GetRequiredService<Generator.GeneratorClient>();
        var duplexStream = client.EventsSubscriptionDuplex(cancellationToken: stoppingToken);
        bool statusReconnect = false;
        
        while (!stoppingToken.IsCancellationRequested)
        {
            
            while (statusReconnect)
            {
                duplexStream = client.EventsSubscriptionDuplex(cancellationToken: stoppingToken);
                await Task.Delay(10000, stoppingToken);
                statusReconnect = false;
            }

            try { await WriteUpdateAsync(duplexStream.RequestStream, _setting, stoppingToken); }
            catch { statusReconnect = true;}
        
            try { await duplexStream.RequestStream.CompleteAsync(); }
            catch { statusReconnect = true;}
                
            try { await EventsRead(duplexStream.ResponseStream, stoppingToken); }
            catch { statusReconnect = true;}
        }
        
    }

    private async Task EventsRead(IAsyncStreamReader<EventSensorResponse> responseStream, CancellationToken token)
    {
        var eventsSensorTemp = new List<EventWeather.BL.EventSensor>();
        while (await responseStream.MoveNext(token))
        {
            foreach (var eventSensor in responseStream.Current.EventSensor)
            {
                var eventSensorConverted = new EventWeather.BL.EventSensor()
                {
                    Id = eventSensor.Id,
                    Humidity = eventSensor.Humidity,
                    Ppm = eventSensor.Ppm,
                    Temperature = eventSensor.Temperature,
                    CreatedAt = eventSensor.CreatedAt.ToDateTime()
                };
                _storage.AddEvent(eventSensorConverted);

                if (eventsSensorTemp.Count == 0)
                {
                    eventsSensorTemp.Add(eventSensorConverted);
                }
                else if(eventSensorConverted.CreatedAt <= eventsSensorTemp[0].CreatedAt.AddMilliseconds(_setting.GetSpanTime()))
                {
                    eventsSensorTemp.Add(eventSensorConverted);
                }
                else
                {
                    _storage.AddEventAggregated(eventsSensorTemp.ToArray(), _setting.GetSpanTime());
                    eventsSensorTemp.Clear();
                }
            }
        }
        
        if (eventsSensorTemp.Count > 0)
        {
            _storage.AddEventAggregated(eventsSensorTemp.ToArray(), _setting.GetSpanTime());
        }
    }

    private async Task WriteUpdateAsync(IAsyncStreamWriter<EventStreamDuplexRequest> requestStream, IEventSetting setting, CancellationToken token)
    {
        await requestStream.WriteAsync(new EventStreamDuplexRequest {SettingDeley = setting.GetSpanGenerationEvent()});
        await Task.Delay(2000, token);
    }
}