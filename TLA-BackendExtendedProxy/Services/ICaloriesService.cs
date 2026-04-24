using TLA_BackendExtendedProxy.DTOs;

namespace TLA_BackendExtendedProxy.Services
{
    public interface ICaloriesService
    {
        
        Task<CaloriesResponseDto> GetCaloriesAsync(CaloriesRequestDto request);
    }
}