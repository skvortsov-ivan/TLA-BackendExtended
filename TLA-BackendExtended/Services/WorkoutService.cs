using Microsoft.Extensions.Caching.Hybrid;
using System.Diagnostics.Metrics;
using System.Text.Json;
using System.Xml.Linq;
using TLA_BackendExtended.Clients;
using TLA_BackendExtended.DTOs;
using TLA_BackendExtended.Models;

namespace TLA_BackendExtended.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutClient _client;
        private readonly HybridCache _cache;
        private static int _apiCallAmount = 0;

        public WorkoutService(IWorkoutClient client, HybridCache cache)
        {
            _client = client;
            _cache = cache;
        }

        public async Task<WorkoutCalories> GetCaloriesAsync(CaloriesRequestDTO request)
        {

            var cacheKey = $"workout_{request.WorkoutCategory.ToLower()}_weight_{request.Weight}_duration_{request.Duration}";

            bool cacheMiss = false;

            var totalSw = System.Diagnostics.Stopwatch.StartNew();
            // Retrieve from cache or fetch from API if missing
            var cached = await _cache.GetOrCreateAsync(cacheKey, async cancel =>
            {
                cacheMiss = true;

                _apiCallAmount++;
                Console.WriteLine($"Making API call number {_apiCallAmount}");

                var sw = System.Diagnostics.Stopwatch.StartNew();
                var model = await _client.FetchCaloriesAsync(request);
                sw.Stop();

                Console.WriteLine($"[CACHE MISS] Fetching total calories burned for: {request.WorkoutCategory}. It took {sw.ElapsedMilliseconds} ms");

                return new WorkoutCalories
                {
                    WorkoutCategory = model.WorkoutCategory,
                    CaloriesPerHour = model.CaloriesPerHour,
                    DurationMinutes = model.DurationMinutes,
                    TotalCalories = model.TotalCaloriesBurned
                };
            });

            totalSw.Stop();
            if (!cacheMiss)
            {
                Console.WriteLine($"[CACHE HIT] Returning cached data for {request.WorkoutCategory}. It took {totalSw.ElapsedMilliseconds} ms");
            }

            return new WorkoutCalories
            {
                WorkoutCategory = cached.WorkoutCategory,
                CaloriesPerHour = cached.CaloriesPerHour,
                DurationMinutes = cached.DurationMinutes,
                TotalCalories = cached.TotalCalories
            };
        }
    }
}
