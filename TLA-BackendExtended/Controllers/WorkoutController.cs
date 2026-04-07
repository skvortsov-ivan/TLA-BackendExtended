using Microsoft.AspNetCore.Mvc;
using TLA_BackendExtended.Clients;

namespace TLA_BackendExtended.Controllers
{
    [ApiController]
    [Route("api/workouts")]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutClient _workoutClient;

        public WorkoutController(IWorkoutClient workoutClient)
        {
            _workoutClient = workoutClient;
        }

        [HttpGet("calories")]
        public async Task<IActionResult> GetCalories([FromQuery] string activity, [FromQuery] int weight, [FromQuery] int duration)
        {
            var data = await _workoutClient.GetCaloriesAsync(activity, weight, duration);
            return Ok(data);
        }
    }
}

