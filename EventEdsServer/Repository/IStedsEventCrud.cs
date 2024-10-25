using EventEdsServer.Models;
using MongoDB.Bson;

namespace EventEdsServer.Repository;

public interface IStedsEventCrud
{
    public Task<List<StedsEvent>> GetAllEventsAsync();
    public Task<StedsEvent?> GetEventByIdAsync(ObjectId id);
    public Task<StedsEvent?> CreateEventAsync(StedsEvent newStedsEvent);
    public Task<StedsEvent?> UpdateEventAsync(StedsEvent updatedStedsEvent);
    public Task<bool> DeleteEventAsync(ObjectId id);
}