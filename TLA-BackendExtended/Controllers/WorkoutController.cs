using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Retrieves a list of workouts with filtering and pagination.
        /// </summary>
        /// <param name="category">Filter by workout category (e.g., Cardio, Strength).</param>
        /// <param name="search">Search by workout name.</param>
        /// <param name="page">The page number to retrieve (Default is 1).</param>
        /// <param name="pageSize">The number of items per page (Default is 10).</param>
        /// <returns>A paged list of workouts and total count.</returns>
        [HttpGet]
        public async Task<IActionResult> GetWorkouts(
            [FromQuery] string? category,
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            // Fetching all data from service
            var allWorkouts = await _service.GetAllWorkoutsAsync();

            // Creating a queryable object for filtering
            var query = allWorkouts.AsQueryable();

            // Applying Filters
            if (!string.IsNullOrEmpty(category))
                query = query.Where(w => w.Category == category);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(w => w.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

            // Applying Pagination (Epic 5)
            var pagedData = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Returning the structured response
            return Ok(new
            {
                TotalCount = query.Count(),
                Page = page,
                PageSize = pageSize,
                Data = pagedData
            });
        }

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
            var model = await _service.GetCaloriesAsync(workout, weight, duration);
            return Ok(model);
        }
    }
}