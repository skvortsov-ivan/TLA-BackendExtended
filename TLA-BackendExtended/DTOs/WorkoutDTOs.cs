using System.Text.Json.Serialization;

namespace TLA_BackendExtended.DTOs
{
    public record CaloriesResponse
    {
        [JsonPropertyName("exercise_category")]
        public string WorkoutCategory { get; set; } = string.Empty;

        [JsonPropertyName("calories_per_hour")]
        public int CaloriesPerHour { get; set; }

        [JsonPropertyName("duration_minutes")]
        public int DurationMinutes { get; set; }

        [JsonPropertyName("total_calories")]
        public int TotalCaloriesBurned { get; set; }
    }

}
