using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace EventWeather.BL;

public class EventSensorStorage : IEventSensorStorage
{
    private readonly ConcurrentBag<EventSensor> _eventSensorStorage = new();

    private readonly ConcurrentBag<EventSensorAggregated> _eventSensorAggregatedStorage = new();

    public void AddEvent(EventSensor eventSensor)
    {
        _eventSensorStorage.Add(eventSensor);
    }

    public void AddEventAggregated(EventSensor[] eventSensors, int spanTime)
    {
        foreach (var eventSensor in eventSensors.DistinctBy(x => x.Id))
        {
            var newEvents = eventSensors
                .Where(x => x.Id == eventSensor.Id);
            var eventSensorAgg = new EventSensorAggregated
            {
                CreatedAt = DateTime.Now,
                Id = eventSensor.Id,
                HumidityAvg = newEvents.Average(x => x.Humidity),
                PpmMax = newEvents.Max(x => x.Ppm),
                PpmMin = newEvents.Min(x => x.Ppm),
                TemperatureAvg = newEvents.Average(x => x.Temperature),
                SpanTime = spanTime
            };
            
            _eventSensorAggregatedStorage.Add(eventSensorAgg);
        }
    }

    public EventSensor[] GetAllEvents()
    {
        return _eventSensorStorage.ToArray();
    }

    public EventSensor[] GetAllEventsToSensorId(int id)
    {
        return _eventSensorStorage.Where(x => x.Id == id).ToArray();
    }

    public EventSensorAggregated[] GetSectionEventsAggregated(DateTime referenceTime, int periodMinute)
    {
        var result = new List<EventSensorAggregated>();
        
        foreach (var eventSensor in _eventSensorAggregatedStorage.DistinctBy(x => x.Id))
        {
            var newEventsPeriod = _eventSensorAggregatedStorage
                .Where(x => x.Id == eventSensor.Id)
                .Where(x => x.CreatedAt >= referenceTime && x.CreatedAt <= referenceTime.AddMinutes(periodMinute));
            var eventSensorAggPeriod = new EventSensorAggregated
            {
                CreatedAt = DateTime.Now,
                Id = eventSensor.Id,
                HumidityAvg = newEventsPeriod.Average(x => x.HumidityAvg),
                PpmMax = newEventsPeriod.Max(x => x.PpmMax),
                PpmMin = newEventsPeriod.Min(x => x.PpmMin),
                TemperatureAvg = newEventsPeriod.Average(x => x.TemperatureAvg),
                SpanTime = periodMinute * 60000
            };
            result.Add(eventSensorAggPeriod);
        }

        return result.ToArray();
    }

    public EventSensorAggregated[] GetLastEventsAggregated()
    {
        var result = new EventSensorAggregated[2];
        result[0] = _eventSensorAggregatedStorage
            .Where(x => x.Id == 1)
            .OrderByDescending(x => x.CreatedAt)
            .First();
        result[1] = _eventSensorAggregatedStorage
            .Where(x => x.Id == 2)
            .OrderByDescending(x => x.CreatedAt)
            .First();

        return result;
    }
}