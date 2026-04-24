using System.Net.Http.Json;
using System.Text.Json.Serialization;
using TLA_BackendExtended.DTOs;

namespace TLA_BackendExtended.Clients
{
    public interface IWorkoutClient
    {
        Task<CaloriesResponse> FetchCaloriesAsync(CaloriesRequestDTO request);
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

        public async Task<CaloriesResponse> FetchCaloriesAsync(CaloriesRequestDTO request)
        {
            var url = $"api/calories?activity={request.WorkoutCategory}"+$"&weight={request.Weight}"+$"&duration={request.Duration}";
            _httpClient.DefaultRequestHeaders.Add("ServiceCommunicationApiKey", _config["ServiceCommunicationApiKey"]);
            var response = await _httpClient.GetFromJsonAsync<CaloriesResponse>(url);
            return response ?? new CaloriesResponse();
        }
    }
}
