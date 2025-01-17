using EventEdsServer.Models;
using EventEdsServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventEdsServer.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class StedsEventController : ControllerBase
{
    private readonly ILogger<StedsEventController> _logger;
    private readonly IStedsEventService _stedsEventService;

    public StedsEventController(ILogger<StedsEventController> logger, IStedsEventService stedsEventService)
    {
        _logger = logger;
        _stedsEventService = stedsEventService;
    }

    [HttpGet]
    public async Task<IActionResult> GetStedsEvents(int page = 1, int pageSize = 10)
    {
        _logger.LogInformation("Get StedsEvents");
        var events = await _stedsEventService.GetStedsEvents(page, pageSize);
        return Ok(events);
    }

    [HttpGet("{eventId}")]
    public async Task<IActionResult> GetStedsEvent(Guid eventId)
    {
        _logger.LogInformation($"Get StedsEvent {eventId}");
        
        var stedsEvent = await _stedsEventService.GetStedsEventById(eventId);
        return Ok(stedsEvent);
    }

    [HttpPost]
    public async Task<IActionResult> AddStedsEvent(StedsEventDto stedsEvent)
    {
        _logger.LogInformation($"Add StedsEvent {stedsEvent}");
        
        var createdEvent = await _stedsEventService.CreateStedsEvent(stedsEvent);

        if (createdEvent == null)
        {
            return BadRequest("StedsEvent not created");
        }
        
        return Ok(createdEvent);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStedsEvent(Guid id, StedsEvent stedsEvent)
    {
        _logger.LogInformation($"Update StedsEvent {stedsEvent}");
        var result = await _stedsEventService.UpdateStedsEvent(id, stedsEvent);

        if (result == null)
        {
            return BadRequest("StedsEvent not updated");
        }
        
        return Ok(result);
    }

    [HttpDelete("{eventId}")]
    public async Task<IActionResult> DeleteStedsEvent(Guid eventId)
    {
        _logger.LogInformation($"Delete StedsEvent {eventId}");
        
        var res = await _stedsEventService.DeleteStedsEvent(eventId);

        if (res)
        {
            return NoContent();
        }

        return NotFound();
    }
}