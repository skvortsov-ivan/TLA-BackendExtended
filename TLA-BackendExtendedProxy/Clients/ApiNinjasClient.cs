using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using TLA_BackendExtendedProxy.DTOs;
using static System.Net.WebRequestMethods;

namespace TLA_BackendExtendedProxy.Clients
{
    public class ApiNinjasClient
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public ApiNinjasClient(HttpClient http, IConfiguration config)
        {
            _http = http;
            _config = config;
        }

        public async Task<List<CaloriesResponseDto>> FetchCaloriesAsync(CaloriesRequestDto request)
        {
            _http.DefaultRequestHeaders.Add("X-Api-Key", _config["ApiNinjas:ApiKey"]);

            var url =
                $"https://api.api-ninjas.com/v1/caloriesburned" +
                $"?activity={request.WorkoutCategory}" +
                $"&weight={request.Weight}" +
                $"&duration={request.Duration}";

            var result = await _http.GetFromJsonAsync<List<CaloriesResponseDto>>(url);

            return result ?? new List<CaloriesResponseDto>();
        }
    }
}
