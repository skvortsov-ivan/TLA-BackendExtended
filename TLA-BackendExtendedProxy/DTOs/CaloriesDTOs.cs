using System.Text.Json.Serialization;

namespace TLA_BackendExtendedProxy.DTOs
{
    public record CaloriesResponseDto
    {
        [JsonPropertyName("exercise_category")]
        public string WorkoutCategory { get; set; } = string.Empty;

        [JsonPropertyName("calories_per_hour")]
        public int CaloriesPerHour { get; set; }

        [JsonPropertyName("duration_minutes")]
        public int DurationMinutes { get; set; }

        [JsonPropertyName("total_calories")]
        public int TotalCalories { get; set; }
    }

    public record CaloriesRequestDto
    {
        public string WorkoutCategory { get; set; } = string.Empty;

        public int Weight { get; set; }

        public int Duration { get; set; }
    }
}
