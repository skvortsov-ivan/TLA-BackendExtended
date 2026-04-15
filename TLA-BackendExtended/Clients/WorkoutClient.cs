using System.Net.Http.Json;
using System.Text.Json.Serialization;
using TLA_BackendExtended.DTOs;

namespace TLA_BackendExtended.Clients
{
    public interface IWorkoutClient
    {
        Task<CaloriesResponse> GetCaloriesAsync(string workout, int weight, int duration);
    }

    public class WorkoutClient : IWorkoutClient
    {
        private readonly HttpClient _httpClient;

        public WorkoutClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CaloriesResponse> GetCaloriesAsync(string workout, int weight, int duration)
        {
            var url = $"api/calories?activity={workout}&weight={weight}&duration={duration}";
            var response = await _httpClient.GetFromJsonAsync<CaloriesResponse>(url);
            return response ?? new CaloriesResponse();
        }
    }
}
