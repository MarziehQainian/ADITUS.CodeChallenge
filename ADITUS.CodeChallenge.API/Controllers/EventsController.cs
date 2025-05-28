using ADITUS.CodeChallenge.API.Domain;
using ADITUS.CodeChallenge.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ADITUS.CodeChallenge.API.Controllers
{
  [ApiController]
  [Route("events")]
  public class EventsController : ControllerBase
  {
    private readonly IEventService _eventService;
    private readonly IEventStatisticsService _eventStatisticsService;


    public EventsController(
        IEventService eventService,
        IEventStatisticsService eventStatisticsService,
        IHardwareReservationService hardwareReservationService)
    {
      _eventService = eventService;
      _eventStatisticsService = eventStatisticsService;
    
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetEvents()
    {
      var events = await _eventService.GetEvents();
      return Ok(events);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetEvent(Guid id)
    {
      var @event = await _eventService.GetEvent(id);
      if (@event == null)
      {
        return NotFound();
      }

      // Fetch statistics based on event type
      if (@event.Type == Domain.EventType.Online)
      {
        @event.Statistics = await _eventStatisticsService.GetOnlineStatisticsAsync(@event.Id);
      }
      else if (@event.Type == Domain.EventType.OnSite)
      {
        @event.Statistics = await _eventStatisticsService.GetOnsiteStatisticsAsync(@event.Id);
      }
      else if (@event.Type == Domain.EventType.Hybrid)
      {
        // For Hybrid, merge both statistics (simple merge for demo)
        var onlineStats = await _eventStatisticsService.GetOnlineStatisticsAsync(@event.Id);
        var onsiteStats = await _eventStatisticsService.GetOnsiteStatisticsAsync(@event.Id);
        @event.Statistics = new Domain.EventStatistics
        {
          Participants = (onlineStats?.Participants ?? 0) + (onsiteStats?.Participants ?? 0),
          CheckIns = (onlineStats?.CheckIns ?? 0) + (onsiteStats?.CheckIns ?? 0),
          DevicesUsed = (onlineStats?.DevicesUsed ?? 0) + (onsiteStats?.DevicesUsed ?? 0)
        };
      }

      return Ok(@event);
    }
       
  }
}