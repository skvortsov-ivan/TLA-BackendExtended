
using TLA_BackendExtended.Clients;
using TLA_BackendExtended.Models;

namespace TLA_BackendExtended.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutClient _client;

        public WorkoutService(IWorkoutClient client)
        {
            _client = client;
        }

        public async Task<WorkoutCalories> GetCaloriesAsync(string workout, int weight, int duration)
        {
            var model = await _client.FetchCaloriesAsync(workout, weight, duration);

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

