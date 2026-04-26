using TLA_BackendExtendedProxy.Clients;
using TLA_BackendExtendedProxy.DTOs;
using Microsoft.Extensions.Caching.Hybrid;

namespace TLA_BackendExtendedProxy.Services
{
    public class CaloriesService : ICaloriesService
    {
        private readonly ApiNinjasClient _client;
        private readonly HybridCache _cache;

        public CaloriesService(ApiNinjasClient client, HybridCache cache)
        {
            _client = client;
            _cache = cache;
        }

        public async Task<CaloriesResponseDto> GetCaloriesAsync(CaloriesRequestDto request)
        {
            string cacheKey = $"calories_{request.WorkoutCategory}_{request.Weight}_{request.Duration}";

          
            return await _cache.GetOrCreateAsync(cacheKey, async cancel =>
            {
                var results = await _client.FetchCaloriesAsync(request);
                var result = results.FirstOrDefault() ?? new CaloriesResponseDto();

               
                return result;
            });
        }
    }
}