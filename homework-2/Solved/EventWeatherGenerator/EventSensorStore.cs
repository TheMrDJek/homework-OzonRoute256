using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace EventWeatherGenerator;

class EventSensorStore : IEventSensorStore
{
    private readonly ConcurrentDictionary<int, EventSensor> _eventSensors = new();
    private readonly ConcurrentDictionary<int, int> _subEventIds = new();

    public EventSensorStore()
    {
        EventSensor eventSensorHome = new();
        EventSensor eventSensorStreet = new();
        _eventSensors.AddOrUpdate(1, _ => eventSensorHome, (_, _) => eventSensorHome);
        _eventSensors.AddOrUpdate(2, _ => eventSensorStreet, (_, _) => eventSensorStreet);
    }

    public void Update(int id, EventSensor eventSensor)
    {
        _eventSensors[id].Id = eventSensor.Id;
        _eventSensors[id].Humidity = eventSensor.Humidity;
        _eventSensors[id].Ppm = eventSensor.Ppm;
        _eventSensors[id].Temperature = eventSensor.Temperature;
        _eventSensors[id].CreatedAt = eventSensor.CreatedAt;
    }

    public void Subscribe(int id)
    {
        if (_eventSensors.ContainsKey(id))
        {
            _subEventIds.AddOrUpdate(id, _ => id, (_, _) => id);
        }
        
    }

    public void UnSubscribe(int id)
    {
        if (_subEventIds.ContainsKey(id))
        {
            _subEventIds.Remove(id, out _);
        }
    }

    public List<EventSensor> GetSubscribe()
    {
        var result = new List<EventSensor>();

        foreach (var id in _subEventIds)
        {
            result.Add(_eventSensors[id.Key]);
        }

        return result;
    }

    public List<EventSensor> GetAllEvent()
    {
        return _eventSensors.Select(x => x.Value).ToList();
    }
}