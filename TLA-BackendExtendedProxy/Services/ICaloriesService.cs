using TLA_BackendExtendedProxy.DTOs;

namespace TLA_BackendExtendedProxy.Services
{
    public interface ICaloriesService
    {
        Task<List<CaloriesBurnedDto>> GetCaloriesAsync(string activity, int weight, int duration);
    }
}
