using EventEdsServer.Models;
using MongoDB.Bson;
using MongoDB.Driver.Linq;

namespace EventEdsServer.Repository.Crud;

public class StedsStedsEventCrud : IStedsEventCrud
{
    private readonly MongoContext _context;

    public StedsStedsEventCrud(MongoContext context)
    {
        _context = context;
    }
    
    public async Task<List<StedsEvent>> GetAllEventsAsync()
    {
        return await _context.Events.ToListAsync();
    }

    public async Task<StedsEvent?> GetEventByIdAsync(ObjectId id)
    {
        return await _context.Events.FindAsync(id);
    }

    public async Task<StedsEvent?> CreateEventAsync(StedsEvent newStedsEvent)
    {
        var result = await _context.Events.AddAsync(newStedsEvent);
        return result.Entity;
    }

    public async Task<StedsEvent?> UpdateEventAsync(StedsEvent updatedStedsEvent)
    {
        var stedsEvent = await _context.Events.FirstOrDefaultAsync(x => x.Id == updatedStedsEvent.Id);
        if (stedsEvent == null)
        {
            return null;
        }
        
        stedsEvent.Description = updatedStedsEvent.Description;
        stedsEvent.StartDate = updatedStedsEvent.StartDate;
        stedsEvent.EndDate = updatedStedsEvent.EndDate;
        stedsEvent.Location = updatedStedsEvent.Location;
        
        await _context.SaveChangesAsync();
        return stedsEvent;
    }

    public async Task<bool> DeleteEventAsync(ObjectId id)
    {
        var stedsEvent = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);
        if (stedsEvent == null)
        {
            return false;
        }
        
        _context.Events.Remove(stedsEvent);
        await _context.SaveChangesAsync();
        return true;
    }
}