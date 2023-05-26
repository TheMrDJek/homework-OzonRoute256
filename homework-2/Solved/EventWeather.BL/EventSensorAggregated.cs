using System;

namespace EventWeather.BL;

public class EventSensorAggregated
{
    public int Id { get; set; }

    public double TemperatureAvg { get; set; }

    public double HumidityAvg { get; set; }

    public int PpmMax { get; set; }
    
    public int PpmMin { get; set; }
    
    public int SpanTime { get; set; } //Интервал для аггерегирование данных

    public DateTime CreatedAt { get; set; }
}