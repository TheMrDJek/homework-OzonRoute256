using System;

namespace EventWeather.BL;

public class EventSensor
{
    public int Id { get; set; }

    public double Temperature { get; set; }

    public double Humidity { get; set; }

    public int Ppm { get; set; }

    public DateTime CreatedAt { get; set; }
}