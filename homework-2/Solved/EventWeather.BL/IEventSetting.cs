namespace EventWeather.BL;

public interface IEventSetting
{
    void SetSpanGenerationEvent(int deley);

    int GetSpanGenerationEvent();

    void SetSpanTime(int period);

    int GetSpanTime();
}