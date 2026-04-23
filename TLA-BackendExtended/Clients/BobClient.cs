using System.Net.Http.Json;
using System.Text.Json;
using TLA_BackendExtended.DTOs;

namespace TLA_BackendExtended.Clients
{
    public interface IAiBobClient
    {
        Task<BobResponseDto> SendPromptAsync(BobRequestDto request);
    }

    public class AiBobClient : IAiBobClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public AiBobClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<BobResponseDto> SendPromptAsync(BobRequestDto request)
        {
            _httpClient.DefaultRequestHeaders.Add("ServiceCommunicationApiKey", _config["ServiceCommunicationApiKey"]);

            var response = await _httpClient.PostAsJsonAsync("api/ai-requests", request);

            var raw = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var dto = JsonSerializer.Deserialize<BobResponseDto>(raw, options);

            return dto ?? new BobResponseDto();
        }
    }
}
