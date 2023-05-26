using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventWeather.BL;
using EventWeatherGenerator;
using Microsoft.AspNetCore.Mvc;
using EventSensor = EventWeather.BL.EventSensor;

namespace EventWeatherClient.Controllers;

[Route("events")]
public class EventsController : Controller
{
    private readonly Generator.GeneratorClient _generatorClient;
    private readonly IEventSetting _setting;
    private readonly IEventSensorStorage _storage;

    public EventsController(Generator.GeneratorClient generatorClient, IEventSetting setting, IEventSensorStorage storage)
    {
        _generatorClient = generatorClient;
        _setting = setting;
        _storage = storage;

    }

    #region Subscribe

    [HttpGet("subscribe")]
    public async Task<ActionResult<List<SubscribeResponse>>> Subscribe([FromQuery(Name = "id")] List<int> ids)
    {
        var list = new List<SubscribeResponse>();
        
        foreach (var id in ids)
        {
            list.Add(_generatorClient.Subscribe(new SubscribeRequest {Id = {id}}));
        }

        return Ok(list);
    }

    #endregion

    #region UnSubscribe

    [HttpGet("unsubscribe")]
    public async Task<ActionResult<List<UnSubscribeResponse>>> UnSubscribe([FromQuery(Name = "id")] List<int> ids)
    {
        var list = new List<UnSubscribeResponse>();
        
        foreach (var id in ids)
        {
            list.Add(_generatorClient.UnSubscribe(new UnSubscribeRequest {Id = {id}}));
        }

        return Ok(list);
    }
    
    #endregion
    
    #region UpdateDaley //Интервал генерации событий

    [HttpGet("updateDaley")]//
    public async Task<ActionResult<string>> UpdateEventDaley([FromQuery(Name = "deley")] int daley)
    {
        _setting.SetSpanGenerationEvent(daley);
        return Ok("Обновлен на " + daley);
    }

    #endregion
    
    #region UpdateSpanTime //Интервал аггрегации

    [HttpGet("updateSpanTime")]//
    public async Task<ActionResult<string>> UpdateSpanTime([FromQuery(Name = "deley")] int daley)
    {
        _setting.SetSpanTime(daley);
        return Ok("Интервал аггрегации изменен: " + daley);
    }

    #endregion
    
    #region GetEventsAgg //Информация аггрегированных событий

    [HttpGet("getEventsAgg")]//
    public async Task<ActionResult<List<EventSensorAggregated>>> GetEventsAgg()
    {
        var result = _storage.GetLastEventsAggregated().ToList();
        return Ok(result);
    }

    #endregion
    
    #region GetSectionEventsAgg //Информация аггрегированных событий в разрезе формат даты: YYYY-MM-DDTHH:MM:SS

    [HttpGet($"getSectionEventsAgg")]//
    public async Task<ActionResult<List<EventSensorAggregated>>> GetSectionEventsAgg([FromQuery] DateTime refTime, int periodMinute)
    {
        
        var result = _storage.GetSectionEventsAggregated(refTime,periodMinute).ToList();
        return Ok(result);
    }

    #endregion
    
    #region GetAllEvents //для диагностики

    [HttpGet($"getAllEvents")]//
    public async Task<ActionResult<List<EventSensor>>> GetAllEvents(DateTime refTime, int periodMinute)
    {
        var result = _storage.GetAllEvents().ToList();
        return Ok(result);
    }

    #endregion
}