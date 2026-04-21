using TLA_BackendExtendedProxy.Clients;
using TLA_BackendExtendedProxy.DTOs;
using Microsoft.Extensions.Caching.Memory;

namespace TLA_BackendExtendedProxy.Services
{
    public class CaloriesService : ICaloriesService
    {
        private readonly ApiNinjasClient _client;
        private readonly IMemoryCache _cache;

        
        public CaloriesService(ApiNinjasClient client, IMemoryCache cache)
        {
            _client = client;
            _cache = cache;
        }

        public async Task<CaloriesResponseDto> GetCaloriesAsync(string workout, int weight, int duration)
        {
            string cacheKey = $"calories_{workout}_{weight}_{duration}";

            
            if (!_cache.TryGetValue(cacheKey, out CaloriesResponseDto? cachedResult))
            {
                var request = new CaloriesRequestDto
                {
                    WorkoutCategory = workout,
                    Weight = weight,
                    Duration = duration
                };

                var results = await _client.FetchCaloriesAsync(request);
                var result = results.FirstOrDefault() ?? new CaloriesResponseDto();

                result.WorkoutCategory = workout;

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                _cache.Set(cacheKey, result, cacheOptions);

                return result;
            }
            return cachedResult!;
        }
    }
}