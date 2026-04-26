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

        // 

        public async Task<WorkoutCalories> GetCaloriesAsync(CaloriesRequestDto request)
        {
            // 
            var model = await _client.FetchCaloriesAsync(request);

            //  WorkoutCalories
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