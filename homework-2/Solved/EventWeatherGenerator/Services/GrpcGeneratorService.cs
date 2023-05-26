using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventWeather.BL;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EventWeatherGenerator.Services;

public class GrpcGeneratorService : Generator.GeneratorBase
{
    private readonly IEventSensorStore _eventSensorStore;
    private readonly ILogger<GrpcGeneratorService> _logger;
    private readonly IEventSetting _setting;

    public GrpcGeneratorService(IEventSensorStore eventSensorStore, IEventSetting setting, ILogger<GrpcGeneratorService> logger)
    {
        _eventSensorStore = eventSensorStore;
        _logger = logger;
        _setting = setting;

    }

    public override async Task EventsSubscriptionDuplex(
        IAsyncStreamReader<EventStreamDuplexRequest> requestStream, 
        IServerStreamWriter<EventSensorResponse> responseStream, 
        ServerCallContext context)
    {
        try
        {
            while (!context.CancellationToken.IsCancellationRequested)
            {
                try { await WriteUpdateAsync(responseStream, _eventSensorStore); } catch {/* Ignored */}

                try { await HandleActions(requestStream, _eventSensorStore, context.CancellationToken); } catch {/* Ignored */}
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("A operation was canceled");
        }
        
        
    }

    public async Task HandleActions(
        IAsyncStreamReader<EventStreamDuplexRequest> requestStream, 
        IEventSensorStore eventSensorStore, 
        CancellationToken token)
    {
        while (await requestStream.MoveNext(token))
        {
            var stream = requestStream.Current;
            _setting.SetSpanGenerationEvent(stream.SettingDeley);
        }
    }
    
    private async Task WriteUpdateAsync(
        IAsyncStreamWriter<EventSensorResponse> responseStream, 
        IEventSensorStore eventSensorStore)
    {
        try
        {
            var result = new EventSensorResponse();

            foreach (var eventSensor in eventSensorStore.GetSubscribe())
            {
                result.EventSensor.Add(eventSensor);
            }

            await Task.Delay(_setting.GetSpanGenerationEvent());
            await responseStream.WriteAsync(result);

        }
        catch (Exception e)
        {
            _logger.LogError($"Failed to write message: {e.Message}");
        }
    }

    public override Task<SubscribeResponse> Subscribe(SubscribeRequest request, ServerCallContext context)
    {
        var result = new SubscribeResponse();

        foreach (var id in request.Id)
        {
            _eventSensorStore.Subscribe(Convert.ToInt32(id));
            result.Id.Add(id);
        }
        
        return Task.FromResult(result);
    }

    public override Task<UnSubscribeResponse> UnSubscribe(UnSubscribeRequest request, ServerCallContext context)
    {
        var result = new UnSubscribeResponse();

        foreach (var id in request.Id)
        {
            _eventSensorStore.UnSubscribe(id);
            result.Id.Add(id);
        }
        
        return Task.FromResult(result);
    }
}