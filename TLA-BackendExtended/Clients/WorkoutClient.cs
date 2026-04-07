using System.Net.Http.Json;
using System.Text.Json.Serialization;
using TLA_BackendExtended.DTOs;

namespace TLA_BackendExtended.Clients
{
    public interface IWorkoutClient
    {
        Task<List<CaloriesBurnedResponse>> GetCaloriesAsync(string activity, int weight, int duration);
    }

    public class WorkoutClient : IWorkoutClient
    {
        private readonly HttpClient _httpClient;

        public WorkoutClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CaloriesBurnedResponse>> GetCaloriesAsync(string activity, int weight, int duration)
        {
            var url = $"api/calories?activity={activity}&weight={weight}&duration={duration}";
            var raw = await _httpClient.GetFromJsonAsync<List<CaloriesBurnedResponse>>(url);
            return raw ?? new List<CaloriesBurnedResponse>();
        }

        //public async Task<List<CaloriesBurnedResponse>> GetCaloriesAsync(string activity, int weight, int duration)
        //{
        //    // Build correct API Ninjas URL (relative to BaseAddress)
        //    var url = $"caloriesburned?activity={activity}&weight={weight}&duration={duration}";

        //    // Call API Ninjas
        //    var raw = await _httpClient.GetFromJsonAsync<List<ApiNinjasRequest>>(url);

        //    if (raw == null)
        //        return new List<CaloriesBurnedResponse>();

        //    // Map API Ninjas DTO → Your DTO
        //    return raw.Select(x => new CaloriesBurnedResponse(
        //        WorkoutCategoryName: x.Name,
        //        CaloriesPerHour: x.CaloriesPerHour,
        //        DurationMinutes: x.DurationMinutes,
        //        TotalCaloriesBurned: x.TotalCalories
        //    )).ToList();
        //}
    }
}
