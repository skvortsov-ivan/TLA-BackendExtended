using TLA_BackendExtended.Clients;
using TLA_BackendExtended.Models;
using TLA_BackendExtended.DTOs;

namespace TLA_BackendExtended.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutClient _client;

        public WorkoutService(IWorkoutClient client)
        {
            _client = client;
        }

        
        public async Task<IEnumerable<Workout>> GetAllWorkoutsAsync()
        {
           
            var workouts = new List<Workout>
            {
                new Workout { Name = "Running", Category = "Cardio" },
                new Workout { Name = "Weightlifting", Category = "Strength" },
                new Workout { Name = "Swimming", Category = "Cardio" },
                new Workout { Name = "Yoga", Category = "Flexibility" }
            };

            return await Task.FromResult(workouts);
        }
        // ------------------------------------------------

        public async Task<WorkoutCalories> GetCaloriesAsync(CaloriesRequestDTO request)
        {
            var model = await _client.FetchCaloriesAsync(request);

            return new WorkoutCalories
            {
                WorkoutCategory = model.WorkoutCategory,
                CaloriesPerHour = model.CaloriesPerHour,
                DurationMinutes = model.DurationMinutes,
                TotalCalories = model.TotalCaloriesBurned
            };
        }
    }
}