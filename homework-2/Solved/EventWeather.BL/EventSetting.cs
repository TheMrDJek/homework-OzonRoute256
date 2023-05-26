using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace EventWeather.BL;

public class EventSetting : IEventSetting
{
    private ConcurrentDictionary<int, int> _setting = new();

    public EventSetting()
    {
        _setting.AddOrUpdate(1,_ => 2000, (_,_) => 2000);
        _setting.AddOrUpdate(2,_ => 60000, (_,_) => 60000);
    }

    public void SetSpanGenerationEvent(int deley)
    {
        _setting.AddOrUpdate(1,_ => deley, (_,_) => deley);
    }

    public int GetSpanGenerationEvent()
    {
        return _setting[1];
    }
    
    public void SetSpanTime(int periodMinute)
    {
        _setting.AddOrUpdate(1,_ => periodMinute * 60000, (_,_) => periodMinute * 60000);
    }

    public int GetSpanTime()
    {
        return _setting[2];
    }
}