using Microsoft.Extensions.Caching.Memory;
using TLA_BackendExtendedProxy.Clients;
using TLA_BackendExtendedProxy.DTOs;

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

       
        public async Task<CaloriesResponseDto> GetCaloriesAsync(CaloriesRequestDto request)
        {
            string cacheKey = $"calories_{request.WorkoutCategory}_{request.Weight}_{request.Duration}";

            if (!_cache.TryGetValue(cacheKey, out CaloriesResponseDto? cachedResult))
            {
                var results = await _client.FetchCaloriesAsync(request);
                var result = results.FirstOrDefault() ?? new CaloriesResponseDto();

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