using EventEdsServer.Models;

namespace EventEdsServer.Repository;

public interface IEventCrud
{
    public Task<IEnumerable<Event>> GetAllEventsAsync();
    public Task<Event?> GetEventByIdAsync(Guid id);
    public Task<Event> CreateEventAsync(Event newEvent);
    public Task<Event> UpdateEventAsync(Event updatedEvent);
    public Task<bool> DeleteEventAsync(Guid id);
}