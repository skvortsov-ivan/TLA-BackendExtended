using System.Text.Json.Serialization;


namespace TLA_BackendExtended.Models
{
    public class WorkoutCalories
    {
        public string WorkoutCategory { get; set; } = string.Empty;
        public int CaloriesPerHour { get; set; }
        public int DurationMinutes { get; set; }

        public int TotalCalories { get; set; }
    }
}
