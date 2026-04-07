using System.Net.Http.Json;
using TLA_BackendExtendedProxy.DTOs;

namespace TLA_BackendExtendedProxy.Services
{

    public class CaloriesService : ICaloriesService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public CaloriesService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<CaloriesBurnedDto>> GetCaloriesAsync(string activity, int weight, int duration)
        {
            var apiKey = _config["ApiNinjas:ApiKey"];

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.api-ninjas.com/v1/caloriesburned?activity={activity}&weight={weight}&duration={duration}"
            );

            request.Headers.Add("X-Api-Key", apiKey);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<List<CaloriesBurnedDto>>();
            return result ?? new List<CaloriesBurnedDto>();
        }
    }
}

