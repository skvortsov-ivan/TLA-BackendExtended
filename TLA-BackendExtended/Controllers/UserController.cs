using TLA_BackendExtended.Services;
using TLA_BackendExtended.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace TLA_BackendExtended.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // Adds a new user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var entry = await _userService.CreateUserAsync(request.UserName, request.Password, request.Age, request.Weight, request.Location, request.DarkMode);
            return Ok(entry);
        }

    }
}
