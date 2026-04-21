using TLA_BackendExtended.Models;

namespace TLA_BackendExtended.Services
{
    public interface IWorkoutService
    {
        Task<WorkoutCalories> GetCaloriesAsync(string workout, int weight, int duration);

     
        Task<IEnumerable<Workout>> GetAllWorkoutsAsync();
    }
}
