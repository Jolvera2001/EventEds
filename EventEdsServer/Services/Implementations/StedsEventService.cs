using EventEdsServer.Models;
using EventEdsServer.Repository;
using MongoDB.Bson;

namespace EventEdsServer.Services.Implementations;

public class StedsEventService : IStedsEventService
{
    private readonly IStedsEventCrud _eventCrud;
    private readonly ILogger<StedsEventService> _logger;

    public StedsEventService(IStedsEventCrud eventCrud, ILogger<StedsEventService> logger)
    {
        _eventCrud = eventCrud;
        _logger = logger;
    }
    
    public async Task<List<StedsEvent>> GetStedsEvents()
    {
        try
        {
            _logger.LogInformation("Retrieving events");
            var result = await _eventCrud.GetAllEventsAsync();
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to fetch all events: " + ex.Message);
            throw;
        }
    }

    public async Task<StedsEvent?> GetStedsEventById(string id)
    {
        try
        {
            _logger.LogInformation("Retrieving event by id");
            var stedsEvent = await _eventCrud.GetEventByIdAsync(id);
            if (stedsEvent == null)
            {
                return null;
            }

            return stedsEvent;
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to get event by id: " + ex.Message);
            throw;
        }
    }

    public async Task<StedsEvent?> CreateStedsEvent(StedsEventDto stedsEvent)
    {
        try
        {
            _logger.LogInformation("Creating event");
            StedsEvent newEvent = new StedsEvent
            {
                Id = ObjectId.GenerateNewId().ToString(),
                CreatedOn = DateTime.Now,
                Title = stedsEvent.Title,
                Description = stedsEvent.Description,
                StartDate = stedsEvent.StartDate,
                EndDate = stedsEvent.EndDate,
                Location = stedsEvent.ToLocation()
            };
            var result = await _eventCrud.CreateEventAsync(newEvent);

            if (result == null)
            {
                return null;
            }

            return result;
        }
        catch (Exception ex)
        {
           _logger.LogError("Failed to create a new event: " + ex.Message);
           throw;
        }
    }

    public async Task<StedsEvent?> UpdateStedsEvent(StedsEvent stedsEvent)
    {
        try
        {
            _logger.LogInformation("Updating event");
            var result = await _eventCrud.UpdateEventAsync(stedsEvent);
            if (result == null)
            {
                return null;
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to update event: " + ex.Message);
            throw;
        }
    }

    public async Task<bool> DeleteStedsEvent(string id)
    {
        try
        {
            _logger.LogInformation("Deleting event");
            var result = await _eventCrud.DeleteEventAsync(id);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to delete event: " + ex.Message);
            throw;
        }
    }
}