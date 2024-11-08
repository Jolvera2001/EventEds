using EventEdsServer.Models;
using EventEdsServer.Repository;
using MongoDB.Bson;

namespace EventEdsServer.Services.Implementations;

public class StedsEventService : IStedsEventService
{
    private readonly MySqlContext _context;
    private readonly ILogger<StedsEventService> _logger;

    public StedsEventService(MySqlContext context, ILogger<StedsEventService> logger)
    {
        _context = context;
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

    public async Task<StedsEvent?> GetStedsEventById(Guid id)
    {
        try
        {
            _logger.LogInformation("Retrieving event by id");
            
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
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to update event: " + ex.Message);
            throw;
        }
    }
    

    public async Task<bool> DeleteStedsEvent(Guid id)
    {
        try
        {
            _logger.LogInformation("Deleting event");
            
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to delete event: " + ex.Message);
            throw;
        }
    }
}