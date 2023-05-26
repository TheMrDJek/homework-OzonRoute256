using System;

namespace EventWeather.BL;

public interface IEventSensorStorage
{
    void AddEvent(EventSensor eventSensor);

    void AddEventAggregated(EventSensor[] eventSensors, int spanTime);

    EventSensor[] GetAllEvents();

    EventSensor[] GetAllEventsToSensorId(int id);

    EventSensorAggregated[] GetSectionEventsAggregated(DateTime referenceTime, int periodMinute);

    EventSensorAggregated[] GetLastEventsAggregated();
}