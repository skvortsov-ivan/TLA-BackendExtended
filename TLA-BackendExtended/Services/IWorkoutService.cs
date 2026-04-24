using TLA_BackendExtended.DTOs;
using TLA_BackendExtended.Models;

namespace TLA_BackendExtended.Services
{
    public interface IWorkoutService
    {
        Task<IEnumerable<Workout>> GetAllWorkoutsAsync();
        Task<WorkoutCalories> GetCaloriesAsync(CaloriesRequestDTO request);
    }
}
