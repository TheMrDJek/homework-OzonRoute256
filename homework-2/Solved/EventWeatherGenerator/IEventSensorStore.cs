using System.Collections.Concurrent;
using System.Collections.Generic;

namespace EventWeatherGenerator;

public interface IEventSensorStore
{

    void Update(int id, EventSensor eventSensor);

    void Subscribe(int id);

    void UnSubscribe(int id);

    List<EventSensor> GetSubscribe();

    List<EventSensor> GetAllEvent();
}