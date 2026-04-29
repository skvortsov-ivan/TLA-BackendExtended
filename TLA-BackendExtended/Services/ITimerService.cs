using TLA_BackendExtended.Models;
using TLA_BackendExtended.DTOs;
using TimerModel = TLA_BackendExtended.Models.Timer;

namespace TLA_BackendExtended.Services
{
    public interface ITimerService
    {
        Task<TimerModel> StartTimerAsync(string category);
        Task<TimerModel> EndTimerAsync(decimal timeInterval);
        Task<TimerModel> UpdateTimerAsync(int id, decimal timeInterval, string category);
        Task DeleteTimerAsync(int id);
        Task<TimerModel> GetLatestTimerAsync();
        Task<PagedResponse<TimerResponse>> GetPagedTimersAsync(int page, int pageSize);
    }
}

