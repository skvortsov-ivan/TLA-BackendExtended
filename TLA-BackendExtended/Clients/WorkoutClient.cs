using System.Net.Http.Json;
using System.Text.Json.Serialization;
using TLA_BackendExtended.DTOs;

namespace TLA_BackendExtended.Clients
{
    public interface IWorkoutClient
    {
        Task<CaloriesResponse> FetchCaloriesAsync(string workout, int weight, int duration);
    }

    public class WorkoutClient : IWorkoutClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public WorkoutClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<CaloriesResponse> FetchCaloriesAsync(string workout, int weight, int duration)
        {
            var url = $"api/calories?activity={workout}&weight={weight}&duration={duration}";
            _httpClient.DefaultRequestHeaders.Add("X-API-Key", _config["ServiceCommunicationApiKey"]);
            var response = await _httpClient.GetFromJsonAsync<CaloriesResponse>(url);
            return response ?? new CaloriesResponse();
        }
    }
}
