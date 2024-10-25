using EventEdsServer.Models;
using MongoDB.Bson;

namespace EventEdsServer.Services;

public interface IStedsEventService
{
    public Task<List<StedsEvent>> GetStedsEvents();
    public Task<StedsEvent?> GetStedsEventById(ObjectId id);
    public Task<StedsEvent?> CreateStedsEvent(StedsEventDto stedsEvent);
    public Task<StedsEvent?> UpdateStedsEvent(StedsEventDto stedsEvent);
    public Task DeleteStedsEvent(ObjectId id);
}