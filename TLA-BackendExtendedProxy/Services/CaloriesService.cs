using TLA_BackendExtendedProxy.Clients;
using TLA_BackendExtendedProxy.DTOs;

namespace TLA_BackendExtendedProxy.Services
{
    public class CaloriesService : ICaloriesService
    {
        private readonly ApiNinjasClient _client;

        public CaloriesService(ApiNinjasClient client)
        {
            _client = client;
        }

        public async Task<CaloriesResponseDto> GetCaloriesAsync(string workout, int weight, int duration)
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

            return result;
        }
    }
}
