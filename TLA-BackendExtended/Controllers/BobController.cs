using Microsoft.AspNetCore.Mvc;
using TLA_BackendExtended.DTOs;
using TLA_BackendExtended.Services;
using Microsoft.AspNetCore.RateLimiting;

namespace TLA_BackendExtended.Controllers
{
    [ApiController]
    [EnableRateLimiting("sliding")]
    [Route("api/ai-bob/prompts")]
    public class BobController : ControllerBase
    {
        private readonly IBobService _service;

        public BobController(IBobService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> SendPrompt([FromQuery] string prompt)
        {
            var result = await _service.CreateAsync(prompt);
            return Ok(result);
        }
    }
}