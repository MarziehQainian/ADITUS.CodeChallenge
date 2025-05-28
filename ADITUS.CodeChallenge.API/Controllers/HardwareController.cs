using ADITUS.CodeChallenge.API.Domain;
using ADITUS.CodeChallenge.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ADITUS.CodeChallenge.API.Controllers
{
  [ApiController]
  [Route("hardwarereservation")]
  public class HardwareReservationController : ControllerBase
  {
    private readonly IHardwareReservationService _hardwareReservationService;
    private readonly IEventService _eventService;

    public HardwareReservationController(
       IEventService eventService,
      IHardwareReservationService hardwareReservationService)
    {
      _eventService = eventService;
      _hardwareReservationService = hardwareReservationService;
    }
    [HttpPost]
    public async Task<IActionResult> ReserveHardware(HardwareReservationRequest request)
    {
      var @event = await _eventService.GetEvent(request.EventId);
      if (@event == null)
        return NotFound();

      var reservation = await _hardwareReservationService.ReserveHardwareAsync(request, @event.StartDate);
      if (reservation == null)
        return BadRequest("Reservation not possible (too late, already reserved, or insufficient stock).");

      return Ok(reservation);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetHardwareReservation(Guid id)
    {
      var reservation = await _hardwareReservationService.GetReservationAsync(id);
      if (reservation == null)
        return NotFound();

      return Ok(reservation);
    }
  }
}
