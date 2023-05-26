using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EventWeatherGenerator.Controllers;

[Route("events")]
public class EventsSensorController : Controller
{
    private readonly ILogger<EventsSensorController> _logger;
    private readonly IEventSensorStore _eventSensorStore;

    public EventsSensorController(ILogger<EventsSensorController> logger, IEventSensorStore eventSensorStore)
    {
        _logger = logger;
        _eventSensorStore = eventSensorStore;
    }
    
    [HttpGet] //url/events
    public async Task<ActionResult<List<EventSensor>>> GetAllEvents()
    {
        var eventsSensor = _eventSensorStore.GetAllEvent();

        return Ok(eventsSensor);
    }
}