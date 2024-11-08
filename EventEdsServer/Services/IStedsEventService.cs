using EventEdsServer.Models;

namespace EventEdsServer.Services;

public interface IStedsEventService
{
    public Task<List<StedsEvent>> GetStedsEvents();
    public Task<StedsEvent?> GetStedsEventById(Guid id);
    public Task<StedsEvent?> CreateStedsEvent(StedsEventDto stedsEvent);
    public Task<StedsEvent?> UpdateStedsEvent(StedsEvent stedsEvent);
    public Task<bool> DeleteStedsEvent(Guid id);
}