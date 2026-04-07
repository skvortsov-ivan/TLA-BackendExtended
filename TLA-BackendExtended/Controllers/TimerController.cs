using Microsoft.AspNetCore.Mvc;
using TLA_BackendExtended.DTOs;
using TLA_BackendExtended.Services;

namespace TLA_BackendExtended.Controllers
{
    [ApiController]
    [Route("api/timers")]
    public class TimerController : ControllerBase
    {
        private readonly ITimerService _timerService;

        public TimerController(ITimerService timerService)
        {
            _timerService = timerService;
        }

        // Start a timer
        [HttpPost("start")]
        public async Task<IActionResult> StartTimer([FromBody] StartTimerRequest request)
        {
            var timer = await _timerService.StartTimerAsync(request.Category);
            return Ok(timer);
        }

        // End a timer
        [HttpPost("end")]
        public async Task<IActionResult> EndTimer([FromBody] EndTimerRequest request)
        {
            var timer = await _timerService.EndTimerAsync(request.TimeInterval);
            return Ok(timer);
        }

        // Update timer
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimer(int id, [FromBody] UpdateTimerRequest request)
        {
            var timer = await _timerService.UpdateTimerAsync(
                id,
                request.TimerInterval,
                request.Category
            );

            return Ok(timer);
        }

        // Delete timer
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimer(int id)
        {
            await _timerService.DeleteTimerAsync(id);
            return Ok($"Timer with ID: '{id}' has been deleted.");
        }

        // Get latest timer
        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestTimer()
        {
            var timer = await _timerService.GetLatestTimerAsync();
            return Ok(timer);
        }

        // Get all timers
        [HttpGet]
        public async Task<IActionResult> GetAllTimers()
        {
            var timers = await _timerService.GetAllTimersAsync();
            return Ok(timers);
        }
    }
}
