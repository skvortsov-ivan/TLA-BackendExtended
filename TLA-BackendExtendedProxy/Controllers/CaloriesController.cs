using Microsoft.AspNetCore.Mvc;
using TLA_BackendExtendedProxy.Services;

namespace TLA_BackendExtendedProxy.Controllers
{
    [ApiController]
    [Route("api/calories")]
    public class CaloriesController : ControllerBase
    {
        private readonly ICaloriesService _service;

        public CaloriesController(ICaloriesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCalories(
            [FromQuery] string activity,
            [FromQuery] int weight,
            [FromQuery] int duration)
        {
            var result = await _service.GetCaloriesAsync(activity, weight, duration);
            return Ok(result);
        }
    }
}
