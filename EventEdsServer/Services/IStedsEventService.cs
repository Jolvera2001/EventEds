using EventEdsServer.Models;
using MongoDB.Bson;

namespace EventEdsServer.Services;

public interface IStedsEventService
{
    public Task<List<StedsEvent>> GetStedsEvents();
    public Task<StedsEvent?> GetStedsEventById(string id);
    public Task<StedsEvent?> CreateStedsEvent(StedsEventDto stedsEvent);
    public Task<StedsEvent?> UpdateStedsEvent(StedsEvent stedsEvent);
    public Task<bool> DeleteStedsEvent(string id);
}