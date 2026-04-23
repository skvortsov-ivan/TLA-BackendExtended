using System.Text.Json.Serialization;

namespace TLA_BackendExtendedProxy.DTOs
{
    public record OllamaRequestDto
    {
        public string Prompt { get; set; } = string.Empty;
    }

    public record OllamaResponseDto
    {
        [JsonPropertyName("generatedText")]
        public string GeneratedText { get; set; } = string.Empty;
    }
}
