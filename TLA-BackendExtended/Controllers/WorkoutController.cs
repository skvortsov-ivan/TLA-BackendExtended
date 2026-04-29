using Microsoft.AspNetCore.Mvc;
using TLA_BackendExtended.DTOs;
using TLA_BackendExtended.Services;

namespace TLA_BackendExtended.Controllers
{
    [ApiController]
    [Route("api/workouts")]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _service;

        public WorkoutController(IWorkoutService service)
        {
            _service = service;
        }

        [HttpGet("calories")]
        public async Task<IActionResult> GetCalories([FromQuery] string workout, [FromQuery] int weight,[FromQuery] int duration)
        {
            var request = new CaloriesRequestDTO
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