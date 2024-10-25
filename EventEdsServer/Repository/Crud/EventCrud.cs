using EventEdsServer.Models;
using Microsoft.EntityFrameworkCore;

namespace EventEdsServer.Repository.Crud;

public class EventCrud : IEventCrud
{
    private readonly MongoContext _context;

    public EventCrud(MongoContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Event>> GetAllEventsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Event?> GetEventByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Event> CreateEventAsync(Event newEvent)
    {
        throw new NotImplementedException();
    }

    public async Task<Event> UpdateEventAsync(Event updatedEvent)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteEventAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}