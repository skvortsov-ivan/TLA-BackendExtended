namespace TLA_BackendExtended.Models
{
    public class Workout
    {
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int CaloriesBurned { get; set; } 
        public int DurationInMinutes { get; set; }
        public string Intensity { get; set; } = "Medium";
    }
}