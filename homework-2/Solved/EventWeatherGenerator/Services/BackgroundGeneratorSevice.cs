using System;
using System.Threading;
using System.Threading.Tasks;
using EventWeather.BL;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventWeatherGenerator.Services;

public class BackgroundGeneratorSevice : BackgroundService
{
    private readonly ILogger<BackgroundGeneratorSevice> _logger;
    private readonly IEventSetting _setting;
    private readonly IEventSensorStore _eventSensorStore;
    private readonly Random _random;

    public BackgroundGeneratorSevice(ILogger<BackgroundGeneratorSevice> logger, IEventSetting setting,IEventSensorStore eventSensorStore)
    {
        _logger = logger;
        _setting = setting;
        _eventSensorStore = eventSensorStore;
        _random = new Random();
        
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var eventSensorHome = new EventSensor()
            {
                Id = 1,
                Humidity = _random.Next(0, 100),
                Temperature = _random.Next(0, 100),
                Ppm = _random.Next(0, 100),
                CreatedAt = Timestamp.FromDateTime(DateTime.UtcNow)
            };
            var eventSensorStreet = new EventSensor()
            {
                Id = 2,
                Humidity = _random.Next(0, 100),
                Temperature = _random.Next(0, 100),
                Ppm = _random.Next(0, 100),
                CreatedAt = Timestamp.FromDateTime(DateTime.UtcNow)
            };
            
            _eventSensorStore.Update(1,eventSensorHome);
            _eventSensorStore.Update(2,eventSensorStreet);
            await Task.Delay(_setting.GetSpanGenerationEvent(), stoppingToken);
        }
    }
}