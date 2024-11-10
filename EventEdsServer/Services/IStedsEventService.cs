using EventEdsServer.Models;

namespace EventEdsServer.Services;

public interface IStedsEventService
{
    public Task<List<StedsEvent>> GetStedsEvents(int page, int pageSize);
    public Task<StedsEvent?> GetStedsEventById(Guid id);
    public Task<StedsEvent?> CreateStedsEvent(StedsEventDto stedsEvent);
    public Task<StedsEvent?> UpdateStedsEvent(Guid id, StedsEvent stedsEvent);
    public Task<bool> DeleteStedsEvent(Guid id);
}