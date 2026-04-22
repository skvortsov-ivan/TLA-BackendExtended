using TLA_BackendExtended.DTOs;
using TLA_BackendExtended.Models;

namespace TLA_BackendExtended.Services
{
    public interface IWorkoutService
    {
        Task<WorkoutCalories> GetCaloriesAsync(CaloriesRequestDTO request);
    }
}
