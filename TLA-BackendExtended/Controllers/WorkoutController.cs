using Microsoft.AspNetCore.Mvc;
using TLA_BackendExtended.DTOs;
using TLA_BackendExtended.Services;
using Microsoft.AspNetCore.RateLimiting;

namespace TLA_BackendExtended.Controllers
{
    [EnableRateLimiting("fixed")]
    [ApiController]
    [Route("api/v1/workouts")]
    [Route("api/workouts")]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _service;

        public WorkoutController(IWorkoutService service)
        {
            _service = service;
        }

        // 
        /// <summary>
        /// Calculates calories burned for a specific workout based on user weight and duration.
        /// </summary>
        /// <param name="workout">The type or name of the workout.</param>
        /// <param name="weight">User's weight in kilograms.</param>
        /// <param name="duration">Duration of the exercise in minutes.</param>
        /// <returns>Calculation results from the external API.</returns>
        [HttpGet("calories")]
        public async Task<IActionResult> GetCalories(
            [FromQuery] string workout,
            [FromQuery] int weight,
            [FromQuery] int duration)
        {
            // 
            var request = new CaloriesRequestDto
            {
                WorkoutCategory = workout,
                Weight = weight,
                Duration = duration
            };

            var model = await _service.GetCaloriesAsync(request);
            return Ok(model);
        }
    }
}