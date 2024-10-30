using EventEdsServer.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EventEdsServer.Repository.Crud;

public class StedsStedsEventCrud(IMongoClient mongoClient) : IStedsEventCrud
{
    private readonly IMongoCollection<StedsEvent> _events = mongoClient.GetDatabase("main").GetCollection<StedsEvent>("Events");

    public async Task<List<StedsEvent>> GetAllEventsAsync()
    {
        return await _events.Find(_ => true).ToListAsync();
    }

    public async Task<StedsEvent?> GetEventByIdAsync(ObjectId id)
    {
        return await _events.Find(e => e.Id == id).FirstOrDefaultAsync();
    }

    public async Task<StedsEvent?> CreateEventAsync(StedsEvent newStedsEvent)
    {
        await _events.InsertOneAsync(newStedsEvent);
        return newStedsEvent;
    }

    public async Task<StedsEvent?> UpdateEventAsync(StedsEvent updatedStedsEvent)
    {
        var result = await _events.ReplaceOneAsync(e => e.Id == updatedStedsEvent.Id, updatedStedsEvent);
        return result.ModifiedCount > 0 ? updatedStedsEvent : null;
    }

    public async Task<bool> DeleteEventAsync(ObjectId id)
    {
        var result = await _events.DeleteOneAsync(e => e.Id == id);
        return result.DeletedCount > 0;
    }
}