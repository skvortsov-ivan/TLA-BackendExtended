using System.ComponentModel.DataAnnotations;
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

    public record CaloriesRequestDTO {
        [Required(ErrorMessage = "Activity is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Activity must be between 3 and 50 characters.")]
        public string WorkoutCategory { get; set; } = string.Empty;

        [Required(ErrorMessage = "Weight is required.")]
        [Range(2, 635, ErrorMessage = "Weight must be between 2 and 635 kg.")]
        public int Weight { get; set; } 

        [Required(ErrorMessage = "Duration is required.")]
        [Range(1, 600, ErrorMessage = "Duration must be between 1 and 600 minutes.")]
        public int Duration { get; set; }
    };
}
