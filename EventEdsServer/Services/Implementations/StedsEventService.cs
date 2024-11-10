using EventEdsServer.Models;
using Microsoft.EntityFrameworkCore;

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
    
    public async Task<List<StedsEvent>> GetStedsEvents(int page, int pageSize)
    {
        try
        {
            _logger.LogInformation("Retrieving events");
            return await _context.Events
                .Include(e => e.Location)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

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
            return await _context.Events
                .Include(e => e.Location)
                .SingleAsync(e => e.Id == id);

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

            var location = new Location
            {
                Id = Guid.NewGuid(),
                Latitude = stedsEvent.Location.Latitude,
                Longitude = stedsEvent.Location.Longitude,
                Name = stedsEvent.Location.Name,
            };

            var newEvent = new StedsEvent
            {
                Id = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                Title = stedsEvent.Title,
                Description = stedsEvent.Description,
                StartDate = stedsEvent.StartDate,
                EndDate = stedsEvent.EndDate,
                Location = location,
                LocationId = location.Id
            };
            
            // implicitly adds both the event and location if it's new
            await _context.Events.AddAsync(newEvent);
            await _context.SaveChangesAsync();
            return newEvent;

        }
        catch (Exception ex)
        {
           _logger.LogError("Failed to create a new event: " + ex.Message);
           throw;
        }
    }

    public async Task<StedsEvent?> UpdateStedsEvent(Guid id, StedsEvent stedsEvent)
    {
        try
        {
            _logger.LogInformation("Updating event");
            var existingEvent = await _context.Events
                .Include(e => e.Location)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (existingEvent == null)
            {
                _logger.LogWarning($"Event {id} is not found");
                return null;
            }
            
            existingEvent.Title = stedsEvent.Title;
            existingEvent.Description = stedsEvent.Description;
            existingEvent.StartDate = stedsEvent.StartDate;
            existingEvent.EndDate = stedsEvent.EndDate;
            
            existingEvent.Location.Name = stedsEvent.Location.Name;
            existingEvent.Location.Latitude = stedsEvent.Location.Latitude;
            existingEvent.Location.Longitude = stedsEvent.Location.Longitude;
            
            await _context.SaveChangesAsync();
            
            return existingEvent;
            
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
            var stedsEvent = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);

            if (stedsEvent == null)
            {
                return false;
            }
            
            _context.Events.Remove(stedsEvent);
            await _context.SaveChangesAsync();
            
            return true;

        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to delete event: " + ex.Message);
            throw;
        }
    }
}