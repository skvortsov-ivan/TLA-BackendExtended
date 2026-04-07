using System.Text.Json.Serialization;

namespace TLA_BackendExtended.DTOs
{
    public record CaloriesBurnedResponse(
        string WorkoutCategoryName,
        int CaloriesPerHour,
        int DurationMinutes,
        int TotalCaloriesBurned
    );
}
